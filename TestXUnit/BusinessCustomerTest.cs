﻿using System;
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
                CustomerID = "d2a1c808-1e92-44cd-9693-b1fe2c04e5a1",
                CompanyName = "Test Company1",
                CVR = "12345678",
                PhoneNumber = "+1234567890"
            };

            // Act
            _businessCustomerDataLogic.CreateBusinessCustomer(customerDto);

            // Assert
            var retrievedCustomer = _businessCustomerDataLogic.GetBusinessCustomerByCustomerID(customerDto.CustomerID);
            Assert.NotNull(retrievedCustomer);
            Assert.Equal(customerDto.CustomerID, retrievedCustomer.CustomerID);

            
            _createdCustomerIDs.Add(customerDto.CustomerID);
        }

        [Fact]
        public void Test_GetBusinessCustomerByCustomerID()
        {
            // Arrange
            var customerDto = new BusinessCustomerDto
            {
                CustomerID = "d2a1c808-1e92-44cd-9693-b1fe2c04e5a1",
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

            
            bool customerExists = false;
            foreach (var customerDto in customers)
            {
                
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
            
            foreach (var customerId in _createdCustomerIDs)
            {
                _businessCustomerDataLogic.RemoveBusinessCustomer(customerId);
            }
            _createdCustomerIDs.Clear();
        }
    }
}
