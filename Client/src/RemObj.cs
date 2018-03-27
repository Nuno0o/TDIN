using System;

namespace Client.Remoting
{
    class RemObj : MarshalByRefObject
    {
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
