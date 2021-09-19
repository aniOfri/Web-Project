using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VR_Web_Project
{
    public partial class Register : System.Web.UI.Page
    {
        public string RegStatus = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            DAL DAL = new DAL();
            if (Session["status"] != null)
            {
                RegStatus = Session["status"].ToString();
                if (RegStatus == "200")
                    RegStatus = "Successfully registered.";
                else
                    RegStatus = "מספר טלפון או שם משתמש נמצאים בשימוש";
            }

            if (Request["submit"] != null)
            {
                string username = Request["name"].Replace(" ", "");
                string phone = Request["phone"].Replace(" ", "");
                string password = Request["password"].Replace(" ", "");

                string selectQuery = "SELECT * FROM Member";
                selectQuery += " WHERE ";
                string selectQueryPhone = selectQuery + " PhoneNumber ='" + phone + "'";
                string selectQueryUsername = selectQuery + " Username ='" + username + "'";
                if (DAL.IsExist(selectQueryPhone) || DAL.IsExist(selectQueryUsername))
                {   
                    Session["status"] = "405";
                    Response.Redirect("Register.aspx");
                }
                else
                {
                    User user = new User(username, password);

                    if (user.Register(phone))
                    {
                        Session["status"] = "200";
                        Response.Redirect("Register.aspx");
                    }
                    else
                    {
                        Session["status"] = "405";
                        Response.Redirect("Register.aspx");
                    }
                }
            }
        }
    }
}