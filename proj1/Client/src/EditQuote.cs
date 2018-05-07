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
    public partial class EditQuote : Form
    {

        public EditQuote(string text)
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            label1.Text = text;
        }
        /*
         * When submit is clicked, the quote changes, as long as the user has unsatisfied orders
         */
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {


<<<<<<< HEAD:proj1/Client/src/Operations.cs
        #region menu

        public static double GetBalance(string username)
        {
            double balance = 0.0;
            try
            {
                string json = Client.stub.GetBalance(Client.username);
                dynamic obj = JsonConvert.DeserializeObject<dynamic>(json).balance;
                balance = (double)obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return balance;
        }

        public static int GetDiginotes(string username)
        {
            int diginotes = 0;
            try
            {
                string json = Client.stub.GetDiginotes(Client.username);
                dynamic obj = JsonConvert.DeserializeObject<dynamic>(json).diginotes;
                diginotes = (int)obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return diginotes;
        }

        public static List<Object> GetBuyOrders(string username)
        {
            List<Object> buyorders = new List<object>();
            try
            {
                string json = Client.stub.GetBuyOrders(Client.username);
                buyorders = JsonConvert.DeserializeObject<List<Object>>(json);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return buyorders;
        }

        public static List<Object> GetSellOrders(string username)
        {
            List<Object> sellorders = new List<object>();
            try
            {
                string json = Client.stub.GetSellOrders(Client.username);
                sellorders = JsonConvert.DeserializeObject<List<Object>>(json);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return sellorders;
        }

        #endregion menu

        #region add

        public static void AddBuyOrder(string username, int amount, double price)
        {
            int success = 0;
            try
            {
                string json = Client.stub.AddBuyOrder(username, amount, price);
                success = JsonConvert.DeserializeObject<int>(json);
                if(success != 1)
                {
                    throw new Exception("Failed to add");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void AddSellOrder(string username, int amount, double price)
        {
            int success = 0;
            try
            {
                string json = Client.stub.AddSellOrder(username, amount, price);
                success = JsonConvert.DeserializeObject<int>(json);
                if (success != 1)
                {
                    throw new Exception("Failed to add");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void RemoveBuyOrder(int id)
        {
            int success = 0;
            try
            {
                string json = Client.stub.RemoveBuyOrder(id);
                success = JsonConvert.DeserializeObject<int>(json);
                if (success != 1)
                {
                    throw new Exception("Failed to remove");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void RemoveSellOrder(int id)
        {
            int success = 0;
            try
            {
                string json = Client.stub.RemoveSellOrder(id);
                success = JsonConvert.DeserializeObject<int>(json);
                if (success != 1)
                {
                    throw new Exception("Failed to remove");
                }
            }
            catch (Exception ex)
            {
                throw ex;
=======
                double newquote = Convert.ToDouble(this.textBox1.Text, System.Globalization.CultureInfo.InvariantCulture);
                if (newquote < Client.GetCurrentQuote() && Client.sell_orders.Count == 0)
                {
                    label1.Text = "Can't decrease quote when " + Environment.NewLine + " you don't have active sell orders";
                    return;
                }
                if (newquote > Client.GetCurrentQuote() && Client.buy_orders.Count == 0)
                {
                    label1.Text = "Can't increase quote when " + Environment.NewLine + " you don't have active buy orders";
                    return;
                }
                Console.WriteLine(newquote);
                Client.services.SetQuote(Client.token, newquote);
                Dispose();
            }catch(Exception ex)
            {

>>>>>>> final:proj1/Client/src/EditQuote.cs
            }
        }
    }
}
