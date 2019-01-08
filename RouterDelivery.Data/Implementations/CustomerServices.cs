using LibCommon;
using LibCommon.Common;
using RouteDelivery.Models;
using RouterDelivery.Entities.Entities;
using RouterDelivery.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RouterDelivery.Services.Implementations
{
    public class CustomerServices : ICustomerServices
    {
        private readonly IUnitOfWork _uow;
        //private readonly IRepository<Customer> _customer;
        public CustomerServices(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public List<CustomerViewModel> GetAll()
        {
            var query = _uow.Customers.FindAll()
                .Select(x => new CustomerViewModel
                {
                    Id = x.Id,
                    CustomerName = x.CustomerName,
                    CustomerLocation = x.CustomerLocation
                }).ToList();
            return query;
        }

        public CustomerViewModel GetEdit(int id)
        {
            var query = _uow.Customers.FindFirst(x => x.Id == id);
            return new CustomerViewModel
            {
                Id = query.Id,
                CustomerName = query.CustomerName,
                CustomerLocation = query.CustomerLocation
            };
        }

        public void Insert(InsertCustomerViewModel dto, out bool Status)
        {
            try
            {
                var model = new Customer
                {
                    CustomerName = dto.CustomerName,
                    CustomerLocation = dto.CustomerLocation
                };
                _uow.Customers.Add(model);
                _uow.SaveChanges();
                Status = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void InsertMultiple(List<Customer> list, out bool Status)
        {
            try
            {
                _uow.Customers.AddRange(list);
                _uow.SaveChanges();
                Status = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(CustomerViewModel dto, out bool Status)
        {
            try
            {
                Status = false;
                var data = GetByID(dto.Id);
                if (data != null)
                {
                    data.CustomerName = dto.CustomerName;
                    data.CustomerLocation = dto.CustomerLocation;

                    _uow.Customers.Update(data);
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
                _uow.Customers.Remove(data);
                _uow.SaveChanges();
                Status = true;
            }
        }

        private Customer GetByID(int id)
        {
            var query = _uow.Customers.FindByID(id);
            return query;

        }
    }
}
