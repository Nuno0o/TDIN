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
            FormBorderStyle = FormBorderStyle.FixedSingle;
        }
        private void login_button_Click(object sender, EventArgs e)
        {
            string username = username_input.Text;
            string password = password_input.Text;

            if (username.Length < 4 || username.Length > 16) return;
            if (password.Length < 4 || password.Length > 16) return;
            string json;
            dynamic res;
            try
            {
                json = Client.services.GetSalt(username);
                res = JsonConvert.DeserializeObject(json);
                if (res == null)
                {
                    label3.Text = "Invalid credentials!";
                    label3.ForeColor = System.Drawing.Color.Red;
                    return;
                }
            }
            catch(Exception ex)
            {
                label3.Text = "Login failed!";

                label3.ForeColor = System.Drawing.Color.Red;
                return;
            }

            string salt = res.salt;
            string hash = Client.Hash(password + salt);
            try
            {
                json = Client.services.Login(username, hash);
            }catch(Exception ex)
            {
                label3.Text = "Login failed!";

                label3.ForeColor = System.Drawing.Color.Red;
                return;
            }
            
            res = JsonConvert.DeserializeObject(json);
            if (res == null)
            {

                label3.Text = "Login failed!";

                label3.ForeColor = System.Drawing.Color.Red;
                return;
            }         

            Client.token = res.token;
            Client.menu = new Menu();
            Client.login.Visible = false;
            Client.menu.Visible = true;
            password_input.Text = "";
            label3.Text = "Login or Register";
            label3.ForeColor = System.Drawing.Color.Black;
        }
        private void register_button_Click(object sender, EventArgs e)
        {
            string username = username_input.Text;
            string password = password_input.Text;

            if (username.Length < 4 || username.Length > 16 || password.Length < 4 || password.Length > 16)
            {
                label3.Text = "Invalid username and/or password!";
                label3.ForeColor = System.Drawing.Color.Red;
                return;
            }

            string salt = Client.Salt();
            string hash = Client.Hash(password + salt);
            string json;
            try
            {
                json = Client.services.Register(username, hash, salt);
            }
            catch(Exception ex)
            {
                label3.Text = "Registration failed!";

                label3.ForeColor = System.Drawing.Color.Red;
                return;
            }
               
            dynamic res = JsonConvert.DeserializeObject(json);
            if (res == null)
            {
                label3.Text = "User already exists!";

                label3.ForeColor = System.Drawing.Color.Red;
                return;
            }
            
            Client.token = res.token;
            Client.menu = new Menu();
            Client.login.Visible = false;
            Client.menu.Visible = true;
            password_input.Text = "";
            label3.Text = "Login or Register";
            label3.ForeColor = System.Drawing.Color.Black;
        }        
    }
}
