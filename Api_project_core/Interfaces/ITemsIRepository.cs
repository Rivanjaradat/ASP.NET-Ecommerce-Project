using Api_project_core.DTOs;
using Api_project_core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_project_core.Interfaces
{
    public interface ITemsIRepository
    {
        Task<PagedResponse<ItemsDTO>> GetItemsAsync(int page_index, int page_size);
       Task<PagedResponse<ItemsDTO>> PaginationAsync(IQueryable<ItemsDTO> query, int page_index, int page_size);
      

    }
}
