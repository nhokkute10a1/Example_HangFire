using LibCommon;
using LibCommon.Common;
using RouterDelivery.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace RouteDelivery.OptimizationEngine
{
    public class OptimizationEngine : IOptimizationEngine
    {
        private readonly IUnitOfWork _uow;
        private Random _rnd = new Random();

        public OptimizationEngine(IUnitOfWork uow) => _uow = uow;

        public void OptimizeDeliveries(int requestID)
        {
            //get OptimizationRequest by id
            var request = GetRequest(requestID);
            //get all customer
            var customers = GetCustomers(request);
            //get all Deliveries
            var deliveries = GetDeliveries(request);
            //get all Drivers
            var drivers = GetDrivers(request);
            //get status
            var status = GetStatus(StatusContanst.COMPLETE);

            var optimizedScheduled = new List<DeliverySchedule>();

            var deliveryNo = 0;

            foreach (var c in customers)
            {
                var customerDistanceFromWareHouse = GetCustomerDistanceFromWareHouse(c);
                deliveryNo = 0;

                var getDeliveries = deliveries.Where(d => d.CustomerId == c.Id);

                foreach (var d in getDeliveries)
                {
                    var getTransportType = GetTransportType((int)d.TransportTypeId);
                    var idealDriver = GetIdealDriver(drivers, getTransportType.TypeName, customerDistanceFromWareHouse);
                    deliveryNo++;

                    if (idealDriver != null)
                    {
                        optimizedScheduled.Add(new DeliverySchedule()
                        {
                            CustomerId = c.Id,
                            DriverName = idealDriver.DriverName,
                            OptimizationRequestId = request.Id,
                            PackageId = d.Id,
                            TransportTypeId = d.TransportType.Id,
                            EstimatedTime = ((DateTime)request.ScheduleDate).AddHours(deliveryNo)
                            //Id = deliveryNo
                        });
                    }
                }
            }

            request.StatusId = status.Id;
            _uow.DeliverySchedule.AddRange(optimizedScheduled);
            _uow.SaveChanges();
        }


        #region Get Data Related To Request
        private IEnumerable<Driver> GetDrivers(object request)
        {
            return _uow.Drivers.FindAll();
        }

        private IEnumerable<Delivery> GetDeliveries(object request)
        {
            return _uow.Deliveries.FindAll();
        }

        private IEnumerable<Customer> GetCustomers(object request)
        {
            return _uow.Customers.FindAll();
        }

        private OptimizationRequest GetRequest(int requestID)
        {
            return _uow.OptimizationRequests.FindByID(requestID);
        }
        #endregion

        #region Optimize Delivery Helper Methods
        private Driver GetIdealDriver(IEnumerable<Driver> drivers, string transportTypeName, int customerDistanceFromWareHouse)
        {  
            Thread.Sleep(500);
            var query = drivers.FirstOrDefault(d => d.TransportType.TypeName == transportTypeName && _rnd.Next(1, 4) == _rnd.Next(1, 4));
            return query;
        }

        private int GetCustomerDistanceFromWareHouse(Customer c)
        {
            Thread.Sleep(500);
            return _rnd.Next(1, 100);
        }
        #endregion

        private RequestStatus GetStatus(string typeName)
        {
            var query = _uow.RequestStatus.FindFirst(x => x.StatusName == typeName);
            return query;
        }

        private TransportType GetTransportType(int id)
        {
            var query = _uow.TransportType.FindByID(id);
            return query;
        }
    
    }
}
