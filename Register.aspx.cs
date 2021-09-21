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
            if (Session["status"] != null)
            {
                // IF SO, UPDATE THE GLOBAL VARIABLE TO DISPLAY THE STATUS
                string status = Session["status"].ToString();
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
                    Session["status"] = "405";
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
                        Session["status"] = "200";
                        Response.Redirect("Register.aspx");
                    }
                    else
                    {
                        // FAIL
                        Session["status"] = "405";
                        Response.Redirect("Register.aspx");
                    }
                }
            }
        }
    }
}