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
        public string PayStatus = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            // CHECKS IF STATUS SESSION IS NOT NULL
            if (Session["paystatus"] != null)
            {
                // IF SO, UPDATE THE GLOBAL VARIABLE TO DISPLAY THE STATUS
                string status = Session["paystatus"].ToString();
                if (status == "200")
                    PayStatus = "";
                else if (status == "450") PayStatus = "העסקה נדחתה";
            }

            // CHECKS IF THE SITE HAS BEEN RELOADED DUE TO A SUBMIT PRESS
            if (Request["submit"] != null)
            {
                // DECLARE TWO STRINGS AS USERNAME AND PASSWORD USING THE SUBMIT DATA
                string creditCardNumber = Request["CCN"].Replace(" ", "");
                string expirationMonth = Request["month"].Replace(" ", "");
                string expirationYear = Request["year"].Replace(" ", "");
                string firstName = Request["fn"].Replace(" ", "");
                string lastName = Request["ln"].Replace(" ", "");
                string cvv = Request["cvv"].Replace(" ", "");

                // DECLARE A USER USING THE FORMER STRINGS
                // PRICES ARRAY
                int[] prices = new int[6] { 140, 120, 115, 110, 105, 100 };
                int price = prices[(int)Session["Partic"]-1] * (int)Session["Partic"];

                // WEB SERVICE
                CreditCardWS.CCServiceSoapClient service = new CreditCardWS.CCServiceSoapClient();
                bool verification = service.Charge(price, creditCardNumber, expirationMonth, expirationYear, firstName, lastName, cvv);


                // LOGIN AND CONTINUE IF SUCCESS
                if (verification)
                {
                    // UPDATE THE GLOBAL VARIABLE (SUCCESS)
                    Session["paystatus"] = 200;

                    // GET USER
                    User user = (User)Session["User"];

                    // DECLARE AN APPOINTMENT FROM SESSION AND ASSIGN THE USERS PHONE NUMBER TO TO APPOINTMENT
                    Appointment appointment = (Appointment)Session["RedirectOrder"];
                    appointment.UserId = user.Id.ToString();

                    // CREATE RECEIPT
                    Receipt receipt = new Receipt(DateTime.Now, user.Id, appointment.Id, price, creditCardNumber.Substring(12), firstName, lastName);

                    // INSERT DATA TO DB
                    receipt.Insert();
                    appointment.Order();

                    // REDIRECT PROFILE.ASPX
                    Session["RedirectOrder"] = null;
                    Response.Redirect("Profile.aspx");
                }
                else
                {
                    // UPDATE THE GLOBAL VARIABLE (FAIL)
                    Session["paystatus"] = 450;

                    // REDIRECT Payment.ASPX
                    Response.Redirect("Payment.aspx");
                    Response.End();
                }
            }
        }
    }
}