using System;

namespace VR_Web_Project
{
    public partial class GiftCard1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // CHECK IF RETURNING TO PAGE AFTER PAYING
            if (Session["GiftCardCode"] != null)
            {
                // DECLARE code VARIABLE AS THE SESSION
                string code = (string)Session["GiftCardCode"];
                
                // CHANGE THE VALUES IN MODAL AND DISPLAY IT
                popupInfo.Text = code;
                myModal.Attributes["class"] = "modal visible";

                // RESET THE SESSION
                Session["GiftCardCode"] = null;
            }

            // CHECK IF A GIFTCARD HAS BEEN SELECTED
            if (Request["giftCard"] != null)
            {
                // SET giftCardPrice AS THE PRICE OF THE SELECTED GIFTCARD
                int giftCardPrice = int.Parse(Request["giftCard"].ToString().Replace("₪", ""));

                // DECLARE giftCard AS A GIFTCARD CLASS
                GiftCard giftCard = new GiftCard(price2: giftCardPrice);

                // REDIRECT TO PAYMENT.ASPX
                Session["RedirectOrder"] = giftCard;
                Response.Redirect("Payment.aspx");
                Response.End();
            }
        }
    }
}