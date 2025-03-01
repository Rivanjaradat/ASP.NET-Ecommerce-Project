using Api_project_core.DTOs;
using Api_project_core.Interfaces;
using Api_project_core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        //private readonly ITemsIRepository itemsIRepository;

        public ItemsController(/*ITemsIRepository itemsIRepository*/ IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            //this.itemsIRepository = itemsIRepository;
        }
        [HttpGet("Items")]
        public async Task<ActionResult<IEnumerable<ItemsDTO>>> GetItems(int page_index=1, int page_size=5)
        {
            var items = await unitOfWork.itemRepository.GetItemsAsync(page_index,page_size);
            if (items == null)
            {
                return NotFound("items not exists");
            }
            return Ok(items);
        }
    }
}
