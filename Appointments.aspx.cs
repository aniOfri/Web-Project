using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

                setDateTime();

                createSchedule();
            }
        }

        private void setDateTime()
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

        private void createSchedule()
        {
            string cmdString = "SELECT * FROM Appointment";
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\VirtuariaDB.mdf;Integrated Security=True";

            string[][] week = new string[8][];
            week[0] = new string[15] { "08:00", "09:15", "10:30", "11:45", "12:00", "13:15", "14:30", "15:45", "16:00", "17:15", "18:30", "19:45", "20:00", "21:15", "22:30" };
            for (int i = 1; i < 8; i++)
            {
                week[i] = new string[15] { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(cmdString, connection);
                connection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    DateTime date = (DateTime)reader["DateTime"];
                    int days = (date - DateTime.Today).Days;
                    int hours = (date - DateTime.Today).Hours - 8;
                    if (days > 0)
                    {
                        week[days][hours] = ((int)reader["Participants"]).ToString();
                    }
                    label1.Text = hours.ToString();
                }

                string[] daysValue = { "DAYS/TIMES", "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
                
                DataTable dt = new DataTable("Appointments");

                dt.Columns.Add(new DataColumn(daysValue[0]));
                dt.Columns.Add(new DataColumn(daysValue[1]));
                dt.Columns.Add(new DataColumn(daysValue[2]));
                dt.Columns.Add(new DataColumn(daysValue[3]));
                dt.Columns.Add(new DataColumn(daysValue[4]));
                dt.Columns.Add(new DataColumn(daysValue[5]));
                dt.Columns.Add(new DataColumn(daysValue[6]));
                dt.Columns.Add(new DataColumn(daysValue[7]));


                for (int i = 0; i < 15; i++)
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


                reader.Close();
            }



            Session["Schedule"] = week;
        }

        protected void ParticipantsOrder(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            label1.Text = "מספר משתתפים נבחר: " + btn.Attributes["CustomParameter"].ToString();
            Session["choosePartic"] = true;

            if ((bool)Session["choosePartic"] && (bool)Session["chooseDay"]) {
                label3.CssClass = "span3";
            }

        }

        protected void DayOrder(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            label2.Text = "תאריך נבחר: " + btn.Text;
            Session["chooseDay"] = true;

            if ((bool)Session["choosePartic"] && (bool)Session["chooseDay"])
            {
                label3.CssClass = "span3";
            }
        }

        protected void Next(object sender, EventArgs e)
        {

        }
    }
}