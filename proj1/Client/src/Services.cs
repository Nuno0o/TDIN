﻿using System;

namespace Server
{
    class Services : MarshalByRefObject
    {        
        public string Login(string username, string hash)
        {
            return null;
        }
        public string Register(string username, string hash, string salt)
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
        public string GetQuotes(int limit = 1)
        {
            return null;
        }
        public string SetQuote(string username, double value)
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
        public string AddBuyOrder(string username, int amount)
        {
            return null;
        }
        public string AddSellOrder(string username, int amount)
        {
            return null;
        }
        public string ActivateBuyOrder(string username,int id, int amount)
        {
            return null;
        }
        public string ActivateSellOrder(string username,int id, int amount)
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
        public string DepositBalance(string username, double amount)
        {
            return null;
        }
        public string WithdrawBalance(string username, double amount)
        {
            return null;
        }
        public string GetSalt(string username)
        {
            return null;
        }
    }
}
