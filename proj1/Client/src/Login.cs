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
        }

        private void Login_Load(object sender, EventArgs e)
        {
            
        }

        private void login_button_Click(object sender, EventArgs e)
        {
            string username = username_input.Text;
            string password = password_input.Text;
            string json = Client.stub.Login(username, password);
            dynamic obj = JsonConvert.DeserializeObject(json);
            if (!obj) return;
            Client.username = username;
            Client.login.Visible = false;
            Client.menu = new Menu();                      
            Client.menu.Visible = true;
        }

        private void register_button_Click(object sender, EventArgs e)
        {
            string username = username_input.Text;
            string password = password_input.Text;
            if (password.Length < 4) return;
            string json = Client.stub.Register(username, password);
            dynamic obj = JsonConvert.DeserializeObject(json);            
            if (!obj) return;
            Client.login.Visible = false;
            Client.menu.Visible = true;
        }
    }
}
