using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace Client
{
    public partial class Menu : Form
    {
        Thread updater;
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
            orders_grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            orders_grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            orders_grid.RowHeadersVisible = false;
            orders_grid.ReadOnly = true;
            orders_grid.Columns["ID"].Visible = false;
            orders_grid.MultiSelect = false;

            updater = new Thread(Poll_Info);
            updater.IsBackground = true;
            updater.Start();
            

            FormClosing += Menu_FormClosing;
        }
        private void Poll_Info()
        {
            while (true)
            {
                /* get updated information */
                string json = null;
                json = Client.stubs.GetBalance(Client.username);
                double balance = JsonConvert.DeserializeObject<dynamic>(json).balance;
                json = Client.stubs.GetDiginotes(Client.username);
                int diginotes = JsonConvert.DeserializeObject<dynamic>(json).diginotes;
                json = Client.stubs.GetBuyOrders(Client.username);
                List<dynamic> buy_orders = JsonConvert.DeserializeObject<List<dynamic>>(json);
                json = Client.stubs.GetSellOrders(Client.username);
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
               
                /* wait 2 seconds */
                Thread.Sleep(2000);
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
            Edit ed = new Edit(id, type, amount, price);
            ed.ShowDialog();
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
                json = Client.stubs.RemoveBuyOrder(id);
                if (JsonConvert.DeserializeObject(json) == null) return;
                object order = Client.buy_orders.Find(x => ((dynamic)x).id == id);
                Client.buy_orders.Remove(order);
            }
            else if ((string)row.Cells[1].Value == "Sell")
            {
                json = Client.stubs.RemoveSellOrder(id);
                if (JsonConvert.DeserializeObject(json) == null) return;
                object order = Client.sell_orders.Find(x => ((dynamic)x).id == id);
                Client.sell_orders.Remove(order);
            }
        }

        private void add_button_Click(object sender, EventArgs e)
        {
            Add a = new Add();
            a.ShowDialog();
        }

        private void logout_button_Click(object sender, EventArgs e)
        {
            updater.Abort();                     
            Client.buy_orders = null;
            Client.sell_orders = null;
            Client.balance = -1.0;
            Client.diginotes = -1;
            Visible = false;
            Client.login.Visible = true;
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
