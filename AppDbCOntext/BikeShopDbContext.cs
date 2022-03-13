using BikeShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BikeShop.AppDbContext
{
    public class BikeShopDbContext:IdentityDbContext<IdentityUser>
    {
        public BikeShopDbContext(DbContextOptions<BikeShopDbContext> options):base(options)
        {

        }

        public DbSet<Make> Makes { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Bike> Bikes { get; set; }  
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}
