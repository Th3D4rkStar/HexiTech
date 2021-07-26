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
                    {
                        Name = "Mobile phones and tablets"
                    },
                    new Category
                    {
                        Name = "Example Category"
                    });

                context.SaveChanges();
            }

            if (!context.ProductTypes.Any())
            {
                context.ProductTypes.AddRange(
                    new ProductType
                    {
                        Name = "Phones",
                        Category = context.Categories.FirstOrDefault(c => c.Name == "Mobile phones and tablets"),
                    });

                context.SaveChanges();
            }
        }
    }
}