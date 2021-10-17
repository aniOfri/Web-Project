using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace VR_Web_Project
{
    public partial class Appointments : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                // GET SCHEDULE FROM THE APPOINTMENT CLASS
                Session["Schedule"] = Appointment.CreateSchedule();
                
                // RESET DATA SESSIONS
                Session["Day"] = null;
                Session["Partic"] = null;
                Session["Time"] = null;

                // UPDATE THE WEBSITE
                SetDateTime();
                UpdateTimes(-1);
            }
        }

        // A private function which sets the values of the next dates relative to today on the dropdownlist
        // INPUT: none
        // OUTPUT: void
        private void SetDateTime()
        {
            // GET TODAYS DATE
            DateTime today = DateTime.Today;

            // FOR LOOPS OVER EVERY BUTTON ON THE DROP DOWN LIST AND UPDATES TO VALUES
            for (int i = 0; i < 7; i++)
            {
                Button btn = Master.FindControl("TitlePlaceHolder").FindControl("date"+(i+1).ToString()) as Button;
                btn.Text = today.AddDays(i).ToShortDateString();
            }
        }

        // A private function which updates time values for the "time" dropdownlist
        // INPUT: int as day
        // OUTPUT: void
        private void UpdateTimes(int day)
        {
            // GET SCHEDULE week FROM SESSION
            string[][] week = (string[][])Session["Schedule"];
            string[] times = new string[14];
            
            // FILL THE DROPDOWNLIST WITH THE TIMES
            Time time = new Time(8, 0);
            for (int i = 0; i < 14; i++)
            {
                times[i] = time.GetTime();
                time.AddTime(1, 15);
            }
            
            // SET AS UNAVAILABLE THE OCCUPIED APPOINTMENT TIMES
            if (day != -1)
            {
                for (int i = 0; i < 13; i++)
                {
                    if (week[day + 1][i] != "")
                        times[i] = "לא זמין";
                }
            }

            // FOR LOOPS OVER EVERY BUTTON ON THE DROP DOWN LIST AND UPDATES TO VALUES
            for (int i = 0; i < 14; i++)
            {
                Button l = Master.FindControl("TitlePlaceHolder").FindControl("time" + (i + 1)) as Button;
                l.Text = times[i];
            }
        }

        // A private functions which checks if all paramaters (time, date, and partc.) has been chosen
        // INPUT: none
        // OUTPUT: bool as an allowance to proceed
        private bool AllowToProceed()
        {
            return Session["Partic"] != null 
                && Session["Day"] != null
                && Session["Time"] != null;
        }

        // A Button press function for participants-related objects
        protected void ParticipantsOrder(object sender, EventArgs e)
        {
            // GET BUTTON AS OBJECT
            Button btn = sender as Button;
            
            // DECLARE PARAMETER FROM THE BUTTON CUSTOM PARAMETER
            int parameter = int.Parse(btn.Attributes["CustomParameter"].ToString());

            // DECLARE AN ARRAY FOR EASY TRANSITION BETWEEN INT AND TEXT
            string[] inText = { "אחד", "זוג", "שלישייה", "רביעייה", "חמישייה", "שישייה" };

            // UPDATE LABELS AND SESSION VALUES        
            label1.Text = "מספר משתתפים נבחר: " + inText[parameter];
            Session["Partic"] = parameter + 1;

            // IF ALL VALUES BEEN CHOSEN, DISPLAY THE PROCEED BUTTON
            if (AllowToProceed())
                label3.CssClass = "span3";
        }
        // A Button press function for date-related objects
        protected void DayOrder(object sender, EventArgs e)
        {
            // GET BUTTON AS OBJECT
            Button btn = sender as Button;

            // UPDATE LABELS AND SESSION VALUES         
            label2.Text = "תאריך נבחר: " + btn.Text;
            Session["Day"] = btn.Text;

            // IF ALL VALUES BEEN CHOSEN, DISPLAY THE PROCEED BUTTON
            if (AllowToProceed())
                label3.CssClass = "span3";

            // RESET THE TIME CHOSEN VALUES
            label4.Text = "הזמנת זמן";
            Session["Time"] = null;

            // UPDATE THE TIME DROPDOWNLIST FOR THE NEW CHOSEN DAY
            DateTime date = Convert.ToDateTime(btn.Text);
            UpdateTimes((int)date.DayOfWeek);
        }

        // A Button press function for time-related objects
        protected void TimeOrder(object sender, EventArgs e)
        {
            // GET BUTTON AS OBJECT
            Button btn = sender as Button;

            // IF TIME IS AVAILABLE
            if (btn.Text != "לא זמין")
            {
                // UPDATE LABELS AND SESSION VALUES
                label4.Text = "זמן נבחר: " + btn.Text;
                Session["Time"] = btn.Text;

                // IF ALL VALUES BEEN CHOSEN, DISPLAY THE PROCEED BUTTON
                if (AllowToProceed())
                    label3.CssClass = "span3";
            }
        }

        // A Button press function which orders the appointment
        protected void Order(object sender, EventArgs e)
        {
            // CREATE AN APPOINTMENT CLASS WITHOUT THE PHONE NUMBER VARIABLE
            DateTime dateTime = DateTime.Parse
                ($"{(string)Session["Day"]} {(string)Session["Time"]}");
            int participants = (int)Session["Partic"];

            Appointment appointment = new Appointment
                ("", dateTime, participants);

            // CHECKS IF THE USER IS LOGGED IN 
            if (Session["User"] != null)
            {
                // REDIRECT HOME.ASPX
                Session["RedirectOrder"] = appointment;
                Response.Redirect("Payment.aspx");
                Response.End();
            }
            else
            {
                /* IF USER IS NOT LOGGED IN, THE SITE WILL REDIRECT
                    TO THE LOGIN/REGISTER SITE AND THE REST OF THE OPERATION WILL BE DONE THERE*/
                Session["RedirectOrder"] = appointment;
                Response.Redirect("Login.aspx");
                Response.End();
            }
        }
    }
}