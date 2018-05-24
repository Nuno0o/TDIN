using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace TTDepartment
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();

            dynamic departments = Operations.GetDepartments();
            foreach (dynamic department in departments)
            {
                if (department.name == "Information Technology")
                    continue;
                department_drop_down.Items.Add(department.name);
            }
        }

        private void back_button_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void register_button_Click(object sender, EventArgs e)
        {
            string name = name_input.Text;
            string email = email_input.Text;
            string password = password_input.Text;
            string repeat_password = repeat_password_input.Text;
            int department = department_drop_down.SelectedIndex + 2;
            Debug.WriteLine(department);

            if(password != repeat_password)
            {
                status_display.Text = "Passwords don't match!";
            }
            else if (Operations.Register(name, email, password, department))
            {
                Dispose();
            }
            else
            {
                status_display.Text = "Invalid Information!";
            }
        }
    }
}
