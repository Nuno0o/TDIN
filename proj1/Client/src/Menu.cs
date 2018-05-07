using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace Client
{
    public partial class Menu : Form
    {
        Poll_Information poll;
        Thread updater;
        public Menu()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            orders_grid.ColumnCount = 5;
            orders_grid.Columns[0].Name = "ID";
            orders_grid.Columns[1].Name = "Type";
<<<<<<< HEAD
            orders_grid.Columns[2].Name = "Diginotes";
            orders_grid.Columns[3].Name = "Price";
            orders_grid.Columns[4].Name = "Date";           
=======
            orders_grid.Columns[2].Name = "Amount";
            orders_grid.Columns[3].Name = "Date";
            orders_grid.Columns[4].Name = "Active";
>>>>>>> final

            orders_grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            orders_grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            orders_grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            orders_grid.AllowUserToAddRows = false;
            orders_grid.RowHeadersVisible = false;
            orders_grid.ReadOnly = true;
<<<<<<< HEAD
            //orders_grid.Columns["ID"].Visible = false;
=======
            orders_grid.Columns["ID"].Visible = false;
            orders_grid.MultiSelect = false;

            poll += update_client;
            poll += update_interface;
            poll += update_timers;
>>>>>>> final

            updater = new Thread(Poll_Info);
            updater.IsBackground = true;
            updater.Start();
            

<<<<<<< HEAD
            this.FormClosing += CloseActions;
=======
            FormClosing += Menu_FormClosing;
>>>>>>> final
        }
        delegate void Poll_Information();
        private void Poll_Info()
        {
            while (true)
            {
<<<<<<< HEAD
                UpdateInformation(true);
                System.Threading.Thread.Sleep(Consts.POLLINGRATE);
=======
                poll();
                /* wait 2 seconds */
                Thread.Sleep(2000);
>>>>>>> final
            }
        }
        private void update_client()
        {
            try
            {
                /* get updated information */
                string json = null;
                json = Client.services.GetQuotes(10);
                List<dynamic> quotes = JsonConvert.DeserializeObject<List<dynamic>>(json);
                json = Client.services.GetBalance(Client.token);
                double balance = JsonConvert.DeserializeObject<dynamic>(json).balance;
                json = Client.services.GetDiginotes(Client.token);
                int diginotes = JsonConvert.DeserializeObject<dynamic>(json).diginotes;
                int realdiginotes = diginotes;
                json = Client.services.GetBuyOrders(Client.token);
                List<dynamic> buy_orders = JsonConvert.DeserializeObject<List<dynamic>>(json);
                json = Client.services.GetSellOrders(Client.token);
                List<dynamic> sell_orders = JsonConvert.DeserializeObject<List<dynamic>>(json);

<<<<<<< HEAD
        }
        public void UpdateInformation(bool request = false)
        {
            UpdateBalance(request);
            UpdateDiginotes(request);
            UpdateBuyOrders(request);
            UpdateSellOrders(request);
        }
        public void UpdateBalance(bool request = false)
        {
            if (request)
            {
                try
                {
                    Client.balance = Operations.GetBalance(Client.username);
                }catch(Exception ex)
                {

                }
            }    
                    
            balance_display.Invoke(new Action(()=>balance_display.Text = Client.balance.ToString()));          
=======
                /* subtract sell orders' amount from diginotes */
                foreach (dynamic sell_order in sell_orders)
                    diginotes -= (int)sell_order.amount;
                /* protected region for assigning updated values */
                Client.mut.WaitOne();
                Client.quotes = quotes;
                Client.balance = balance;
                Client.diginotes = diginotes;
                Client.realdiginotes = realdiginotes;
                Client.buy_orders = buy_orders;
                Client.sell_orders = sell_orders;
                Client.mut.ReleaseMutex();
                /* end of protected region */
                Client.timeout_counter = 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Client.timeout_counter++;
                if (Client.timeout_counter >= Client.MAX_TIMEOUT)
                {
                    Client.login.Visible = true;
                    Client.login.SetText("Client timed out", System.Drawing.Color.Red);
                }
                Invoke(new Action (() => Dispose()));
            }
           
>>>>>>> final
        }
        private void update_interface()
        {
            Invoke(new Action(() =>
            {
<<<<<<< HEAD
                try
                {
                    Client.diginotes = Operations.GetDiginotes(Client.username);
                }
                catch (Exception ex)
                {

                }
            }
                
            diginotes_display.Invoke(new Action(() => diginotes_display.Text = Client.diginotes.ToString()));           
=======
                Text = "Current quote : " + Client.GetCurrentQuote();
                balance_display.Text = Client.balance.ToString();
                diginotes_display.Text = Client.realdiginotes.ToString() + " (" + Client.diginotes.ToString() + " avail.)";
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
                            buy_order.date,
                            buy_order.active
                        });
                }
                foreach (dynamic sell_order in Client.sell_orders)
                {
                    orders_grid.Rows.Add(new[] {
                            sell_order.id,
                            "Sell",
                            sell_order.amount,
                            sell_order.date,
                            sell_order.active
                        });
                }
                foreach (DataGridViewRow row in orders_grid.Rows)
                    if (row.ToString() == painted)
                    {
                        row.Selected = true;
                        break;
                    }
            }));
>>>>>>> final
        }
        private void update_timers()
        {
            long current_time = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            /* Add inactive orders to the timer dictionary */
            foreach (dynamic buy_order in Client.buy_orders)
            {
<<<<<<< HEAD
                try
                {
                    Client.buy_orders = Operations.GetBuyOrders(Client.username);
                }
                catch (Exception ex)
                {

=======
                int id = Convert.ToInt32(buy_order.id);
                if (buy_order.active == 0 && !Client.b_activateTimers.ContainsKey(id))
                {
                    Client.b_activateTimers.Add(id, current_time);
>>>>>>> final
                }
            }
            foreach (dynamic sell_order in Client.sell_orders)
            {
<<<<<<< HEAD
                bool inGrid = false;
                int id = buy_order.id;
                foreach (DataGridViewRow row in orders_grid.Rows)
                {
                    if (row.Cells[1].Value.ToString().Equals("Buy") && row.Cells[0].Value.ToString().Equals(id + ""))
                    {
                        inGrid = true;
                    }
                }
                if(inGrid == false)
                    orders_grid.Invoke(new Action(()=>orders_grid.Rows.Add(new string[]
                    {
                        buy_order.id,
                        "Buy",
                        buy_order.amount,
                        buy_order.price,
                        buy_order.date
                    })));

            }
            foreach (DataGridViewRow row in orders_grid.Rows)
            {
                if (!row.Cells[1].Value.ToString().Equals("Buy"))
                    continue;
                bool deleted = true;
                foreach (dynamic buy_order in Client.buy_orders)
                {
                    int id = buy_order.id;
                    if ((id + "").Equals(row.Cells[0].Value.ToString()))
                    {
                        deleted = false;
                    }
                }
                if(deleted == true)
                {
                    orders_grid.Invoke(new Action(() => orders_grid.Rows.Remove(row)));
                }
            }
        }
        private void UpdateSellOrders(bool request = false)
        {
            if (request)
            {
                try
                {
                    Client.sell_orders = Operations.GetSellOrders(Client.username);
                }
                catch (Exception ex)
                {

                }
            }
            foreach (dynamic sell_order in Client.sell_orders)
            {
                bool inGrid = false;
                int id = sell_order.id;
                foreach (DataGridViewRow row in orders_grid.Rows)
                {
                    if (row.Cells[1].Value.ToString().Equals("Sell") && row.Cells[0].Value.ToString().Equals(id + ""))
                    {
                        inGrid = true;
                    }
                }
                if(inGrid == false)
                    orders_grid.Invoke(new Action(() => orders_grid.Rows.Add(new string[]
                    {
                        sell_order.id,
                        "Sell",
                        sell_order.amount,
                        sell_order.price,
                        sell_order.date
                    })));
            }
            foreach (DataGridViewRow row in orders_grid.Rows)
            {
                if (!row.Cells[1].Value.ToString().Equals("Sell"))
                    continue;
                bool deleted = true;
                foreach (dynamic sell_order in Client.sell_orders)
                {
                    int id = sell_order.id;
                    if ((id + "").Equals(row.Cells[0].Value.ToString()))
                    {
                        deleted = false;
                    }
                }
                if (deleted == true)
                {
                    orders_grid.Invoke(new Action(() => orders_grid.Rows.Remove(row)));
                }
=======
                int id = Convert.ToInt32(sell_order.id);
                if (sell_order.active == 0 && !Client.s_activateTimers.ContainsKey(id))
                {
                    Client.b_activateTimers.Add(id, current_time);
                }
            }
            /* When 60 seconds have passed, re-activate order */

            /* orders to remove, since they cant be removed while enumerating */
            List<int> buytoremove = new List<int>();
            List<int> selltoremove = new List<int>();
            foreach (KeyValuePair<int, long> timer in Client.b_activateTimers)
            {
                dynamic order = Client.BuyOrderById(timer.Key);
                if (order == null)
                {
                    buytoremove.Add(timer.Key);
                    continue;
                }
                int id = Convert.ToInt32(order.id);
                int amount = Convert.ToInt32(order.amount);
                if (current_time - timer.Value > Client.DIFFERENCE && order.active == 0)
                {
                    try
                    {
                        buytoremove.Add(timer.Key);
                        Client.services.ActivateBuyOrder(Client.token, id, amount);

                    }
                    catch (Exception ex)
                    {

                        selltoremove.Add(timer.Key);
                        Client.services.ActivateSellOrder(Client.token, id, amount);
                    }
                }                
            }
            foreach (KeyValuePair<int, long> timer in Client.s_activateTimers)
            {
                dynamic order = Client.SellOrderById(timer.Key);
                if (order == null)
                {
                    selltoremove.Add(timer.Key);
                    continue;
                }
                int id = Convert.ToInt32(order.id);
                int amount = Convert.ToInt32(order.amount);
                if (current_time - timer.Value > Client.DIFFERENCE && order.active == 0)
                {

                    try
                    {
                        selltoremove.Add(timer.Key);
                        Client.services.ActivateSellOrder(Client.token, id, amount);
                    }
                    catch (Exception ex)
                    {
                    }
                }                                   
            }
            foreach (int elem in buytoremove)
            {
                Client.b_activateTimers.Remove(elem);
            }
            foreach (int elem in selltoremove)
            {
                Client.s_activateTimers.Remove(elem);
>>>>>>> final
            }
        }
        private void ClearOrdersGrid()
        {
            orders_grid.Invoke(new Action(() => orders_grid.Rows.Clear()));
        }

        private void remove_button_Click(object sender, EventArgs e)
        {
<<<<<<< HEAD
            DataGridViewRow selectedRow = orders_grid.CurrentCell.OwningRow;
            if (selectedRow == null)
                return;
            string diginotes = selectedRow.Cells[2].Value.ToString();
            string price = selectedRow.Cells[3].Value.ToString();
            string id = selectedRow.Cells[0].Value.ToString();
            string type = selectedRow.Cells[1].Value.ToString();
            Edit ed = new Edit(id,type,diginotes,price);
            ed.ShowDialog();
            UpdateInformation(true);
        }

        private void remove_button_Click(object sender, EventArgs e)
        {
            DataGridViewRow selectedRow = orders_grid.CurrentCell.OwningRow;
            if (selectedRow == null)
                return;
            string id = selectedRow.Cells[0].Value.ToString();
            string type = selectedRow.Cells[1].Value.ToString();
            try
            {
                if (type.Equals("Buy"))
                {
                    Operations.RemoveBuyOrder(int.Parse(id));
                }
                else
                {
                    Operations.RemoveSellOrder(int.Parse(id));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            UpdateInformation(true);
=======
            try
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
                    json = Client.services.RemoveBuyOrder(id);
                    if (JsonConvert.DeserializeObject(json) == null) return;
                    object order = Client.buy_orders.Find(x => ((dynamic)x).id == id);
                    Client.buy_orders.Remove(order);
                }
                else if ((string)row.Cells[1].Value == "Sell")
                {
                    json = Client.services.RemoveSellOrder(id);
                    if (JsonConvert.DeserializeObject(json) == null) return;
                    object order = Client.sell_orders.Find(x => ((dynamic)x).id == id);
                    Client.sell_orders.Remove(order);
                }
            }catch(Exception ex)
            {
>>>>>>> final

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
            Client.login.Visible = true;
            Dispose();
        }

        private void funds_button_Click(object sender, EventArgs e)
        {
            Funds f = new Funds();
            f.ShowDialog();
        }

        private void CloseActions(object sender,EventArgs e)
        {
            logout_button_Click(sender, e);
<<<<<<< HEAD
            updater.Abort();
            ClearOrdersGrid();
=======
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EditQuote ed = new EditQuote("");
            ed.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {


                if (orders_grid.SelectedRows.Count != 1) return;
                var row = orders_grid.SelectedRows[0];
                if (row.Cells[0].Value == null) return;
                int id = Convert.ToInt32(row.Cells[0].Value);
                string type = Convert.ToString(row.Cells[1].Value);
                int amount = Convert.ToInt32(row.Cells[2].Value);
                int active = Convert.ToInt32(row.Cells[4].Value);
                if (type.Equals("Buy") && active == 0)
                {
                    Client.services.ActivateBuyOrder(Client.token, id, amount);
                }
                else if (type.Equals("Sell") && active == 0)
                {
                    Client.services.ActivateSellOrder(Client.token, id, amount);
                }
            }catch(Exception ex)
            {

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Diginotes d = new Diginotes();
            d.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Transactions t = new Transactions();
            t.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Quotes q = new Quotes();
            q.ShowDialog();
>>>>>>> final
        }
    }
}
