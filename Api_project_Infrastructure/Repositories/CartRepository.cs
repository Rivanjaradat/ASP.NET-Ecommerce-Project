using Api_project_core.DTOs;
using Api_project_core.Interfaces;
using Api_project_core.Models;
using Api_project_Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Api_project_Infrastructure.Repositories { 
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext appDbContext;

        public CartRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<string> AddBulkToCartAsync(CartItemDTO DTO, int ? userId)
        {
            var items = await appDbContext.Items.FindAsync(DTO.ItemCode);
            var stores = await appDbContext.Stores.FindAsync(DTO.Store_Id);
            if (items == null || stores == null)
            {
                return "item or store not found";
            }
            var existingItem = appDbContext.ShoppingCartItems.FirstOrDefault(x => x.Cus_Id == userId && x.Item_id == DTO.ItemCode && x.Store_Id == DTO.Store_Id);
            if (existingItem != null)
            {
                existingItem.Quantity = DTO.Quantity;
                existingItem.Unit_Id = DTO.UnitCode;
                existingItem.Store_Id = DTO.Store_Id;
                existingItem.UpdatedAt = DateTime.Now;


            }
            else
            {
                var shopingCartItem = new ShoppingCartItems
                {
                    Cus_Id = (int)userId,

                    Item_id = DTO.ItemCode,
                    Quantity = DTO.Quantity,
                    Unit_Id = DTO.UnitCode,
                    Store_Id = DTO.Store_Id,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = null,
                };
                appDbContext.Add(shopingCartItem);
               

            }
            await appDbContext.SaveChangesAsync();
            return "Item added to cart successfully";
        }

        public async Task<string> AddOneQuantityToCartAsync(CartItemDTO cartItemDto, int ? userId)
        {
            var item = await appDbContext.Items.FindAsync(cartItemDto.ItemCode);
            var store = await appDbContext.Stores.FindAsync(cartItemDto.Store_Id);
            if (item == null || store == null)
            {
                return "item or store not found";
            }
            var existingItem = await appDbContext.ShoppingCartItems.FirstOrDefaultAsync(x => x.Cus_Id == userId && x.Item_id == cartItemDto.ItemCode && x.Store_Id == cartItemDto.Store_Id);
            if (existingItem != null)
            {
                existingItem.Quantity += 1;
                existingItem.UpdatedAt = DateTime.Now;
            }
            else
            {
                var shopingCartItem = new ShoppingCartItems
                {
                    Cus_Id = (int)userId,

                    Item_id = cartItemDto.ItemCode,
                    Quantity = 1,
                    Unit_Id = cartItemDto.UnitCode,
                    Store_Id = cartItemDto.Store_Id,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = null,
                };
                await appDbContext.ShoppingCartItems.AddAsync(shopingCartItem);
            }
            await appDbContext.SaveChangesAsync();
            return "Item Added to cart successfuly";
        }

        public async Task<IEnumerable<UserCartItemDTO>> getAllItemsFromCart(int? customerId)
        {
           var cartItem= await appDbContext.ShoppingCartItems.Where(x => x.Cus_Id == customerId)
                .Include(x => x.Items)
                .Include(x=>x.Items.ItemsUnits).ThenInclude(x => x.Units)
              
                .ToListAsync();
            var itemDto = cartItem.Select(x => new UserCartItemDTO
            {
                name = x.Items.Name,
                price = x.Items.Price,
                itemUnit = x.Items.ItemsUnits
                .Where(unit => unit.Unit_Id == x.Unit_Id&& x.Item_id == unit.Item_Id)
                .Select(unit => unit.Units.Name)
                .FirstOrDefault(),
                quantity = x.Quantity,


            }).ToList();
            return itemDto;
        }
    }
}
