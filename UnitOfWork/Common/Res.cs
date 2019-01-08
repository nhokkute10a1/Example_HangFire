using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace LibCommon.Common
{
    public class Res
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
    public class ResUser
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public string Token { get; set; }
        public string ExpireDays { get; set; }
    }
}
