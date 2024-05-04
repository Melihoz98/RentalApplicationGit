using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Xunit;
using RentAppMVC.BusinessLogicLayer;
using RentAppMVC.Models;
using RentAppMVC.ServiceLayer;

namespace RentAppMVC.UnitTests
{
    public class ProductLogicTests
    {
        [Fact]
        public async Task GetProductById_Returns_Product_When_Successful()
        {
            // Arrange
            int productId = 12;
            Product expectedProduct = new Product
            {
                ProductID = 12,
                ProductName = "Hard Hat",
                Description = "Safety hard hat for construction workers.",
                HourlyPrice = 5.00m,
                CategoryID = 23,
                ImagePath = "/images/hard_hat.jpg"
            };

            var mockProductAccess = new Mock<IProductAccess>();
            mockProductAccess.Setup(access => access.GetProductById(productId))
                .ReturnsAsync(expectedProduct);

            var productLogic = new ProductLogic(mockProductAccess.Object);

            // Act
            var result = await productLogic.GetProductById(productId);

            // Print the returned object to the console
            Console.WriteLine($"Returned Product: {result}");

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
