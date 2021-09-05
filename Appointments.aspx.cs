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
        protected void Page_Init(object sender, EventArgs e)
        {
            Session["chooseDay"] = false;
            Session["choosePartic"] = false;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            DateTime today = DateTime.Today;
            date1.Text = today.AddDays(1).ToShortDateString();
            date2.Text = today.AddDays(2).ToShortDateString();
            date3.Text = today.AddDays(3).ToShortDateString();
            date4.Text = today.AddDays(4).ToShortDateString();
            date5.Text = today.AddDays(5).ToShortDateString();
            date6.Text = today.AddDays(6).ToShortDateString();
            date7.Text = today.AddDays(7).ToShortDateString();
        }

        protected void ParticipantsOrder(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            label1.Text = "מספר משתתפים נבחר: " + btn.Attributes["CustomParameter"].ToString();
            Session["choosePartc"] = true;

            if ((bool)Session["choosePartic"] && (bool)Session["chooseDay"]) {                 
                label3.Style.Remove("opacity");
            }

        }

        protected void DayOrder(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            label2.Text = "תאריך נבחר: " + btn.Text;
            Session["chooseDay"] = true;

            if ((bool)Session["choosePartic"] && (bool)Session["chooseDay"])
            {
                label3.Style.Remove("opacity");
            }
        }
    }
}