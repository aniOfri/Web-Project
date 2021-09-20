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
                headerNav.Style["width"] = "75%";

                logoutNav.Style["visibility"] = "visible";
                logoutNav.Style["opacity"] = "1";
                logoutNav.Style["display"] = "inline";

                string sessionUsername = Session["User"].ToString();
                if (sessionUsername == "Manager")
                {
                    string currentUrl = HttpContext.Current.Request.Url.LocalPath;
                    if (!currentUrl.EndsWith("Manager.aspx"))
                    {
                        managerNav.Style["display"] = "inline";
                    }
                }
            }
        }
    }
}