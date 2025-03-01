using Api_project_core.DTOs;
using Api_project_core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_project_core.Interfaces
{
    public  interface ICartRepository
    {
        Task<string> AddBulkToCartAsync(CartItemDTO cartItemDto ,int? userId);
        Task<string> AddOneQuantityToCartAsync(CartItemDTO cartItemDto, int ?userId);
        Task<IEnumerable<UserCartItemDTO>> getAllItemsFromCart(int? customerId);

    }
}
