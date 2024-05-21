using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using RentalService.Business;
using RentalService.DataAccess;
using RentalService.DTO;
using Xunit;

namespace RentalService.Tests
{
    public class CategoryDataLogicTests : IDisposable
    {
        private readonly ICategoryAccess _categoryAccess;
        private readonly CategoryDataLogic _categoryDataLogic;
        private readonly List<int> _createdCategoryIds = new List<int>();

        public CategoryDataLogicTests()
        {
            
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            
            _categoryAccess = new CategoryAccess(configuration);
            _categoryDataLogic = new CategoryDataLogic(_categoryAccess);
        }

        [Fact]
        public void Test_CreateCategory()
        {
            // Arrange
            var categoryDto = new CategoryDto
            {
                CategoryName = "Test Category",
                ImagePath = "testpath.jpg"
            };

            // Act
            int newCategoryId = _categoryDataLogic.CreateCategory(categoryDto);

            // Assert
            Assert.True(newCategoryId > 0);

            
            _createdCategoryIds.Add(newCategoryId);
        }

        [Fact]
        public void Test_GetCategoryById()
        {
            // Arrange
            var categoryDto = new CategoryDto
            {
                CategoryName = "Test Category",
                ImagePath = "testpath.jpg"
            };
            int newCategoryId = _categoryDataLogic.CreateCategory(categoryDto);
            _createdCategoryIds.Add(newCategoryId);

            // Act
            var retrievedCategory = _categoryDataLogic.GetById(newCategoryId);

            // Assert
            Assert.NotNull(retrievedCategory);
            Assert.Equal("Test Category", retrievedCategory.CategoryName);
        }

        [Fact]
        public void Test_GetAllCategories()
        {
            // Act
            var categories = _categoryDataLogic.GetAllCategories();

            // Assert
            Assert.NotNull(categories);
            Assert.NotEmpty(categories);
        }

        [Fact]
        public void Test_DeleteCategory()
        {
            // Arrange
            var categoryDto = new CategoryDto
            {
                CategoryName = "Test Category to Delete",
                ImagePath = "testdelete.jpg"
            };
            int newCategoryId = _categoryDataLogic.CreateCategory(categoryDto);

            // Act
            _categoryDataLogic.DeleteCategory(newCategoryId);
            var deletedCategory = _categoryDataLogic.GetById(newCategoryId);

            // Assert
            Assert.Null(deletedCategory);
        }

        public void Dispose()
        {
            
            foreach (var categoryId in _createdCategoryIds)
            {
                _categoryDataLogic.DeleteCategory(categoryId);
            }
        }
    }
}
