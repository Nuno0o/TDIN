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
        public string GetDiginotesList(string username)
        {
            return null;
        }
        public string GetTransactions(string username)
        {
            return null;
        }
        public string GetBuyOrders(string username)
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
