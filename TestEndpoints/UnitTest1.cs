using System;
using Xunit;
using RentalService.Business;
using RentalService.DataAccess;
using RentalService.DTO;
using RentalService.Models;
using Moq;
using System.Collections.Generic;

namespace TestEndpoints
{
    public class BusinessCustomerDataLogicTests
    {
        [Fact]
        public void GetAllBusinessCustomers_ReturnsListOfBusinessCustomers()
        {
            // Arrange
            var mockAccess = new Mock<IBusinessCustomerAccess>();
            mockAccess.Setup(access => access.GetAllBusinessCustomers())
                      .Returns(new List<BusinessCustomer>());

            var logic = new BusinessCustomerDataLogic(mockAccess.Object);

            // Act
            var result = logic.GetAllBusinessCustomers();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<BusinessCustomerDto>>(result);
        }

        [Fact]
        public void GetBusinessCustomerByCustomerID_WithValidID_ReturnsBusinessCustomerDto()
        {
            // Arrange
            var mockAccess = new Mock<IBusinessCustomerAccess>();
            mockAccess.Setup(access => access.GetBusinessCustomerByCustomerID(It.IsAny<string>()))
                      .Returns(new BusinessCustomer());

            var logic = new BusinessCustomerDataLogic(mockAccess.Object);

            // Act
            var result = logic.GetBusinessCustomerByCustomerID("validID");

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BusinessCustomerDto>(result);
        }

        [Fact]
        public void GetBusinessCustomerByCustomerID_WithInvalidID_ReturnsNull()
        {
            // Arrange
            var mockAccess = new Mock<IBusinessCustomerAccess>();
            mockAccess.Setup(access => access.GetBusinessCustomerByCustomerID(It.IsAny<string>()))
                      .Returns<BusinessCustomer>(null);

            var logic = new BusinessCustomerDataLogic(mockAccess.Object);

            // Act
            var result = logic.GetBusinessCustomerByCustomerID("invalidID");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void CreateBusinessCustomer_WithValidDto_CallsCreateBusinessCustomerOnAccess()
        {
            // Arrange
            var mockAccess = new Mock<IBusinessCustomerAccess>();
            var logic = new BusinessCustomerDataLogic(mockAccess.Object);
            var customerDto = new BusinessCustomerDto();

            // Act
            logic.CreateBusinessCustomer(customerDto);

            // Assert
            mockAccess.Verify(access => access.CreateBusinessCustomer(It.IsAny<BusinessCustomer>()), Times.Once);
        }
    }
}
