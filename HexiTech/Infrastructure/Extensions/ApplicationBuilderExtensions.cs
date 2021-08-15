namespace HexiTech.Infrastructure.Extensions
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    using Data;
    using HexiTech.Data.Models;

    using static WebConstants;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(
            this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;

            MigrateDatabase(services);

            SeedCategories(services);
            SeedAdministrator(services);

            return app;
        }

        private static void MigrateDatabase(IServiceProvider services)
        {
            var db = services.GetRequiredService<HexiTechDbContext>();

            db.Database.Migrate();
        }

        private static void SeedCategories(IServiceProvider services)
        {
            var db = services.GetRequiredService<HexiTechDbContext>();

            if (db.Categories.Any())
            {
                return;
            }

            db.Categories.AddRange(new[]
            {
                new Category { Name = "Mobile phones and tablets" },
                new Category { Name = "Appliances" },
                new Category { Name = "Outdoor and camping" },
                new Category { Name = "Sports and activities" },
            });

            db.SaveChanges();

            db.ProductTypes.AddRange(new[]
            {
                new ProductType { Name = "Smartphones", Category = db.Categories.FirstOrDefault(c => c.Name == "Mobile phones and tablets") },
                new ProductType { Name = "Tablets", Category = db.Categories.FirstOrDefault(c => c.Name == "Mobile phones and tablets") },

                new ProductType { Name = "Mixers", Category = db.Categories.FirstOrDefault(c => c.Name == "Appliances") },
                new ProductType { Name = "Blenders", Category = db.Categories.FirstOrDefault(c => c.Name == "Appliances") },

                new ProductType { Name = "Cooler boxes", Category = db.Categories.FirstOrDefault(c => c.Name == "Outdoor and camping") },
                new ProductType { Name = "Mosquito repellents", Category = db.Categories.FirstOrDefault(c => c.Name == "Outdoor and camping") },

                new ProductType { Name = "Electric scooters", Category = db.Categories.FirstOrDefault(c => c.Name == "Sports and activities") },
                new ProductType { Name = "Treadmills", Category = db.Categories.FirstOrDefault(c => c.Name == "Sports and activities") },
            });

            db.SaveChanges();
        }

        private static void SeedAdministrator(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task
                .Run(async () =>
                {
                    if (await roleManager.RoleExistsAsync(AdministratorRoleName))
                    {
                        return;
                    }

                    var role = new IdentityRole { Name = AdministratorRoleName };

                    await roleManager.CreateAsync(role);

                    const string adminEmail = "admin@hex.com";
                    const string adminPassword = "admin123";

                    var user = new User
                    {
                        Email = adminEmail,
                        UserName = adminEmail,
                        FullName = "Admin"
                    };

                    await userManager.CreateAsync(user, adminPassword);

                    await userManager.AddToRoleAsync(user, role.Name);
                })
                .GetAwaiter()
                .GetResult();
        }
    }
}