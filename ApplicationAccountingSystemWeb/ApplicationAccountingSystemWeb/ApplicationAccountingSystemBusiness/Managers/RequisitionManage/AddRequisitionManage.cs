using ApplicationAccountingSystemBusiness.Controllers;
using ApplicationAccountingSystemBusiness.IBusiness;
using ApplicationAccountingSystemModel;
using ApplicationAccountingSystemWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApplicationAccountingSystemBusiness.RequisitionManage
{
    public static class AddRequisitionManage
    {
        public static void AddRequisition(List<MessageModel> lm)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                foreach (var item in lm)
                {
                    IUserApplication ua = new UserBusinessController();
                    string um = ua.GetUserByMail(context, item.FromName);
                    if (um != null)
                    {
                        Requisition rq = new Requisition();
                        rq.UserCreate = um;
                        rq.Text = item.Body;
                        rq.StatusId = 2;
                        rq.DateCreate = DateTime.Now;
                        context.Requisitions.Add(rq);
                    }
                }
                context.SaveChanges();
            }
        }
    }
}