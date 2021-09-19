using System;

namespace VR_Web_Project
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            Session["User"] = null;
            Response.Redirect("Home.aspx");
            Response.End();
        }
    }
}