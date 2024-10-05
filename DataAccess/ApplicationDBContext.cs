using EcommerceShoppingApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


namespace EcommerceShoppingApp.DataAccess
{
    public class ApplicationDBContext: DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
