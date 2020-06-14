using ApplicationAccountingSystemModel;
using ApplicationAccountingSystemWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationAccountingSystemBusiness.IBusiness
{
    public interface IMailSend
    {
        MailModel GetMailModel(ApplicationContext context, string Email, int Id, DateTime DateChange, string UserChange);
    }
}
