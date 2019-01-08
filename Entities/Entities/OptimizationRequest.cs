using System;
using System.Collections.Generic;

namespace RouterDelivery.Entities.Entities
{
    public partial class OptimizationRequest
    {
        public OptimizationRequest()
        {
            DeliverySchedule = new HashSet<DeliverySchedule>();
        }

        public int Id { get; set; }
        public DateTime? RequestDate { get; set; }
        public DateTime? ScheduleDate { get; set; }
        public int? StatusId { get; set; }
        public int? OptimizeAfterMinuntes { get; set; }
        public DateTime? OptimizeDateTime { get; set; }
        public string RecurringSchedule { get; set; }

        public virtual RequestStatus Status { get; set; }
        public virtual ICollection<DeliverySchedule> DeliverySchedule { get; set; }
    }
}
