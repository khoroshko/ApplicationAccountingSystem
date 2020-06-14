using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationAccountingSystemModel;
using ApplicationAccountingSystemWeb.Models;

namespace ApplicationAccountingSystemBusiness.IBusiness
{
    public interface IRequisition
    {
        IEnumerable<Requisition> List(string status, string userId, ApplicationContext context, int page = 1);
        IEnumerable<ApplicationUser> AddGet(ApplicationContext context);
        void AddPost(Requisition rq, ApplicationContext context, string userId);
        Requisition Preview(int? id, ApplicationContext context);
        Requisition EditGet(int? id, ApplicationContext context);
        string EditPost(Requisition rq, ApplicationContext context, string userId);
        List<Status> GetStatuses(ApplicationContext context);
    }
}
