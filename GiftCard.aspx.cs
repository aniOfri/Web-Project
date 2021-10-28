using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VR_Web_Project
{
    public partial class GiftCard1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["GiftCardCode"] != null)
            {
                System.Diagnostics.Debug.WriteLine("IN");
                string code = (string)Session["GiftCardCode"];
                popupInfo.Text = code;
                myModal.Attributes["class"] = "modal visible";
                Session["GiftCardCode"] = null;
            }
            if (Request["giftCard"] != null)
            {
                int giftCardPrice = int.Parse(Request["giftCard"].ToString().Replace("₪", ""));
                GiftCard giftCard = new GiftCard(price2: giftCardPrice);

                // REDIRECT HOME.ASPX
                Session["RedirectOrder"] = giftCard;
                Response.Redirect("Payment.aspx");
                Response.End();
            }
        }
    }
}