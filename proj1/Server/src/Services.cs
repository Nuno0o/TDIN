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
        public string GetQuotes(int n)
        {
            return JsonConvert.SerializeObject(Database.GetQuotes(n));
        }
        public string SetQuote(string username, double value)
        {
            Console.WriteLine("SET_QUOTE " + username + " " + value);
            if (value < 0) return null;
            return JsonConvert.SerializeObject(Database.SetQuote(value, username));
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
        public string AddBuyOrder(string username, int amount)
        {   
            Console.WriteLine("ADD_BUY_ORDER "+ username + " " + amount);

            if (amount <= 0) return JsonConvert.SerializeObject(null);

            dynamic user = Database.GetUser(username);
            double quote = Database.GetQuotes(1)[0].value;

            if (user == null) return JsonConvert.SerializeObject(user);

            int remaining = DoBuyOrder(username, amount, quote);
            /* Order is added to the database if it isn't fullfilled */
            if(remaining > 0)
            {
                dynamic res = Database.AddBuyOrder(username, remaining);
                if (res == null) return JsonConvert.SerializeObject(null);
            }
            return JsonConvert.SerializeObject(new { remaining = remaining });
        }
        public string AddSellOrder(string username, int amount)
        {
            Console.WriteLine("ADD_SELL_ORDER " + username + " " + amount);

            if (amount <= 0) return JsonConvert.SerializeObject(null);

            dynamic user = Database.GetUser(username);
            double quote = Database.GetQuotes(1)[0].value;

            if (user == null) return JsonConvert.SerializeObject(null);
            List<dynamic> sell_orders = Database.GetSellOrders(username);
            int diginotes = (int)Database.GetDiginotes(username).diginotes - amount;
            foreach (dynamic sell_order in sell_orders) diginotes -= amount;
            if (amount < 0) return JsonConvert.SerializeObject(null);

            int remaining = DoSellOrder(username, amount, quote);
            if (remaining > 0)
            {
                dynamic res = Database.AddSellOrder(username, remaining);
                if (res == null) return JsonConvert.SerializeObject(null);
            }
            return JsonConvert.SerializeObject(new { remaining = remaining });
        }
        private int DoBuyOrder(string username, int amount, double quote)
        {
            //Remaining diginotes to buy, starts at amount in buy order
            int remainder = amount;
            //Tries to find a sell order that satisfies the buy order, either partially or fully
            dynamic order;
            while (((order = Database.GetBestSellOrder(username, remainder)) != null) && remainder > 0)
            {
                //Max amount of diginotes to be transfered, 
                //Equal to the minimum between order amount and amount in sell order
                int maxamount = System.Math.Min(remainder, order.amount);
                //Transfer diginotes from seller to buyer at seller's price(lower or equal than buyers price always)
                dynamic res = Database.TransferDiginotes(order.user, username, maxamount, quote);
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
                    Database.EditSellOrder(order.id, order.amount - amount);
                }
                //Remaining diginotes to buy reduced by the amount sold in this iteration
                remainder -= maxamount;
            }
            //Returns number of diginotes that couldn't be satisfied
            return remainder;
        }
        private int DoSellOrder(string username, int amount, double quote)
        {
            //Remaining diginotes to sell, starts at amount in sell order
            int remainder = amount;
            //Tries to find a buy order that satisfies the sell order, either partially or fully
            dynamic order;
            while (((order = Database.GetBestBuyOrder(username, remainder)) != null) && remainder > 0)
            {
                //Max amount of diginotes to be transfered, 
                //Equal to the minimum between order amount and amount in sell order
                int maxamount = System.Math.Min(remainder, order.amount);
                //Transfer diginotes from seller to buyer at buyer's price(lower or equal than seller price always)
                dynamic res = Database.TransferDiginotes(username, order.user, maxamount, quote);
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
                    Database.EditBuyOrder(order.id, order.amount - amount);
                }
                //Remaining diginotes to buy reduced by the amount sold in this iteration
                remainder -= maxamount;
            }
            //Returns number of diginotes that couldn't be satisfied
            return remainder;
        }
        public string ActivateBuyOrder(string username,int id, int amount)
        {
            double quote = Database.GetQuotes(1)[0].value;

            int remaining = DoBuyOrder(username, amount, quote);
            /* Order is kept in the database if it isn't fullfilled */
            if (remaining > 0)
            {
                dynamic res = Database.EditBuyOrder(id, remaining,1);
                if (res == null) return JsonConvert.SerializeObject(null);
            }else if(remaining == 0)
            {
                dynamic res = Database.RemoveBuyOrder(id);
                if (res == null) return JsonConvert.SerializeObject(null);
            }
            return JsonConvert.SerializeObject(new { remaining = remaining });
        }
        public string ActivateSellOrder(string username,int id, int amount)
        {
            double quote = Database.GetQuotes(1)[0].value;

            int remaining = DoSellOrder(username, amount, quote);
            /* Order is kept in the database if it isn't fullfilled */
            if (remaining > 0)
            {
                dynamic res = Database.EditSellOrder(id, remaining, 1);
                if (res == null) return JsonConvert.SerializeObject(null);
            }
            else if (remaining == 0)
            {
                dynamic res = Database.RemoveSellOrder(id);
                if (res == null) return JsonConvert.SerializeObject(null);
            }
            return JsonConvert.SerializeObject(new { remaining = remaining });
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
