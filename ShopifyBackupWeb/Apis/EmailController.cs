using Microsoft.AspNetCore.Mvc;
using ShopifyBackupWeb.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShopifyBackupWeb.Apis
{
    [Route("api/shopify")]
    public class EmailController : Controller
    {
        [HttpPost("sendEmailFullFieldImport")]
        public bool Get([FromBody]List<ExcelExportModel> listData)
        {
            foreach(var dataEmail in listData)
            {
                Utils.AddDataToFile(dataEmail.Order, "ListFullField");
                try
                {
                    if (Utils.shopName == null)
                    {
                        Utils.updateShopNameDefault();
                    }
                    string strackingNumber = dataEmail.TrackingUrl;
                    string carrier = dataEmail.Carrer;
                    Utils.AddDataToFile(dataEmail.Order + "|" + dataEmail.TrackingUrl + "|" + strackingNumber + "|" + carrier, "Tracking");
                    int SmtpPort = 25;
                    string SmtpServer = "smtp.yandex.ru";

                    MailMessage EmailMsg = new MailMessage();

                    EmailMsg.From = new MailAddress(Utils.emailModel.EmailSend, Utils.emailModel.Host);
                    EmailMsg.To.Add(dataEmail.EMail);
                    //EmailMsg.ReplyToList.Add("info@do main.com");

                    EmailMsg.Subject = String.Format("A shipment from order {0} is on the way", dataEmail.Order);
                    var data = "";
                    data = data + "<hr/><div><img style='width: 73px; height: 76px;' src='" + dataEmail.Image + "'/><span>" + dataEmail.Order + " x " + dataEmail.Quantity + "</span><div><hr/>";
                    var path = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot" + "\\Data\\shipment_confirm.html");
                    var dataHtml = System.IO.File.ReadAllText(path);
                    //StringBuilder stringBuilder = new StringBuilder();
                    dataHtml = dataHtml.Replace("{sp}", data);
                    var vendor = Utils.shopName.Name;
                    var boday = dataHtml.Replace("{vendor}", vendor);
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
            }
            return true;
        }

        [HttpPost("sendEmailFullField")]
        public bool Get([FromBody]EmailFullFieldModel dataEmail)
        {
            Utils.AddDataToFile(dataEmail.Order.name, "ListFullField");
            string strackingNumber = dataEmail.TrackingUrl.Split("/")[1];
            string carrier = dataEmail.Carrier;
            Utils.AddDataToFile(dataEmail.Order.name+"|"+dataEmail.TrackingUrl+"|"+ strackingNumber+"|"+ carrier , "Tracking");
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
                var vendor = Utils.shopName.Name;
                foreach (var item in dataEmail.ListItem)
                {
                    vendor=Utils.shopName.Name;
                    if (data == "")
                    {
                        data = data + "<div><img  style='width: 73px; height: 76px;' src='" + item.image + "'/><span>" + item.name + " x " + item.quantity + "</span><div>";
                    }
                    else
                    {
                        data = data + "<hr/><div><img  style='width: 73px; height: 76px;' src='" + item.image + "'/><span>" + item.name + " x " + item.quantity + "</span><div>";
                    }
                }
                var path = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot" + "\\Data\\shipment_confirm.html");
                var dataHtml = System.IO.File.ReadAllText(path);
                //StringBuilder stringBuilder = new StringBuilder();
                dataHtml = dataHtml.Replace("{sp}", data);
                var boday = dataHtml.Replace("{vendor}", vendor);
                
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
                if (Utils.shopName == null)
                {
                    Utils.updateShopNameDefault();
                }
                int SmtpPort = 25;
                string SmtpServer = "smtp.yandex.ru";
                string dataStr = "";
                if (Convert.ToDecimal(dataEmail.MoneyRefunds) >= Convert.ToDecimal(dataEmail.Order.total_price))
                {
                    dataStr = dataEmail.Order.name+ "~refunded";
                }
                else
                {
                    dataStr = dataEmail.Order.name + "~partial refunded";
                }
                Utils.AddDataToFile(dataStr, "Refunds");
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
                var path = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot" + "\\Data\\email_refund.html");
                var dataHtml = System.IO.File.ReadAllText(path);
                //StringBuilder stringBuilder = new StringBuilder();
                var boday = dataHtml.Replace("{sp}", data);
                var linkDetails = dataEmail.TrackingUrl;
                boday = boday.Replace("{linkDetails}", linkDetails);
                boday = boday.Replace("{manualRefund}", dataEmail.MoneyRefunds);
                boday = boday.Replace("{moneyRefund}", dataEmail.MoneyRefunds);
                boday = boday.Replace("{shipping}", dataEmail.Order.total_price);
                boday = boday.Replace("{subTotal}", dataEmail.Order.total_price);
                boday = boday.Replace("{manual}", dataEmail.Order.total_price);
                boday = boday.Replace("{total}", dataEmail.Order.total_price);
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
