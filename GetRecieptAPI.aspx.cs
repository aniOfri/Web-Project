using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

namespace VR_Web_Project
{
    public partial class GetRecieptAPI : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            string appointmentId = Request.Form["AppointmentId"];
            Dictionary<string, string> dic = Receipt.BuildReceiptString(appointmentId);

            string json = JsonConvert.SerializeObject(dic);
            Response.Write(json);
        }
    }
}