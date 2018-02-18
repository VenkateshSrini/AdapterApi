using System;
using System.Collections.Generic;
using System.Text;

namespace AdpterApi.Model
{
    class OutgoingMailModel
    {
        
        public string FromAddress { get; set; }
        
        public List<string> ToAddress { get; set; }

        public List<string> Bcc { get; set; }

        public List<string> Cc { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }
        public OutgoingMailModel()
        {
            Bcc = new List<string>();
            Cc = new List<string>();
        }
    }
}
