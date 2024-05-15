using Moq;
using RentalService.Business;
using RentalService.DataAccess;
using RentalService.DTO;
using RentalService.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace RentalService.Tests
{
    public class BusinessCustomerDataLogicTests
    {
        [Fact]
        public void GetAllBusinessCustomers_ReturnsListOfBusinessCustomers()
        {
            // Arrange
            var mockAccess = new Mock<IBusinessCustomerAccess>();
            mockAccess.Setup(access => access.GetAllBusinessCustomers())
                      .Returns(new List<BusinessCustomer> { new BusinessCustomer() });

            var logic = new BusinessCustomerDataLogic(mockAccess.Object);

            // Act
            var result = logic.GetAllBusinessCustomers();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<BusinessCustomerDto>>(result);
            Assert.Single(result); // Assuming only one business customer is returned in the mock
        }

        [Fact]
        public void GetBusinessCustomerByCustomerID_ReturnsBusinessCustomerDto()
        {
            // Arrange
            var mockAccess = new Mock<IBusinessCustomerAccess>();
            mockAccess.Setup(access => access.GetBusinessCustomerByCustomerID(It.IsAny<string>()))
                      .Returns(new BusinessCustomer());

            var logic = new BusinessCustomerDataLogic(mockAccess.Object);

            // Act
            var result = logic.GetBusinessCustomerByCustomerID("123");

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BusinessCustomerDto>(result);
        }

        [Fact]
        public void CreateBusinessCustomer_CallsAccessMethod()
        {
            // Arrange
            var mockAccess = new Mock<IBusinessCustomerAccess>();
            var logic = new BusinessCustomerDataLogic(mockAccess.Object);
            var customerToAdd = new BusinessCustomerDto();

            // Act
            logic.CreateBusinessCustomer(customerToAdd);

            // Assert
            mockAccess.Verify(access => access.CreateBusinessCustomer(It.IsAny<BusinessCustomer>()), Times.Once);
        }
    }
}
