using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

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
            // IF POST IS FIRST TIME LOADING
            if (!Page.IsPostBack)
            {
                if (Session["DayOffset"] == null)
                    Session["DayOffset"] = 0;
                if (Session["UserOffset"] == null)
                    Session["UserOffset"] = 0;
                if (Session["GiftCardOffset"] == null)
                    Session["GiftCardOffset"] = 0;
                if (Session["AppointmentOffset"] == null)
                    Session["AppointmentOffset"] = 0;
            }

            // CREATE SCHEDULE
            updateSchedule();

            // SET USERS ON PANEL
            UsersSetOnPanel();


            ReceiptsSetOnPanel();
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
                daysValue[i + 1] = $"{(DayOfWeek)i} {Time.Next((DayOfWeek)i, (int)Session["DayOffset"])}";
            
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
        // A private function which sets the users on a panel
        // INPUT: none
        // OUTPUT: void

        private void UsersSetOnPanel()
        {
            // CLEAR THE PANEL
            usersArea.Controls.Clear();

            // GET THE OFFSET FROM SESSION
            int userOffset = (int)Session["UserOffset"];

            // CHECK IF OFFSET IS LARGER THEN THE NUMBER OF USERS IN DB
            if (VR_Web_Project.User.CountForId() < userOffset)
                userOffset = 0;

            // DECLARE DATATABLE Appointments
            DataTable dt = VR_Web_Project.User.GetUsers(userOffset);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];

                // DECLARE userid AS THE USER ID FROM DB
                string userid = ((int)dr[0]).ToString();

                // BUILD A STRING AS INNERTEXT FOR TEXT ELEMENT
                string text;
                text = " ID: " + userid;
                text += " | שם משתמש: " + (string)dr[1];
                text += " | מספר טלפון: " + (string)dr[3];
                text += " | מנהל?: " + ((bool)dr[4] == false ? "לא" :"כן")  ;

                // CREATE TEXT ELEMENT USING THE FORMER STRING
                var a = new HtmlGenericControl("a") { InnerText = text };

                // CREATE DIV
                var div = new HtmlGenericControl("div");
                div.Attributes.Add("class", "hiddenbuttons");

                // CREATE DELETE USER BUTTON
                Button delete = new Button();
                delete.CommandArgument = userid;
                delete.Click += new EventHandler(Delete_Click);
                delete.Text = "מחק";
                div.Controls.Add(delete);

                // CREATE TOGGLE PERMISSIONS BUTTON
                Button toggle = new Button();
                toggle.CommandArgument = userid;
                toggle.Click += new EventHandler(TogglePerms_Click);
                toggle.Text = "החלף גישות";
                div.Controls.Add(toggle);

                // CREATE RESET PASSWORD BUTTON
                Button reset = new Button();
                reset.CommandArgument = userid;
                reset.Click += new EventHandler(ResetPass_Click);
                reset.Text = "אפס סיסמה";
                div.Controls.Add(reset);


                // CREATE WRAPPER
                var userDiv = new HtmlGenericControl("div");
                userDiv.Attributes.Add("class", "userWrapper");

                // ADD ELEMENTS TO DIV
                userDiv.Controls.Add(a);
                userDiv.Controls.Add(div);
                    
                // ADD DIV TO PAGE
                usersArea.Controls.Add(userDiv);
            }
        }

        private void ReceiptsSetOnPanel()
        {
            // CLEAR THE PANEL
            giftCardArea.Controls.Clear();
            appointmentsArea.Controls.Clear();

            // GET THE OFFSET FROM SESSION
            int giftCardOffset = (int)Session["GiftCardOffset"];
            int appointmentOffset = (int)Session["AppointmentOffset"];

            // CHECK IF OFFSET IS LARGER THEN THE NUMBER OF GIFTCARDS IN DB
            if (Receipt.CountForId("GiftCardReceipt") < giftCardOffset)
                giftCardOffset = 0;

            // CHECK IF OFFSET IS LARGER THEN THE NUMBER OF GIFTCARDS IN DB
            if (Receipt.CountForId("AppointmentReceipt") < appointmentOffset)
                appointmentOffset = 0;

            // DECLARE DATATABLE Appointments
            DataTable dt = Receipt.GetReceipts(giftCardOffset, "GiftCardReceipt");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];

                // DECLARE userid AS THE USER ID FROM DB
                string userid = ((int)dr[0]).ToString();

                // BUILD A STRING AS INNERTEXT FOR TEXT ELEMENT
                string text;
                text = " ID: " + userid;
                text += " | תאריך: " + (DateTime)dr[1];
                text += " | מחיר: " + (int)dr[2];
                text += " | מס' כרטיס: " + (string)dr[3];

                // CREATE TEXT ELEMENT USING THE FORMER STRING
                var a = new HtmlGenericControl("a") { InnerText = text };

                // CREATE WRAPPER
                var userDiv = new HtmlGenericControl("div");
                userDiv.Attributes.Add("class", "userWrapper");

                // ADD ELEMENTS TO DIV
                userDiv.Controls.Add(a);

                // ADD DIV TO PAGE
                giftCardArea.Controls.Add(userDiv);
            }

            dt = Receipt.GetReceipts(appointmentOffset, "AppointmentReceipt");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];

                // DECLARE userid AS THE USER ID FROM DB
                string userid = ((int)dr[0]).ToString();

                // BUILD A STRING AS INNERTEXT FOR TEXT ELEMENT
                string text;
                text = " ID: " + userid;
                text += " | תאריך: " + (DateTime)dr[1];
                text += " | מחיר: " + (int)dr[3];
                text += " | מס' כרטיס: " + (string)dr[4];

                // CREATE TEXT ELEMENT USING THE FORMER STRING
                var a = new HtmlGenericControl("a") { InnerText = text };

                // CREATE WRAPPER
                var userDiv = new HtmlGenericControl("div");
                userDiv.Attributes.Add("class", "userWrapper");

                // ADD ELEMENTS TO DIV
                userDiv.Controls.Add(a);

                // ADD DIV TO PAGE
                appointmentsArea.Controls.Add(userDiv);
            }

        }

        // TOGGLE PERMS BUTTON CLICK
        protected void TogglePerms_Click(object sender, EventArgs e)
        {
            // GET BUTTON AS VARIABLE
            Button button = (Button)sender;
            // EXTRACT THE ARGUMENT FROM THE BUTTON AND SET IT AS userid
            string userid = button.CommandArgument;

            // CHECK WHETHER THE USER IS MANAGER OR NOT
            bool newValue = !VR_Web_Project.User.GetManager(userid);
            // AND SET IT'S NEW "MANAGER VALUE" AS THE OPPOSITE
            VR_Web_Project.User.SetManager(userid, newValue);
            
            // REDIRECT BACK TO MANAGER
            Response.Redirect("Manager.aspx");
            Response.End();
        }

        // RESET PASS BUTTON CLICK
        protected void ResetPass_Click(object sender, EventArgs e)
        {
            // GET BUTTON AS VARIABLE
            Button button = (Button)sender;
            // EXTRACT THE ARGUMENT FROM THE BUTTON AND SET IT AS userid
            string userid = button.CommandArgument;

            // GET USERNAME USING THE ID
            string username = VR_Web_Project.User.GetUsername(userid);
            // RESET THE USER's PASSWORD
            VR_Web_Project.User.ChangePassword(username, id: userid);

            // REDIRECT TO MANAGER
            Response.Redirect("Manager.aspx");
            Response.End();
        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            // GET BUTTON AS VARIABLE
            Button button = (Button)sender;
            // EXTRACT THE ARGUMENT FROM THE BUTTON AND SET IT AS userid
            string userid = button.CommandArgument;

            // DELETE THE USER USING THE ID
            VR_Web_Project.User.Delete(int.Parse(userid));

            // REDIRECT TO MANAGER
            Response.Redirect("Manager.aspx");
            Response.End();
        }

        protected void Calendar_Click(object sender, EventArgs e)
        {
            // GET BUTTON AS OBJECT
            Button btn = sender as Button;

            // DECLARE PARAMETER FROM THE BUTTON CUSTOM PARAMETER
            int parameter = int.Parse(btn.Attributes["CustomParameter"].ToString());


            if (parameter != 0)
                updateSchedule(parameter);
            else
            {
                Session["DayOffset"] = 0;
                updateSchedule(0);
            }

            // REDIRECT
            Response.Redirect("Manager.aspx");
            Response.End(); 
        }

        protected void Users_Click(object sender, EventArgs e)
        {
            // GET BUTTON AS OBJECT
            Button btn = sender as Button;

            // DECLARE PARAMETER FROM THE BUTTON CUSTOM PARAMETER
            int parameter = int.Parse(btn.Attributes["CustomParameter"].ToString());

            // GET OFFSET FROM SESSION
            int offset = (int)Session["UserOffset"];

            if (parameter == -1){
                if (offset >= 10)
                    offset -= 10;
            }
            else{
                if (VR_Web_Project.User.CountForId() > offset + 10)
                    offset += 10;
            }

            // SET SESSION AS NEW OFFSET
            Session["UserOffset"] = offset;

            // REDIRECT
            Response.Redirect("Manager.aspx");
            Response.End();
        }

        protected void GiftCard_Click(object sender, EventArgs e)
        {
            // GET BUTTON AS OBJECT
            Button btn = sender as Button;

            // DECLARE PARAMETER FROM THE BUTTON CUSTOM PARAMETER
            int parameter = int.Parse(btn.Attributes["CustomParameter"].ToString());

            // GET OFFSET FROM SESSION
            int offset = (int)Session["GiftCardOffset"];

            if (parameter == -1)
            {
                if (offset >= 10)
                    offset -= 10;
            }
            else
            {
                if (Receipt.CountForId("GiftCardReceipt") > offset + 10)
                    offset += 10;
            }

            // SET SESSION AS NEW OFFSET
            Session["GiftCardOffset"] = offset;

            // REDIRECT TO MANAGER
            Response.Redirect("Manager.aspx");
            Response.End();
        }
    }
}