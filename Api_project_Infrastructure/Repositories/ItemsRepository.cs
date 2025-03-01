using Api_project_core.DTOs;
using Api_project_core.Interfaces;
using Api_project_core.Mapping_Profiles;
using Api_project_core.Models;
using Api_project_Infrastructure.Data;
using Mapster;
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

namespace Api_project_Infrastructure.Repositories
{
    public class ItemsRepository : ITemsIRepository
    {
        private readonly AppDbContext appDbContext;

        public ItemsRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        //public async Task<IEnumerable<ItemsDTO>> GetItemsAsync()
        //{
        //    var items = await appDbContext.Items
        //        .Include(x => x.ItemsUnits)
        //        .Select(x => new ItemsDTO
        //        {
        //            Id = x.Id,
        //            Name = x.Name,
        //            Description = x.Description,
        //            Price = x.Price,
        //            ItemUnits = x.ItemsUnits.Select(y => y.Units.Name).ToList(),
        //            Stores = x.InvItemStores.Select(y => y.Store.Name).ToList()
        //        }).ToListAsync();
        //    return items;




        //}
        public async Task<PagedResponse<ItemsDTO>> GetItemsAsync(int page_index,int page_size)
        {
            var config = Mapping_Profile.Config();
            var items =  appDbContext.Items.ProjectToType<ItemsDTO>(config).AsQueryable();
            var result= await PaginationAsync(items, page_index, page_size);

            return result;




        }

        public async Task<PagedResponse<ItemsDTO>> PaginationAsync(IQueryable<ItemsDTO> query, int page_index, int page_size)
        {
            var total_items = await query.CountAsync();
            var items = await query.Skip((page_index - 1) * page_size).Take(page_size).ToListAsync();
            return new PagedResponse <ItemsDTO>
            { 
                total_items = total_items,
                page_index = page_index,
                page_size = page_size,
                items = items
            };
        }
    }
}
