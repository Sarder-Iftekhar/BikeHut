using BikeShop.AppDbContext;
using BikeShop.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeShop
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<BikeShopDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default")));
            //Error!! without changes this 
            //services.AddDefaultIdentity<IdentityUser>()
            //    .AddEntityFrameworkStores<BikeShopDbContext>();
            //to this
            //add identity role pass it as parameter
            //add DefaultUI and DefaultTokenProviders
            services.AddIdentity<IdentityUser, IdentityRole>()
               .AddEntityFrameworkStores<BikeShopDbContext>()
               .AddDefaultUI()
               .AddDefaultTokenProviders();
            //for automatic data base migration added this line
            services.AddScoped<IDBInitializer, DBInitializer>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //parameter IDBInitializer is added for auto database migration
        public void Configure(IApplicationBuilder app, IHostingEnvironment env ,IDBInitializer dBInitializer)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            //this is added for auto migration
            dBInitializer.Initialize();



            app.UseCookiePolicy();
            //Convention based routes

            app.UseMvc(routes =>
            {
                //create conventional cutom route for the url
                //http:mysite.com/make/bike/2018/01
                //name
                //url template
                //default
                //add constraint :int
                //constrain separate by semicolon
                //note:there are one cutom but may have 100 of custom route

                //routes.MapRoute(
                //    "ByYearMonth",
                //    "make/bikes/{year:int:length(4)}/{month:int:range(1,12)}",
                //    new { controller = "make", action = "ByYearMonth" }
                //    );
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
