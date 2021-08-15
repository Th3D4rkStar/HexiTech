namespace HexiTech.Infrastructure
{
    using AutoMapper;

    using HexiTech.Data.Models;
    using HexiTech.Services.Cart.Models;
    using HexiTech.Services.Products.Models;
    using Models.Products;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Category, ProductCategoryServiceModel>();
            this.CreateMap<ProductType, ProductTypeServiceModel>();

            this.CreateMap<Product, LatestProductServiceModel>();
            this.CreateMap<ProductDetailsServiceModel, ProductFormModel>();
            this.CreateMap<UserShoppingCart, CartItemServiceModel>();


            this.CreateMap<Product, ProductServiceModel>()
              .ForMember(p => p.CategoryName, cfg => cfg.MapFrom(p => p.Category.Name));

            this.CreateMap<Product, ProductDetailsServiceModel>()
                .ForMember(p => p.CategoryName, cfg => cfg.MapFrom(p => p.Category.Name));
        }
    }
}