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
        /*
         * By pressing confirm, a new order is added to the database, as long as it meets certain constraints
         * If the order is not fully satisfied, the user is asked to change the quote value
         */
        private void confirm_button_Click(object sender, EventArgs e)
        {
            int amount = Convert.ToInt32(diginotes_input.Text);
            if (amount <= 0) return;

            double balance = Client.balance;
            int diginotes = Client.diginotes;

            string json = null;
            try
            {
                if (buy_radio.Checked)
                    json = Client.services.AddBuyOrder(Client.token, amount);
                else if (sell_radio.Checked && amount <= diginotes)
                    json = Client.services.AddSellOrder(Client.token, amount);
                else return;

                dynamic obj = JsonConvert.DeserializeObject(json);
                if (obj == null) return;

                int remaining = ((dynamic)obj).remaining;

                if (remaining > 0)
                {
                    EditQuote ed = new EditQuote("Your order couldn't be fully satisfied,"+ Environment.NewLine + " you can change quote value");
                    ed.ShowDialog();
                }

            }
            catch(Exception ex)
            {
                Dispose();
                return;
            }          

            Dispose();
        }       
    }
}
