using System;
using System.Web;

namespace VR_Web_Project
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        public string type = "";
        public string participants = "";
        public string date = "";
        public string time = "";
        public string worth = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            // CHECK IF AN ORDER EXISTS IN SESSION
            if (Session["RedirectOrder"] != null)
            {
                // IF SO, SET THE GLOBAL VARS AS THE APPOINTMENT VALUES
                Order order = (Order)Session["RedirectOrder"];
                if (order is Appointment) {
                    Appointment appointment = (Appointment)order;
                    type = "הזמנת מקום:";
                    participants = "שחקנים: " + appointment.Participants.ToString();
                    date = "תאריך: " + appointment.Date.ToString("MM/dd/yyyy");
                    time = "זמן: " + appointment.Date.ToString("HH:mm");
                    
                    particLbl.Visible = true;
                    timeLbl.Visible = true;
                    dateLbl.Visible = true;
                    worthLbl.Visible = false;
                }
                else if (order is GiftCard)
                {
                    type = "כרטיס מתנה:";
                    worth = "מחיר: " + order.GetPrice().ToString();

                    particLbl.Visible = false;
                    timeLbl.Visible = false;
                    dateLbl.Visible = false;
                    worthLbl.Visible = true;
                }
                else
                    currentOrder.Attributes["class"] = "nodisplay";
            }
            else
                // IF NOT, DONT DISPLAY THE "CURRENT ORDER"
                currentOrder.Attributes["class"] = "nodisplay";

            string currentUrl = HttpContext.Current.Request.Url.LocalPath;


            if (currentUrl.EndsWith("Login.aspx"))
            {
                form.Attributes["onsubmit"] = "return LogValid();";
                form.Method = "get";
            }

            else if (currentUrl.EndsWith("Register.aspx"))
            {
                form.Attributes["onsubmit"] = "return RegValid();";
                form.Method = "get";
            }

            else if (currentUrl.EndsWith("Payment.aspx"))
            {
                form.Attributes["onsubmit"] = "return PayValid();";
                form.Method = "get";
            }

            // CHECKS IF THE USER IS LOGGED IN
            if (Session["User"] != null)
            {
                /* AND IF THE PAGE IS NOT CURRENTLY A PROFILE PAGE
                    TRANSFORM THE LOGIN BUTTON TO A PROFILE BUTTON */
                if (!currentUrl.EndsWith("Profile.aspx")) 
                {
                    form.Attributes["onsubmit"] = "return passValid();";
                    loginNav.HRef = "Profile.aspx";
                    loginNav.InnerHtml = "פרופיל";
                }
                else
                {
                    loginNav.Style["display"] = "none";
                }
                
                // DISPLAY A LOGOUT BUTTON
                logoutNav.Style["visibility"] = "visible";
                logoutNav.Style["opacity"] = "1";
                logoutNav.Style["display"] = "inline";

                // CHECKS IF THE USER IS A MANAGER
                if ((bool)Session["Manager"])
                {
                    /* AND IF THE PAGE IS NOT CURRENTLY A MANAGER PAGE, 
                            DISPLAY A MANAGER PAGE BUTTON */
                    if (!currentUrl.EndsWith("Manager.aspx"))
                        managerNav.Style["display"] = "inline";
                }
            }
        }
    }
}