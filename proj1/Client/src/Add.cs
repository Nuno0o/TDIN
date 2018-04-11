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

                if(remaining == 0)
                {
                    return;
                }else if (remaining > 0)
                {
                    EditQuote ed = new EditQuote("Your order couldn't be fully satisfied,"+ Environment.NewLine + " you can change quote value");
                    ed.ShowDialog();
                }

            }
            catch(Exception ex)
            {
                return;
            }



            Visible = false;           

            Close();
        }       
    }
}
