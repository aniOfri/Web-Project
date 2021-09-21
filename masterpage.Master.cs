using System;
using System.Web;

namespace VR_Web_Project
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // CHECKS IF THE USER IS LOGGED IN
            if (Session["User"] != null)
            {
                //  IF SO, GET CURRENT URL
                string currentUrl = HttpContext.Current.Request.Url.LocalPath;

                /* AND IF THE PAGE IS NOT CURRENTLY A PROFILE PAGE
                    TRANSFORM THE LOGIN BUTTON TO A PROFILE BUTTON */
                if (!currentUrl.EndsWith("Profile.aspx")) 
                { 
                    loginNav.HRef = "Profile.aspx";
                    loginNav.InnerHtml = "פרופיל";
                }
                else
                {
                    loginNav.Style["display"] = "none";
                }

                // LENGTHEN THE NAVBAR
                headerNav.Style["width"] = "75%";
                
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