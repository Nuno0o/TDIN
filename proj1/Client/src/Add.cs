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
        }

        private void confirm_button_Click(object sender, EventArgs e)
        {
            double price = Convert.ToDouble(price_input.Text);
            if (price <= 0.0) return;

            int amount = Convert.ToInt32(diginotes_input.Text);
            if (amount <= 0) return;

            double balance = Client.balance;
            int diginotes = Client.diginotes;

            string json = null;

            if (buy_radio.Checked && price <= balance)
                json = Client.stubs.AddBuyOrder(Client.username,amount,price);
            else if (sell_radio.Checked && amount <= diginotes)
                json = Client.stubs.AddSellOrder(Client.username,amount,price);
            else return;

            dynamic obj = JsonConvert.DeserializeObject(json);
            if (obj == null) return;

            Visible = false;           

            Close();
        }       
    }
}
