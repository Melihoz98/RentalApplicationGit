using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Moq;
using Xunit;
using RentAppMVC.Models;
using RentAppMVC.ServiceLayer;

namespace RentAppMVC.UnitTests
{
    public class ProductAccessTests
    {
        [Fact]
        public async Task GetProductById_Returns_Correct_Product()
        {
            // Arrange
            int productId = 12;
            var expectedProduct = new Product
            {
                ProductID = 12,
                ProductName = "Hard Hat",
                Description = "Safety hard hat for construction workers.",
                HourlyPrice = 5.00m,
                CategoryID = 23,
                ImagePath = "/images/hard_hat.jpg"
            };

            var mockServiceConnection = new Mock<IServiceConnection>();
            // Set up the mock to return the expected product when GetById is called with the correct URL
            mockServiceConnection.Setup(conn => conn.GetById($"https://localhost:7023/api/Product/{productId}"))
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(expectedProduct))
                });

            var productAccess = new ProductAccess(mockServiceConnection.Object);

            // Act
            var result = await productAccess.GetProductById(productId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedProduct.ProductID, result.ProductID);
            Assert.Equal(expectedProduct.ProductName, result.ProductName);
            Assert.Equal(expectedProduct.Description, result.Description);
            Assert.Equal(expectedProduct.HourlyPrice, result.HourlyPrice);
            Assert.Equal(expectedProduct.CategoryID, result.CategoryID);
            Assert.Equal(expectedProduct.ImagePath, result.ImagePath);
        }
    }
}
