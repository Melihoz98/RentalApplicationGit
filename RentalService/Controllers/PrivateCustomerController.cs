﻿using Microsoft.AspNetCore.Http;
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
        private readonly IPrivateCustomerdata _privateCustomerLogicCtrl;

        public PrivateCustomerController(IPrivateCustomerdata privateCustomerLogicCtrl)
        {
            _privateCustomerLogicCtrl = privateCustomerLogicCtrl;
        }

        [HttpGet]
        public ActionResult<List<PrivateCustomerDto>> Get()
        {
            ActionResult<List<PrivateCustomerDto>> foundReturn;
            List<PrivateCustomerDto?>? foundCustomers = _privateCustomerLogicCtrl.GetAll();
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
        public ActionResult<PrivateCustomerDto> Get(int id)
        {
            PrivateCustomerDto foundCustomer = _privateCustomerLogicCtrl.GetById(id);

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
        public IActionResult Post([FromBody] PrivateCustomerDto privateCustomerDto)
        {
            try
            {
                _privateCustomerLogicCtrl.Add(privateCustomerDto);
                // Return 201 Created status without including PrivateCustomerDto in response
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
