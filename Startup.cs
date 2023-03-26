using HenriJervsonGrainWarehouse.Data;
using HenriJervsonGrainWarehouse.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace HenriJervsonGrainWarehouse
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
            services.AddIdentity<WarehouseUser, WarehouseRole>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddDefaultUI()
                .AddEntityFrameworkStores<MyDbContext>();

            services.AddControllersWithViews();

            services.AddRazorPages();

            services.AddDbContext<MyDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("MyConnectionString")));

            services.AddScoped<CargoRepository>(provider =>
            {
                var options = provider.GetRequiredService<DbContextOptions<MyDbContext>>();
                return new CargoRepository(options);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            SetupAppDataAsync(app, env);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
            }

            );
        }

        private async Task SetupAppDataAsync(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using var userManager = serviceScope.ServiceProvider.GetService<UserManager<WarehouseUser>>();
            using var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<WarehouseRole>>();
            using var context = serviceScope.ServiceProvider.GetService<MyDbContext>();
            if (context == null)
            {
                throw new ApplicationException("Problem in services. Can not initialize context");
            }
            while (true)
            {
                try
                {
                    context.Database.OpenConnection();
                    context.Database.CloseConnection();
                    context.Database.EnsureCreated();
                    break;
                }
                catch (SqlException e)
                {
                    if (e.Message.Contains("The login failed.")) { break; }
                    System.Threading.Thread.Sleep(1000);
                }
            }
            await SeedData.SeedIdentity(userManager, roleManager);
            context.SaveChanges();
        }

    }
}
