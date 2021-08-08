namespace HexiTech.Infrastructure
{
    using HexiTech.Data.Models;
    using HexiTech.Services.Products.Models;
    using Models.Products;
    using AutoMapper;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Category, ProductCategoryServiceModel>();
            this.CreateMap<ProductType, ProductTypeServiceModel>();

            this.CreateMap<Product, LatestProductServiceModel>();
            this.CreateMap<ProductDetailsServiceModel, ProductFormModel>();


              this.CreateMap<Product, ProductServiceModel>()
                .ForMember(p => p.CategoryName, cfg => cfg.MapFrom(p => p.Category.Name));

            this.CreateMap<Product, ProductDetailsServiceModel>()
                .ForMember(p => p.CategoryName, cfg => cfg.MapFrom(p => p.Category.Name));
        }
    }
}