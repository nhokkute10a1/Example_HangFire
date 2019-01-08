using System;
using System.Collections.Generic;

namespace RouterDelivery.Entities.Entities
{
    public partial class Customer
    {
        public Customer()
        {
            Delivery = new HashSet<Delivery>();
            DeliverySchedule = new HashSet<DeliverySchedule>();
        }

        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerLocation { get; set; }

        public virtual ICollection<Delivery> Delivery { get; set; }
        public virtual ICollection<DeliverySchedule> DeliverySchedule { get; set; }
    }
}
