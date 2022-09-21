using Moq;
using PayCoreFinalWork.Business.Abstract;
using PayCoreFinalWork.Business.Concrete.Services;
using PayCoreFinalWork.DataAccess.Abstract;
using PayCoreFinalWork.Entity.Concrete.Entities;
using Xunit;

namespace PayCoreFinalWork.Tests
{
    public class ProductTests
    {
        private readonly Mock<IProductSession> _mock;
        private readonly IProductService _productService;

        public ProductTests()
        {
            _mock = new Mock<IProductSession>();
            _productService = new ProductService(_mock.Object);
        }

        [Fact]
        public void Product_category_id_correction()
        {
            var productId = new Guid("90480ce9-9702-48ed-852e-bc17db393bcc");
            var categoryId = new Guid("3eb597f2-ba42-4546-82bf-f52c208f08b0");

            // _mock.Setup(p => p.GetById(productId).Result.Response.CategoryId).Returns(categoryId);
            // var result = _mock.Object.GetById(productId).Result.Response.CategoryId;
            // Assert.Equal(categoryId, result);
        }
    }
}