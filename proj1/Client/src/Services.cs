using System;

namespace Server
{
    class Services : MarshalByRefObject
    {        
        public string Login(string token, string hash)
        {
            return null;
        }
        public string Register(string token, string hash, string salt)
        {
            return null;
        }
        public string GetBalance(string token)
        {
            return null;
        }
        public string GetDiginotes(string token)
        {
            return null;
        }
        public string GetQuotes(int limit = 1)
        {
            return null;
        }
        public string SetQuote(string token, double value)
        {
            return null;
        }
<<<<<<< HEAD:proj1/Client/src/RemObj.cs
        public string GetBuyOrder(int id)
        {
            return null;
        }
        public string GetSellOrder(int id)
        {
            return null;
        }
        public string AddBuyOrder(string username,int amount, double price)
        {
            return null;
        }
        public string AddSellOrder(string username,int amount, double price)
        {
            return null;
        }
        public string RemoveBuyOrder(int id)
        {
            return null;
        }
        public string RemoveSellOrder(int id)
=======
        public string GetDiginotesList(string token)
        {
            return null;
        }
        public string GetTransactions(string token)

        {
            return null;
        }
        public string GetBuyOrders(string token)
        {
            return null;
        }
        public string GetSellOrders(string token)
        {
            return null;
        }
        public string GetBuyOrder(int id)
        {
            return null;
        }
        public string GetSellOrder(int id)
        {
            return null;
        }
        public string AddBuyOrder(string token, int amount)
        {
            return null;
        }
        public string AddSellOrder(string token, int amount)
        {
            return null;
        }
        public string ActivateBuyOrder(string token,int id, int amount)
        {
            return null;
        }
        public string ActivateSellOrder(string token,int id, int amount)
        {
            return null;
        }
        public string RemoveBuyOrder(int id)
        {
            return null;
        }
        public string RemoveSellOrder(int id)
        {
            return null;
        }
        public string DepositBalance(string token, double amount)
>>>>>>> final:proj1/Client/src/Services.cs
        {
            return null;
        }
        public string WithdrawBalance(string token, double amount)
        {
            return null;
        }
        public string GetSalt(string token)
        {
            return null;
        }
    }
}
