using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace VR_Web_Project
{
    public partial class Appointments : System.Web.UI.Page
    {
        //Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Ofri\source\repos\Web-Project\App_Data\VirtuariaDB.mdf;Integrated Security=True
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack){
                Session["Day"] = null;
                Session["Partic"] = null;
                Session["Time"] = null;

                SetDateTime();

                CreateSchedule();

                //setOnGrid();

                UpdateTimes(-1);
            }
        }

        // Set the values of the next dates relative to today
        private void SetDateTime()
        {
            DateTime today = DateTime.Today;
            for (int i = 1; i < 8; i++)
            {
                Button btn = Master.FindControl("TitlePlaceHolder").FindControl("date"+i) as Button;
                btn.Text = today.AddDays(i).ToShortDateString();
            }
        }

        // Create a schedule using the SQL DB
        private void CreateSchedule()
        {
            Session["Schedule"] = Appointment.CreateSchedule();
        }

        // Update time values for the time ddl
        private void UpdateTimes(int day)
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
        private bool AllowToProceed()
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

            if (AllowToProceed())
                label3.CssClass = "span3";
        }
        // Button press for date-related objects
        protected void DayOrder(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            label2.Text = "תאריך נבחר: " + btn.Text;
            Session["Day"] = btn.Text;

            if (AllowToProceed())
                label3.CssClass = "span3";

            label4.Text = "הזמנת זמן";
            Session["Time"] = null;

            DateTime date = Convert.ToDateTime(btn.Text);
            UpdateTimes((int)date.DayOfWeek);
        }

        // Button press for time-related objects
        protected void TimeOrder(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn.Text != "לא זמין")
            {
                label4.Text = "זמן נבחר: " + btn.Text;
                Session["Time"] = btn.Text;

                if (AllowToProceed())
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