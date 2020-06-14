using ApplicationAccountingSystemBusiness.MailManage;
using ApplicationAccountingSystemBusiness.RequisitionManage;
using ApplicationAccountingSystemModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace ApplicationAccountingSystemWeb.Modules
{
    public class TimerModule: IHttpModule
    {
        static Timer timer;
        long interval = 30000; //30 секунд
        static object synclock = new object();
        static bool sent = false;

        public void Init(HttpApplication app)
        {
            timer = new Timer(new TimerCallback(ReadEmail), null, 0, interval);
        }

        private void ReadEmail(object obj)
        {
            lock (synclock)
            {
                DateTime dd = DateTime.Now;
                if (dd.Hour == 1 && dd.Minute == 30 && sent == false)
                {
                    List<MessageModel> lm = ReadMailManage.GetEmails();
                    AddRequisitionManage.AddRequisition(lm);

                }
                else if (dd.Hour != 1 && dd.Minute != 30)
                {
                    sent = false;
                }
            }
        }
        public void Dispose()
        { }
    }
}