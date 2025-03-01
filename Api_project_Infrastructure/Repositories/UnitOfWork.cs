using Api_project_core.Interfaces;
using Api_project_core.Models;
using Api_project_Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_project_Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext context;
        private readonly UserManager<Users> userManager;
        private readonly SignInManager<Users> signInManager;
        private readonly IConfiguration configuration;

        public IAuthRepository authRepository { get; }

        public IInvoiceRepository invoiceRepository { get; }

        public ITemsIRepository itemRepository { get; }

        public ICartRepository cartRepository { get; }

        public UnitOfWork(AppDbContext context,UserManager<Users> userManager, SignInManager<Users>signInManager,IConfiguration configuration)
        {
           
            this.context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
            authRepository = new AuthRepository(userManager,signInManager,configuration);
            invoiceRepository = new InvoiceRepository(context);
            itemRepository = new ItemsRepository(context);
            cartRepository = new CartRepository(context);
        }

        public async Task<int> SaveAsync()
        {
          return await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
