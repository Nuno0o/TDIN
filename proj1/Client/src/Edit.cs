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
        public Edit()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int amount = int.Parse(this.textBox2.Text);
            double price = double.Parse(this.textBox1.Text);
            
            
            this.Close();
        }

        private void Edit_Load(object sender, EventArgs e)
        {

        }
    }
}
