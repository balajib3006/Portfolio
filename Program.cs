using BalajiPortfolio.Data;
using BalajiPortfolio.Interfaces;
using BalajiPortfolio.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BalajiPortfolio
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Configure Entity Framework Core with SQLite
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") 
                    ?? "Data Source=portfolio.db"));

            // Register Repositories
            builder.Services.AddScoped<IPersonalRepository, PersonalRepository>();

            var app = builder.Build();

            // Ensure Database is Created on Startup
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.EnsureCreated();
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Personal}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
