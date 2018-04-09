using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class EditQuote : Form
    {
        public EditQuote(string text)
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            label1.Text = text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double newquote = Convert.ToDouble(this.textBox1.Text, System.Globalization.CultureInfo.InvariantCulture);
            if(newquote < Client.GetCurrentQuote() && Client.sell_orders.Count == 0)
            {
                label1.Text = "Can't decrease quote when " + Environment.NewLine+" you don't have active sell orders";
                return;
            }
            if (newquote > Client.GetCurrentQuote() && Client.buy_orders.Count == 0)
            {
                label1.Text = "Can't increase quote when " + Environment.NewLine + " you don't have active buy orders";
                return;
            }
            Console.WriteLine(newquote);
            Client.stubs.SetQuote(Client.username, newquote);
            Dispose();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void EditQuote_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
