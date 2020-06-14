using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationAccountingSystemModel;
using ApplicationAccountingSystemWeb.Models;

namespace ApplicationAccountingSystemBusiness.Controllers
{
    public class RequisitionBusinessController : IBusiness.IRequisition
    {
        public IEnumerable<Requisition> List(string status, string userId, ApplicationContext context, int page = 1)
        {
            
            IQueryable<Requisition> rq = context.Requisitions;
            IQueryable<Status> sub = context.Statuses;
            var profile = context.Users.Where(p => p.Id == userId).FirstOrDefault().IsExecutor;
            if (profile != null)
            {
                if (profile == false)
                {
                    if (!String.IsNullOrEmpty(status))
                    {
                        rq = rq.Where(p => p.StatusId == Convert.ToInt32(status) && p.UserCreate == userId);
                    }
                    if (status == null)
                    {
                        rq = rq.Where(p => p.UserCreate == userId);
                    }
                }
                else
                { 
                    if (!String.IsNullOrEmpty(status))
                    {
                        var subId = sub.FirstOrDefault(s => s.Name == status);
                        rq = rq.Where(p => p.StatusId == subId.Id && p.UserExecutorId == userId);
                    }
                    if (status == null)
                    {
                        rq = rq.Where(p => p.UserExecutorId == userId);
                    }
                }

                if (rq != null)
                {
                    return rq.ToList();
                }
                return null;
            }
            return null;

        }
        public IEnumerable<ApplicationUser> AddGet(ApplicationContext context)
        {
            if (context != null)
            {
                IEnumerable<ApplicationUser> ex = context.Users.Where(p => p.IsExecutor == true);
                return ex;
            }
            return null;
        }
        public void AddPost(Requisition rq, ApplicationContext context, string userId)
        {
            if (rq != null && context != null)
            {
                var profile = context.Users.Where(p => p.Id == userId).FirstOrDefault().Email;
                if (profile != null)
                {
                    rq.UserCreate = profile;
                    rq.DateCreate = DateTime.Now;
                    rq.StatusId = 1;
                    context.Requisitions.Add(rq);
                    context.SaveChanges();
                }
            }

        }
        public Requisition Preview(int? id, ApplicationContext context)
        {
            if (id != null && context != null)
            {
                return context.Requisitions.FirstOrDefault(x => x.Id == id);
            }
            return null;
        }
        public Requisition EditGet(int? id, ApplicationContext context)
        {
            if (id != null && context != null)
            {
                Requisition rq = context.Requisitions.Find(id);
                return rq;
            }
            return null;
        }
        public string EditPost(Requisition rq, ApplicationContext context, string userId)
        {
            if (rq != null && context != null)
            {
                var profile = context.Users.Where(p => p.Id == userId).FirstOrDefault().Email;
                if (profile != null)
                {
                    rq.DateChange = DateTime.Now;
                    rq.UserChange = profile;
                    context.Entry(rq).State = EntityState.Modified;
                    context.SaveChanges();
                    return "OK";
                }
            }
            return null;
        }

        public List<Status> GetStatuses(ApplicationContext context)
        {
            List<Status> stl = context.Statuses.ToList();
            return stl;
        }
    }
}
