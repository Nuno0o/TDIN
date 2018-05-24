using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TTClient
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            dynamic departments = Operations.GetDepartments();
            foreach (dynamic department in departments)
            {
                ListItem item = new ListItem(
                    (string)department.name,
                    (string)department.id
                );
                department_drop_down.Items.Add(item);
            }            
        }

        protected void register_button_Click(object sender, EventArgs e)
        {
            string name = name_input.Text;
            string email = email_input.Text;
            string password = password_input.Text;
            string repeat_password = repeat_password_input.Text;
            int department = Convert.ToInt32(department_drop_down.SelectedItem.Value);

            if (password != repeat_password)
            {
                status_display.Text = "Passwords don't match!";
            }
            else if (Operations.Register(name, email, password, department))
            {
                Response.Redirect("Login.aspx", true);
            }
            else
            {
                status_display.Text = "Invalid Information!";
            }
        }

        protected void back_button_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx", true);
        }
    }
}