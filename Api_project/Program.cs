
using Api_project_core.Interfaces;
using Api_project_core.Models;
using Api_project_Infrastructure.Data;
using Api_project_Infrastructure.Repositories;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Api_project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddIdentity<Users, IdentityRole<int>>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;

                options.Password.RequiredUniqueChars = 0;

            }).AddEntityFrameworkStores<AppDbContext>();
            //builder.Services.AddScoped<IAuthRepository, AuthRepository>();
            //builder.Services.AddScoped<ITemsIRepository, ItemsRepository>();
            //builder.Services.AddScoped<ICartRepository, CartRepository>();
            //builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            var config = TypeAdapterConfig.GlobalSettings;
            builder.Services.AddSingleton(config);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
