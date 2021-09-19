using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VR_Web_Project
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null)
            {
                loginNav.HRef = "Profile.aspx";
                loginNav.InnerHtml = "פרופיל";

                logoutNav.Style["visibility"] = "visible";
                logoutNav.Style["opacity"] = "1";
                logoutNav.Style["display"] = "inline";

                string sessionUsername = Session["User"].ToString();
                if (sessionUsername == "Manager")
                {
                    managerNav.Style["visibility"] = "visible";
                    managerNav.Style["opacity"] = "1";
                    managerNav.Style["display"] = "inline";
                }
            }
        }
    }
}