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
        }
        private void login_button_Click(object sender, EventArgs e)
        {
            string username = username_input.Text;
            string password = password_input.Text;

            if (username.Length < 4 || username.Length > 16) return;
            if (password.Length < 4 || password.Length > 16) return;

            string json = Client.stubs.GetSalt(username);
            dynamic res = JsonConvert.DeserializeObject(json);
            if (res == null) return;

            string salt = res.salt;
            string hash = Client.Hash(password + salt);

            json = Client.stubs.Login(username, hash);
            res = JsonConvert.DeserializeObject(json);

            if (res == null) return;
            if (!res) return;

            Client.username = username;
            Client.menu = new Menu();
            Client.login.Visible = false;
            Client.menu.Visible = true;
            password_input.Text = "";
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
