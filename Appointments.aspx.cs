using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VR_Web_Project
{
    public partial class appointments : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void One_Click(object sender, EventArgs e)
        {
            label1.Text = "משתתף אחד";
        }

        protected void Two_Click(object sender, EventArgs e)
        {
            label1.Text = "זוג משתתפים";
        }

        protected void Three_Click(object sender, EventArgs e)
        {
            label1.Text = "שלושה משתתפים";
        }

        protected void Four_Click(object sender, EventArgs e)
        {
            label1.Text = "ארבעה משתתפים";
        }

        protected void Five_Click(object sender, EventArgs e)
        {
            label1.Text = "חמישה משתתפים";
        }
        
        protected void Six_Click(object sender, EventArgs e)
        {
            label1.Text = "שישה משתתפים";
        }
    }
}