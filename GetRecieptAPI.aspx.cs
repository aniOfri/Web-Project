using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace VR_Web_Project
{
    public partial class GetRecieptAPI : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            string appointmentId = Request.Form["AppointmentId"];
            Dictionary<string, string> dic = Receipt.BuildReceiptDictionary(appointmentId);

            string json = JsonConvert.SerializeObject(dic);
            Response.Write(json);
        }
    }
}