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
        public void SetText(string text,System.Drawing.Color color)
        {
            label3.Text = text;
            label3.ForeColor = color;
        }
        private void login_button_Click(object sender, EventArgs e)
        {
            try
            {
                string username = username_input.Text;
                string password = password_input.Text;

                if (username.Length < 4 || username.Length > 16) return;
                if (password.Length < 4 || password.Length > 16) return;
                string json;
                dynamic res;

                json = Client.services.GetSalt(username);
                res = JsonConvert.DeserializeObject(json);
                if (res == null)
                {
                    label3.Text = "Account doesn't exist";
                    label3.ForeColor = System.Drawing.Color.Red;
                    return;
                }


                string salt = res.salt;
                string hash = Client.Hash(password + salt);

                json = Client.services.Login(username, hash);


                res = JsonConvert.DeserializeObject(json);
                if (res == null)
                {

                    label3.Text = "Incorrect password";

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
            catch (Exception ex)
            {
                label3.Text = "Login failed!";

                label3.ForeColor = System.Drawing.Color.Red;
                return;
            }

        }
        private void register_button_Click(object sender, EventArgs e)
        {
            try
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

                json = Client.services.Register(username, hash, salt);


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
            catch (Exception ex)
            {
                label3.Text = "Registration failed!";

                label3.ForeColor = System.Drawing.Color.Red;
                return;
            }
        }
    }
}
