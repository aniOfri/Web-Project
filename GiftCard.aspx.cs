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
            if (Request["giftCard"] != null)
            {
                string giftCardPrice = Request["giftCard"].ToString().Replace("₪", "");
                GiftCard giftCard = new GiftCard(giftCardPrice);

                // REDIRECT HOME.ASPX
                Session["RedirectOrder"] = giftCard;
                Response.Redirect("Payment.aspx");
                Response.End();
            }
        }
    }
}