namespace HexiTech.Controllers.Api
{
    using HexiTech.Models.Api.Products;
    using Services.Products;
    using HexiTech.Services.Products.Models;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/products")]
    public class ProductsApiController : ControllerBase
    {
        private readonly IProductService products;

        public ProductsApiController(IProductService products)
            => this.products = products;

        [HttpGet]
        public ProductQueryServiceModel All([FromQuery] AllProductsApiRequestModel query)
            => this.products.All(
                query.Brand,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                query.ProductsPerPage);
    }
}