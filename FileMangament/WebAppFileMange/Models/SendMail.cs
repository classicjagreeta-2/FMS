using System;

using System.IO;
using System.Text;

using System.Net.Mail;
using System.Collections;

namespace WebAppFileMange.Models
{
    public class SendMail
    {

        public string SendMailOnly(string body, string toaddress, string EmailPort, string EmailServer, string EmailUsr, string EmailPass, string EmailDefault, string title, string CC_Address)
        {
            string RUL = "";
            MailMessage mail = new MailMessage();
            SmtpClient client = new SmtpClient();
            client.Port = Convert.ToInt32(EmailPort);
            client.Host = EmailServer;
            client.EnableSsl = true;
            client.Timeout = 100000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(EmailUsr, EmailPass);
            MailAddress from = new MailAddress(EmailUsr, EmailDefault);
            MailAddress to = new MailAddress(toaddress);//toaddress
            MailMessage mm = new MailMessage(from, to);
            mm.Subject = title;
            mm.Body = body;
            mm.IsBodyHtml = true;
            mm.Priority = MailPriority.High;
            mm.BodyEncoding = UTF8Encoding.UTF8;
            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            if (CC_Address.Length > 0)
            {
                string[] CCId = CC_Address.Split(',');
                foreach (string CCEmail in CCId)
                {
                    mm.CC.Add(new MailAddress(CCEmail)); //Adding Multiple CC email Id
                }
            }
            try
            {
                client.Send(mm);
                System.Threading.Thread.Sleep(10000);
                RUL = "OK";
            }
            catch (Exception ex)
            {
                RUL = "NO";
            }
            return RUL;
        }

        public string[] SendMailAttchment(byte[] body, MemoryStream memoryStream, string toaddress, string company_name, int Flag,
            string EmailPort, string EmailServer, string EmailUsr,
            string EmailPass, string EmailFrom, string Subtitle, string head, string filename, string CC_Address,
            string BodyMatter, string FilesourcePath, string sFileName)
        {


            string[] str = new string[2];
            string RUL = "";
            MailMessage mail = new MailMessage();
            SmtpClient client = new SmtpClient();
            client.Port = Convert.ToInt32(EmailPort);
            client.Host = EmailServer;
            client.EnableSsl = true;
            client.Timeout = 100000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = true;
            client.Credentials = new System.Net.NetworkCredential(EmailUsr, EmailPass);

            //MailAddress to = new MailAddress(toaddress);
            mail.From = new MailAddress(EmailFrom);
            if (toaddress.Length > 0)
            {
                string[] ToAddress = toaddress.Split(',');
                foreach (string ToEmail in ToAddress)
                {

                    mail.To.Add(new MailAddress(ToEmail));
                }
            }

            // MailMessage mm = new MailMessage(from, to);
            mail.Subject = Subtitle;
            string ss = "<table style='font-family:Calibri' border='0' cellpadding='3' cellspacing='0' style='width:500px; height:350px'>";
            // ss = ss + "<tr style='vertical-align:top; background-color:#004a8f; color:White; font-size:20px; font-family:Calibri; font-weight:bold;width:500px'><td align='center' style='height:20px'>" + head + "</td></tr>";
            ss = ss + " <tr><td style='height:10px'>Dear Sir/Madam,<br /><br /></td></tr>";
            ss = ss + "<tr><td style='height:10px'>" + BodyMatter + "<br /><br /></td></tr>";
            ss = ss + " <tr><td style='height:10px'>Thanking You,<br /></td></tr>";
            ss = ss + "<tr><td style='height:10px'>From - " + company_name + "<br /></td></tr>";
            ss = ss + "<tr><td><br /></td></tr>";
            // ss = ss + " <tr><td align='center' style='vertical-align:bottom;height:10px; background-color:#e2effa;width:500px'>*This is an auto generated mail*&nbsp; &nbsp;&nbsp;   </td></tr></table>";
            ss = ss + " <tr></tr></table>";
            mail.Body = ss;
            if (Flag == 1)
            {
                FilesourcePath = System.Web.HttpContext.Current.Server.MapPath(FilesourcePath);
                DirectoryInfo dir = new DirectoryInfo(FilesourcePath);
                foreach (FileInfo file in dir.GetFiles("*.*"))
                {
                    if (file.Exists)
                    {
                        if (file.ToString() == sFileName)
                            mail.Attachments.Add(new Attachment(file.FullName));
                    }
                }
            }
            if (Flag == 2)
            {
                mail.Attachments.Add(new Attachment(memoryStream, filename, "application/pdf"));
            }
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;
            mail.BodyEncoding = UTF8Encoding.UTF8;
            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            if (CC_Address.Length > 0)
            {
                string[] CCId = CC_Address.Split(',');
                foreach (string CCEmail in CCId)
                {
                    mail.CC.Add(new MailAddress(CCEmail)); //Adding Multiple CC email Id
                }
            }
            try
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                client.Send(mail);
                System.Threading.Thread.Sleep(10000);
                RUL = "1";
                str[0] = RUL;
            }
            catch (Exception ex)
            {
                //Logger logger = LogManager.GetCurrentClassLogger();
                //logger.ErrorException("Email Sending : ", ex);
                RUL = "0";
                str[0] = RUL;
                str[1] = ex.Message;


            }
            return str;
        }

        public int SetASCII(string value)
        {
            string s = value;
            byte[] bytes = Encoding.ASCII.GetBytes(s);
            int result = BitConverter.ToInt32(bytes, 0);
            return result;
        }

        public string GetASCII(int value)
        {
            int i = value;
            byte[] bytes2 = BitConverter.GetBytes(i);
            string s2 = Encoding.ASCII.GetString(bytes2);
            return s2;
        }

        public string SendMail_MultipleAttach(byte[] body, string toaddress, string user_name, int Flag, string EmailPort, string EmailServer, string EmailUsr, string EmailPass, string EmailDefault, string title, string head, string filename, string CC_Address, ArrayList Attachment)
        {
            string RUL = "";
            MailMessage mail = new MailMessage();
            SmtpClient client = new SmtpClient();
            client.Port = 587; //Convert.ToInt32(EmailPort);
            client.Host = EmailServer;
            client.EnableSsl = true;
            client.Timeout = 100000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(EmailUsr, EmailPass);
            MailAddress from = new MailAddress(EmailUsr, EmailDefault);
            MailAddress to = new MailAddress(toaddress);
            MailMessage mm = new MailMessage(from, to);
            mm.Subject = title;
            string ss = "<table style='font-family:Calibri' border='0' cellpadding='3' cellspacing='0' style='width:500px; height:350px'>";
            ss = ss + "<tr style='vertical-align:top; background-color:#004a8f; color:White; font-size:20px; font-family:Calibri; font-weight:bold;width:500px'><td align='center' style='height:20px'>" + head + "</td></tr>";
            ss = ss + " <tr><td style='height:10px'>Dear Sir/Madam,<br /><br /></td></tr>";
            ss = ss + "<tr><td style='height:10px'>Please find the attachment along with this mail.<br /><br /></td></tr>";
            ss = ss + " <tr><td style='height:10px'>Thanking You,<br /></td></tr>";
            ss = ss + "<tr><td style='height:10px'>" + user_name + "<br /></td></tr>";
            ss = ss + "<tr><td><br /></td></tr>";
            ss = ss + " <tr><td align='center' style='vertical-align:bottom;height:10px; background-color:#e2effa;width:500px'>*This is an auto generated mail*&nbsp; &nbsp;&nbsp;    Prepared  By - CLASSIC</td></tr></table>";
            mm.Body = ss;
            if (Flag == 1)
            {
                mm.Attachments.Add(new Attachment(new MemoryStream(body), filename, "application/vnd.ms-excel"));
                for (int i = 0; i <= Attachment.Count - 1; i++)
                {
                    byte[] atta = (byte[])Attachment[i];
                    mm.Attachments.Add(new Attachment(new MemoryStream(atta), filename, "application/vnd.ms-excel"));
                }
            }
            else if (Flag == 2)
            {
                mm.Attachments.Add(new Attachment(new MemoryStream(body), filename, "application/pdf"));
            }
            else
            {
                mm.Attachments.Add(new Attachment(new MemoryStream(body), filename, "application/pdf"));
                for (int i = 0; i <= Attachment.Count - 1; i++)
                {
                    byte[] atta = (byte[])Attachment[i];
                    mm.Attachments.Add(new Attachment(new MemoryStream(atta), filename, "application/vnd.ms-excel"));
                }
            }

            mm.IsBodyHtml = true;
            mm.Priority = MailPriority.High;
            mm.BodyEncoding = UTF8Encoding.UTF8;
            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            int cnt = 0;
            if (CC_Address.Length > 0)
            {
                string[] CCId = CC_Address.Split(',');
                foreach (string CCEmail in CCId)
                {
                    if (cnt > 0)
                        mm.CC.Add(new MailAddress(CCEmail)); //Adding Multiple CC email Id
                    cnt++;
                }
            }
            try
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                client.Send(mm);
                //System.Threading.Thread.Sleep(10000);
                RUL = "Success";
            }
            catch (Exception ex)
            {
                RUL = "Failed";
            }
            return RUL;
        }
    }
}