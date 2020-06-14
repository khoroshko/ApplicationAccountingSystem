using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ApplicationAccountingSystemModel
{
    public class MailModel
    {
        public string From = "readnandlisten@gmail.com";
        public string To { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string MyPassword = "readnlisten123!";
    }
}