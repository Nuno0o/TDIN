using Newtonsoft.Json;
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
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
            orders_grid.ColumnCount = 5;
            orders_grid.Columns[0].Name = "ID";
            orders_grid.Columns[1].Name = "Type";
            orders_grid.Columns[2].Name = "Amount";
            orders_grid.Columns[3].Name = "Price";
            orders_grid.Columns[4].Name = "Date";           

            orders_grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            orders_grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            orders_grid.RowHeadersVisible = false;
            orders_grid.ReadOnly = true;
            orders_grid.Columns["ID"].Visible = false;

            UpdateBalance(true);
            UpdateDiginotes(true);
            UpdateBuyOrders(true);
            UpdateSellOrders(true);           
        }
        private void Menu_Load(object sender, EventArgs e)
        {
            

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
        public void UpdateBalance(bool request = false)
        {
            if (request)
            {
                string json = Client.stub.GetBalance(Client.username);
                Client.balance = JsonConvert.DeserializeObject<dynamic>(json).balance;
            }    
                    
            balance_display.Text = Client.balance.ToString();          
        }
        private void UpdateDiginotes(bool request = false)
        {
            if (request)
            {
                string json = Client.stub.GetDiginotes(Client.username);
                Client.diginotes = JsonConvert.DeserializeObject<dynamic>(json).diginotes;
            }
                
            diginotes_display.Text = Client.diginotes.ToString();           
        }
        private void UpdateBuyOrders(bool request = false)
        {
            if (request)
            {
                string json = Client.stub.GetBuyOrders(Client.username);
                Client.buy_orders = JsonConvert.DeserializeObject<List<Object>>(json);
            }
            
            foreach (dynamic buy_order in Client.buy_orders)
            {
                orders_grid.Rows.Add(new string[]
                {
                    buy_order.id,
                    "Buy",
                    buy_order.amount,
                    buy_order.price,
                    buy_order.date
                });
            }          
        }
        private void UpdateSellOrders(bool request = false)
        {
            if (request)
            {
                string json = Client.stub.GetSellOrders(Client.username);
                Client.sell_orders = JsonConvert.DeserializeObject<List<Object>>(json);
            }              

            foreach (dynamic sell_order in Client.sell_orders)
            {
                orders_grid.Rows.Add(new string[]
                {
                    sell_order.id,
                    "Sell",
                    sell_order.amount,
                    sell_order.price,
                    sell_order.date
                });
            }
        }

        private void edit_button_Click(object sender, EventArgs e)
        {

        }

        private void remove_button_Click(object sender, EventArgs e)
        {

        }

        private void add_button_Click(object sender, EventArgs e)
        {

        }

        private void logout_button_Click(object sender, EventArgs e)
        {
            Client.login.Visible = true;
            Client.menu.Visible = false;
            Client.menu = null;           
            Client.buy_orders = null;
            Client.sell_orders = null;
            Client.balance = -1.0;
            Client.diginotes = -1;
        }

        private void funds_button_Click(object sender, EventArgs e)
        {
            new Funds().Visible = true;
        }
    }
}
