using OpenPop.Mime;
using OpenPop.Pop3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApplicationAccountingSystemModel;

namespace ApplicationAccountingSystemBusiness.MailManage
{
    public static class ReadMailManage
    {
        public static List<MessageModel> GetEmails()
        {
            Pop3Client objPop3Client = new Pop3Client();
            MessageModel message = new MessageModel();
            List<MessageModel> lMessageModel = new List<MessageModel>();

            string host = "";
            string user = "";
            string password = "";
            int port = 110;
            bool useSsl = false;

            try
            {
                objPop3Client.Connect(host,port,useSsl);
                objPop3Client.Authenticate(user, password);

                for(int i = 1; i<= objPop3Client.GetMessageCount();i++)
                {
                    message = GetEmailContent(i,ref objPop3Client);
                    if(message != null)
                    {
                        lMessageModel.Add(message);
                    }
                }
            }
            catch(Exception ex)
            {

            }
            finally
            {
                objPop3Client.Disconnect();
            }
            return lMessageModel;
        }

        public static MessageModel GetEmailContent(int intMessageNumber, ref Pop3Client objPop3Client)
        {
            MessageModel message = new MessageModel();
            Message objMessage;
            MessagePart plainTextPart = null, HTMLTextPart = null;
            objMessage = objPop3Client.GetMessage(intMessageNumber);
            message.MessageId = objMessage.Headers.MessageId == null ? "" : objMessage.Headers.MessageId.Trim();
            message.FromId = objMessage.Headers.From.Address.Trim();
            message.FromName = objMessage.Headers.From.DisplayName.Trim();
            message.Subject = objMessage.Headers.Subject.Trim();

            plainTextPart = objMessage.FindFirstPlainTextVersion();
            message.Body = (plainTextPart == null ? "" : plainTextPart.GetBodyAsText().Trim());

            HTMLTextPart = objMessage.FindFirstPlainTextVersion();
            message.Html = (HTMLTextPart == null ? "" : HTMLTextPart.GetBodyAsText().Trim());
            return message;
        }
    }
}