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
            if (Session["User"] == null)
            {
                Response.Redirect("Login.aspx");
                Response.End();
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            User user = (User)Session["User"];

            username.Text = "שלום, "+ user.Username.ToString();

            // CHECKS IF STATUS SESSION IS NOT NULL
            if (Session["status"] != null)
            {
                // IF SO, UPDATE THE GLOBAL VARIABLE TO DISPLAY THE STATUS
                string status = Session["status"].ToString();
                if (status == "200")
                    PasswordChangeLog = "הסיסמה שונתה בהצלחה";
                else if (status == "450") PasswordChangeLog = "קרתה תקלה במערכת";
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
                    if (user.ChangePassword(newPass))
                        Session["status"] = 200;
                    else Session["status"] = 450;
                }
                else
                    // UPDATE THE GLOBAL VARIABLE (FAIL)
                    Session["status"] = "fail";

                Response.Redirect("Profile.aspx");
                Response.End();
            }
        }
    }
}