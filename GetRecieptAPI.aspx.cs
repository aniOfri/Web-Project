using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VR_Web_Project
{
    public partial class GetRecieptAPI : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            string appointmentId = Request.Form["AppointmentId"];
            string str = Receipt.BuildReceiptString(appointmentId);

           // string json = Json.Encode(str);
            //Response.Write(json);
        }
    }
}