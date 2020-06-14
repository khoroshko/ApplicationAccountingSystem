using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApplicationAccountingSystemModel
{
    public class MessageModel
    {
        public string MessageId { get; set; }
        public string FromId { get; set; }
        public string FromName { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Html { get; set; }
    }
}