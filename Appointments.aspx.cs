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
                Session["chooseDay"] = false;
                Session["choosePartic"] = false;
                Session["chooseTime"] = false;

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
                Button l = (Button)Page.FindControl("date" + i);
                l.Text = today.AddDays(i).ToShortDateString();
            }

            /*date1.Text = today.AddDays(1).ToShortDateString();
            date2.Text = today.AddDays(2).ToShortDateString();
            date3.Text = today.AddDays(3).ToShortDateString();
            date4.Text = today.AddDays(4).ToShortDateString();
            date5.Text = today.AddDays(5).ToShortDateString();
            date6.Text = today.AddDays(6).ToShortDateString();
            date7.Text = today.AddDays(7).ToShortDateString();*/
        }

        // Create a schedule using the SQL DB
        private void createSchedule()
        {
            DAL DAL = new DAL();

            string[][] week = new string[8][];
            week[0] = new string[14] { "08:00", "09:15", "10:30", "11:45", "12:00", "13:15", "14:30", "15:45", "16:00", "17:15", "18:30", "19:45", "20:00", "21:15"};
            for (int i = 1; i < 8; i++)
            {
                week[i] = new string[14] { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0"};
            }

            string cmdString = "SELECT * FROM Appointment";
            SqlDataReader reader = DAL.GetReader(cmdString);
            while (reader.Read())
            {
                DateTime date = (DateTime)reader["DateTime"];
                int days = (date - DateTime.Today).Days + 1 + (int)DateTime.Today.DayOfWeek;
                if (days > 7) days = days - 7;
                int hours = (date - DateTime.Today).Hours - 8;
                if (days > 0)
                {
                    week[days][hours] = ((int)reader["Participants"]).ToString();
                }
            }
            reader.Close();

            Session["Schedule"] = week;
        }
        private void setOnGrid()
        {
            string[][] week = (string[][])Session["Schedule"];

            string[] daysValue = { "DAYS/TIMES", "Sunday " + Next(DayOfWeek.Sunday),
                                        "Monday " + Next(DayOfWeek.Monday),
                                        "Tuesday" + Next(DayOfWeek.Tuesday),
                                        "Wednesday" + Next(DayOfWeek.Wednesday),
                                        "Thursday" + Next(DayOfWeek.Thursday),
                                        "Friday" + Next(DayOfWeek.Friday),
                                        "Saturday" + Next(DayOfWeek.Saturday) };

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

            return "\n (" + from.ToShortDateString() + ")";
        }
        // Update time values for the time ddl
        private void updateTimes(int day)
        {
            string[][] week = (string[][])Session["Schedule"];

            string[] times = new string[14] { "08:00", "09:15", "10:30", "11:45", "12:00", "13:15", "14:30", "15:45", "16:00", "17:15", "18:30", "19:45", "20:00", "21:15"};

            if (day != -1)
            {
                for (int i = 0; i < 13; i++)
                {
                    if (week[day + 1][i] != "0")
                    {
                        times[i] = "לא זמין";
                    }
                }
            }

            for (int i = 0; i < 14; i++)
            {
                Label l = Page.FindControl("label" + (i + 1)) as Label;
                l.Text = times[i];
            }

            /*time1.Text = times[0];
            time2.Text = times[1];
            time3.Text = times[2];
            time4.Text = times[3];
            time5.Text = times[4];
            time6.Text = times[5];
            time7.Text = times[6];
            time8.Text = times[7];
            time9.Text = times[8];
            time10.Text = times[9];
            time11.Text = times[10];
            time12.Text = times[11];
            time13.Text = times[12];
            time14.Text = times[13];*/
        }

        // Button press for participants-related objects
        protected void ParticipantsOrder(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            label1.Text = "מספר משתתפים נבחר: " + btn.Attributes["CustomParameter"].ToString();
            Session["choosePartic"] = true;

            if ((bool)Session["choosePartic"] && (bool)Session["chooseDay"] && (bool)Session["chooseTime"])
            {
                label3.CssClass = "span3";
            }
        }
        // Button press for date-related objects
        protected void DayOrder(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            label2.Text = "תאריך נבחר: " + btn.Text;
            Session["chooseDay"] = true;

            if ((bool)Session["choosePartic"] && (bool)Session["chooseDay"] && (bool)Session["chooseTime"])
            {
                label3.CssClass = "span3";
            }

            label4.Text = "הזמנת זמן";
            Session["chooseTime"] = false;

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
                Session["chooseTime"] = true;

                if ((bool)Session["choosePartic"] && (bool)Session["chooseDay"] && (bool)Session["chooseTime"])
                {
                    label3.CssClass = "span3";
                }
            }
        }
        protected void Nextpage(object sender, EventArgs e)
        {

        }
    }
}