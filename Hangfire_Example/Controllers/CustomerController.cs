using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RouterDelivery.Services.IServices;

namespace Hangfire_Example.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        public readonly ICustomerServices _customerServices;

        public CustomerController(ICustomerServices customerServices)
        {
            _customerServices = customerServices;
        }

        [HttpGet("test")]
        public IActionResult GetAll()
        {
            try
            {
                var model = _customerServices.GetAll();
                return Ok(model);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}