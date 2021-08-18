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
    using Data.Enums;
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

            db.Products.AddRange(new[]
            {
                new Product
                {
                    Brand = "Samsung", Series = "Galaxy", Model = "S21", ImageUrl = "https://localhost:5001/img/Products/samsung-galaxy-s21.jpg",
                    ProductType = db.ProductTypes.FirstOrDefault(pt=>pt.Name == "Smartphones"), Category = db.Categories.FirstOrDefault(c => c.Name == "Mobile phones and tablets"),
                    Price = 800M, Quantity = 5, Availability = ProductAvailability.Available, IsPublic = false,
                    Description = "With the new Samsung Galaxy S21 5G, you have the ability to record in 8K. Which means your videos are double the resolution of 4K video. Or use the 64MP camera for still shots that come out clear whether it’s day or night. Single Take AI transcends the usual restrictions of photo and video editing to capture life’s greatest moments in one single take. Samsung Galaxy S21, made for the epic in every day.",
                    Specifications = "Network Speed - 5G Capable | Screen size - 6.2\" FHD+ Dynamic AMOLED | Camera - Triple 12MP, 12MP, 64MP rear, 10MP front | Operating System - Android 11 | Processor - Octa-core 2.84GHz | Battery Life - 4,000 mAh battery | Memory - 128GB, no microSD slot | RAM - 8GB"
                },
                new Product
                {
                    Brand = "Apple", Series = "iPhone", Model = "12", ImageUrl = "https://localhost:5001/img/Products/apple-iPhone-12.jpg",
                    ProductType = db.ProductTypes.FirstOrDefault(pt=>pt.Name == "Smartphones"), Category = db.Categories.FirstOrDefault(c => c.Name == "Mobile phones and tablets"),
                    Price = 1200M, Quantity = 8, Availability = ProductAvailability.AtWarehouse, IsPublic = false,
                    Description = "Blast past fast. Say hello to the fastest smartphone chip ever. 5G speed. An advanced dual-camera system. Ceramic Shield for 4x better drop performance. And a brighter, more colorful OLED display. iPhone 12 is a total powerhouse — in two great sizes.",
                    Specifications = "Network Speed - 5G capable | Screen size - 6.1\" Super Retina XDR OLED display (460 ppi) | Camera - Dual 12MP rear cameras, 12MP front camera | Operating System - iOS 14 | Processor - A14 Bionic Chip | SIM Card - Dual SIM (Nano and eSIM) | Battery Life - Up to 17 hour video, 65 hour audio playback | Memory - 64GB, 128GB or 256GB"
                },

                new Product
                {
                Brand = "Prestigio", Model = "Q Pro", ImageUrl = "https://localhost:5001/img/Products/prestigio-qPro.png",
                ProductType = db.ProductTypes.FirstOrDefault(pt=>pt.Name == "Tablets"), Category = db.Categories.FirstOrDefault(c => c.Name == "Mobile phones and tablets"),
                Price = 300M, Quantity = 0, Availability = ProductAvailability.OutOfStock, IsPublic = false,
                Description = "A FAMILY-CENTRED TABLET You also receive a set of children's accessories bundled with the tablet as a gift: a bright “dinosaur” stand for the tablet, a protective silicone case, a screen cleaning cloth and a screen protector. Children will adore all the wonderful additions. | HANDY DESIGN Made of durable textured plastic, the tablet is pleasant to the eye and smooth to the touch. The case can be easily held in one hand and includes scratch protection coating. The model line-up is available in dark grey, red and mint colours. | PARENTAL CONTROL FUNCTION Parents can restrict their child’s access to content and control the applications and programs they use. In addition, you can install useful content for children: special educational games and applications. | REALISTIC 8 - INCH IPS SCREEN Equipped with an 8 - inch HD screen, the tablet displays bright and lively content. IPS guarantees natural colour rendition and wide view angles, making it possible to look at the screen from any angle without any distortions. Your tablet is convenient and mobile and ensures that you can pass the time by watching your favourite movie or scrolling social networks.",
                Specifications = "SOFTWARE: Operating System - Android 9.0; | PROCESSOR: CPU - Spreadtrum SC9832E; CPU Internal Clock Rate - 1.40 GHz; CPU Core Quantity - 4; | DISPLAY: Display Size - 8.0\"; Display Resolution - 1280x800; Display Technology - IPS LCD; Display Features - Capacitive Multi-touch Screen; | MEMORY: Internal Memory - 16 GB; Installed RAM - 2 GB; Flash Card - Supported 128GB MicroSD/MicroSDHC/MircoSDXC Card; | MOBILE WEB: SIM Card - Single Sim; SIM Card Type - Micro-SIM; Connectivity Technology - 4G; Mobile Network Bands - WCDMA 900/2100 MHz, GSM 850/900/1800/1900 MHz, LTE 800/1800/2600 MHz; Call Function - Yes; | INTERFACE: Interfaces - USB 2.0, Wi-Fi, BT; Interface Wi-Fi - IEEE 802.11b/g/n; Interface Bluetooth - Bluetooth 4.2; Interface USB - 1 x Micro USB 2.0; Interface Audio - 1 x 3.5 mm mini jack; | BATTERY: Battery Technology - Lithium Ion Polymer; Battery Current Capacity - 5000 mAh; | MISCELLANEOUS: Built-in Devices - Microphone, Speaker; Included Accessories - Carrying Case, USB Cable, AC Adapter, Screen Protector, Cleaning Cloth, Stand; | DIMENSIONS & WEIGHT DEVICE: Width (mm) - 125; Height (mm) - 209; Depth (mm) - 9.8; Nominal Weight - 0.351 kg; | WARRANTY: Warranty Term (month) - 24;"
                }
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