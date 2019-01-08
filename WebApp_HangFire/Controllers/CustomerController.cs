using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RouterDelivery.Services.IServices;
using LibCommon.Common;
using RouteDelivery.Models;
using RouterDelivery.Entities.Entities;
using Hangfire;

namespace WebApp_HangFire.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerServices _customerServices;

        public CustomerController(ICustomerServices customerServices)
        {
            _customerServices = customerServices;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var Result = new Res();
            try
            {
                BackgroundJob.Enqueue(() => _customerServices.GetAll());
                var data = await Task.Run(() => _customerServices.GetAll());
                if (data != null)
                {
                    Result.Data = data;
                    Result.Status = true;
                    Result.Message = MessageResponse.CALL_API_SUCCESS;
                    Result.StatusCode = HttpStatusCode.OK;
                }
                else
                {
                    Result.Data = null;
                    Result.Status = false;
                    Result.Message = MessageResponse.DATA_NOT_FOUND;
                    Result.StatusCode = HttpStatusCode.NotFound;
                }

                return Ok(Result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GeById(int id)
        {
            var Result = new Res();
            try
            {
                BackgroundJob.Enqueue(() => _customerServices.GetEdit(id));
                var data = await Task.Run(() => _customerServices.GetEdit(id));
                if (data != null)
                {
                    Result.Data = data;
                    Result.Status = true;
                    Result.Message = MessageResponse.CALL_API_SUCCESS;
                    Result.StatusCode = HttpStatusCode.OK;
                }
                else
                {
                    Result.Data = null;
                    Result.Status = false;
                    Result.Message = MessageResponse.DATA_NOT_FOUND;
                    Result.StatusCode = HttpStatusCode.NotFound;
                }

                return Ok(Result);

            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Route("InsertAsync")]
        [HttpPost]
        public async Task<IActionResult> InsertAsync(InsertCustomerViewModel dto)
        {
            var Status = false;
            var Result = new Res();
            BackgroundJob.Enqueue(() => _customerServices.Insert(dto, out Status));
            await Task.Run(() => _customerServices.Insert(dto, out Status));
            if (Status)
            {
                Result.Status = true;
                Result.Message = MesssageContant.SAVE_SUCCESS;
                Result.StatusCode = HttpStatusCode.OK;
            }
            else
            {
                Result.Data = null;
                Result.Status = Status;
                Result.Message = MesssageContant.SAVE_FAIL;
                Result.StatusCode = HttpStatusCode.InternalServerError;
            }
            return Ok(Result);
        }

        [Route("InsertMultipleAsync")]
        [HttpPost]
        public async Task<IActionResult> InsertMultipleAsync(List<Customer> dto)
        {
            var Status = false;
            var Result = new Res();

            BackgroundJob.Enqueue(() => _customerServices.InsertMultiple(dto, out Status));

            await Task.Run(() => _customerServices.InsertMultiple(dto, out Status));
            if (Status)
            {
                Result.Status = true;
                Result.Message = MesssageContant.SAVE_SUCCESS;
                Result.StatusCode = HttpStatusCode.OK;
            }
            else
            {
                Result.Data = null;
                Result.Status = Status;
                Result.Message = MesssageContant.SAVE_FAIL;
                Result.StatusCode = HttpStatusCode.InternalServerError;
            }
            return Ok(Result);
        }

        [Route("UpdateAsync")]
        [HttpPost]
        public async Task<IActionResult> UpdateAsync(CustomerViewModel dto)
        {
            var Status = false;
            var Message = string.Empty;
            var Result = new Res();

            BackgroundJob.Enqueue(() => _customerServices.Update(dto, out Status));

            await Task.Run(() => _customerServices.Update(dto, out Status));
            if (Status)
            {
                Result.Status = true;
                Result.Message = MesssageContant.UPDATE_SUCCESS;
                Result.StatusCode = HttpStatusCode.OK;
            }
            else
            {
                Result.Data = null;
                Result.Status = Status;
                Result.Message = MesssageContant.UPDATE_FAIL;
                Result.StatusCode = HttpStatusCode.InternalServerError;
            }
            return Ok(Result);
        }

        [Route("DeleteAsync")]
        [HttpPost]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var Status = false;
            var Message = string.Empty;
            var Result = new Res();

            BackgroundJob.Enqueue(() => _customerServices.Delete(id, out Status));


            await Task.Run(() => _customerServices.Delete(id, out Status));
            if (Status)
            {
                Result.Status = true;
                Result.Message = MesssageContant.DELETE_SUCCESS;
                Result.StatusCode = HttpStatusCode.OK;
            }
            else
            {
                Result.Data = null;
                Result.Status = Status;
                Result.Message = MesssageContant.DELETE_FAIL;
                Result.StatusCode = HttpStatusCode.InternalServerError;
            }
            return Ok(Result);
        }
    }
}