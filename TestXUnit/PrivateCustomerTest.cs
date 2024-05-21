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
    public class PrivateCustomerDataLogicTests : IDisposable
    {
        private readonly IPrivateCustomerAccess _privateCustomerAccess;
        private readonly PrivateCustomerDataLogic _privateCustomerDataLogic;
        private readonly List<string> _createdCustomerIDs = new List<string>();

        public PrivateCustomerDataLogicTests()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            _privateCustomerAccess = new PrivateCustomerAccess(configuration);
            _privateCustomerDataLogic = new PrivateCustomerDataLogic(_privateCustomerAccess);
        }

        [Fact]
        public void Test_CreatePrivateCustomer()
        {
            // Arrange
            var customerDto = new PrivateCustomerDto
            {
                CustomerID = "ce02d268-d4fd-4659-9bcc-b382b95ab5c4",
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "+1234567890"
            };

            // Act
            _privateCustomerDataLogic.createPrivateCustomer(customerDto);

            // Assert
            var retrievedCustomer = _privateCustomerDataLogic.GetPrivateCustomerById(customerDto.CustomerID);
            Assert.NotNull(retrievedCustomer);
            Assert.Equal(customerDto.CustomerID, retrievedCustomer.CustomerID);
            Assert.Equal(customerDto.FirstName, retrievedCustomer.FirstName);
            Assert.Equal(customerDto.LastName, retrievedCustomer.LastName);
            Assert.Equal(customerDto.PhoneNumber, retrievedCustomer.PhoneNumber);

            
            _createdCustomerIDs.Add(customerDto.CustomerID);
        }

        [Fact]
        public void Test_GetPrivateCustomerById()
        {
            // Arrange
            var customerDto = new PrivateCustomerDto
            {
                CustomerID = "ce02d268-d4fd-4659-9bcc-b382b95ab5c4",
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "+1234567890"
            };

            _privateCustomerDataLogic.createPrivateCustomer(customerDto);
            _createdCustomerIDs.Add(customerDto.CustomerID);

            // Act
            var retrievedCustomer = _privateCustomerDataLogic.GetPrivateCustomerById(customerDto.CustomerID);

            // Assert
            Assert.NotNull(retrievedCustomer);
            Assert.Equal(customerDto.CustomerID, retrievedCustomer.CustomerID);
            Assert.Equal(customerDto.FirstName, retrievedCustomer.FirstName);
            Assert.Equal(customerDto.LastName, retrievedCustomer.LastName);
            Assert.Equal(customerDto.PhoneNumber, retrievedCustomer.PhoneNumber);
        }

        [Fact]
        public void Test_GetAllPrivateCustomers()
        {
            // Arrange
            var customerDto = new PrivateCustomerDto
            {
                CustomerID = "ce02d268-d4fd-4659-9bcc-b382b95ab5c4",
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "+1234567890"
            };

            _privateCustomerDataLogic.createPrivateCustomer(customerDto);
            _createdCustomerIDs.Add(customerDto.CustomerID);

            // Act
            var customers = _privateCustomerDataLogic.GetAllPrivateCustomers();

            // Assert
            Assert.NotNull(customers);
            Assert.True(customers.Count > 0, "Expected at least one private customer in the database.");

            bool customerExists = false;

            foreach (var customer in customers)
            {
                if (customer.CustomerID == customerDto.CustomerID)
                {
                    Assert.Equal(customerDto.FirstName, customer.FirstName);
                    Assert.Equal(customerDto.LastName, customer.LastName);
                    Assert.Equal(customerDto.PhoneNumber, customer.PhoneNumber);
                    customerExists = true;
                    break;
                }
            }

            Assert.True(customerExists, $"Expected customer with ID {customerDto.CustomerID} to exist in the database.");
        }

        [Fact]
        public void Test_DeletePrivateCustomer()
        {
            // Arrange
            var customerDto = new PrivateCustomerDto
            {
                CustomerID = "ce02d268-d4fd-4659-9bcc-b382b95ab5c4",
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "+1234567890"
            };

            _privateCustomerDataLogic.createPrivateCustomer(customerDto);
            _createdCustomerIDs.Add(customerDto.CustomerID);

            // Act
            _privateCustomerDataLogic.DeletePrivateCustomer(customerDto.CustomerID);

            var retrievedCustomer = _privateCustomerDataLogic.GetPrivateCustomerById(customerDto.CustomerID);

            // Assert
            Assert.Null(retrievedCustomer);
        }

        public void Dispose()
        {
            
            foreach (var customerId in _createdCustomerIDs)
            {
                _privateCustomerAccess.DeletePrivateCustomer(customerId);
            }
            _createdCustomerIDs.Clear();
        }
    }
}
