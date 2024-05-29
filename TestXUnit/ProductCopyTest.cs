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
    public class ProductCopyDataLogicTests : IDisposable
    {
        private readonly IProductCopyAccess _productCopyAccess;
        private readonly ProductCopyDataLogic _productCopyDataLogic;
        private readonly List<string> _createdProductCopySerialNumbers = new List<string>();

        public ProductCopyDataLogicTests()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            _productCopyAccess = new ProductCopyAccess(configuration);
            _productCopyDataLogic = new ProductCopyDataLogic(_productCopyAccess);
        }

        [Fact]
        public void Test_CreateProductCopy()
        {
            // Arrange
            var productCopyDto = new ProductCopyDto
            {
                SerialNumber = "TestSerialNumber",
                ProductID = 9,
             
            };

            // Act
            _productCopyDataLogic.CreateProductCopy(productCopyDto);

            // Assert
            var createdProductCopy = _productCopyDataLogic.GetBySerialNumber("TestSerialNumber");
            Assert.NotNull(createdProductCopy);
            Assert.Equal("TestSerialNumber", createdProductCopy.SerialNumber);

            
            _createdProductCopySerialNumbers.Add("TestSerialNumber");
        }

        [Fact]
        public void Test_GetProductCopyBySerialNumber()
        {
            // Arrange
            var productCopyDto = new ProductCopyDto
            {
                SerialNumber = "TestSerialNumber",
                ProductID = 9,
               
            };
            _productCopyDataLogic.CreateProductCopy(productCopyDto);
            _createdProductCopySerialNumbers.Add("TestSerialNumber");

            // Act
            var retrievedProductCopy = _productCopyDataLogic.GetBySerialNumber("TestSerialNumber");

            // Assert
            Assert.NotNull(retrievedProductCopy);
            Assert.Equal("TestSerialNumber", retrievedProductCopy.SerialNumber);
        }

        [Fact]
        public void Test_GetAllProductCopies()
        {
            // Act
            var productCopies = _productCopyDataLogic.GetAllProductCopies();

            // Assert
            Assert.NotNull(productCopies);
            Assert.True(productCopies.Count > 0, "Expected at least one product copy in the database.");
        }

        [Fact]
        public void Test_DeleteProductCopy()
        {
            // Arrange
            var productCopyDto = new ProductCopyDto
            {
                SerialNumber = "TestSerialNumberToDelete",
                ProductID = 1
               
            };
            _productCopyDataLogic.CreateProductCopy(productCopyDto);

            // Act
            _productCopyDataLogic.DeleteProductCopy("TestSerialNumberToDelete");
            var deletedProductCopy = _productCopyDataLogic.GetBySerialNumber("TestSerialNumberToDelete");

            // Assert
            Assert.Null(deletedProductCopy);
        }

        [Fact]
        public void Test_GetAllProductCopiesByProductID()
        {
            // Arrange
            var productCopyDto = new ProductCopyDto
            {
                SerialNumber = "TestSerialNumber3",
                ProductID = 9
              
            };
            _productCopyDataLogic.CreateProductCopy(productCopyDto);
            _createdProductCopySerialNumbers.Add("TestSerialNumber3");

            // Act
            var productCopies = _productCopyDataLogic.GetAllProductCopiesByProductID(9);

            // Assert
            Assert.NotNull(productCopies);
            Assert.True(productCopies.Count > 0, "Expected at least one product copy for product ID 1.");
        }

        [Fact]
        public void Test_GetAllAvailableProductCopiesByProductID()
        {
            // Arrange
            var productCopyDto = new ProductCopyDto
            {
                SerialNumber = "TestSerialNumber4",
                ProductID = 9
              
            };
            _productCopyDataLogic.CreateProductCopy(productCopyDto);
            _createdProductCopySerialNumbers.Add("TestSerialNumber4");

            var startDate = DateTime.Now.Date;
            var endDate = startDate.AddDays(7);
            var startTime = new TimeSpan(9, 0, 0);
            var endTime = new TimeSpan(17, 0, 0);

            // Act
            var productCopies = _productCopyDataLogic.GetAllAvailableProductCopyByProductID(9, startDate, endDate, startTime, endTime);

            // Assert
            Assert.NotNull(productCopies);
            Assert.True(productCopies.Count > 0, "Expected at least one available product copy for product ID 1.");
        }

        public void Dispose()
        {
            
            foreach (var serialNumber in _createdProductCopySerialNumbers)
            {
                _productCopyDataLogic.DeleteProductCopy(serialNumber);
            }
        }
    }
}
