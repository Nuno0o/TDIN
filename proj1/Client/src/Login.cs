using Newtonsoft.Json;
using System;
using System.Windows.Forms;

namespace Client
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();

            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        }

        private void Login_Load(object sender, EventArgs e)
        {
            
        }

        private void login_button_Click(object sender, EventArgs e)
        {
            string username = username_input.Text;
            string password = password_input.Text;
            try
            {
                Operations.login(username, password);
                username_input.Text = null;
                password_input.Text = null;
            }catch (Exception ex)//login failed
            {
                this.label3.Text = "Login failed";
                this.label3.ForeColor = System.Drawing.Color.Red;
                return;
            }
            Client.username = username;
            Client.login.Visible = false;
            Client.menu = new Menu();                      
            Client.menu.Visible = true;
        }

        private void register_button_Click(object sender, EventArgs e)
        {
            string username = username_input.Text;
            string password = password_input.Text;
            if (password.Length < 4)
            {
                this.label3.Text = "Password needs more than 4 characters";
                this.label3.ForeColor = System.Drawing.Color.Red;
                return;
            };
            try
            {
                Operations.register(username, password);
            }catch(Exception ex)
            {
                this.label3.Text = "Register failed";
                this.label3.ForeColor = System.Drawing.Color.Red;
                return;
            }
            this.label3.Text = "Register successful. You can now login";
            this.label3.ForeColor = System.Drawing.Color.Green;
        }
    }
}
