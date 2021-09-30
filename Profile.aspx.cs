using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VR_Web_Project
{
    public partial class Profile : System.Web.UI.Page
    {
        // DECLARE A GLOBAL VARIABLE TO DISPLAY ERROR/SUCCESS MESSAGES ONs
        public string PasswordChangeLog = "";
        protected void Page_Init(object sender, EventArgs e)
        {
            // CHECK IF A USER IS LOGGED IN
            if (Session["User"] == null)
            {
                // IF NOT, REDIRECT TO PROFILE.ASPX
                Response.Redirect("Login.aspx");
                Response.End();
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            // GET THE LOGGED USER AS A USER FROM "USER" SESSION 
            User user = (User)Session["User"];

            // SET THE WELCOMING TEXT AS: "HELLO, [THE USER'S USERNAME]"
            username.Text = "שלום, "+ user.Username.ToString();

            // CHECKS IF STATUS SESSION IS NOT NULL
            if (Session["status"] != null)
            {
                // IF SO, UPDATE THE GLOBAL VARIABLE TO DISPLAY THE STATUS
                string status = Session["status"].ToString();
                // SUCCESS
                if (status == "200")
                    PasswordChangeLog = "הסיסמה שונתה בהצלחה";
                // INTERNAL ERROR SUCH AS DB ERROR
                else if (status == "450") PasswordChangeLog = "קרתה תקלה במערכת";
                // EXTERNAL ERROR SUCH AS WRONG PASSWORD
                else PasswordChangeLog = "הסיסמה הישנה אינה נכונה";
            }

            // CHECKS IF THE SITE HAS BEEN RELOADED DUE TO A SUBMIT PRESS
            if (Request["submit"] != null)
            {
                // DECLARE TWO STRINGS AS USERNAME AND PASSWORD USING THE SUBMIT DATA
                string oldPass = Request["oldPass"].Replace(" ", "");
                string newPass = Request["newPass"].Replace(" ", "");

                // LOGIN AND CONTINUE IF SUCCESS
                if (oldPass == user.Password)
                {
                    // CHANGE PASSWORD OF user TO newPass
                    if (user.ChangePassword(newPass))
                        // SET STATUS SESSION AS SUCCESS
                        Session["status"] = 200;
                    // SET STATUS SESSION TO AN INTERNAL ERROR
                    else Session["status"] = 450;
                }
                else
                    // SET STATUS SESSION TO AN EXTERNAL ERROR
                    Session["status"] = "fail";

                // REDIRECT TO PROFILE.ASPX
                Response.Redirect("Profile.aspx");
                Response.End();
            }
        }
    }
}