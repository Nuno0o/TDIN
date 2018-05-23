using System;
using System.Windows.Forms;

namespace TTSolver
{
    public partial class Register : Form
    {        
        public Register()
        {
            InitializeComponent();
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
            int department = 1;

            if (password != repeat_password)
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
