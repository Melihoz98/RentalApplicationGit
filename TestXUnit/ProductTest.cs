using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using RentalService.Business;
using RentalService.DataAccess;
using RentalService.DTO;
using RentalService.Models;
using Xunit;

namespace RentalService.Tests
{
    public class ProductDataLogicTests : IDisposable
    {
        private readonly IProductAccess _productAccess;
        private readonly ProductDataLogic _productDataLogic;
        private readonly List<int> _createdProductIds = new List<int>();

        public ProductDataLogicTests()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            _productAccess = new ProductAccess(configuration);
            _productDataLogic = new ProductDataLogic(_productAccess);
        }

        [Fact]
        public void Test_CreateProduct()
        {
            // Arrange
            var productDto = new ProductDto
            {
                ProductName = "Integration Test Product",
                Description = "Test Description",
                HourlyPrice = 10.0m,
                CategoryID = 21,
                ImagePath = "testpath.jpg"
            };

            // Act
            int newProductId = _productDataLogic.CreateProduct(productDto);

            // Assert
            Assert.True(newProductId > 0);

            // Track the created product for cleanup
            _createdProductIds.Add(newProductId);
        }

        [Fact]
        public void Test_GetProductById()
        {
            // Arrange
            var productDto = new ProductDto
            {
                ProductName = "Integration Test Product",
                Description = "Test Description",
                HourlyPrice = 10.0m,
                CategoryID = 21,
                ImagePath = "testpath.jpg"
            };
            int newProductId = _productDataLogic.CreateProduct(productDto);
            _createdProductIds.Add(newProductId);

            // Act
            var retrievedProduct = _productDataLogic.GetById(newProductId);

            // Assert
            Assert.NotNull(retrievedProduct);
            Assert.Equal("Integration Test Product", retrievedProduct.ProductName);
        }

        [Fact]
        public void Test_GetAllProducts()
        {
            // Act
            var products = _productDataLogic.GetAllProducts();

            // Assert
            Assert.NotNull(products);
            Assert.True(products.Count > 0, "Expected at least one product in the database.");
        }

        [Fact]
        public void Test_DeleteProduct()
        {
            // Arrange
            var productDto = new ProductDto
            {
                ProductName = "Integration Test Product to Delete",
                Description = "Test Description",
                HourlyPrice = 10.0m,
                CategoryID = 21,
                ImagePath = "testdelete.jpg"
            };
            int newProductId = _productDataLogic.CreateProduct(productDto);

            // Act
            _productDataLogic.DeleteProduct(newProductId);
            var deletedProduct = _productDataLogic.GetById(newProductId);

            // Assert
            Assert.Null(deletedProduct);
        }

        public void Dispose()
        {
            // Clean up the database by deleting created products
            foreach (var productId in _createdProductIds)
            {
                _productDataLogic.DeleteProduct(productId);
            }
        }
    }
}
