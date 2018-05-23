using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TTSolver
{
    public partial class UnassignedTicket : Form
    {
        int id;
        public UnassignedTicket(int id, string title, string description, string sender)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.textBox1.Text = title;
            this.textBox2.Text = description;
            this.label2.Text = sender;
            this.id = id;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Operations.assignTicket(this.id, Operations.userid);
        }
    }
}
