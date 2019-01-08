using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RouteDelivery.Models
{
    public class InsertCustomerViewModel
    {
        public string CustomerName { get; set; }
        public string CustomerLocation { get; set; }

    }

    public class CustomerViewModel : InsertCustomerViewModel
    {
        public int Id { get; set; }
    }
}