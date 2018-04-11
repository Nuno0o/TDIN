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
            if(obj == null) return JsonConvert.SerializeObject(obj != null);
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
        public string GetQuotes(int limit = 1)
        {
            Console.WriteLine("GET_QUOTES " + limit);
            return JsonConvert.SerializeObject(Database.GetQuotes(limit));
        }
        public string SetQuote(string username, double value)
        {
            Console.WriteLine("SET_QUOTE " + username + " " + value);
            if (value < 0) return null;
            return JsonConvert.SerializeObject(Database.SetQuote(value, username));
        }
        public string GetDiginotesList(string username)
        {
            Console.WriteLine("GET DIGINOTE LIST " + username);
            return JsonConvert.SerializeObject(Database.GetDiginotesList(username));
        }
        public string GetTransactions(string username)
        {
            Console.WriteLine("GET DIGINOTE LIST " + username);
            return JsonConvert.SerializeObject(Database.GetTransactions(username));
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
        public string AddBuyOrder(string username, int amount)
        {   
            Console.WriteLine("ADD_BUY_ORDER "+ username + " " + amount);

            if (amount <= 0) return JsonConvert.SerializeObject(null);

            dynamic user = Database.GetUser(username);
            double quote = Database.GetQuotes(1)[0].value;

            if (user == null) return JsonConvert.SerializeObject(user);

            int remaining = Server.DoBuyOrder(username, amount, quote);
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

            int remaining = Server.DoSellOrder(username, amount, quote);
            if (remaining > 0)
            {
                dynamic res = Database.AddSellOrder(username, remaining);
                if (res == null) return JsonConvert.SerializeObject(null);
            }
            return JsonConvert.SerializeObject(new { remaining = remaining });
        }        
        public string ActivateBuyOrder(string username,int id, int amount)
        {
            Console.WriteLine("ACTIVATE_BUY_ORDER " + username + " " + id + " " + amount);

            double quote = Database.GetQuotes(1)[0].value;

            int remaining = Server.DoBuyOrder(username, amount, quote);
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
            Console.WriteLine("ACTIVATE_SELL_ORDER " + username + " " + id + " " + amount);
            double quote = Database.GetQuotes(1)[0].value;

            int remaining = Server.DoSellOrder(username, amount, quote);
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
