using Newtonsoft.Json;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Client
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }
        private void login_button_Click(object sender, EventArgs e)
        {
            string username = username_input.Text;
            string password = password_input.Text;

            if (username.Length < 4 || username.Length > 16) return;
            if (password.Length < 4 || password.Length > 16) return;
            try
            { 

            string json = Client.stubs.GetSalt(username);
            dynamic res = JsonConvert.DeserializeObject(json);
            if (res == null) return;

            string salt = res.salt;
            string hash = Client.Hash(password + salt);
            
                json = Client.stubs.Login(username, hash);
                res = JsonConvert.DeserializeObject(json);
                if (res == null)
                {
                    this.label3.Text = "User doesn't exist";
                    this.label3.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                if (!res)
                {
                    this.label3.Text = "Wrong password";
                    this.label3.ForeColor = System.Drawing.Color.Red;
                    return;
                }
            }catch(Exception ex)
            {
                this.label3.Text = "Login failed";
                this.label3.ForeColor = System.Drawing.Color.Red;
                return;
            }


            Client.username = username;
            Client.menu = new Menu();
            Client.login.Visible = false;
            Client.menu.Visible = true;
            password_input.Text = "";
            this.label3.Text = "Login or register";
            this.label3.ForeColor = System.Drawing.Color.Black;
        }
        private void register_button_Click(object sender, EventArgs e)
        {
            string username = username_input.Text;
            string password = password_input.Text;

            if (username.Length < 4 || username.Length > 16) return;
            if (password.Length < 4 || password.Length > 16) return;

            string salt = Client.Salt();
            string hash = Client.Hash(password + salt);

            string json = Client.stubs.Register(username, hash, salt);
            dynamic res = JsonConvert.DeserializeObject(json);

            if (res == null) return;
            if (!res) return;

            Client.username = username;
            Client.menu = new Menu();
            Client.login.Visible = false;
            Client.menu.Visible = true;
            password_input.Text = "";
        }        
    }
}
