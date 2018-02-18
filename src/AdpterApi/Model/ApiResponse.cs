using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AdpterApi.Model
{
    public class APIResponse
    {
        public string Status { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string CorelationID { get; set; }
       
    }
}
