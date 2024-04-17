using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentalService.DTO;
using RentalService.Business;
using System.Collections.Generic;

namespace RentalService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryData _businessLogicCtrl;

        public CategoryController(ICategoryData inBusinessLogicCtrl)
        {
            _businessLogicCtrl = inBusinessLogicCtrl;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<CategoryDto> foundCategories = _businessLogicCtrl.Get();

            if (foundCategories != null && foundCategories.Count > 0)
            {
                return Ok(foundCategories);
            }
            else
            {
                return NoContent(); // No categories found
            }
        }


        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            // Implement logic to retrieve category by id
            return NotFound(); // Placeholder response
        }
    }
}
