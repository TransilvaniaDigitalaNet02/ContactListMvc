using ContactListMvc.Business;
using ContactListMvc.Infrastructure;

namespace ContactListMvc.Web
{
    public class Program
    {
        // For a brief description of a "traditional" N-tier app structure
        // see: https://learn.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures#traditional-n-layer-architecture-applications
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddBusinessLogic();

            string connectionString = builder.Configuration.GetConnectionString("DatabaseContext")
                ?? throw new InvalidOperationException("Connection string 'DatabaseContext' not found.");

            builder.Services.AddInfrastructre(connectionString);

            WebApplication app = builder.Build();

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
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            /*
            // Example of custom routing:
            app.MapControllerRoute(
                name: "my-custom-route",
                pattern: "contact-list",
                defaults: new { controller = "ContactListEntries", action = "Index" });
            */

            app.Run();
        }
    }
}