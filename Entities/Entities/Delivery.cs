using System;
using System.Collections.Generic;

namespace RouterDelivery.Entities.Entities
{
    public partial class Delivery
    {
        public Delivery()
        {
            DeliverySchedule = new HashSet<DeliverySchedule>();
        }

        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public int? TransportTypeId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual TransportType TransportType { get; set; }
        public virtual ICollection<DeliverySchedule> DeliverySchedule { get; set; }
    }
}
