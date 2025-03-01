using Api_project_core.DTOs;
using Api_project_core.Interfaces;
using Api_project_core.Models;
using Api_project_Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_project_Infrastructure.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly AppDbContext appDbContext;

        public InvoiceRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public  async Task<string> CreateInvoiceAsync(int customer_Id)
        {
          var cartItems= await appDbContext.ShoppingCartItems
                .Include(c=>c.Items)
                .Where(c => c.Cus_Id == customer_Id).ToListAsync();
            if (cartItems == null || !cartItems.Any())
            {
                return "No Items in the cart  to create invoice";
            }
            var invoice = new Invoice
    
            {
                Cus_Id = customer_Id,
               CreatedAt = DateTime.Now,
               
               NetPrice=0,
                Transaction_Type = 1,
                Payment_Type = 1,
                isPosted = true,
                IsClosed = false,
                IsReviewed = false,

            };
          await  appDbContext.Invoice.AddAsync(invoice);
            await appDbContext.SaveChangesAsync();
            var unAvailableItems = new List<string>();
            double totalNetPrice = 0;
            foreach (var item in cartItems)
            {
                
              var itemsSrore=appDbContext.InvItemStores  
                    .FirstOrDefault(i => i.Item_Id == item.Item_id&& i.Store_Id==item.Store_Id);
                if (itemsSrore == null)
                {
                    unAvailableItems.Add(item.Items.Name);
                    continue;

                }
                double availableQuantity = itemsSrore.Balance -  itemsSrore.ReservedQuantity;
                if (availableQuantity < item.Quantity)
                {
                    unAvailableItems.Add(item.Items.Name);
                    continue;
                }
                double unitePrice = item.Items.Price;
                double totalPrice = (unitePrice * item.Quantity);
                totalNetPrice += totalPrice;
                var invoiceDetail = new InvoiceDetails
                {
                    Invoice_Id=invoice.Id,
                    Item_Id=item.Item_id,
                    Quantity=item.Quantity,
                    Factor=1,
                    price=(int)unitePrice,
                    Unit_Id=item.Unit_Id,
                    CreatedAt=DateTime.Now,
                };
                appDbContext.InvoiceDetails.Add(invoiceDetail);
                itemsSrore.ReservedQuantity += item.Quantity;
                appDbContext.InvItemStores.Update(itemsSrore);




            }

            invoice.NetPrice = totalNetPrice;
            appDbContext.ShoppingCartItems.RemoveRange(cartItems.Where (i=>!unAvailableItems.Contains(i.Items.Name)));
            
            await appDbContext.SaveChangesAsync();
            if (unAvailableItems.Any())
            {
                var unAvailableItemsMessage = string.Join(",", unAvailableItems.Select(item =>
                {
                    var cartItem = cartItems.FirstOrDefault(c => c.Items.Name == item);
                    if (cartItem != null)
                    {
                        var itemStore = appDbContext.InvItemStores
                             .FirstOrDefault(i => i.Item_Id == cartItem.Item_id);
                        if (itemStore != null)
                        {

                            double availableQuantity = itemStore.Balance - itemStore.ReservedQuantity;
                            return $"{item} available quantity is {availableQuantity}";
                        }
                    }
                    return item;
                }));
                return $"invoice created succesfully with ID :{invoice.Id} and total price:{totalNetPrice},However the following items were unavailable{unAvailableItemsMessage}";
                
            }

            return $"invoice created succesfully with ID :{invoice.Id} and total price:{totalNetPrice}";

        }

        public async Task<InvoiceRecieptDTO> GetInvoiceReciept(int customerId, int invoice_Id)
        {
            var invoice = await appDbContext.Invoice
                .Include(i => i.InvoiceDetails)
                .ThenInclude(i => i.Item)
                .FirstOrDefaultAsync(i => i.Id == invoice_Id && i.Cus_Id == customerId);
            if (invoice == null)
            {

            }
            double total_price = 0;
            foreach (var item in invoice.InvoiceDetails)
            {
               double item_price = item.price * item.Quantity;
                total_price += item_price;

            }
            invoice.NetPrice = total_price;
            await appDbContext.SaveChangesAsync();
            var invoiceReciept = new InvoiceRecieptDTO
            {
                customer_Id = invoice.Cus_Id,
                created_at = invoice.CreatedAt,
                invoice_Id = invoice.Id,
                total_price = total_price,
              
                items =  invoice.InvoiceDetails.Select( i => new InvoiceItemDTO
                {
                   
                    item_name = i.Item.Name,
                   
                    quantity = i.Quantity,
                    unit_name = appDbContext.Units.FirstOrDefault(u => u.Id == i.Unit_Id)?.Name??"unKnown",
                   price_per_unit = i.price,
                    total_price = i.price * i.Quantity,



                }).ToList()
            };
            return invoiceReciept;
        }
    }
}
