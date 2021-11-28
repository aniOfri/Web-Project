using System;

namespace VR_Web_Project
{
    public partial class CancelOrder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["RedirectOrder"] = null;

            Response.Redirect("Home.aspx");
            Response.End();
        }
    }
}