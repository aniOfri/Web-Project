using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VR_Web_Project
{
    public partial class Manager : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["User"] != null)
            {
                if (!(bool)Session["Manager"])
                {
                    Response.Redirect("Home.aspx");
                    Response.End();
                }
            }
            else
            {
                Response.Redirect("Home.aspx");
                Response.End();
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Schedule"] = Appointment.createSchedule();
            setOnGrid();
        }

        // Get the date of the next /weekday/
        private string Next(DayOfWeek dayOfWeek)
        {
            DateTime from = DateTime.Today;
            if (from.DayOfWeek != dayOfWeek)
            {
                int start = (int)from.DayOfWeek;
                int target = (int)dayOfWeek;
                if (target <= start)
                    target += 7;

                from = from.AddDays(target - start);
            }

            return "(" + from.ToShortDateString() + ")";
        }

        private void setOnGrid()
        {
            string[][] week = (string[][])Session["Schedule"];

            string[] daysValue = new string[8];
            // Add the days and dates to the daysValue array by casting an integer to a "DayOfWeek" enum
            daysValue[0] = "DAYS/TIMES";
            for (int i = 0; i < 7; i++)
                daysValue[i + 1] = $"{(DayOfWeek)i} {Next((DayOfWeek)i)}";

            DataTable dt = new DataTable("Appointments");

            for (int i = 0; i < 8; i++)
                dt.Columns.Add(new DataColumn(daysValue[i]));

            for (int i = 0; i < 14; i++)
            {
                DataRow dr = dt.NewRow();
                for (int j = 0; j < 8; j++)
                {
                    dr[daysValue[j]] = week[j][i];
                }
                dt.Rows.Add(dr);
            }

            grid.DataSource = dt;
            grid.DataBind();
        }
    }
}