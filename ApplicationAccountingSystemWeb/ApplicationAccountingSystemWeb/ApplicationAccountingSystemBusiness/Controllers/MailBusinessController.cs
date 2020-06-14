using ApplicationAccountingSystemBusiness.IBusiness;
using ApplicationAccountingSystemModel;
using ApplicationAccountingSystemWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationAccountingSystemBusiness.Controllers
{
    public class MailBusinessController: IMailSend
    {
        public MailModel GetMailModel(ApplicationContext context, string Email, int Id,DateTime DateChange, string UserChange)
        {
            MailModel mm = new MailModel();
            mm.To = Email;
            mm.Subject = "Успешное закрытие заявки";
            mm.Body = string.Format("Заявка номер {0} была успешно закрыта {1} исполнителем {2}!", Id, DateChange, UserChange);
            return mm;
        }
    }
}
