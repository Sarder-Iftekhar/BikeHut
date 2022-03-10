using BikeShop.Models;
using Microsoft.EntityFrameworkCore;

namespace BikeShop.AppDbCOntext
{
    public class BikeShopDbContext:DbContext
    {
        public BikeShopDbContext(DbContextOptions<BikeShopDbContext> options):base(options)
        {

        }

        public DbSet<Make> Makes { get; set; }
        public DbSet<Model> Models { get; set; }
    }
}
