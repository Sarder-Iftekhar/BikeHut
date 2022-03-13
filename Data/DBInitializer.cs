using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BikeShop.AppDbContext;
using BikeShop.Helpers;
using BikeShop.Models;
using BikeShop.Data;


namespace BikeShop.Data
{
    public class DBInitializer : IDBInitializer
    {
        private readonly BikeShopDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DBInitializer(BikeShopDbContext db, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }


        public async void Initialize()
        {
            //Add pending migration if exists
            if (_db.Database.GetPendingMigrations().Count() >0)
            {
                _db.Database.Migrate();
            }

            //Exit if role already exists
            if (_db.Roles.Any(r => r.Name == Helpers.Roles.Admin)) return;

            //Create Admin role
            _roleManager.CreateAsync(new IdentityRole(Roles.Admin)).GetAwaiter().GetResult();

            //Create Admin user
            _userManager.CreateAsync(new ApplicationUser
            {
                UserName = "Admin",
                Email = "Admin@gmail.com",
                EmailConfirmed = true,
            }, "@Admin22").GetAwaiter().GetResult();

            //Assign role to Admin user
            await _userManager.AddToRoleAsync(await _userManager.FindByNameAsync("Admin"), Roles.Admin);

        }
    }
}
