using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Menu : Form
    {
        Thread updater;
        public Menu()
        {
            InitializeComponent();

            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;

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
            orders_grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            orders_grid.MultiSelect = false;

            updater = new Thread(Poll_Info);
            updater.IsBackground = true;
            updater.Start();
            

            this.FormClosing += Menu_FormClosing;
        }
        private void Poll_Info()
        {
            while (true)
            {
                /* get updated information */
                string json = null;
                json = Client.stub.GetBalance(Client.username);
                double balance = JsonConvert.DeserializeObject<dynamic>(json).balance;
                json = Client.stub.GetDiginotes(Client.username);
                int diginotes = JsonConvert.DeserializeObject<dynamic>(json).diginotes;
                json = Client.stub.GetBuyOrders(Client.username);
                List<dynamic> buy_orders = JsonConvert.DeserializeObject<List<dynamic>>(json);
                json = Client.stub.GetSellOrders(Client.username);
                List<dynamic> sell_orders = JsonConvert.DeserializeObject<List<dynamic>>(json);

                /* subtract buy orders' price from balance */
                foreach (dynamic buy_order in buy_orders)
                    balance -= (double)buy_order.price;

                /* subtract sell orders' amount from diginotes */
                foreach (dynamic sell_order in sell_orders)
                    diginotes -= (int)sell_order.amount;

                /* protected region for assigning updated values */
                Client.mut.WaitOne();
                Client.balance = balance;
                Client.diginotes = diginotes;
                Client.buy_orders = buy_orders;
                Client.sell_orders = sell_orders;
                Client.mut.ReleaseMutex();
                /* end of protected region */

                /* updating interface */
                Invoke(new Action(() =>
                {
                    balance_display.Text = Client.balance.ToString();
                    diginotes_display.Text = Client.diginotes.ToString();
                    string painted = null;
                    if (orders_grid.SelectedRows.Count != 0)
                        painted = orders_grid.SelectedRows[0].ToString();
                    orders_grid.Rows.Clear();                    
                    foreach (dynamic buy_order in Client.buy_orders)
                    {
                        orders_grid.Rows.Add(new[] {
                            buy_order.id,
                            "Buy",
                            buy_order.amount,
                            buy_order.price,
                            buy_order.date
                        });                        
                    }
                    foreach (dynamic sell_order in Client.sell_orders)
                    {
                        orders_grid.Rows.Add(new[] {
                            sell_order.id,
                            "Sell",
                            sell_order.amount,
                            sell_order.price,
                            sell_order.date
                        });
                    }
                    foreach (DataGridViewRow row in orders_grid.Rows)
                        if (row.ToString() == painted)
                        {
                            row.Selected = true;
                            break;
                        }
                }));
               
                /* wait 5 seconds */
                System.Threading.Thread.Sleep(2000);
            }
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
                    
            balance_display.Invoke(new Action(()=>balance_display.Text = Client.balance.ToString()));          
        }
        private void UpdateDiginotes(bool request = false)
        {
            if (request)
            {
                string json = Client.stub.GetDiginotes(Client.username);
                Client.diginotes = JsonConvert.DeserializeObject<dynamic>(json).diginotes;
            }
                
            diginotes_display.Invoke(new Action(() => diginotes_display.Text = Client.diginotes.ToString()));           
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
                orders_grid.Invoke(new Action(()=>orders_grid.Rows.Add(new string[]
                {
                    buy_order.id,
                    "Buy",
                    buy_order.amount,
                    buy_order.price,
                    buy_order.date
                })));
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
                orders_grid.Invoke(new Action(() => orders_grid.Rows.Add(new string[]
                {
                    sell_order.id,
                    "Sell",
                    sell_order.amount,
                    sell_order.price,
                    sell_order.date
                })));
            }
        }

        private void edit_button_Click(object sender, EventArgs e)
        {
            if (orders_grid.SelectedRows.Count != 1) return;
            var row = orders_grid.SelectedRows[0];
            if (row.Cells[0].Value == null) return;
            int id = Convert.ToInt32(row.Cells[0].Value);
            string type = Convert.ToString(row.Cells[1].Value);
            int amount = Convert.ToInt32(row.Cells[2].Value);
            double price = Convert.ToDouble(row.Cells[3].Value);
            new Edit(id,type,amount,price).Visible = true;
        }

        private void remove_button_Click(object sender, EventArgs e)
        {
            if (orders_grid.SelectedRows.Count != 1) return;
            var row = orders_grid.SelectedRows[0];
            object cell = row.Cells[0].Value;
            if (cell == null) return;
            int id = Convert.ToInt32(cell);
            int index = row.Index;
            string json = null;
            if ((string)row.Cells[1].Value == "Buy")
            {
                json = Client.stub.RemoveBuyOrder(id);
                if (JsonConvert.DeserializeObject(json) == null) return;
                object order = Client.buy_orders.Find(x => ((dynamic)x).id == id);
                Client.buy_orders.Remove(order);
            }
            else if ((string)row.Cells[1].Value == "Sell")
            {
                json = Client.stub.RemoveSellOrder(id);
                if (JsonConvert.DeserializeObject(json) == null) return;
                object order = Client.sell_orders.Find(x => ((dynamic)x).id == id);
                Client.sell_orders.Remove(order);
            }
            //Invoke(new Action(() => orders_grid.Rows.Remove(row)));

        }

        private void add_button_Click(object sender, EventArgs e)
        {
            new Add().Visible = true;
        }

        private void logout_button_Click(object sender, EventArgs e)
        {
            updater.Abort();
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

        private void Menu_FormClosing(object sender,EventArgs e)
        {
            logout_button_Click(sender, e);
        }
    }
}
