using LibCommon;
using RouteDelivery.Models;
using RouterDelivery.Entities.Entities;
using RouterDelivery.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RouterDelivery.Services.Implementations
{
    public class OptimizationEngineServices : IOptimizationEngineServices
    {
        private readonly IUnitOfWork _uow;
        public OptimizationEngineServices(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public List<OptimizationRequestViewModel> GetAll()
        {
            var query = _uow.OptimizationRequests.FindAll()
                .Select(x => new OptimizationRequestViewModel
                {
                    Id = x.Id,
                    RequestDate = x.RequestDate,
                    ScheduleDate = x.ScheduleDate,
                    StatusId = x.StatusId
                }).ToList();
            return query;
        }

        public OptimizationRequestViewModel GetEdit(int id)
        {
            var query = _uow.OptimizationRequests.FindFirst(x => x.Id == id);
            return new OptimizationRequestViewModel
            {
                Id = query.Id,
                RequestDate = query.RequestDate,
                ScheduleDate = query.ScheduleDate,
                StatusId = query.StatusId
            };
        }

        public void Insert(InsertOptimizationRequestViewModel dto, out bool Status)
        {
            try
            {
                var model = new OptimizationRequest
                {
                    RequestDate = dto.RequestDate,
                    ScheduleDate = dto.ScheduleDate,
                    StatusId = dto.StatusId
                };
                _uow.OptimizationRequests.Add(model);
                _uow.SaveChanges();
                Status = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(OptimizationRequestViewModel dto, out bool Status)
        {
            try
            {
                Status = false;
                var data = GetByID(dto.Id);
                if (data != null)
                {
                    data.RequestDate = dto.RequestDate;
                    data.ScheduleDate = dto.ScheduleDate;
                    data.StatusId = dto.StatusId;

                    _uow.OptimizationRequests.Update(data);
                    _uow.SaveChanges();
                    Status = true;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public void Delete(int id, out bool Status)
        {
            Status = false;
            var data = GetByID(id);
            if (data != null)
            {
                _uow.OptimizationRequests.Remove(data);
                _uow.SaveChanges();
                Status = true;
            }
        }

        public OptimizationRequestViewModel GetIdLasted()
        {
            var query = _uow.OptimizationRequests.FindAll().OrderByDescending(x => x.Id).FirstOrDefault();
            return new OptimizationRequestViewModel
            {
                Id = query.Id,
                RequestDate = query.RequestDate,
                ScheduleDate = query.ScheduleDate,
                StatusId = query.StatusId
            };
        }

        private OptimizationRequest GetByID(int id)
        {
            var query = _uow.OptimizationRequests.FindByID(id);
            return query;

        }

    }
}
