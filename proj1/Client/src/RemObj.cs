using System;
using System.Collections.Generic;

namespace Server
{
    class RemObj : MarshalByRefObject
    {        
        public Boolean Login(string username, string password)
        {
            return false;
        }
        public Boolean Register(string username, string password)
        {
            return false;
        }
        public Object GetBalance()
        {
            return null;
        }
        public Object GetDigicoins()
        {
            return null;
        }
        public Object GetBuyOrders()
        {
            return null;
        }
        public Object GetSellOrders()
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
