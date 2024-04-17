using RentalService.DataAccess;

using Microsoft.Data.SqlClient;
using RentalService.Models;
using System;
using Xunit.Abstractions;
using Microsoft.Extensions.Configuration;

namespace PersonDataTest
{
    public class TestCategoryDataAccess
    {
        private readonly string _connectionString = "Server=hildur.ucn.dk;Initial Catalog=DMA-CSD-V23_10478728;User ID=DMA-CSD-V23_10478728;Password=Password1!;Encrypt=false";
        private readonly ITestOutputHelper _extraOutput;
        private readonly ICategoryAccess _categoryAccess;

        public TestCategoryDataAccess(ITestOutputHelper tOutput)
        {
            _extraOutput = tOutput;
            _categoryAccess = new CategoryAccess(_connectionString);
        }

        [Fact]
        public void TestGetPersonAll()
        {
            // Arrange

            // Act
            List<Category> readPersons = _categoryAccess.GetCategoryAll();
            bool personsWereRead = (readPersons.Count > 0);
            // Print additional output
            _extraOutput.WriteLine("Number of persons: " + readPersons.Count);

            // Assert
            Assert.True(personsWereRead);
        }

    }
}