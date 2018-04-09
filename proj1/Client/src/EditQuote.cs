﻿using System;
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
        public EditQuote()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double newquote = Convert.ToDouble(this.textBox1.Text, System.Globalization.CultureInfo.InvariantCulture);
            Client.stubs.SetQuote(Client.username, newquote);
            Dispose();
        }
    }
}
