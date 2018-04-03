﻿using Newtonsoft.Json;
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
    public partial class Funds : Form
    {
        public Funds()
        {
            InitializeComponent();
        }

        private void funds_button_Click(object sender, EventArgs e)
        {
            double amount = Convert.ToDouble(amount_input.Text);
            if (amount <= 0.0) return;

            double balance = Client.balance;
            string json = null;

            if (deposit_radio.Checked)
                json = Client.stub.DepositBalance(Client.username,amount);
            else if (withdraw_radio.Checked && amount <= balance)
                json = Client.stub.WithdrawBalance(Client.username, amount);
            else return;

            Visible = false;

            dynamic obj = JsonConvert.DeserializeObject(json);
            if (obj == null) return;

            balance = obj.balance;            
            Client.balance = balance;
            //Client.menu.UpdateBalance();                       
        }
    }
}
