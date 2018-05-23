using System;
using System.Windows.Forms;

namespace TTDepartment
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void login_button_Click(object sender, EventArgs e)
        {
            string email = email_input.Text;
            string password = password_input.Text;

            if(Operations.Login(email, password))
            {
                Operations.main = new Main();
                Hide();
                Operations.main.ShowDialog();
                Show();
                status_display.Text = "";
                password_input.Text = "";
            }
            else
            {
                status_display.Text = "Invalid Credentials!";
            }
        }

        private void register_button_Click(object sender, EventArgs e)
        {
            Register r = new Register();
            r.ShowDialog();
            password_input.Text = "";
        }
    }
}
