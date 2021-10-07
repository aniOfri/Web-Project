using System;

namespace VR_Web_Project
{
    public partial class Logout : System.Web.UI.Page
    {
        // THE FOLLOWING CODE RUNS BEFORE THE PAGE EVEN LOADS 
        protected void Page_Init(object sender, EventArgs e)
        {
            // RESET USER SESSION AND REDIRECT HOME
            Session["User"] = null;
            Session["regstatus"] = null;
            Session["logstatus"] = null;
            Session["passstatus"] = null;

            Response.Redirect("Home.aspx");
            Response.End();
        }
    }
}