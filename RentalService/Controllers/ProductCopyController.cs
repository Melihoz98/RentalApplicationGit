using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentalService.DTO;
using RentalService.Business;
using System.Collections.Generic;


namespace RentalService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCopyController : ControllerBase
    {
        private readonly IProductCopyData _businessLogicCtrl;

        public ProductCopyController(IProductCopyData inBusinessLogicCtrl)
        {
            _businessLogicCtrl = inBusinessLogicCtrl;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<ProductCopyDto> foundProductCopies = _businessLogicCtrl.GetProductCopiesAll();

            if (foundProductCopies != null && foundProductCopies.Count > 0)
            {
                return Ok(foundProductCopies);
            }
            else
            {
                return NoContent(); // No product copies found
            }
        }

        [HttpGet("{serialNumber}")]
        public IActionResult Get(string serialNumber)
        {
            ProductCopyDto productCopy = _businessLogicCtrl.GetBySerialNumber(serialNumber);

            if (productCopy != null)
            {
                return Ok(productCopy);
            }
            else
            {
                return NotFound(); // Product copy with the specified serial number not found
            }
        }
    }
}
