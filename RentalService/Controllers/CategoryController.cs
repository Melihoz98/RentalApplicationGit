using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentalService.DTO;
using RentalService.Business;
using System.Collections.Generic; // Added for List<T>

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
        public ActionResult<List<CategoryDto>> Get() // Added missing List<>
        {
            ActionResult<List<CategoryDto>> foundReturn;
            List<CategoryDto>? foundCategories = _businessLogicCtrl.Get(); // Corrected method call

            if (foundCategories != null)
            {
                if (foundCategories.Count > 0)
                {
                    foundReturn = Ok(foundCategories);
                }
                else
                {
                    foundReturn = NoContent(); // Changed to NoContent() for 204
                }
            }
            else
            {
                foundReturn = StatusCode(500); // Changed to StatusCode() for 500
            }

            return foundReturn;
        }

        [HttpGet, Route("{id}")]
        public ActionResult<CategoryDto> Get(int id)
        {
            return null;
        }

    }
}
