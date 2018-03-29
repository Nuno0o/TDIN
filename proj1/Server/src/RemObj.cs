using System;

namespace Server
{
    class RemObj : MarshalByRefObject
    {        
        public Boolean Login(string username, string password)
        {
            Console.WriteLine("LOGIN "+username+" "+password);
            dynamic user = Database.GetUser(username);
            if (user == null) return false;
            return user.password == password;
        }
        public Boolean Register(string username, string password)
        {
            Console.WriteLine("REGISTER " + username + " " + password);
            dynamic added = Database.AddUser(username, password);
            return added == 1;
        }
        public String GetBalance()
        {
            return null;
        }
        public String GetDigicoins()
        {
            return null;
        }
        public String GetBuyOrders()
        {
            return null;
        }
        public String GetSellOrders()
        {
            return null;
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
