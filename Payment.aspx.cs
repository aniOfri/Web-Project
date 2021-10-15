using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VR_Web_Project
{
    public partial class Payment : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["RedirectOrder"] == null || Session["User"] == null)
                Response.Redirect("Home.aspx");
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            // CHECKS IF THE SITE HAS BEEN RELOADED DUE TO A SUBMIT PRESS
            if (Request["submit"] != null)
            {
                // DECLARE TWO STRINGS AS USERNAME AND PASSWORD USING THE SUBMIT DATA
                string ccn = Request["CCN"].Replace(" ", "");
                string month = Request["month"].Replace(" ", "");
                string year = Request["year"].Replace(" ", "");
                string firstname = Request["fn"].Replace(" ", "");
                string lastname = Request["ln"].Replace(" ", "");
                string cvv = Request["cvv"].Replace(" ", "");

                // DECLARE A USER USING THE FORMER STRINGS
                // PRICES ARRAY
                int[] prices = new int[6] { 140, 120, 115, 110, 105, 100 };
                int price = prices[(int)Session["Partic"]-1] * (int)Session["Partic"];

                // WEB SERVICE
                CreditCardWS.CCServiceSoapClient service = new CreditCardWS.CCServiceSoapClient();
                bool verification = service.Charge(price, ccn, month, year, firstname, lastname, cvv);


                // LOGIN AND CONTINUE IF SUCCESS
                if (verification)
                {
                    // GET USER
                    User user = (User)Session["User"];

                    // DECLARE AN APPOINTMENT FROM SESSION AND ASSIGN THE USERS PHONE NUMBER TO TO APPOINTMENT
                    Appointment appointment = (Appointment)Session["RedirectOrder"];
                    appointment.UserId = user.Id.ToString();

                    // CREATE RECEIPT
                    Receipt receipt = new Receipt(DateTime.Now, user.Id, appointment.Id, price, ccn.Substring(12));

                    // INSERT DATA TO DB
                    receipt.Insert();
                    appointment.Order();

                    // REDIRECT PROFILE.ASPX
                    Session["RedirectOrder"] = null;
                    Response.Redirect("Profile.aspx");
                }
                else
                {
                    // REDIRECT Payment.ASPX
                    Response.Redirect("Payment.aspx");
                    Response.End();
                }
            }
        }
    }
}