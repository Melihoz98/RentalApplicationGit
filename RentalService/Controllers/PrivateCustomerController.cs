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
    public class PrivateCustomerController : ControllerBase
    {
        private readonly IPrivateCustomerData _privateCustomerLogicCtrl;

        public PrivateCustomerController(IPrivateCustomerData privateCustomerLogicCtrl)
        {
            _privateCustomerLogicCtrl = privateCustomerLogicCtrl;
        }

        [HttpGet]
        public ActionResult<List<PrivateCustomerDto>> Get()
        {
            ActionResult<List<PrivateCustomerDto>> foundReturn;
            List<PrivateCustomerDto?>? foundCustomers = _privateCustomerLogicCtrl.GetAllPrivateCustomers();
            if (foundCustomers != null)
            {
                if (foundCustomers.Count > 0)
                {
                    foundReturn = Ok(foundCustomers);
                }
                else
                {
                    foundReturn = new StatusCodeResult(204); 
                }
            }
            else
            {
                foundReturn = new StatusCodeResult(500); 
            }
            return foundReturn;
        }

        [HttpGet, Route("{id}")]
        public ActionResult<PrivateCustomerDto> Get(string id)
        {
            PrivateCustomerDto foundCustomer = _privateCustomerLogicCtrl.GetPrivateCustomerById(id);

            if (foundCustomer != null)
            {
                return Ok(foundCustomer); 
            }
            else
            {
                return NotFound(); 
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] PrivateCustomerDto privateCustomerDto)
        {
            try
            {
                _privateCustomerLogicCtrl.createPrivateCustomer(privateCustomerDto);
                
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
