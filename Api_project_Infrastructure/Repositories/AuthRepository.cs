using Api_project_core.Interfaces;
using Api_project_core.Models;
using Microsoft.AspNetCore.Identity;
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
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<Users> userManager;
        private readonly SignInManager<Users> signInManger;
        private readonly IConfiguration configuration;

        public AuthRepository(UserManager<Users> userManager, SignInManager<Users> signInManger, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManger = signInManger;
            this.configuration = configuration;
        }
        public async Task<string> RegisterAsync(Users user, string password)
        {
            var result = await userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                return "User Registered successfully";
            }
            var errorMessage = result.Errors.Select(error => error.Description).ToList();
            return string.Join(",", errorMessage);

        }
        public Task<string> ChangePasswordAsync(string email, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }
        public async Task<string> LoginAsync(string username, string password)
        {
            var user = await userManager.FindByNameAsync(username);
            if (user == null)
            {
                return "Invalid Username or Password";
            }
            var result = await signInManger.PasswordSignInAsync(user, password, false, false);

            if (!result.Succeeded)
            {
                return null;
            }

            return GenerateToken(user);
        }

        private string GenerateToken(Users user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),

            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: configuration["JWT:issuer"],
                audience: configuration["JWT:audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
