using Api_project_core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_project_Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<Users, IdentityRole<int>, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ItemsUnits>().HasKey(i => new { i.Item_Id, i.Unit_Id });
            builder.Entity<CustomerStores>().HasKey(i => new { i.Cus_Id, i.Store_Id });
            builder.Entity<InvItemStores>().HasKey(i => new { i.Store_Id, i.Item_Id });
            builder.Entity<ShoppingCartItems>().HasKey(i => new { i.Item_id, i.Cus_Id, i.Store_Id });
            builder.Entity<InvoiceDetails>().HasKey(i => new { i.Invoice_Id, i.Item_Id });



        }
        public DbSet<Items> Items { get; set; }
        public DbSet<Units> Units { get; set; }
        public DbSet<Stores> Stores { get; set; }
        public DbSet<ItemsUnits> ItemsUnits { get; set; }

        public DbSet<CustomerStores> CustomerStores { get; set; }
        public DbSet<InvItemStores> InvItemStores { get; set; }
        public DbSet<ShoppingCartItems> ShoppingCartItems { get; set; }
        public DbSet<Invoice> Invoice { get; set; }
        public DbSet<InvoiceDetails> InvoiceDetails { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Zones> Zones { get; set; }
        public DbSet<Cities> Cities { get; set; }
        public DbSet<Goverments> Goverments { get; set; }
        public DbSet<Classifications> Classifications { get; set; }
        public DbSet<MainGroup> MainGroups { get; set; }
        public DbSet<SubGroup> SubGroups { get; set; }
        public DbSet<SubGroup2> SubGroup2s { get; set; }





    }
}
