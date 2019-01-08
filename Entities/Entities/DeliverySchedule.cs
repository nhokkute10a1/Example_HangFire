using System;
using System.Collections.Generic;

namespace RouterDelivery.Entities.Entities
{
    public partial class DeliverySchedule
    {
        public int Id { get; set; }
        public int? OptimizationRequestId { get; set; }
        public int? CustomerId { get; set; }
        public int? PackageId { get; set; }
        public int? TransportTypeId { get; set; }
        public string DriverName { get; set; }
        public DateTime? EstimatedTime { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Delivery CustomerNavigation { get; set; }
        public virtual OptimizationRequest OptimizationRequest { get; set; }
        public virtual TransportType TransportType { get; set; }
    }
}
