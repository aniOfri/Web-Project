using System;

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
            if (Request["CCN"] != null)
            {
                // DECLARE TWO STRINGS AS USERNAME AND PASSWORD USING THE SUBMIT DATA
                string creditCardNumber = Request["CCN"].Replace(" ", "");
                string expirationMonth = Request["month"].Replace(" ", "");
                string expirationYear = Request["year"].Replace(" ", "");
                string firstName = Request["fn"].Replace(" ", "");
                string lastName = Request["ln"].Replace(" ", "");
                string cvv = Request["cvv"].Replace(" ", "");
                string giftCardCode = Request["giftcard"].Replace(" ", "");

                // DECLARE A USER USING THE FORMER STRINGS
                // PRICES ARRAY
                int[] prices = new int[6] { 140, 120, 115, 110, 105, 100 };
                int price = prices[(int)Session["Partic"] - 1] * (int)Session["Partic"];

                GiftCard giftCard = new GiftCard(giftCardCode);
                int receiptId = -1;
                if (!giftCard.IsExpired)
                {
                    price = giftCard.ApplyGiftCard(price);
                    receiptId = giftCard.ReceiptID;
                }

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
                    Order order = (Order)Session["RedirectOrder"];
                    if (order is Appointment)
                    {
                        Appointment appointment = (Appointment)order;
                        appointment.UserId = user.Id.ToString();

                        // CREATE RECEIPT
                        Receipt receipt = new Receipt(receiptId, DateTime.Now, user.Id, price, creditCardNumber.Substring(12), firstName, lastName);
                        appointment.ReceiptId = receipt.Id;

                        // INSERT DATA TO DB
                        receipt.Insert();
                        appointment.Order();
                    }
                    else if (order is GiftCard)
                    {
                        GiftCard giftcard = (GiftCard)order;

                        Receipt receipt = new Receipt(giftCard.ReceiptID, DateTime.Now, -1, giftcard.GetPrice(), creditCardNumber.Substring(12), firstName, lastName);

                       // INSERT DATA TO DB
                        receipt.Insert();
                        GiftCard.Order(giftcard.GetPrice());
                    }
                        



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