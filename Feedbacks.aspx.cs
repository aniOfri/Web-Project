using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace VR_Web_Project
{
    public partial class Feedbacks : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // IF POST IS FIRST TIME LOADING
            if (!Page.IsPostBack)
            {
                if (Session["FeedbackOffset"] == null)
                    Session["FeedbackOffset"] = 0;
            }

            // CHECK FOR A NEW FEEDBACK - THE USER MUST BE LOGGED IN AND STARS RADIO BUTTON MUST BE SELECTED
            if (Request["stars"] != null && Session["User"] != null)
            {
                // GET RATING FROM STARS 
                int stars = int.Parse(Request["stars"]);

                // GET CONTENT FROM TEXTAREA
                string content = Request["content"];

                // GET THE USERS ID
                User user = (User)Session["User"];
                int userId = user.Id;

                // GET CURRENT DATE
                DateTime now = DateTime.Now;

                // DECLARE A NEW FEEDBACK CLASS USING THE GATHERED DATA
                Feedback feedback = new Feedback(now, userId, stars, content);

                // IF HASNT USER ALREADY FEEDBACKED 
                if (!feedback.Exist(userId))
                {
                    // INSERT THE FEEDBACK TO THE DB
                    if (feedback.Insert())
                        FeedbackStatus = "הפידבאק עבר בהצלחה";
                }
                else
                {
                    // ELSE, UPDATE THE FEEDBACK THE USER HAS ALREADY DONE.
                    if (feedback.Update())
                        FeedbackStatus = "הפידבאק התעדכן בהצלחה";
                }
            } 

            FeedbacksSetOnPanel();
        }
        public string FeedbackStatus = "";

        private void FeedbacksSetOnPanel()
        {
            // CLEAR THE PANEL
            feedBackArea.Controls.Clear();

            // GET THE OFFSET FROM SESSION
            int feedbackOffset = (int)Session["FeedbackOffset"];

            // CHECK IF OFFSET IS LARGER THEN THE NUMBER OF USERS IN DB
            if (Feedback.CountForId() < feedbackOffset)
                feedbackOffset = 0;

            // DECLARE DATATABLE Appointments
            DataTable dt = Feedback.GetFeedbacks(feedbackOffset);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];

                // DECLARE userid AS THE USER ID FROM DB
                int rating = ((int)dr[3]);

                // BUILD A STRING AS INNERTEXT FOR TEXT ELEMENT
                string text = "☆☆☆☆☆";
                switch (rating)
                {
                    case 1:
                        text = "★☆☆☆☆";
                        break;
                    case 2:
                        text = "★★☆☆☆";
                        break;
                    case 3:
                        text = "★★★☆☆";
                        break;
                    case 4:
                        text = "★★★★☆";
                        break;
                    case 5:
                        text = "★★★★★";
                        break;
                }

                // GET USERNAME OF THE FEEDBACKER
                string userId = ((int)dr[2]).ToString();
                string username = VR_Web_Project.User.GetUsername(userId);

                text += " " + username;

                // GET DATE OF THE FEEDBACK
                DateTime date = (DateTime)(dr[1]);
                string dateStr = date.ToString("MM/dd/yyyy");

                text += " (" + dateStr + ")";

                // CREATE TEXT ELEMENT USING THE FORMER STRING
                var a = new HtmlGenericControl("a") { InnerText = text };

                // CREATE DIV
                var div = new HtmlGenericControl("div");
                div.Attributes.Add("class", "content");
                div.Style["margin-top"] = "0px";

                string content = ((string)dr[4]);
                var p = new HtmlGenericControl("p") { InnerText = content };
                p.Style["margin-top"] = "0px";
                div.Controls.Add(p);

                // CREATE WRAPPER
                var feedbackDiv = new HtmlGenericControl("div");
                feedbackDiv.Attributes.Add("class", "userWrapper");

                // ADD ELEMENTS TO DIV
                feedbackDiv.Controls.Add(a);
                feedbackDiv.Controls.Add(div);

                // ADD DIV TO PAGE
                feedBackArea.Controls.Add(feedbackDiv);
            }
        }

        protected void Feedback_Click(object sender, EventArgs e)
        {
            // GET BUTTON AS OBJECT
            Button btn = sender as Button;

            // DECLARE PARAMETER FROM THE BUTTON CUSTOM PARAMETER
            int parameter = int.Parse(btn.Attributes["CustomParameter"].ToString());

            // GET OFFSET FROM SESSION
            int offset = (int)Session["FeedbackOffset"];

            if (parameter == -1)
            {
                if (offset >= 10)
                    offset -= 10;
            }
            else
            {
                if (Feedback.CountForId() > offset + 10)
                    offset += 10;
            }

            // SET SESSION AS NEW OFFSET
            Session["FeedbackOffset"] = offset;

            // REDIRECT
            Response.Redirect("Feedbacks.aspx");
            Response.End();
        }
    }
}