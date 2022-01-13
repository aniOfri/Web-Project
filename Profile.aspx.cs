using System;
using System.Data;
using System.Web.UI.HtmlControls;

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
            SetOnGrid(user);

            // SET THE WELCOMING TEXT AS: "HELLO, [THE USER'S USERNAME]"
            username.Text = "שלום, "+ user.Username.ToString();

            // CHECKS IF STATUS SESSION IS NOT NULL
            if (Session["passstatus"] != null)
            {
                // IF SO, UPDATE THE GLOBAL VARIABLE TO DISPLAY THE STATUS
                string status = Session["passstatus"].ToString();
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
                if (VR_Web_Project.User.VerifyPassword(oldPass, user.Password))
                {
                    // CHANGE PASSWORD OF user TO newPass
                    if (VR_Web_Project.User.ChangePassword(newPass, user: user))
                        // SET STATUS SESSION AS SUCCESS
                        Session["passstatus"] = 200;
                    // SET STATUS SESSION TO AN INTERNAL ERROR
                    else Session["passstatus"] = 450;
                }
                else
                    // SET STATUS SESSION TO AN EXTERNAL ERROR
                    Session["passstatus"] = "fail";

                // REDIRECT TO PROFILE.ASPX
                Response.Redirect("Profile.aspx");
                Response.End();
            }
        }

        // SET ALKL APPOINTMENT ON GRID
        private void SetOnGrid(User user)
        {
            // DECLARE DATATABLE dt WHICH HAS ALL APPOINTMENTS FROM THE CHOSEN USERS
            DataTable dt = Appointment.GetAppointments(user);

            // LOOP OVER THE DATATABLE
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                // GET ROW
                DataRow dr = dt.Rows[i];

                // GET TIME
                DateTime dateTime = (DateTime)dr[2];

                // BUILD A STRING USING THE DATAROW
                string text;
                text = " תאריך: "+ dateTime.ToString("MM/dd/yyyy");
                text += " זמן: " + dateTime.ToString("HH:mm");
                text += " מספר משתתפים: " + dr[3];

                // DISPLAY THE STRING ONLY IF DATETIME IS AFTER TODAY
                if (dateTime > DateTime.Today)
                {
                    var a = new HtmlGenericControl("a") { InnerText = text };
                    appointmentsArea.Controls.Add(a);
                }
            }
        }
    }
}