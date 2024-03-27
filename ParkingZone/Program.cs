using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ParkingZone.Data;
namespace ParkingZone
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? 
                throw new InvalidOperationException("Connection string 'ParkingZoneDbContextConnection' not found.");

            builder.Services.AddDbContext<ParkingZoneDbContext>(options => options.UseSqlServer(connectionString));
            


            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ParkingZoneDbContext>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();
            var app = builder.Build();



            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
           
            app.MapControllerRoute(
               name: "Admin",
               pattern: "{area:exists}/{controller=ParkingZones}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}
