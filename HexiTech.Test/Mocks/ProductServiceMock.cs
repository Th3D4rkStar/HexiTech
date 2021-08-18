namespace HexiTech.Test.Mocks
{
    using Moq;

    using Services.Products;

    public static class ProductServiceMock
    {
        public static IProductService Instance
        {
            get
            {
                var serviceMock = new Mock<IProductService>();

                return serviceMock.Object;
            }
        }
    }
}