using Api_project_core.DTOs;
using Api_project_core.Interfaces;
using Api_project_core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            this.authRepository = authRepository;
        }
        //api/Auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = new Users
            {
                UserName = registerDTO.UserName,
                Email = registerDTO.Email,
                Gov_Id = registerDTO.Gov_Code,
                City_Id = registerDTO.City_Code,
                Zone_Id = registerDTO.Zone_Code,
                Class_Id = registerDTO.Cus_classId,

            };
            var result = await authRepository.RegisterAsync(user, registerDTO.Password);
            return new JsonResult(result);


        }
        //api/Auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var token = await authRepository.LoginAsync(loginDTO.UserName, loginDTO.Password);
                if (token == null)
                {
                    return Unauthorized(new { Message = "Invalid Username or Password" });
                }
                return Ok(token);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during login: {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }


        }

    }
}
