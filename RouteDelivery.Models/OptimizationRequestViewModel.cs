using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteDelivery.Models
{
    public enum RequestStatus
    {
        Waiting,
        Processing,
        Complete,
        Failed
    }
    public class InsertOptimizationRequestViewModel
    {
        public InsertOptimizationRequestViewModel()
        {
            RequestDate = System.DateTime.Now;
            ScheduleDate = System.DateTime.Now.Date;
            StatusId = 1;
            RecurringSchedule = "Daily";
        }
        public DateTime? RequestDate { get; set; }
        public DateTime? ScheduleDate { get; set; }
        public int? StatusId { get; set; }
        public int OptimizeAfterMinuntes { get; set; }
        public DateTime? OptimizeDateTime { get { return ((DateTime)RequestDate).AddMinutes(OptimizeAfterMinuntes); } }
        public string RecurringSchedule { get; set; }
    }

    public class OptimizationRequestViewModel : InsertOptimizationRequestViewModel
    {
        public int Id { get; set; }
    }
}
