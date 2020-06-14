using ApplicationAccountingSystemWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ApplicationAccountingSystemModel;
using ApplicationAccountingSystemBusiness;
using ApplicationAccountingSystemBusiness.Controllers;
using ApplicationAccountingSystemBusiness.IBusiness;
using ApplicationAccountingSystemBusiness.MailManage;
using Microsoft.AspNet.Identity;

namespace ApplicationAccountingSystemWeb.Controllers
{
    [Authorize]
    public class RequisitionController : Controller
    {
        public ActionResult List(string status, int page = 1)
        {
            ApplicationContext context = new ApplicationContext();
            try
            {
                IRequisition rq = new RequisitionBusinessController();
                string userId = User.Identity.GetUserId();
                var rql = rq.List(status, userId, context,page);
                var st = rq.GetStatuses(context);
                RequisitionViewModel rqvm = new RequisitionViewModel
                {
                    Requisitions = rql.ToList(),
                    Statuses = new SelectList(st, "Id", "Name"),
                };

                int pageSize = 5;
                IEnumerable<Requisition> rList = rqvm.Requisitions.Skip((page - 1) * pageSize).Take(pageSize);

                PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = rqvm.Requisitions.Count() };
                RequisitionViewModel stvm = new RequisitionViewModel { PageInfo = pageInfo, Requisitions = rList };

                SelectList stat = new SelectList(st, "Id", "Name");
                ViewBag.status = stat;

                return View(stvm);
            }
            catch
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            finally
            {
                context.Dispose();
            }
        }

        public ActionResult Add()
        {
            ApplicationContext context = new ApplicationContext();
            try
            {
                IRequisition rq = new RequisitionBusinessController();
                IEnumerable<ApplicationUser> ex = rq.AddGet(context);
                return View();
            }
            catch
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            finally
            {
                context.Dispose();
            }
        }

        [HttpPost]
        public ActionResult Add(Requisition rqm)
        {
            ApplicationContext context = new ApplicationContext();
            if (!ModelState.IsValid)
            {
                return View(rqm);
            }
            try
            {
                string userId = User.Identity.GetUserId();
                IRequisition rq = new RequisitionBusinessController();
                rq.AddPost(rqm, context, userId);

                return RedirectToAction("Preview", new { Id = rqm.Id });
            }
            catch
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            finally
            {
                context.Dispose();
            }
        }

        public ActionResult Preview(int? id)
        {
            ApplicationContext context = new ApplicationContext();
            try
            {
                IRequisition rq = new RequisitionBusinessController();
                var model = rq.Preview(id, context);
                if (model != null)
                {
                    return View(model);
                }
                else
                {
                    throw new HttpException(404, "Заявка не найдена");
                }
            }
            catch
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            finally
            {
                context.Dispose();
            }
        }

        [Authorize(Roles = "executor")]
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            ApplicationContext context = new ApplicationContext();
            try
            {
                if (id == null)
                {
                    return HttpNotFound();
                }
                IRequisition rq = new RequisitionBusinessController();
                var rqm = rq.EditGet(id, context);
                if (rqm != null)
                {
                    SelectList st = new SelectList(context.Statuses, "Id", "Name");
                    ViewBag.Statuses = st;
                    return View(rq);
                }
            }
            catch
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            finally
            {
                context.Dispose();
            }
            return HttpNotFound();
        }

        [Authorize(Roles = "executor")]
        [HttpPost]
        public ActionResult Edit(Requisition rqm)
        {
            ApplicationContext context = new ApplicationContext();
            if (!ModelState.IsValid)
            {
                return View(rqm);
            }
            try
            {
               
                var userId = User.Identity.GetUserId();
                IRequisition rq = new RequisitionBusinessController();
                var res = rq.EditPost(rqm, context, userId);
                if (res == "OK")
                {
                    IUserApplication ua = new UserBusinessController();
                    UserModel um = ua.GetUser(context, userId);
                    IMailSend imm = new MailBusinessController();
                    MailModel mm = imm.GetMailModel(context, um.Email, rqm.Id, rqm.DateChange, rqm.UserChange);
                    return RedirectToAction("SendMail", "Requisition", mm);
                }
                else
                {
                    throw new HttpException(404, "Ошибка при редактирование");
                }
            }
            catch
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            finally
            {
                context.Dispose();
            }
        }

        public ActionResult SendMail(MailModel model)
        {
            SendMailManage.SendEmailAsync(model);
            return RedirectToAction("List");
        }
    }
}