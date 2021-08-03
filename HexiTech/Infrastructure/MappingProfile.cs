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
            //this.CreateMap<Product, LatestProductServiceModel>();
            this.CreateMap<ProductDetailsServiceModel, ProductFormModel>();
            this.CreateMap<Product, ProductDetailsServiceModel>();
        }
    }
}