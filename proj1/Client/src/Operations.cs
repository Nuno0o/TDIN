using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Client
{
    static class Operations
    {
        #region login
        public static void login(string username, string password)
        {
            try
            {
                string json = Client.stub.Login(username, password);
                dynamic obj = JsonConvert.DeserializeObject(json);
                if (!obj) throw new System.Exception("Login failed");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return;
        }

        public static void register(string username, string password)
        {
            try
            {
                string json = Client.stub.Register(username, password);
                dynamic obj = JsonConvert.DeserializeObject(json);
                if (!obj) throw new System.Exception("Register failed");
            }catch(Exception ex)
            {
                throw ex;
            }
            return;
        }

        #endregion login

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
            }
        }

        #endregion add
    }
}
