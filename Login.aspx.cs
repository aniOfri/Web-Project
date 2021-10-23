using System;

namespace VR_Web_Project
{
    public partial class Login : System.Web.UI.Page
    {
        // DECLARE A GLOBAL VARIABLE TO DISPLAY ERROR/SUCCESS MESSAGES ONs
        public string LogStatus = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            // CHECKS IF STATUS SESSION IS NOT NULL
            if (Session["logstatus"] != null)
            {
                // IF SO, UPDATE THE GLOBAL VARIABLE TO DISPLAY THE STATUS
                string status = Session["logstatus"].ToString();
                if (status == "200")
                    LogStatus = "התחברת בהצלחה";
                else if (status == "450") LogStatus = "שם משתמש או סיסמה אינם נכונים";
            }

            // CHECKS IF THE SITE HAS BEEN RELOADED DUE TO A SUBMIT PRESS
            else if (Request["name"] != null)
            {
                // CHECK IF SUBMIT WAS ACTUALLY "CANCEL ORDER"
                if (Request["name"] == "")
                {
                    Response.Redirect("Logout.aspx");
                    Response.Redirect("Home.aspx");
                    Response.End();
                }
                // DECLARE TWO STRINGS AS USERNAME AND PASSWORD USING THE SUBMIT DATA
                string username = Request["name"].Replace(" ", "");
                string password = Request["password"].Replace(" ", "");

                // DECLARE A USER USING THE FORMER STRINGS
                User user = new User(username, password);

                // LOGIN AND CONTINUE IF SUCCESS
                if (user.Login())
                {
                    // UPDATE THE GLOBAL VARIABLE (SUCCESS)
                    Session["logstatus"] = 200;

                    // UPDATE THE SESSIONS
                    Session["User"] = user;
                    Session["Manager"] = user.IsManager;

                    // CHECKS IF THE USER HAS BEEN REDIRECTED TO THE LOGIN PAGE AFTER DOING AN ORDER
                    if (Session["RedirectOrder"] != null)
                    {
                        // DECLARE AN APPOINTMENT FROM SESSION AND ASSIGN THE USERS PHONE NUMBER TO TO APPOINTMENT
                        Appointment appointment = (Appointment)Session["RedirectOrder"];
                        appointment.UserId = user.Id.ToString();

                        // REDIRECT PAYMENT.ASPX
                        Session["RedirectOrder"] = appointment;
                        Response.Redirect("Payment.aspx");
                        Response.End();
                    }
                    else
                    {
                        // REDIRECT HOME.ASPX
                        Response.Redirect("Home.aspx");
                        Response.End();
                    }
                }
                else
                {
                    // UPDATE THE GLOBAL VARIABLE (FAIL)
                    Session["logstatus"] = 450;


                    // REDIRECT LOGIN.ASPX
                    Response.Redirect("Login.aspx");
                    Response.End();
                }
            }
        }
    }
}