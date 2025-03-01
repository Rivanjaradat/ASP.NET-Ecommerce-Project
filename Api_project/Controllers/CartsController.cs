using Api_project.HelperFunctions;
using Api_project_core.DTOs;
using Api_project_core.Interfaces;
using Api_project_core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_project.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ICartRepository cartRepository;

        public CartsController(ICartRepository cartRepository)
        {
            this.cartRepository = cartRepository;
        }
        [HttpPost("add_to_cart")]
        public async Task<ActionResult> AddBulkItemsToCart([FromBody] CartItemDTO dto)
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer", "");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("token is missing");
            }
            try
            {
                var userId = ExtractClaims.ExtractUserId(token);
                if(!userId.HasValue)
                {
                    return Unauthorized("Invalid token");
                }
                var result = await cartRepository.AddBulkToCartAsync(dto, userId.Value);
                if(result == "Item added to cart successfully")
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return Unauthorized("invalid Token :"+ex.Message);

            }
        }
        [HttpPost("add_one_to_cart")]
        public async Task<ActionResult > addOneItemToCart([FromBody]CartItemDTO dto){

            var token = Request.Headers["Authorization"].ToString().Replace("Bearer", "");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("token is missing");
            }
            try
            {
                var userId = ExtractClaims.ExtractUserId(token);
                if (!userId.HasValue)
                {
                    return Unauthorized("Invalid token");
                }
                var result = await cartRepository.AddOneQuantityToCartAsync(dto, userId.Value);
                if (result == "Item added to cart successfully")
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return Unauthorized("invalid Token :" + ex.Message);

            }

        }

        [HttpGet("get/cart/allitems")]
        public async Task<IActionResult> GetAllItemsFromCart()
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer", "");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("token is missing");
            }

            try
            {
                var userId = ExtractClaims.ExtractUserId(token);
                if (!userId.HasValue)
                {
                    return Unauthorized("Invalid token");
                }
                var result = await cartRepository.getAllItemsFromCart(userId);
                if (result == null)
                {
                    return NotFound("no items in your cart");

                }
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest("Server error"+ex.Message);
                

            }
        }

    }
}
