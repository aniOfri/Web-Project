using System;
using System.Net;
using System.Net.Mail;

namespace VR_Web_Project
{
    public partial class SendEmailAPI : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            // GET ALL PARAMETERS FROM FORM
            string emailAddr = Request.Form["EmailAddr"];
            string subject = Request.Form["EmailSubject"];
            string code = Request.Form["Code"];

            // DECLARE NEW SMTPCLIENT
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

            // SET THE EMAIL DETAILS
            MailAddress fromEmail = new MailAddress("virtuariacorp@gmail.com", "Virtuaria Corp.");
            MailAddress toEmail = new MailAddress(emailAddr, "Someone");

            // BUILD THE EMAIL's BODY
            string body = "<p dir=RTL>" +
                     "תודה שקניתם בוירטואריה!<br>" +
                    " קוד כרטיס המתנה שלכם הינו:<br>" +
                    "<b>" + code + "</b><br>" +
                    " מצפים לראותכם! <br>" +
                    "</p>";

            // DECLARE MAIL
             MailMessage mail = new MailMessage()
            {
                From = fromEmail,
                Subject = subject,
                Body = body
            };
            mail.IsBodyHtml = true;
            mail.To.Add(toEmail);

            // SEND THE EMAIL
            smtpClient.Send(mail);
        }
    }
}