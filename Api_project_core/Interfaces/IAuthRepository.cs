using Api_project_core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_project_core.Interfaces
{
public interface IAuthRepository
    {
        Task<string> RegisterAsync(Users user, string password);
        Task<string> LoginAsync(string username, string password);
        Task<string> ChangePasswordAsync(string email, string oldPassword, string newPassword);
    }
}
