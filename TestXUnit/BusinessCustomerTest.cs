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
    public class BusinessCustomerDataLogicTests : IDisposable
    {
        private readonly IBusinessCustomerAccess _businessCustomerAccess;
        private readonly BusinessCustomerDataLogic _businessCustomerDataLogic;
        private readonly List<string> _createdCustomerIDs = new List<string>();

        public BusinessCustomerDataLogicTests()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            _businessCustomerAccess = new BusinessCustomerAccess(configuration);
            _businessCustomerDataLogic = new BusinessCustomerDataLogic(_businessCustomerAccess);
        }

        [Fact]
        public void Test_CreateBusinessCustomer()
        {
            // Arrange
            var customerDto = new BusinessCustomerDto
            {
                CustomerID = "dfe92186-6586-4c5a-b7f2-2f465bec239c",
                CompanyName = "Test Company",
                CVR = "12345678",
                PhoneNumber = "+1234567890"
            };

            // Act
            _businessCustomerDataLogic.CreateBusinessCustomer(customerDto);

            // Assert
            var retrievedCustomer = _businessCustomerDataLogic.GetBusinessCustomerByCustomerID(customerDto.CustomerID);
            Assert.NotNull(retrievedCustomer);
            Assert.Equal(customerDto.CustomerID, retrievedCustomer.CustomerID);

            // Track the created customer for cleanup
            _createdCustomerIDs.Add(customerDto.CustomerID);
        }

        [Fact]
        public void Test_GetBusinessCustomerByCustomerID()
        {
            // Arrange
            var customerDto = new BusinessCustomerDto
            {
                CustomerID = "dfe92186-6586-4c5a-b7f2-2f465bec239c",
                CompanyName = "Test Company",
                CVR = "12345678",
                PhoneNumber = "+1234567890"
            };

            _businessCustomerDataLogic.CreateBusinessCustomer(customerDto);
            _createdCustomerIDs.Add(customerDto.CustomerID);

            // Act
            var retrievedCustomer = _businessCustomerDataLogic.GetBusinessCustomerByCustomerID(customerDto.CustomerID);

            // Assert
            Assert.NotNull(retrievedCustomer);
            Assert.Equal(customerDto.CompanyName, retrievedCustomer.CompanyName);
        }
        [Fact]
        public void Test_GetAllBusinessCustomers()
        {
            // Act
            var customers = _businessCustomerDataLogic.GetAllBusinessCustomers();

            // Assert
            Assert.NotNull(customers);
            Assert.True(customers.Count > 0, "Expected at least one business customer in the database.");

            // Verify that the created customer exists in the list
            bool customerExists = false;
            foreach (var customerDto in customers)
            {
                // Check if this customer matches the expected properties
                if (customerDto.CompanyName == "Test Company" &&
                    customerDto.CVR == "12345678" &&
                    customerDto.PhoneNumber == "+1234567890")
                {
                    customerExists = true;
                    break;
                }
            }
            Assert.True(customerExists, $"Expected customer with Test Company to exist in the database.");
        }


        public void Dispose()
        {
            // Clean up the database by deleting created customers
            foreach (var customerId in _createdCustomerIDs)
            {
                _businessCustomerDataLogic.RemoveBusinessCustomer(customerId);
            }
            _createdCustomerIDs.Clear();
        }
    }
}
