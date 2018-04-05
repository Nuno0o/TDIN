using System;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Client
{
    public partial class Edit: Form
    {
        private int id;
        private int amount;
        private string type;
        private double price;
        public Edit(int id, string type, int amount, double price)
        {
            InitializeComponent();
            this.id = id;
            this.amount = amount;
            this.price = price;
            this.type = type;
            diginotes_input.Text = amount.ToString();
            price_input.Text = price.ToString();
        }

        private void confirm_button_Click(object sender, EventArgs e)
        {
            double price = Convert.ToDouble(price_input.Text);
            if (price <= 0.0) return;

            int amount = Convert.ToInt32(diginotes_input.Text);
            if (amount <= 0) return;

            if (price == this.price && amount == this.amount) return;

            double balance = Client.balance;
            int diginotes = Client.diginotes;

            string json = null;

            if (type == "Buy" && price <= balance + this.price)
                json = Client.stub.EditBuyOrder(id,amount,price);
            else if (type == "Sell" && amount <= diginotes + this.amount)
                json = Client.stub.EditSellOrder(id,amount,price);
            else return;

            dynamic obj = JsonConvert.DeserializeObject(json);
            if (obj == null) return;

            Visible = false;

            Close();
        }
    }
}
