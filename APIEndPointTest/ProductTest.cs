using Moq;
using RentalService.Business;
using RentalService.DataAccess;
using RentalService.DTO;
using RentalService.Models;
using RentalService.ModelConversion;
using System;
using System.Collections.Generic;
using Xunit;

namespace RentalService.APIEndPointTest
{
    public class ProductDataLogicTests
    {
        private readonly Mock<IProductAccess> _mockProductAccess;
        private readonly ProductDataLogic _productDataLogic;

        public ProductDataLogicTests()
        {
            _mockProductAccess = new Mock<IProductAccess>();
            _productDataLogic = new ProductDataLogic(_mockProductAccess.Object);
        }

        [Fact]
        public void GetById_ShouldReturnProductDto_WhenProductExists()
        {
            // Arrange
            int productId = 1;
            var product = new Product(productId, "Test Product", "Description", 10.0m, 1, "image.jpg");
            _mockProductAccess.Setup(pa => pa.GetProductById(productId)).Returns(product);

            // Act
            var result = _productDataLogic.GetById(productId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(productId, result.ProductID);
        }

        [Fact]
        public void GetById_ShouldReturnNull_WhenProductDoesNotExist()
        {
            // Arrange
            int productId = 1;
            _mockProductAccess.Setup(pa => pa.GetProductById(productId)).Returns((Product)null);

            // Act
            var result = _productDataLogic.GetById(productId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetAllProducts_ShouldReturnListOfProductDtos_WhenProductsExist()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product(1, "Product 1", "Description 1", 10.0m, 1, "image1.jpg"),
                new Product(2, "Product 2", "Description 2", 20.0m, 2, "image2.jpg")
            };
            _mockProductAccess.Setup(pa => pa.GetProductAll()).Returns(products);

            // Act
            var result = _productDataLogic.GetAllProducts();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void GetAllProducts_ShouldReturnNull_WhenExceptionIsThrown()
        {
            // Arrange
            _mockProductAccess.Setup(pa => pa.GetProductAll()).Throws(new Exception("Database error"));

            // Act
            var result = _productDataLogic.GetAllProducts();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void CreateProduct_ShouldReturnProductId_WhenProductIsAddedSuccessfully()
        {
            // Arrange
            var productDto = new ProductDto { ProductID = 0, ProductName = "New Product", Description = "Description", HourlyPrice = 15.0m, CategoryID = 1, ImagePath = "image.jpg" };
            var product = ProductDtoConvert.ToProduct(productDto);
            _mockProductAccess.Setup(pa => pa.AddProduct(It.IsAny<Product>())).Returns(1);

            // Act
            var result = _productDataLogic.CreateProduct(productDto);

            // Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public void CreateProduct_ShouldReturnZero_WhenExceptionIsThrown()
        {
            // Arrange
            var productDto = new ProductDto { ProductID = 0, ProductName = "New Product", Description = "Description", HourlyPrice = 15.0m, CategoryID = 1, ImagePath = "image.jpg" };
            _mockProductAccess.Setup(pa => pa.AddProduct(It.IsAny<Product>())).Throws(new Exception("Database error"));

            // Act
            var result = _productDataLogic.CreateProduct(productDto);

            // Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void DeleteProduct_ShouldCallDeleteProduct_WhenProductExists()
        {
            // Arrange
            int productId = 1;

            // Act
            _productDataLogic.DeleteProduct(productId);

            // Assert
            _mockProductAccess.Verify(pa => pa.DeleteProduct(productId), Times.Once);
        }

        [Fact]
        public void DeleteProduct_ShouldNotThrowException_WhenExceptionIsCaught()
        {
            // Arrange
            int productId = 1;
            _mockProductAccess.Setup(pa => pa.DeleteProduct(productId)).Throws(new Exception("Database error"));

            // Act
            var exception = Record.Exception(() => _productDataLogic.DeleteProduct(productId));

            // Assert
            Assert.Null(exception);
        }
    }
}
