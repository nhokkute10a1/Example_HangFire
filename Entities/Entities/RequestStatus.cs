using System;
using System.Collections.Generic;

namespace RouterDelivery.Entities.Entities
{
    public partial class RequestStatus
    {
        public RequestStatus()
        {
            OptimizationRequest = new HashSet<OptimizationRequest>();
        }

        public int Id { get; set; }
        public string StatusName { get; set; }

        public virtual ICollection<OptimizationRequest> OptimizationRequest { get; set; }
    }
}
