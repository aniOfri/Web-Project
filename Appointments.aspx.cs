using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace VR_Web_Project
{
    public partial class appointments : System.Web.UI.Page
    {
        //Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Ofri\source\repos\Web-Project\App_Data\VirtuariaDB.mdf;Integrated Security=True
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack){
                Session["Day"] = null;
                Session["Partic"] = null;
                Session["Time"] = null;

                setDateTime();

                createSchedule();

                //setOnGrid();

                updateTimes(-1);
            }
        }

        // Set the values of the next dates relative to today
        private void setDateTime()
        {
            DateTime today = DateTime.Today;
            for (int i = 1; i < 8; i++)
            {
                Button btn = Master.FindControl("TitlePlaceHolder").FindControl("date"+i) as Button;
                btn.Text = today.AddDays(i).ToShortDateString();
            }
        }

        // Create a schedule using the SQL DB
        private void createSchedule()
        {
            Session["Schedule"] = Appointment.createSchedule();
        }
        private void setOnGrid()
        {
            string[][] week = (string[][])Session["Schedule"];

            string[] daysValue = new string[8];
            // Add the days and dates to the daysValue array by casting an integer to a "DayOfWeek" enum
            daysValue[0] = "DAYS/TIMES";
            for (int i = 0; i < 7; i++)
                daysValue[i+1] = $"{(DayOfWeek)i} {Next((DayOfWeek)i)}";

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
        // Update time values for the time ddl
        private void updateTimes(int day)
        {
            string[][] week = (string[][])Session["Schedule"];

            string[] times = new string[14];
            
            Time time = new Time(8, 0);
            for (int i = 0; i < 14; i++)
            {
                times[i] = time.GetTime();
                time.AddTime(1, 15);
            }
            
            if (day != -1)
            {
                for (int i = 0; i < 13; i++)
                {
                    if (week[day + 1][i] != "0")
                        times[i-1] = "לא זמין";
                }
            }

            for (int i = 0; i < 14; i++)
            {
                Button l = Master.FindControl("TitlePlaceHolder").FindControl("time" + (i + 1)) as Button;
                l.Text = times[i];
            }
        }

        // If all paramaters (time, date, and partc.) has been chosen, the function will return true
        private bool allowToProceed()
        {
            return Session["Partic"] != null 
                && Session["Day"] != null
                && Session["Time"] != null;
        }

        // Button press for participants-related objects
        protected void ParticipantsOrder(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int parameter = int.Parse(btn.Attributes["CustomParameter"].ToString());
            string[] inText = { "אחד", "זוג", "שלישייה", "רביעייה", "חמישייה", "שישייה" };
            
            label1.Text = "מספר משתתפים נבחר: " + inText[parameter];
            Session["Partic"] = parameter + 1;

            if (allowToProceed())
                label3.CssClass = "span3";
        }
        // Button press for date-related objects
        protected void DayOrder(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            label2.Text = "תאריך נבחר: " + btn.Text;
            Session["Day"] = btn.Text;

            if (allowToProceed())
                label3.CssClass = "span3";

            label4.Text = "הזמנת זמן";
            Session["Time"] = null;

            DateTime date = Convert.ToDateTime(btn.Text);
            updateTimes((int)date.DayOfWeek);
        }

        // Button press for time-related objects
        protected void TimeOrder(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn.Text != "לא זמין")
            {
                label4.Text = "זמן נבחר: " + btn.Text;
                Session["Time"] = btn.Text;

                if (allowToProceed())
                    label3.CssClass = "span3";
            }
        }
        protected void Order(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Parse
                ($"{(string)Session["Day"]} {(string)Session["Time"]}");
            int participants = (int)Session["Partic"];

            Appointment appointment = new Appointment
                ("", dateTime, participants);
            if (Session["User"] != null)
            {
                User user = (User)Session["User"];
                appointment.PhoneNumber = user.PhoneNumber;

                appointment.Order();
                Response.Redirect("Home.aspx");
                Response.End();
            }
            else
            {
                Session["RedirectOrder"] = appointment;
                Response.Redirect("Login.aspx");
                Response.End();
            }
        }
    }
}