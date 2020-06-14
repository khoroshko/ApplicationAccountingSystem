using ApplicationAccountingSystemModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ApplicationAccountingSystemWeb.Models
{
    public class AppDbInitializer : DropCreateDatabaseAlways<ApplicationContext>
    {
        protected override void Seed(ApplicationContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var role1 = new IdentityRole { Name = "executor" };
            var role2 = new IdentityRole { Name = "customer" };

            roleManager.Create(role1);
            roleManager.Create(role2);

            base.Seed(context);
        }
    }
}