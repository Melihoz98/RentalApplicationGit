using Microsoft.AspNetCore.Mvc;
using RentalService.Business;
using RentalService.DTO;
using System;

namespace RentalService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessCustomerController : ControllerBase
    {
        private readonly IBusinessCustomerData _businessCustomerData;

        public BusinessCustomerController(IBusinessCustomerData businessCustomerData)
        {
            _businessCustomerData = businessCustomerData;
        }

        [HttpGet]
        public IActionResult GetAllBusinessCustomers()
        {
            try
            {
                List<BusinessCustomerDto> customers = _businessCustomerData.GetAllBusinessCustomers();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error getting all business customers: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{customerID}")]
        public IActionResult GetBusinessCustomerByCustomerID(string customerID)
        {
            try
            {
                BusinessCustomerDto customerDto = _businessCustomerData.GetBusinessCustomerByCustomerID(customerID);
                if (customerDto != null)
                {
                    return Ok(customerDto);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error getting business customer by ID: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult CreateBusinessCustomer([FromBody] BusinessCustomerDto customerDto)
        {
            try
            {
                _businessCustomerData.CreateBusinessCustomer(customerDto);
                return CreatedAtRoute("GetBusinessCustomerByCustomerID", new { customerID = customerDto.CustomerID }, customerDto);
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error creating business customer: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
