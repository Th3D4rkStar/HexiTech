namespace HexiTech.Infrastructure
{
    using AutoMapper;

    using Models.Products;
    using HexiTech.Data.Models;
    using HexiTech.Services.Cart.Models;
    using HexiTech.Services.Products.Models;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Category, ProductCategoryServiceModel>();
            this.CreateMap<ProductType, ProductTypeServiceModel>();

            this.CreateMap<ProductDetailsServiceModel, ProductFormModel>();
            this.CreateMap<UserShoppingCart, CartItemServiceModel>();
            this.CreateMap<UserOrdersList, Order>();


              this.CreateMap<Product, ProductServiceModel>()
              .ForMember(p => p.CategoryName, cfg => cfg.MapFrom(p => p.Category.Name));

            this.CreateMap<Product, ProductDetailsServiceModel>()
                .ForMember(p => p.CategoryName, cfg => cfg.MapFrom(p => p.Category.Name));
        }
    }
}