using System;
using System.Collections.Generic;

namespace RouterDelivery.Entities.Entities
{
    public partial class TransportType
    {
        public TransportType()
        {
            Delivery = new HashSet<Delivery>();
            DeliverySchedule = new HashSet<DeliverySchedule>();
            Driver = new HashSet<Driver>();
        }

        public int Id { get; set; }
        public string TypeName { get; set; }

        public virtual ICollection<Delivery> Delivery { get; set; }
        public virtual ICollection<DeliverySchedule> DeliverySchedule { get; set; }
        public virtual ICollection<Driver> Driver { get; set; }
    }
}
