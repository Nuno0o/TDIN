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
            return null;
        }
        public String GetSellOrder(int id)
        {
            return null;
        }
        public string AddBuyOrder(string username, int amount, double price)
        {   
            Console.WriteLine("ADD_BUY_ORDER "+ username + " " + amount + " "+ price);
            if (amount <= 0 || price <= 0.0) return JsonConvert.SerializeObject(null);
            dynamic user = Database.GetUser(username);
            if (user == null) return JsonConvert.SerializeObject(user);
            double balance = user.balance - price;
            List<object> buy_orders = Database.GetBuyOrders(username);
            foreach (dynamic buy_order in buy_orders) balance -= buy_order.price;
            if (balance < 0.0) return JsonConvert.SerializeObject(null);
            dynamic res = Database.AddBuyOrder(username, amount, price);
            if (res == null) return JsonConvert.SerializeObject(null);            
            return JsonConvert.SerializeObject(new { balance = balance });
        }
        public string AddSellOrder(string username, int amount, double price)
        {
            Console.WriteLine("ADD_SELL_ORDER " + username + " " + amount + " " + price);
            if (amount <= 0 || price <= 0.0) return JsonConvert.SerializeObject(null);
            dynamic user = Database.GetUser(username);
            if (user == null) return JsonConvert.SerializeObject(null);
            List<dynamic> sell_orders = Database.GetSellOrders(username);
            int diginotes = (int)Database.GetDiginotes(username).diginotes - amount;
            foreach (dynamic sell_order in sell_orders) diginotes -= amount;
            if (amount < 0) return JsonConvert.SerializeObject(null);
            dynamic res = Database.AddSellOrder(username, amount, price);
            if (res == null) return JsonConvert.SerializeObject(null);           
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
            if(obj == null) return JsonConvert.SerializeObject(null);
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
            return JsonConvert.SerializeObject(new {balance = balance});
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
            dynamic res = Database.EditBuyOrder(id, amount, price);
            if (res == null) return JsonConvert.SerializeObject(null);
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
            dynamic res = Database.EditSellOrder(id, amount, price);
            if (res == null) return JsonConvert.SerializeObject(null);
            return JsonConvert.SerializeObject(new { diginotes = diginotes });
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
