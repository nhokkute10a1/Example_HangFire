using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Hangfire;
using LibCommon.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RouteDelivery.Models;
using RouteDelivery.OptimizationEngine;
using RouterDelivery.Services.IServices;

namespace WebApp_HangFire.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OptimizationRequestRecurringController : ControllerBase
    {
        private readonly IOptimizationEngineServices _optimizationEngineServices;
        private readonly IOptimizationEngine _optimizationEngine;

        public OptimizationRequestRecurringController(IOptimizationEngineServices optimizationEngineServices, IOptimizationEngine optimizationEngine)
        {
            _optimizationEngineServices = optimizationEngineServices;
            _optimizationEngine = optimizationEngine;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var Result = new Res();
            try
            {
                BackgroundJob.Enqueue(() => _optimizationEngineServices.GetAll());
                var data = await Task.Run(() => _optimizationEngineServices.GetAll());
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
                BackgroundJob.Enqueue(() => _optimizationEngineServices.GetEdit(id));
                var data = await Task.Run(() => _optimizationEngineServices.GetEdit(id));
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
        public async Task<IActionResult> InsertAsync(InsertOptimizationRequestViewModel dto)
        {
            var Status = false;
            var Result = new Res();

            await Task.Run(() => _optimizationEngineServices.Insert(dto, out Status));

            var cronType = GetCronFromRecurringType(dto.);
            BackgroundJob.Enqueue(() => _optimizationEngine.OptimizeDeliveries(_optimizationEngineServices.GetIdLasted().Id));

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
        public async Task<IActionResult> UpdateAsync(OptimizationRequestViewModel dto)
        {
            var Status = false;
            var Message = string.Empty;
            var Result = new Res();

            BackgroundJob.Enqueue(() => _optimizationEngineServices.Update(dto, out Status));

            await Task.Run(() => _optimizationEngineServices.Update(dto, out Status));
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

            BackgroundJob.Enqueue(() => _optimizationEngineServices.Delete(id, out Status));


            await Task.Run(() => _optimizationEngineServices.Delete(id, out Status));
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

        private Func<string> GetCronFromRecurringType(CommonContanst.RecurringScheduleType recurringSchedule)
        {
            switch (recurringSchedule)
            {
                case CommonContanst.RecurringScheduleType.Daily:
                    return Cron.Daily;
                case CommonContanst.RecurringScheduleType.Hourly:
                    return Cron.Hourly;
                case CommonContanst.RecurringScheduleType.Minutely:
                    return Cron.Minutely;
                case CommonContanst.RecurringScheduleType.Monthly:
                    return Cron.Monthly;
                case CommonContanst.RecurringScheduleType.Weekly:
                    return Cron.Weekly;
                case CommonContanst.RecurringScheduleType.Yearly:
                    return Cron.Yearly;
                default:
                    return Cron.Daily;
            }
        }

    }
}