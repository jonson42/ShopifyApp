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
        [HttpPost("sendEmailFullFieldImport")]
        public bool Get([FromBody]ExcelExportModel dataEmail)
        {
            Utils.AddOrderFullField(dataEmail.Order);
            try
            {
                int SmtpPort = 25;
                string SmtpServer = "smtp.yandex.ru";

                MailMessage EmailMsg = new MailMessage();

                EmailMsg.From = new MailAddress(Utils.emailModel.EmailSend, Utils.emailModel.Host);
                EmailMsg.To.Add(dataEmail.EMail);
                //EmailMsg.ReplyToList.Add("info@do main.com");

                EmailMsg.Subject = String.Format("A shipment from order {0} is on the way", dataEmail.Order);
                var data = "";
                data = data + "<hr/><div><img style='width: 73px; height: 76px;' src='" + dataEmail.Image + "'/>" + dataEmail.Order + " x " + dataEmail.Quantity + "<div><hr/>";
                var path = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot" + "\\Data\\shipment_confirm.html");
                var dataHtml = System.IO.File.ReadAllText(path);
                //StringBuilder stringBuilder = new StringBuilder();
                var boday = dataHtml.Replace("{sp}", data);
                var linkDetails = dataEmail.TrackingUrl;
                boday = boday.Replace("{linkCollection}", Utils.dnsModel.Name + "/collections");
                boday = boday.Replace("{linkDetails}", linkDetails);
                boday = boday.Replace("{email}", Utils.emailContacts.Name);
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
                SMTP.Credentials = new System.Net.NetworkCredential(Utils.emailModel.EmailSend, Utils.emailModel.PassSend);

                SMTP.Send(EmailMsg);
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        [HttpPost("sendEmailFullField")]
        public bool Get([FromBody]EmailFullFieldModel dataEmail)
        {
            Utils.AddOrderFullField(dataEmail.Order.name);
            try
            {
                int SmtpPort = 25;
                string SmtpServer = "smtp.yandex.ru";

                MailMessage EmailMsg = new MailMessage();

                EmailMsg.From = new MailAddress(Utils.emailModel.EmailSend, Utils.emailModel.Host);
                EmailMsg.To.Add(dataEmail.Email);
                //EmailMsg.ReplyToList.Add("info@do main.com");

                EmailMsg.Subject = String.Format("A shipment from order {0} is on the way", dataEmail.Name);
                var data = "";
                foreach(var item in dataEmail.ListItem)
                {
                    if (data == "")
                    {
                        data = data + "<div><img  style='width: 73px; height: 76px;' src='" + item.image + "'/>" + item.name + " x " + item.quantity + "<div>";
                    }
                    else
                    {
                        data = data + "<hr/><div><img  style='width: 73px; height: 76px;' src='" + item.image + "'/>" + item.name + " x " + item.quantity + "<div>";
                    }
                }
                var path = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot" + "\\Data\\shipment_confirm.html");
                var dataHtml = System.IO.File.ReadAllText(path);
                //StringBuilder stringBuilder = new StringBuilder();
                var boday = dataHtml.Replace("{sp}", data);
                var linkDetails =dataEmail.TrackingUrl;
                boday = boday.Replace("{linkDetails}", linkDetails);
                boday = boday.Replace("{linkCollection}", Utils.dnsModel.Name+"/collections");
                
                boday = boday.Replace("{email}", Utils.emailContacts.Name);
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
                SMTP.Credentials = new System.Net.NetworkCredential(Utils.emailModel.EmailSend, Utils.emailModel.PassSend);

                SMTP.Send(EmailMsg);
            }
            catch(Exception ex)
            {
                return false;
            }
            
            return true;
        }

        [HttpPost("sendEmailRefunds")]
        public bool EmailRefunds([FromBody]EmailRefundsModel dataEmail)
        {
            try
            {
                int SmtpPort = 25;
                string SmtpServer = "smtp.yandex.ru";

                MailMessage EmailMsg = new MailMessage();

                EmailMsg.From = new MailAddress(Utils.emailModel.EmailSend,Utils.emailModel.Host);
                EmailMsg.To.Add(dataEmail.Email);
                //EmailMsg.ReplyToList.Add("info@do main.com");

                EmailMsg.Subject = "Refund notification";
                var data = "";
                foreach (var item in dataEmail.Order.line_items)
                {
                    if (data == "")
                    {
                        data = data + "<div><img  style='width: 73px; height: 76px;' src='" + item.image + "'/>" + item.name + " x " + item.quantity + "<div>";
                    }
                    else
                    {
                        data = data + "<hr/><div><img  style='width: 73px; height: 76px;' src='" + item.image + "'/>" + item.name + " x " + item.quantity + "<div>";
                    }
                   
                }
                var path = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot" + "\\Data\\email_refund.html.html");
                var dataHtml = System.IO.File.ReadAllText(path);
                //StringBuilder stringBuilder = new StringBuilder();
                var boday = dataHtml.Replace("{sp}", data);
                var linkDetails = dataEmail.TrackingUrl;
                boday = boday.Replace("{linkDetails}", linkDetails);
                boday = boday.Replace("{manualRefund}", dataEmail.MoneyRefunds);
                boday = boday.Replace("{moneyRefund}", dataEmail.MoneyRefunds);
                boday = boday.Replace("{subTotal}", dataEmail.Order.total_price);
                boday = boday.Replace("{ total}", dataEmail.Order.total_price);
                boday = boday.Replace("{email}", Utils.emailContacts.Name);
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
                SMTP.Credentials = new System.Net.NetworkCredential(Utils.emailModel.EmailSend, Utils.emailModel.PassSend);

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
