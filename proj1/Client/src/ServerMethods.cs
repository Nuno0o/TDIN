using System;
using System.Collections.Generic;

namespace Server
{
    class RemObj : MarshalByRefObject
    {        
        public string Login(string username, string password)
        {
            return null;
        }
        public string Register(string username, string password)
        {
            return null;
        }
        public string GetBalance(string username)
        {
            return null;
        }
        public string GetDiginotes(string username)
        {
            return null;
        }
        public string GetBuyOrders(string username)
        {
            return null;
        }
        public string GetSellOrders(string username)
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
        public string AddBuyOrder(string username, int amount, double price)
        {
            return null;
        }
        public string AddSellOrder(string username, int amount, double price)
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
        public string EditBuyOrder(int id, int amount, double price)
        {
            return null;
        }
        public string EditSellOrder(int id, int amount, double price)
        {
            return null;
        }
        public string DepositBalance(string username, double amount)
        {
            return null;
        }
        public string WithdrawBalance(string username, double amount)
        {
            return null;
        }
    }
}
