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
        public Object GetBuyOrder(int id)
        {
            return null;
        }
        public Object GetSellOrder(int id)
        {
            return null;
        }
        public Object AddBuyOrder(int amount, double price)
        {
            return null;
        }
        public Object AddSellOrder(int amount, double price)
        {
            return null;
        }
        public Object RemoveBuyOrder(int id)
        {
            return null;
        }
        public Object RemoveSellOrder(int id)
        {
            return null;
        }
        public Object DepositBalance(double amount)
        {
            return null;
        }
        public Object WhitdrawBalance(double amount)
        {
            return null;
        }
    }
}
