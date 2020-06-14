using ApplicationAccountingSystemModel;
using ApplicationAccountingSystemWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationAccountingSystemBusiness.IBusiness
{
    public interface IUserApplication
    {
        UserModel GetUser(ApplicationContext context, string UserId);
        string GetUserByMail(ApplicationContext context, string mail);

    }
}
