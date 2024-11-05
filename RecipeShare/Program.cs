using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RecipeShare.Data;
using RecipeShare.Services;

namespace RecipeShare {
    public class Program {
        public static async Task Main(string[] args) {

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddMemoryCache();
            builder.Services.AddScoped<RecipeCacheService>();

            builder.Services.AddControllersWithViews(options => {
                Console.WriteLine("FILTER ADDED");
                options.Filters.Add(typeof(UserActivityFilter)); // Note: Ensure you have the correct namespace for UserActivityFilter
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment()) {
                app.UseMigrationsEndPoint();
            }
            else {
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
            app.MapRazorPages();

            //Scoped: A new instance is created per request (example: HTTP request)
            using (IServiceScope scope = app.Services.CreateScope()) {

                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                //SEEDING ROLES
                string[] roles = { "Admin", "Moderator", "Regular" };

                foreach (string role in roles) {

                    if (!await roleManager.RoleExistsAsync(role) ) {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }
            }

            using (IServiceScope scope = app.Services.CreateScope()) {

                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

                string adminEmail = "admin@admin.com";
                string adminPassword = "Admin123@";

                //SEEDING ADMIN ACCOUNT
                if (await userManager.FindByEmailAsync(adminEmail) == null) {
                    
                    IdentityUser adminUser = new IdentityUser();

                    adminUser.UserName = adminEmail;
                    adminUser.Email = adminEmail;
                    adminUser.EmailConfirmed = true;
                    
                    await userManager.CreateAsync(adminUser, adminPassword);

                    await userManager.AddToRoleAsync(adminUser, "Admin");
                
                }
            }
            app.Run();
        }
    }
}
