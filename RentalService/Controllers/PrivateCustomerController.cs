using Microsoft.AspNetCore.Mvc;
using RentalService.Business;
using RentalService.DTO;
using RentalService.ModelConversion;

namespace RentalService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrivateCustomerController : ControllerBase
    {
        private readonly IPrivateCustomerData _privateCustomerData;

        public PrivateCustomerController(IPrivateCustomerData privateCustomerData)
        {
            _privateCustomerData = privateCustomerData;
        }

        [HttpGet]

        public IActionResult GetAllPrivateCustomers()
        {
            try
            {
                List<PrivateCustomerDto> custoemrs = _privateCustomerData.GetAllPrivateCustomers();
                return Ok(custoemrs);
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Error getting all private customers: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        
        
        }
       
        [HttpGet, Route("{CustomerID}")]
        public IActionResult GetPrivateCustomerByCustomerID(string customerID)
        {
            try
            {
                PrivateCustomerDto customerDto = _privateCustomerData.GetPrivateCustomerById(customerID);
                if(customerDto != null)
                {
                    return Ok(customerDto);
                }
                else
                {
                    return NotFound();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error getting private customer by ID: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult CreatePrivateCustomer([FromBody] PrivateCustomerDto customerDto)
        {
            try
            {
                _privateCustomerData.createPrivateCustomer(customerDto);
                return CreatedAtRoute("GetPrivateCustomerByCustomerID", new { customerID = customerDto.CustomerID }, customerDto);
            }
            catch (Exception ex)
            {
                // Log the error
                Console.WriteLine($"Error creating business customer: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
