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
                        PasswordChangeLog = "הסיסמה שונתה בהצלחה";
                    else PasswordChangeLog = "קרתה תקלה במערכת";

                    // REDIRECT PROFILE.ASPX
                    Response.Redirect("Profile.aspx");
                    Response.End();
                }
                else
                    // UPDATE THE GLOBAL VARIABLE (FAIL)
                    PasswordChangeLog = "הסיסמה הישנה אינה נכונה";
            }
        }
    }
}