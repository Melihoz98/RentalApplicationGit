using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentalService.Business;
using RentalService.DTO;
using System.Collections.Generic;

namespace RentalService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductData _productData;

        public ProductController(IProductData productData)
        {
            _productData = productData;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<ProductDto?>? foundProducts = _productData.GetAllProducts();

            if (foundProducts != null && foundProducts.Count > 0)
            {
                return Ok(foundProducts);
            }
            else
            {
                return NoContent(); // No products found
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            ProductDto? product = _productData.GetByID(id);

            if (product != null)
            {
                return Ok(product);
            }
            else
            {
                return NotFound(); // Product not found
            }
        }
    }
}
