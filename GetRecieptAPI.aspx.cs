using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace VR_Web_Project
{
    public partial class GetRecieptAPI : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            // GET RECEIPT ID FROM FORM
            string receiptId = Request.Form["ReceiptID"];

            // BUILD A DICTIONARY USING THE BUILDRECEIPTDICTIONARY FUNCTION FOR EASIER DATA TRANSFER
            Dictionary<string, string> dic = AppointmentReceipt.BuildReceiptDictionary(receiptId);

            // JSONIFY
            string json = JsonConvert.SerializeObject(dic);

            // RETURN THE JSON
            Response.Write(json);
        }
    }
}