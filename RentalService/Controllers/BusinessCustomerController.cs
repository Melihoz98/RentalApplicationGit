using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentalService.Business;
using RentalService.DTO;
using System;
using System.Collections.Generic;

namespace RentalService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessCustomerController : ControllerBase
    {
        private readonly IBusinessCustomerdata _businessCustomerLogicCtrl;

        public BusinessCustomerController(IBusinessCustomerdata businessCustomerLogicCtrl)
        {
            _businessCustomerLogicCtrl = businessCustomerLogicCtrl;
        }

        [HttpGet]
        public ActionResult<List<BusinessCustomerDto>> Get()
        {
            ActionResult<List<BusinessCustomerDto>> foundReturn;
            List<BusinessCustomerDto?>? foundCustomers = _businessCustomerLogicCtrl.GetAll();
            if (foundCustomers != null)
            {
                if (foundCustomers.Count > 0)
                {
                    foundReturn = Ok(foundCustomers);
                }
                else
                {
                    foundReturn = new StatusCodeResult(204); // No Content
                }
            }
            else
            {
                foundReturn = new StatusCodeResult(500); // Internal server error
            }
            return foundReturn;
        }

        [HttpGet, Route("{id}")]
        public ActionResult<BusinessCustomerDto> Get(int id)
        {
            BusinessCustomerDto foundCustomer = _businessCustomerLogicCtrl.GetById(id);

            if (foundCustomer != null)
            {
                return Ok(foundCustomer); // Statuscode 200
            }
            else
            {
                return NotFound(); // Statuscode 404
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] BusinessCustomerDto businessCustomerDto)
        {
            try
            {
                _businessCustomerLogicCtrl.Add(businessCustomerDto);
                // Return 201 Created status without including BusinessCustomerDto in response
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                // Handle exception
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
