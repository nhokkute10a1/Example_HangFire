using System;
using System.Collections.Generic;

namespace RouterDelivery.Entities.Entities
{
    public partial class Driver
    {
        public int Id { get; set; }
        public string DriverName { get; set; }
        public int? TransportTypeId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string StartLocation { get; set; }

        public virtual TransportType TransportType { get; set; }
    }
}
