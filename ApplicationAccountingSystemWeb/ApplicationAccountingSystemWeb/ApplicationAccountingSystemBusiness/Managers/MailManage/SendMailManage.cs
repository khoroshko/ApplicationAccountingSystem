using OpenPop.Pop3;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using ApplicationAccountingSystemModel;

namespace ApplicationAccountingSystemBusiness.MailManage
{
    public static class SendMailManage
    {
        public static string SendEmailAsync(MailModel model)
        {
            try
            {
                MailAddress from = new MailAddress(model.From, model.Name);
                MailAddress to = new MailAddress(model.To);
                MailMessage m = new MailMessage(from, to);
                m.Subject = model.Subject;
                m.Body = model.Body;
                m.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential(model.From, model.MyPassword);
                smtp.EnableSsl = true;
                smtp.Send(m);

                return "OK";
            }
            catch (SmtpException ex)
            {
               return ex.InnerException.Message.ToString();
            }
        }
    }
}