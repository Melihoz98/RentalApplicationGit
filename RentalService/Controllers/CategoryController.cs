using RentalService.Business;
using RentalService.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace RentalService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryData _categoryData;

        public CategoryController(ICategoryData categoryData)
        {
            _categoryData = categoryData;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                CategoryDto? categoryDto = _categoryData.GetById(id);
                if (categoryDto != null)
                {
                    return Ok(categoryDto);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet]
        public IActionResult GetAllCategories()
        {
            try
            {
                List<CategoryDto?>? categories = _categoryData.GetAllCategories();
                if (categories != null)
                {
                    return Ok(categories);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost]
        [Authorize(Roles ="Admin")]
        public IActionResult CreateCategory([FromBody] CategoryDto categoryDto)
        {
            try
            {
                int insertedId = _categoryData.CreateCategory(categoryDto);
                return CreatedAtAction(nameof(GetById), new { id = insertedId }, null);
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteCategory(int id)
        {
            try
            {
                _categoryData.DeleteCategory(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
