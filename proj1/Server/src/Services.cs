using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Server
{
    class Services : MarshalByRefObject
    {        
        public string Login(string username, string hash)
        {
            Console.WriteLine("LOGIN "+username+" "+ hash);
            dynamic obj = Database.GetUser(username);
            if (obj == null) return JsonConvert.SerializeObject(null);            
            return JsonConvert.SerializeObject(obj.hash == hash);
        }
        public string Register(string username, string hash, string salt)
        {
            Console.WriteLine("REGISTER " + username + " " + hash + " " + salt);
            dynamic obj = Database.AddUser(username, hash, salt);
            Database.AddDiginotes(username, 20);
            return JsonConvert.SerializeObject(obj != null);
        }
        public string GetBalance(string username)
        {
            Console.WriteLine("GET_BALANCE " + username);
            dynamic obj = Database.GetBalance(username);
            if (obj == null) return JsonConvert.SerializeObject(null);
            return JsonConvert.SerializeObject(obj);
        }
        public string GetDiginotes(string username)
        {
            Console.WriteLine("GET_DIGINOTES " + username);
            dynamic obj = Database.GetDiginotes(username);
            if (obj == null) return JsonConvert.SerializeObject(null);
            return JsonConvert.SerializeObject(obj);
        }
        public string GetQuote()
        {
            string ret = "";
            return ret;
        }
        public string GetBuyOrders(string username)
        {
            Console.WriteLine("GET_BUY_ORDERS " + username);
            return JsonConvert.SerializeObject(Database.GetBuyOrders(username));
        }
        public string GetSellOrders(string username)
        {
            Console.WriteLine("GET_SELL_ORDERS " + username);
            return JsonConvert.SerializeObject(Database.GetSellOrders(username));
        }
        public String GetBuyOrder(int id)
        {
            /* Still haven't needed this ...*/
            return null;
        }
        public String GetSellOrder(int id)
        {
            /* Still haven't needed this ...*/
            return null;
        }
        public string AddBuyOrder(string username, int amount, double price)
        {   
            Console.WriteLine("ADD_BUY_ORDER "+ username + " " + amount + " "+ price);

            if (amount <= 0 || price <= 0.0) return JsonConvert.SerializeObject(null);

            dynamic user = Database.GetUser(username);
            if (user == null) return JsonConvert.SerializeObject(user);
            double balance = user.balance - price*amount;
            List<object> buy_orders = Database.GetBuyOrders(username);
            foreach (dynamic buy_order in buy_orders) balance -= buy_order.price * buy_order.amount;
            if (balance < 0.0) return JsonConvert.SerializeObject(null);

            int remaining = DoBuyOrder(username, amount, price);
            /* Order is added to the database if it isn't fullfilled */
            if(remaining > 0)
            {
                dynamic res = Database.AddBuyOrder(username, remaining, price);
                if (res == null) return JsonConvert.SerializeObject(null);
            }
            return JsonConvert.SerializeObject(new { balance = balance });
        }
        public string AddSellOrder(string username, int amount, double price)
        {
            Console.WriteLine("ADD_SELL_ORDER " + username + " " + amount + " " + price);
            // TODO Check the queues to fullfill sell order

            // TODO Add the sell order to queue if it can't be fullfilled

            /* Order is added to the database because it isn't fullfilled */
            Console.WriteLine("ADD_SELL_ORDER " + username + " " + amount + " " + price);
            if (amount <= 0 || price <= 0.0) return JsonConvert.SerializeObject(null);
            dynamic user = Database.GetUser(username);
            if (user == null) return JsonConvert.SerializeObject(null);
            List<dynamic> sell_orders = Database.GetSellOrders(username);
            int diginotes = (int)Database.GetDiginotes(username).diginotes - amount;
            foreach (dynamic sell_order in sell_orders) diginotes -= amount;
            if (amount < 0) return JsonConvert.SerializeObject(null);

            int remaining = DoSellOrder(username, amount, price);
            if (remaining > 0)
            {
                dynamic res = Database.AddSellOrder(username, remaining, price);
                if (res == null) return JsonConvert.SerializeObject(null);
            }
            return JsonConvert.SerializeObject(new { diginotes = diginotes });
        }
        private int DoBuyOrder(string username, int amount, double price)
        {
            //Remaining diginotes to buy, starts at amount in buy order
            int remainder = amount;
            //Tries to find a sell order that satisfies the buy order, either partially or fully
            dynamic order;
            while (((order = Database.GetBestSellOrder(username, remainder, price)) != null) && remainder > 0)
            {
                //Max amount of diginotes to be transfered, 
                //Equal to the minimum between order amount and amount in sell order
                int maxamount = System.Math.Min(remainder, order.amount);
                //Transfer diginotes from seller to buyer at seller's price(lower or equal than buyers price always)
                dynamic res = Database.TransferDiginotes(order.user, username, maxamount, order.price);
                //If an error ocurred stop execution
                if (res != 1) return remainder;
                //If the sell order was fully satisfied, remove it from the db
                if (maxamount == order.amount)
                {
                    Database.RemoveSellOrder(order.id);
                }
                //If the sell order was only partially satisfied, reduce the amount it's selling
                else
                {
                    Database.EditSellOrder(order.id, order.amount - amount, order.price);
                }
                //Remaining diginotes to buy reduced by the amount sold in this iteration
                remainder -= maxamount;
            }
            //Returns number of diginotes that couldn't be satisfied
            return remainder;
        }
        private int DoSellOrder(string username, int amount, double price)
        {
            //Remaining diginotes to sell, starts at amount in sell order
            int remainder = amount;
            //Tries to find a buy order that satisfies the sell order, either partially or fully
            dynamic order;
            while (((order = Database.GetBestBuyOrder(username, remainder, price)) != null) && remainder > 0)
            {
                System.Console.WriteLine("GOT HERE BOY");
                //Max amount of diginotes to be transfered, 
                //Equal to the minimum between order amount and amount in sell order
                int maxamount = System.Math.Min(remainder, order.amount);
                //Transfer diginotes from seller to buyer at buyer's price(lower or equal than seller price always)
                dynamic res = Database.TransferDiginotes(order.user, username, maxamount, order.price);
                //If an error ocurred stop execution
                if (res != 1) return remainder;
                //If the buy order was fully satisfied, remove it from the db
                if (maxamount == order.amount)
                {
                    Database.RemoveBuyOrder(order.id);
                }
                //If the buy order was only partially satisfied, reduce the amount it's selling
                else
                {
                    Database.EditBuyOrder(order.id, order.amount - amount, order.price);
                }
                //Remaining diginotes to buy reduced by the amount sold in this iteration
                remainder -= maxamount;
            }
            //Returns number of diginotes that couldn't be satisfied
            return remainder;
        }
        public string EditBuyOrder(int id, int amount, double price)
        {
            Console.WriteLine("EDIT_BUY_ORDER " + id + " " + amount + " " + price);

            if (amount <= 0 || price <= 0.0) return JsonConvert.SerializeObject(null);
            dynamic order = Database.GetBuyOrder(id);
            if (order == null) return JsonConvert.SerializeObject(null);
            string username = order.user;
            dynamic user = Database.GetUser(username);
            if (user == null) return JsonConvert.SerializeObject(user);
            double balance = user.balance - price + order.price;
            List<object> buy_orders = Database.GetBuyOrders(username);
            foreach (dynamic buy_order in buy_orders) balance -= buy_order.price;
            if (balance < 0.0) return JsonConvert.SerializeObject(null);

            int remaining = DoBuyOrder(username, amount, price);
            if(remaining > 0)
            {
                dynamic res = Database.EditBuyOrder(id, remaining, price);
                if (res == null) return JsonConvert.SerializeObject(null);
            }else if(remaining == 0)
            {
                dynamic res = Database.RemoveBuyOrder(id);
                if (res == null) return JsonConvert.SerializeObject(null);
            }
            return JsonConvert.SerializeObject(new { balance = balance });
        }
        public string EditSellOrder(int id, int amount, double price)
        {
            Console.WriteLine("EDIT_SELL_ORDER " + id + " " + amount + " " + price);

            if (amount <= 0 || price <= 0.0) return JsonConvert.SerializeObject(null);
            dynamic order = Database.GetSellOrder(id);
            if (order == null) return JsonConvert.SerializeObject(null);
            string username = order.user;
            dynamic user = Database.GetUser(username);
            if (user == null) return JsonConvert.SerializeObject(user);
            int diginotes = (int)Database.GetDiginotes(username).diginotes;
            diginotes += order.amount - amount;
            List<dynamic> sell_orders = Database.GetSellOrders(username);
            foreach (dynamic sell_order in sell_orders) diginotes -= sell_order.amount;
            if (amount < 0) return JsonConvert.SerializeObject(null);

            int remaining = DoSellOrder(username, amount, price);

            if(remaining > 0)
            {
                dynamic res = Database.EditSellOrder(id, amount, price);
                if (res == null) return JsonConvert.SerializeObject(null);
            }else if(remaining == 0)
            {
                dynamic res = Database.RemoveSellOrder(id);
                if (res == null) return JsonConvert.SerializeObject(null);
            }
            return JsonConvert.SerializeObject(new { diginotes = diginotes });
        }
        public String RemoveBuyOrder(int id)
        {
            Console.WriteLine("REMOVE_BUY_ORDER " + " " + id);

            dynamic res = Database.RemoveBuyOrder(id);
            return JsonConvert.SerializeObject(res);
        }
        public String RemoveSellOrder(int id)
        {
            Console.WriteLine("REMOVE_SELL_ORDER " + " " + id);

            dynamic res = Database.RemoveSellOrder(id);
            return JsonConvert.SerializeObject(res);
        }
        public string DepositBalance(string username, double amount)
        {
            Console.WriteLine("DEPOSIT " + username + " " + amount);
            if (amount <= 0.0) return JsonConvert.SerializeObject(null);
            dynamic obj = Database.GetUser(username);
            if (obj == null) return JsonConvert.SerializeObject(null);
            double balance = obj.balance + amount;
            obj = Database.SetBalance(username, balance);
            if (obj == null) return JsonConvert.SerializeObject(null);
            return JsonConvert.SerializeObject(new { balance = balance });
        }
        public string WithdrawBalance(string username, double amount)
        {
            Console.WriteLine("WITHDRAW " + username + " " + amount);
            if (amount <= 0.0) return JsonConvert.SerializeObject(null);
            dynamic obj = Database.GetUser(username);
            if (obj == null) return JsonConvert.SerializeObject(null);
            if (obj.balance < amount) return JsonConvert.SerializeObject(null);
            double balance = obj.balance - amount;
            obj = Database.SetBalance(username, balance);
            if (obj == null) return JsonConvert.SerializeObject(null);
            return JsonConvert.SerializeObject(new { balance = balance });
        }
        public string GetSalt(string username)
        {
            Console.WriteLine("GET_SALT " + username);
            dynamic user = Database.GetUser(username);
            if (user == null) return JsonConvert.SerializeObject(null);
            return JsonConvert.SerializeObject(new { salt = user.salt });
        }       
    }
}
