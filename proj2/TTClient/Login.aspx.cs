using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TTClient
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void login_button_Click(object sender, EventArgs e)
        {
            string email = email_input.Text;
            string password = password_input.Text;

            if (Operations.Login(email, password))
            {
                Session["token"] = Operations.GetToken();
                Response.Redirect("Tickets.aspx", true);
            }
            else
            {
                status_display.Text = "Invalid Credentials!";
            }
        }

        protected void register_button_Click(object sender, EventArgs e)
        {
            Response.Redirect("Register.aspx", true);
        }
    }
}