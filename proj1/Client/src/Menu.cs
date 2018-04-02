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
            buy_orders_grid.ColumnCount = 4;
            buy_orders_grid.Columns[0].Name = "ID";
            buy_orders_grid.Columns[1].Name = "Buying";
            buy_orders_grid.Columns[2].Name = "Price";
            buy_orders_grid.Columns[3].Name = "Date";

            sell_orders_grid.ColumnCount = 4;
            sell_orders_grid.Columns[0].Name = "ID";
            sell_orders_grid.Columns[1].Name = "Selling";
            sell_orders_grid.Columns[2].Name = "Price";
            sell_orders_grid.Columns[3].Name = "Date";

            sell_orders_grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            buy_orders_grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            sell_orders_grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            buy_orders_grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

            sell_orders_grid.RowHeadersVisible = false;
            buy_orders_grid.RowHeadersVisible = false;

            sell_orders_grid.ReadOnly = true;
            buy_orders_grid.ReadOnly = true;

            sell_orders_grid.Columns["ID"].Visible = false;
            buy_orders_grid.Columns["ID"].Visible = false;

            UpdateBalance();
            UpdateDiginotes();
            UpdateBuyOrders();
            UpdateSellOrders();           
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
        private void UpdateBalance()
        {
            string balance_json = Client.stub.GetBalance(Client.username);
            Client.balance = JsonConvert.DeserializeObject<dynamic>(balance_json).balance;
            balance_display.Text = Client.balance.ToString();          
        }
        private void UpdateDiginotes()
        {
            string diginotes_json = Client.stub.GetDiginotes(Client.username);
            Client.diginotes = JsonConvert.DeserializeObject<dynamic>(diginotes_json).diginotes;
            diginotes_display.Text = Client.diginotes.ToString();           
        }
        private void UpdateBuyOrders()
        {
            string buy_orders_json = Client.stub.GetBuyOrders(Client.username);
            Client.buy_orders = JsonConvert.DeserializeObject<List<Object>>(buy_orders_json);

            foreach (dynamic buy_order in Client.buy_orders)
            {
                buy_orders_grid.Rows.Add(new string[]
                {
                    buy_order.id,
                    buy_order.amount,
                    buy_order.price,
                    buy_order.date
                });
            }          
        }
        private void UpdateSellOrders()
        {
            string sell_orders_json = Client.stub.GetSellOrders(Client.username);
            Client.sell_orders = JsonConvert.DeserializeObject<List<Object>>(sell_orders_json);

            foreach (dynamic sell_order in Client.sell_orders)
            {
                sell_orders_grid.Rows.Add(new string[]
                {
                    sell_order.id,
                    sell_order.amount,
                    sell_order.price,
                    sell_order.date
                });
            }
        }
    }
}
