using RouteDelivery.Models;
using RouterDelivery.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RouterDelivery.Services.IServices
{
    public interface ICustomerServices
    {
        List<CustomerViewModel> GetAll();
        CustomerViewModel GetEdit(int id);
        void Insert(InsertCustomerViewModel dto, out bool Status);
        void Update(CustomerViewModel dto, out bool Status);
        void InsertMultiple(List<Customer> list, out bool Status);
        void Delete(int id, out bool Status);
    }
}
