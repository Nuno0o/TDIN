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
            String username = username_input.Text;
            String password = password_input.Text;
            if (!(Boolean)Client.stub.Login(username, password)) return;
            Client.login.Visible = false;
            Client.menu.Visible = true;
        }

        private void register_button_Click(object sender, EventArgs e)
        {
            String username = username_input.Text;
            String password = password_input.Text;
            if (password.Length < 4) return;
            if (!(Boolean)Client.stub.Register(username, password)) return;
            Client.login.Visible = false;
            Client.menu.Visible = true;
        }
    }
}
