using BalajiPortfolio.Helpers.DbContexts;
using BalajiPortfolio.Interfaces;
using BalajiPortfolio.Repositories;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;

namespace BalajiPortfolio
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Kestrel & Request Body Size Limit Configuration
            // Increase Kestrel request body size limit to 100 MB
            builder.WebHost.ConfigureKestrel(options =>
            {
                options.Limits.MaxRequestBodySize = 104857600; // 100 MB
            });

            // Increase request/upload size limit to 100 MB
            builder.Services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = 104857600; // 100 MB
            });
            #endregion

            #region Services & Database Configuration
            builder.Services.AddControllersWithViews();

            // Configure Entity Framework Core with MySQL
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
                ?? builder.Configuration.GetConnectionString("MySQLConnectionString")
                ?? "Server=localhost;Database=balajiPortfolio;User=root;Password=Santhosh@20;Port=3306;";

            builder.Services.AddDbContext<appDBContext>(options =>
                options.UseMySql(
                    connectionString,
                    new MySqlServerVersion(new Version(8, 0, 36))
                ));

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
                try
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<appDBContext>();
                    dbContext.Database.EnsureCreated();
                }
                catch (Exception ex)
                {
                    app.Logger.LogWarning($"Database initialization warning: {ex.Message}");
                }
            }
            #endregion

            #region Pipeline & Forwarded Headers Configuration
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            var forwardedHeadersOptions = new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            };
            forwardedHeadersOptions.KnownNetworks.Clear();
            forwardedHeadersOptions.KnownProxies.Clear();
            app.UseForwardedHeaders(forwardedHeadersOptions);

            app.UseHttpsRedirection();

            // Configure MIME types for static files
            var provider = new FileExtensionContentTypeProvider();
            provider.Mappings[".mp4"] = "video/mp4";
            provider.Mappings[".webm"] = "video/webm";
            provider.Mappings[".ogv"] = "video/ogg";
            provider.Mappings[".step"] = "application/step";
            provider.Mappings[".stp"] = "application/step";
            provider.Mappings[".iges"] = "application/iges";

            app.UseStaticFiles(new StaticFileOptions
            {
                ContentTypeProvider = provider
            });

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
