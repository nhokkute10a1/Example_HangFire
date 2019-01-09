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

        public struct RecurringScheduleType
        {
            public const string Daily = "Daily";
            public const string Hourly = " Hourly";
            public const string Minutely = " Minutely";
            public const string Monthly = " Monthly";
            public const string Weekly = " Weekly";
            public const string Yearly = "Yearly";
        }
    }
}
