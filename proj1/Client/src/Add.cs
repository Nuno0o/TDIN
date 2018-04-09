using Newtonsoft.Json;
using System;
using System.Windows.Forms;

namespace Client
{
    public partial class Add : Form
    {
        public Add()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void confirm_button_Click(object sender, EventArgs e)
        {
            int amount = Convert.ToInt32(diginotes_input.Text);
            if (amount <= 0) return;

            double balance = Client.balance;
            int diginotes = Client.diginotes;

            string json = null;

            if (buy_radio.Checked)
                json = Client.stubs.AddBuyOrder(Client.username,amount);
            else if (sell_radio.Checked && amount <= diginotes)
                json = Client.stubs.AddSellOrder(Client.username,amount);
            else return;

            dynamic obj = JsonConvert.DeserializeObject(json);
            if (obj == null) return;

            Visible = false;           

            Close();
        }       
    }
}
