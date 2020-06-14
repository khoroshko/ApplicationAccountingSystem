using ApplicationAccountingSystemBusiness.IBusiness;
using ApplicationAccountingSystemModel;
using ApplicationAccountingSystemWeb.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationAccountingSystemBusiness.Controllers
{
    public class UserBusinessController: IUserApplication
    {
        public UserModel GetUser(ApplicationContext context, string UserId)
        {
            if (UserId != null && context != null)
            {
                UserModel um = new UserModel();
                um.ID = UserId;
                um.Email = context.Users.Find(UserId).Email;
                return um;
            }
            return null;
        }

        public string GetUserByMail(ApplicationContext context, string mail)
        {
            if (mail != null && context != null)
            {
                 return context.Users.Find(mail).Id;
            }
            return null;
        }
    }
}
