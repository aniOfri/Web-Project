using System;

namespace VR_Web_Project
{
    public partial class Login : System.Web.UI.Page
    {
        public string LogStatus = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["submit"] != null)
            {
                string username = Request["name"].Replace(" ", "");
                string password = Request["password"].Replace(" ", "");

                User user = new User(username, password);
                if (user.Login())
                {
                    LogStatus = "התחברת בהצלחה";

                    Session["User"] = user;
                    Session["Manager"] = user.IsManager;

                    if (Session["RedirectOrder"] != null)
                    {
                        Appointment appointment = (Appointment)Session["RedirectOrder"];
                        appointment.PhoneNumber = user.PhoneNumber;

                        appointment.Order();
                    }
                    Response.Redirect("Home.aspx");
                    Response.End();
                }
                else
                    LogStatus = "שם משתמש או סיסמה אינם נכונים";
            }
        }
    }
}