using System;
using System.Windows.Forms;

namespace TTDepartment
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();

            department_drop_down.Items.Add("Human Relations");
            department_drop_down.Items.Add("Sales and Marketing");
            department_drop_down.Items.Add("Financial and Accounting");
            department_drop_down.Items.Add("Research and Development");
            department_drop_down.Items.Add("Production and SCM");
        }

        private void back_button_Click(object sender, EventArgs e)
        {
            Program.login.Show();
            Close();
        }

        private void register_button_Click(object sender, EventArgs e)
        {
            string name = name_input.Text;
            string email = email_input.Text;
            string password = password_input.Text;
            string repeat_password = repeat_password_input.Text;
            int department = department_drop_down.SelectedIndex + 2;

            if(password != repeat_password)
            {
                status_display.Text = "Passwords don't match!";
            }
            else if (Operations.Register(name, email, password, department))
            {
                Program.login.Show();
                Close();
            }
            else
            {
                status_display.Text = "Invalid Information!";
            }
        }
    }
}
