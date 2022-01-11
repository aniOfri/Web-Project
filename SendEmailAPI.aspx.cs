using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VR_Web_Project
{
    public partial class SendEmailAPI : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            string emailAddr = Request.Form["EmailAddr"];
            string subject = Request.Form["EmailSubject"];
            string code = Request.Form["Code"];

            SmtpClient smtpClient = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential()
                {
                    UserName = "virtuariacorp@gmail.com",
                    Password = "dngMqAMW464X"
                }
            };

            MailAddress fromEmail = new MailAddress("virtuariacorp@gmail.com", "Virturia Corp.");
            MailAddress toEmail = new MailAddress(emailAddr, "Someone");

            string body = "<p dir=RTL>" +
                     "תודה שקניתם בוירטואריה!<br>" +
                    " קוד כרטיס המתנה שלכם הינו:<br>" +
                    "<b>" + code + "</b><br>" +
                    " מצפים לראותכם! <br>" +
                    "</p>";

             MailMessage mail = new MailMessage()
            {
                From = fromEmail,
                Subject = subject,
                Body = body
            };
            mail.IsBodyHtml = true;

            mail.To.Add(toEmail);

            smtpClient.Send(mail);
        }
    }
}