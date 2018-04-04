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
    public partial class Edit : Form
    {
        string id;
        string type;
        public Edit(string id,string type,string amount,string price)
        {
            InitializeComponent();

            textBox2.Text = amount;
            textBox1.Text = price;
            this.id = id;
            this.type = type;

            this.Text = "Editing " + type + " order #" + id;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int amount = int.Parse(this.textBox2.Text);
            double price = double.Parse(this.textBox1.Text);
            try
            {
                if (type.Equals("Buy"))
                {
                    Operations.RemoveBuyOrder(int.Parse(id));
                    Operations.AddBuyOrder(Client.username, int.Parse(textBox2.Text), double.Parse(textBox1.Text));
                }
                else
                {
                    Operations.RemoveSellOrder(int.Parse(id));
                    Operations.AddSellOrder(Client.username, int.Parse(textBox2.Text), double.Parse(textBox1.Text));
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            
            this.Close();
        }

        private void Edit_Load(object sender, EventArgs e)
        {

        }
    }
}
