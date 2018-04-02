using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Server
{
    class RemObj : MarshalByRefObject
    {        
        public string Login(string username, string password)
        {
            Console.WriteLine("LOGIN "+username+" "+password);
            dynamic obj = Database.GetUser(username);
            if (obj == null) return JsonConvert.SerializeObject(null);
            return JsonConvert.SerializeObject(obj.password == password);
        }
        public string Register(string username, string password)
        {
            Console.WriteLine("REGISTER " + username + " " + password);
            int added = Database.AddUser(username, password);
            return JsonConvert.SerializeObject(added == 1);
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
        public String AddBuyOrder(int amount, double price)
        {
            return null;
        }
        public String AddSellOrder(int amount, double price)
        {
            return null;
        }
        public String RemoveBuyOrder(int id)
        {
            return null;
        }
        public String RemoveSellOrder(int id)
        {
            return null;
        }
        public String DepositBalance(double amount)
        {
            return null;
        }
        public String WhitdrawBalance(double amount)
        {
            return null;
        }        
    }
}
