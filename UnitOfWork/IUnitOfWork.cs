using RouterDelivery.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibCommon
{
    public interface IUnitOfWork
    {
        IRepository<Customer> Customers { get; }
        IRepository<Delivery> Deliveries { get; }
        IRepository<Driver> Drivers { get; }
        IRepository<OptimizationRequest> OptimizationRequests { get; }
        IRepository<DeliverySchedule> DeliverySchedule { get; }
        IRepository<RequestStatus> RequestStatus { get; }
        IRepository<TransportType> TransportType { get; }

        void SaveChanges();
        void Dispose();
    }
}
