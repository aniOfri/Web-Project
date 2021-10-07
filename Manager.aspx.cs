using System;
using System.Data;
using System.Web.UI.HtmlControls;

namespace VR_Web_Project
{
    public partial class Manager : System.Web.UI.Page
    {
        // THE FOLLOWING CODE RUNS BEFORE THE PAGE EVEN LOADS
        protected void Page_Init(object sender, EventArgs e)
        {
            // CHECKS IF USER IS LOGGED IN
            if (Session["User"] != null)
            {
                // IF SO, CHECKS IF THE USER IS NOT A MANAGER
                if (!(bool)Session["Manager"])
                {
                    // IF SO, REDIRECTS THE USER TO HOME
                    Response.Redirect("Home.aspx");
                    Response.End();
                }
            }
            else
            {
                // REDIRECTS THE USER TO HOME
                Response.Redirect("Home.aspx");
                Response.End();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Session["DayOffset"] = 0;
                Session["UserOffset"] = 0;
            }

            // CREATE SCHEDULE
            updateSchedule();

            // SET USERS ON PANEL
            SetOnPanel();
        }

        private void updateSchedule(int increment = 0)
        {
            // UPDATE GLOBAL OFFSET
            int offset = (int)Session["DayOffset"];
            offset += increment;
            Session["DayOffset"] = offset;

            // GET SCHEDULE FROM THE APPOINTMENT CLASS
            Session["Schedule"] = Appointment.CreateSchedule(offset);
            
            // SET THE SCHEDULE ON A DATAGRID
            SetOnGrid();
        }

        // A private function which returns the date of the next /weekday/ parameter
        // INPUT: DayOfWeek as a day of the week
        // OUTPUT: string as a the next date of the former day of the week
        private string Next(DayOfWeek dayOfWeek, int offset)
        {
            // DECLARE from AS TODAY
            DateTime from = DateTime.Today;

            // CHECKS IF from IS NOT THE GIVEN PARAMETER
            if (from.DayOfWeek != dayOfWeek)
            {
                // IF SO, DECLARE start AS TODAY AND target AS THE PARAMETER BOTH CASTED TO INT 
                int start = (int)from.DayOfWeek;
                int target = (int)dayOfWeek;

                // IF THE targets DAY HAPPENS BEFORE TODAY IN THE WEEK ADD 7 TO THE target
                if (target <= start)
                    target += 7;

                // ADD TO form THE DIFFERENCE BETWEEN target AND start
                from = from.AddDays(target - start);
            }

            // APPLY OFFSET
            from = from.AddDays(offset);

            // RETURN THE DATE AS STRING IN PARENTHESIS
            return "(" + from.ToShortDateString() + ")";
        }

        // A private function which sets the schedule on a datagrid
        // INPUT: none
        // OUTPUT: void
        private void SetOnGrid()
        {
            // GET SCHEDULE week FROM SESSION
            string[][] week = (string[][])Session["Schedule"];

            string[] daysValue = new string[8];
            // ADD THE DAYS AND DATES TO THE daysValue BY CASTING AN INTEGER TO A "DayOfWeek" ENUM
            daysValue[0] = "DAYS/TIMES";
            for (int i = 0; i < 7; i++)
                daysValue[i + 1] = $"{(DayOfWeek)i} {Next((DayOfWeek)i, (int)Session["DayOffset"])}";
            
            // DECLARE DATATABLE Appointments
            DataTable dt = new DataTable("Appointments");

            // ADD DAYS COLUMNS
            for (int i = 0; i < 8; i++) {
                dt.Columns.Add(new DataColumn(daysValue[i]));
            }

            // TRANSFER THE week 2DARRAY TO THE DATATABLE dt
            for (int i = 0; i < 14; i++)
            {
                DataRow dr = dt.NewRow();
                for (int j = 0; j < 8; j++)
                {
                    dr[daysValue[j]] = week[j][i];
                }
                dt.Rows.Add(dr);
            }

            // SET grid DATASOURCE AND BIND
            grid.DataSource = dt;
            grid.DataBind();
        }

        private void SetOnPanel()
        {
            int userOffset = (int)Session["UserOffset"];

            if (VR_Web_Project.User.CountForId() < userOffset)
                userOffset = 0;

            // DECLARE DATATABLE Appointments
            DataTable dt = VR_Web_Project.User.GetUsers(userOffset);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];

                string text;
                text = " ID: " + ((int)dr[0]).ToString();
                text = " שם משתמש: " + (string)dr[1];
                text += " מספר טלפון: " + (string)dr[3];
                text += " מנהל?: " + ((bool)dr[4]).ToString();

                var a = new HtmlGenericControl("a") { InnerText = text };
                usersArea.Controls.Add(a);
            }
        }

        protected void Back_Click(object sender, EventArgs e)
        {
            updateSchedule(-7);
        }

        protected void Next_Click(object sender, EventArgs e)
        {
            updateSchedule(7);
        }

        protected void Back_Click2(object sender, EventArgs e)
        {
            int offset = (int)Session["UserOffset"];
            if (offset > 10)
            {
                offset -= 10;
                Session["UserOffset"] = offset;
            }
        }

        protected void Next_Click2(object sender, EventArgs e)
        {
            int offset = (int)Session["UserOffset"];

            if (VR_Web_Project.User.CountForId() > offset + 10)
            {
                offset += 10;
                Session["UserOffset"] = offset;
            }
        }
    }
}