using System;

namespace VR_Web_Project
{
    public partial class Register : System.Web.UI.Page
    {
        // DECLARE A GLOBAL VARIABLE TO DISPLAY ERROR/SUCCESS MESSAGES ONs
        public string RegStatus = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            // CHECKS IF STATUS SESSION IS NOT NULL
            if (Session["regstatus"] != null)
            {
                // IF SO, UPDATE THE GLOBAL VARIABLE TO DISPLAY THE STATUS
                string status = Session["regstatus"].ToString();
                if (status == "200")
                    RegStatus = "נרשמת בהצלחה";
                else
                    RegStatus = "מספר טלפון או שם משתמש נמצאים בשימוש";
            }

            // CHECKS IF THE SITE HAS BEEN RELOADED DUE TO A SUBMIT PRESS
            if (Request["submit"] != null)
            {
                // DECLARE THREE STRINGS AS USERNAME, PHONENUMBER AND PASSWORD USING THE SUBMIT DATA
                string username = Request["name"].Replace(" ", "");
                string phone = Request["phone"].Replace(" ", "");
                string password = Request["password"].Replace(" ", "");

                // BUILD STRING AS AN SQL COMMAND
                string selectQuery = "SELECT * FROM Member";
                selectQuery += " WHERE ";
                string selectQueryPhone = selectQuery + " PhoneNumber ='" + phone + "'";
                string selectQueryUsername = selectQuery + " Username ='" + username + "'";
                
                // CHECK IF EITHER THE PHONENUMBER OR USERNAME ALREADY IN USE
                if (DAL.IsExist(selectQueryPhone) || DAL.IsExist(selectQueryUsername))
                {   
                    // IF SO, RETURN ERROR 405
                    Session["regstatus"] = "405";
                    Response.Redirect("Register.aspx");
                }
                else
                {
                    // BUT IF PHONENUMBER AND USERNAME ARE FREE, DECLARE NEW USER
                    User user = new User(username, password);
                    user.PhoneNumber = phone;

                    // REGISTER AND REDIRECT TO REGISTER PAGE
                    if (user.Register())
                    {
                        // SUCCESS
                        Session["regstatus"] = "200";

                        // CHECKS IF THE USER HAS BEEN REDIRECTED TO THE LOGIN PAGE AFTER DOING AN ORDER AND THEN DECIDED TO REGISTER
                        if (Session["RedirectOrder"] != null)
                        {
                            // DECLARE AN APPOINTMENT FROM SESSION AND ASSIGN THE USERS PHONE NUMBER TO TO APPOINTMENT
                            Appointment appointment = (Appointment)Session["RedirectOrder"];
                            appointment.UserId = user.Id.ToString();

                            /*

                            WILL ADD A PAYMENT/VERIFICATION METHOD IN THE FUTURE.

                            */

                            // INSERT THE DATA INTO THE DATABASE
                            appointment.Order();

                            // REDIRECT PROFILE.ASPX
                            Response.Redirect("Profile.aspx");
                        }
                        else
                            Response.Redirect("Register.aspx");
                    }
                    else
                    {
                        // FAIL
                        Session["regstatus"] = "405";
                        Response.Redirect("Register.aspx");
                    }
                }
            }
        }
    }
}