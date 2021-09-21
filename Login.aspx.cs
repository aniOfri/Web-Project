using System;

namespace VR_Web_Project
{
    public partial class Login : System.Web.UI.Page
    {
        // DECLARE A GLOBAL VARIABLE TO DISPLAY ERROR/SUCCESS MESSAGES ONs
        public string LogStatus = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            // CHECKS IF THE SITE HAS BEEN RELOADED DUE TO A SUBMIT PRESS
            if (Request["submit"] != null)
            {
                // DECLARE TWO STRINGS AS USERNAME AND PASSWORD USING THE SUBMIT DATA
                string username = Request["name"].Replace(" ", "");
                string password = Request["password"].Replace(" ", "");

                // DECLARE A USER USING THE FORMER STRINGS
                User user = new User(username, password);

                // LOGIN AND CONTINUE IF SUCCESS
                if (user.Login())
                {
                    // UPDATE THE GLOBAL VARIABLE (SUCCESS)
                    LogStatus = "התחברת בהצלחה";

                    // UPDATE THE SESSIONS
                    Session["User"] = user;
                    Session["Manager"] = user.IsManager;

                    // CHECKS IF THE USER HAS BEEN REDIRECTED TO THE LOGIN PAGE AFTER DOING AN ORDER
                    if (Session["RedirectOrder"] != null)
                    {
                        // DECLARE AN APPOINTMENT FROM SESSION AND ASSIGN THE USERS PHONE NUMBER TO TO APPOINTMENT
                        Appointment appointment = (Appointment)Session["RedirectOrder"];
                        appointment.PhoneNumber = user.PhoneNumber;

                        /*

                        WILL ADD A PAYMENT/VERIFICATION METHOD IN THE FUTURE.

                        */

                        // INSERT THE DATA INTO THE DATABASE
                        appointment.Order();
                    }

                    // REDIRECT HOME.ASPX
                    Response.Redirect("Home.aspx");
                    Response.End();
                }
                else
                    // UPDATE THE GLOBAL VARIABLE (FAIL)
                    LogStatus = "שם משתמש או סיסמה אינם נכונים";
            }
        }
    }
}