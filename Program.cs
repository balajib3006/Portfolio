using BalajiPortfolio.Helpers.DbContexts;
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

            #region Services & Database Configuration
            builder.Services.AddControllersWithViews();

            // Configure Entity Framework Core with SQLite
            builder.Services.AddDbContext<appDBContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") 
                    ?? "Data Source=portfolio.db"));

            // Register Repositories and Response Handler
            builder.Services.AddScoped<IApiResponseRepository, ApiResponseRepository>();
            builder.Services.AddScoped<IPersonalRepository, PersonalRepository>();
            builder.Services.AddScoped<IExperienceRepository, ExperienceRepository>();
            builder.Services.AddScoped<ISkillRepository, SkillRepository>();
            builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
            builder.Services.AddScoped<ICertificationRepository, CertificationRepository>();
            builder.Services.AddScoped<IPublicationRepository, PublicationRepository>();
            #endregion

            var app = builder.Build();

            #region Startup Database Initialization
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<appDBContext>();
                dbContext.Database.EnsureCreated();
            }
            #endregion

            #region HTTP Pipeline Configuration
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
            #endregion

            app.Run();
        }
    }
}
