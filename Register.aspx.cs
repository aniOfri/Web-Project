using System;
using DALLib;

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
                Session["regstatus"] = null;
            }

            // CHECKS IF THE SITE HAS BEEN RELOADED DUE TO A SUBMIT PRESS
            if (Request["name"] != null)
            {

                // CHECK IF SUBMIT WAS ACTUALLY "CANCEL ORDER"
                if (Request["name"] == "")
                {
                    Response.Redirect("Logout.aspx");
                    Response.Redirect("Home.aspx");
                    Response.End();
                }
                // DECLARE THREE STRINGS AS USERNAME, PHONENUMBER AND PASSWORD USING THE SUBMIT DATA
                string username = Request["name"].Replace(" ", "");
                string phone = Request["phone"].Replace(" ", "");
                string password = Request["password"].Replace(" ", "");

                // BUILD STRING AS AN SQL COMMAND
                string selectQuery = "SELECT * FROM Member";
                selectQuery += " WHERE ";
                string selectQueryPhone = selectQuery + " PhoneNumber ='" + phone + "'";
                string selectQueryUsername = selectQuery + " Username = N'" + username + "'";
                
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

                            // REDIRECT PAYMENT.ASPX
                            Session["User"] = user;
                            Session["Manager"] = user.IsManager;
                            Session["RedirectOrder"] = appointment;
                            Response.Redirect("Payment.aspx");
                        }
                        else
                            Response.Redirect("Login.aspx");
                        Response.End();
                    }
                    else
                    {
                        // FAIL
                        Session["regstatus"] = "405";
                        Response.Redirect("Register.aspx");
                        Response.End();
                    }
                }
            }
        }
    }
}