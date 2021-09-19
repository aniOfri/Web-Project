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

                string sessionUsername = Session["User"].ToString();
                if (sessionUsername == "Manager")
                {
                    /*heading.Style["background"] = "#f5316f";
                    background = "background: linear-gradient(to right, #f5316f 50%, #f0004c 50%);";
                    mangLink.Style["display"] = "inline-block";*/
                }
            }
        }
    }
}