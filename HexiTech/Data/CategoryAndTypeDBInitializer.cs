namespace HexiTech.Data
{
    using System.Linq;
    using Models;

    public class CategoryAndTypeDBInitializer
    {
        public static void Seed(HexiTechDbContext context)
        {
            //context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            if (!context.Categories.Any())
            {
                context.Categories.AddRange(
                    new Category
                    { Name = "Mobile phones and tablets" },
                    new Category
                    { Name = "Appliances" },
                    new Category
                    { Name = "Outdoor and camping" },
                    new Category
                    { Name = "Sports and activities" });

                context.SaveChanges();
            }

            if (!context.ProductTypes.Any())
            {
                context.ProductTypes.AddRange(
                    new ProductType
                    {
                        Name = "Smartphones",
                        Category = context.Categories.FirstOrDefault(c => c.Name == "Mobile phones and tablets")
                    },
                    new ProductType
                    {
                        Name = "Tablets",
                        Category = context.Categories.FirstOrDefault(c => c.Name == "Mobile phones and tablets")
                    },
                    new ProductType
                    {
                        Name = "Mixers",
                        Category = context.Categories.FirstOrDefault(c => c.Name == "Appliances")
                    },
                    new ProductType
                    {
                        Name = "Blenders",
                        Category = context.Categories.FirstOrDefault(c => c.Name == "Appliances")
                    },
                    new ProductType
                    {
                        Name = "Cooler boxes",
                        Category = context.Categories.FirstOrDefault(c => c.Name == "Outdoor and camping")
                    },
                    new ProductType
                    {
                        Name = "Mosquito repellents",
                        Category = context.Categories.FirstOrDefault(c => c.Name == "Outdoor and camping")
                    },
                    new ProductType
                    {
                        Name = "Electric scooters",
                        Category = context.Categories.FirstOrDefault(c => c.Name == "Sports and activities")
                    },
                    new ProductType
                    {
                        Name = "Treadmills",
                        Category = context.Categories.FirstOrDefault(c => c.Name == "Sports and activities")
                    });

                context.SaveChanges();
            }
        }
    }
}