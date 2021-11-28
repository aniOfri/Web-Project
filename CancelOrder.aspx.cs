using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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