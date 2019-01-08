using System;
using System.Collections.Generic;
using System.Text;

namespace LibCommon.Common
{
    public struct CommonContanst
    {
        public enum RequestStatus
        {
            Scheduled,
            Processing,
            Complete,
            Failed,
            Removed
        }

        public enum RecurringScheduleType
        {
            Daily,
            Hourly,
            Minutely,
            Monthly,
            Weekly,
            Yearly
        }
    }
}
