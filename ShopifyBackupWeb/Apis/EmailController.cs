using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopifyBackupWeb.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShopifyBackupWeb.Apis
{
    [Route("api/shopify")]
    public class EmailController : Controller
    {
        [HttpPost("sendEmailFullField")]
        public bool Get([FromBody]EmailFullFieldModel dataEmail)
        {
            try
            {
                int SmtpPort = 25;
                string SmtpServer = "smtp.yandex.ru";

                MailMessage EmailMsg = new MailMessage();

                EmailMsg.From = new MailAddress("payment@brassidium.com", "brassidium.com");
                EmailMsg.To.Add(dataEmail.Email);
                //EmailMsg.ReplyToList.Add("info@do main.com");

                EmailMsg.Subject = String.Format("A shipment from order {0} is on the way", dataEmail.Name);
                var data = "";
                foreach(var item in dataEmail.ListItem)
                {
                    data = data + "<hr/><div>" + item.name + " x "+item.quantity+ "<div><hr/>";
                }
                var path = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot" + "\\Data\\shipment_confirm.html");
                var dataHtml = System.IO.File.ReadAllText(path);
                //StringBuilder stringBuilder = new StringBuilder();
                var boday = dataHtml.Replace("{sp}", data);
                var linkDetails = "https://lestweforgetshop.com/9277734990/orders/cb3696307da06c13de1b6339e95d25cc";
                boday = boday.Replace("{linkDetails}", linkDetails);
                //String.Format(dataHtml, data); //stringBuilder.Append(String.Format(dataHtml, data));
                EmailMsg.Body = boday.ToString();

                EmailMsg.IsBodyHtml = true;
                EmailMsg.Priority = MailPriority.Normal;

                System.Net.Mail.SmtpClient SMTP = new System.Net.Mail.SmtpClient();
                SMTP.Host = SmtpServer;
                SMTP.Port = SmtpPort;
                SMTP.EnableSsl = true;
                SMTP.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                SMTP.UseDefaultCredentials = false;
                SMTP.Credentials = new System.Net.NetworkCredential("payment@brassidium.com", "tranyeu04");

                SMTP.Send(EmailMsg);
            }
            catch(Exception ex)
            {
                return false;
            }
            
            return true;
        }
        [HttpPost("sendEmailRefunds")]
        public bool EmailRefunds([FromBody]EmailFullFieldModel dataEmail)
        {
            try
            {
                int SmtpPort = 25;
                string SmtpServer = "smtp.yandex.ru";

                MailMessage EmailMsg = new MailMessage();

                EmailMsg.From = new MailAddress("payment@brassidium.com", "brassidium.com");
                EmailMsg.To.Add(dataEmail.Email);
                //EmailMsg.ReplyToList.Add("info@do main.com");

                EmailMsg.Subject = String.Format("A shipment from order {0} is on the way", dataEmail.Name);
                var data = "";
                foreach (var item in dataEmail.ListItem)
                {
                    data = data + "<hr/><div>" + item.name + " x " + item.quantity + "<div><hr/>";
                }
                var path = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot" + "\\Data\\shipment_confirm.html");
                var dataHtml = System.IO.File.ReadAllText(path);
                //StringBuilder stringBuilder = new StringBuilder();
                var boday = dataHtml.Replace("{sp}", data);
                var linkDetails = "https://lestweforgetshop.com/9277734990/orders/cb3696307da06c13de1b6339e95d25cc";
                boday = boday.Replace("{linkDetails}", linkDetails);
                //String.Format(dataHtml, data); //stringBuilder.Append(String.Format(dataHtml, data));
                EmailMsg.Body = boday.ToString();

                EmailMsg.IsBodyHtml = true;
                EmailMsg.Priority = MailPriority.Normal;

                System.Net.Mail.SmtpClient SMTP = new System.Net.Mail.SmtpClient();
                SMTP.Host = SmtpServer;
                SMTP.Port = SmtpPort;
                SMTP.EnableSsl = true;
                SMTP.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                SMTP.UseDefaultCredentials = false;
                SMTP.Credentials = new System.Net.NetworkCredential("payment@brassidium.com", "tranyeu04");

                SMTP.Send(EmailMsg);
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

    }
}
