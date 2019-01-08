using RouterDelivery.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibCommon
{
    public class UnitOfWork : IUnitOfWork
    {
        private IRepository<Customer> _customers;
        private IRepository<Delivery> _deliveries;
        private IRepository<Driver> _drivers;
        private IRepository<OptimizationRequest> _optimizationRequest;
        private IRepository<DeliverySchedule> _deliverySchedule;
        private IRepository<RequestStatus> _requestStatus;
        IRepository<TransportType> _transportType;

        private RouteDeliveryContext _dbContext;

        public UnitOfWork(RouteDeliveryContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IRepository<Customer> Customers
        {
            get
            {
                if (_customers == null)
                    _customers = new Repository<Customer>(_dbContext);

                return _customers;
            }
        }

        public IRepository<Delivery> Deliveries
        {
            get
            {
                if (_deliveries == null)
                    _deliveries = new Repository<Delivery>(_dbContext);
                return _deliveries;
            }
        }

        public IRepository<DeliverySchedule> DeliverySchedule
        {
            get
            {
                if (_deliverySchedule == null)
                    _deliverySchedule = new Repository<DeliverySchedule>(_dbContext);
                return _deliverySchedule;
            }
        }

        public IRepository<Driver> Drivers
        {
            get
            {
                if (_drivers == null)
                    _drivers = new Repository<Driver>(_dbContext);
                return _drivers;
            }
        }

        public IRepository<OptimizationRequest> OptimizationRequests
        {
            get
            {
                if (_optimizationRequest == null)
                    _optimizationRequest = new Repository<OptimizationRequest>(_dbContext);
                return _optimizationRequest;
            }
        }


        public IRepository<RequestStatus> RequestStatus
        {
            get
            {
                if (_requestStatus == null)
                    _requestStatus = new Repository<RequestStatus>(_dbContext);
                return _requestStatus;
            }
        }

        public IRepository<TransportType> TransportType
        {
            get
            {
                if (_transportType == null)
                    _transportType = new Repository<TransportType>(_dbContext);
                return _transportType;
            }
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
