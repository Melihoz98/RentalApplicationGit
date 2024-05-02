using Microsoft.AspNetCore.Mvc;
using RentalService.Business;
using RentalService.Models;
using System;
using System.Collections.Generic;

namespace RentalService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AspNetUserController : ControllerBase
    {
        private readonly IAspNetUserData _aspNetUserData;

        public AspNetUserController(IAspNetUserData aspNetUserData)
        {
            _aspNetUserData = aspNetUserData;
        }

        [HttpGet("{id}")]
        public IActionResult GetAspNetUserNameById(string id)
        {
            try
            {
                string userName = _aspNetUserData.GetAspNetUserById(id);
                if (userName != null)
                {
                    return Ok(new { UserName = userName });
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                // Log exception
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("username/{userName}")]
        public IActionResult GetAspNetIdByUserName(string userName)
        {
            try
            {
                string id = _aspNetUserData.GetAspNetIdByUserName(userName);
                if (id != null)
                {
                    return Ok(new { Id = id });
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                // Log exception
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
