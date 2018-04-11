using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Lifetime;

namespace Server
{
    class Services : MarshalByRefObject
    {
        private static readonly string NULL = JsonConvert.SerializeObject(null);

        public override object InitializeLifetimeService()
        {
            ILease lease = (ILease)InitializeLifetimeService();
            // Normally, the initial lease time would be much longer.
            // It is shortened here for demonstration purposes.
            if (lease.CurrentState == LeaseState.Initial)
            {
                lease.InitialLeaseTime = TimeSpan.FromMinutes(15);
                lease.RenewOnCallTime = TimeSpan.FromMinutes(5);
                //lease.SponsorshipTimeout = TimeSpan.FromSeconds(10);
            }
            return lease;
        }
        public string Login(string username, string hash)
        {
            Console.WriteLine("LOGIN "+username+" "+ hash);
            dynamic obj = Database.GetUser(username);

            if (obj == null) return NULL;
            if (obj.hash != hash) return NULL;

            string token = null;
            do { token = Server.Salt(); }
            while (Server.clients.ContainsValue(token));

            Server.clients.Add(token, username);

            return JsonConvert.SerializeObject(new { token = token });
        }
        public string Register(string username, string hash, string salt)
        {
            Console.WriteLine("REGISTER " + username + " " + hash + " " + salt);
            dynamic obj = Database.AddUser(username, hash, salt);
            if(obj == null) return JsonConvert.SerializeObject(obj != null);
            Database.AddDiginotes(username, 20);

            string token = null;
            do { token = Server.Salt(); }
            while (Server.clients.ContainsValue(token));

            Server.clients.Add(token, username);

            return JsonConvert.SerializeObject(new { token = token });
        }
        public string GetBalance(string token)
        {
            if (!Server.clients.ContainsKey(token)) return NULL;
            string username = (string)Server.clients[token];

            Console.WriteLine("GET_BALANCE " + username);
            dynamic obj = Database.GetBalance(username);
            if (obj == null) return NULL;
            return JsonConvert.SerializeObject(obj);
        }
        public string GetDiginotes(string token)
        {
            if (!Server.clients.ContainsKey(token)) return NULL;
            string username = (string)Server.clients[token];

            Console.WriteLine("GET_DIGINOTES " + username);
            dynamic obj = Database.GetDiginotes(username);
            if (obj == null) return NULL;
            return JsonConvert.SerializeObject(obj);
        }
        public string GetQuotes(int limit = 1)
        {
            Console.WriteLine("GET_QUOTES " + limit);
            return JsonConvert.SerializeObject(Database.GetQuotes(limit));
        }
        public string SetQuote(string token, double value)
        {
            if (!Server.clients.ContainsKey(token)) return NULL;
            string username = (string)Server.clients[token];

            Console.WriteLine("SET_QUOTE " + username + " " + value);
            if (value < 0) return null;
            return JsonConvert.SerializeObject(Database.SetQuote(value, username));
        }
        public string GetDiginotesList(string username)
        {
            Console.WriteLine("GET DIGINOTE LIST " + username);
            return JsonConvert.SerializeObject(Database.GetDiginotesList(username));
        }
        public string GetTransactions(string username)
        {
            Console.WriteLine("GET DIGINOTE LIST " + username);
            return JsonConvert.SerializeObject(Database.GetTransactions(username));
        }
        public string GetBuyOrders(string username)
        {
            if (!Server.clients.ContainsKey(token)) return NULL;
            string username = (string)Server.clients[token];

            Console.WriteLine("GET_BUY_ORDERS " + username);
            return JsonConvert.SerializeObject(Database.GetBuyOrders(username));
        }
        public string GetSellOrders(string token)
        {
            if (!Server.clients.ContainsKey(token)) return NULL;
            string username = (string)Server.clients[token];

            Console.WriteLine("GET_SELL_ORDERS " + username);
            return JsonConvert.SerializeObject(Database.GetSellOrders(username));
        }      
        public string AddBuyOrder(string token, int amount)
        {
            if (!Server.clients.ContainsKey(token)) return NULL;
            string username = (string)Server.clients[token];

            Console.WriteLine("ADD_BUY_ORDER "+ username + " " + amount);

            if (amount <= 0) return NULL;

            dynamic user = Database.GetUser(username);
            double quote = Database.GetQuotes(1)[0].value;

            if (user == null) return JsonConvert.SerializeObject(user);

            int remaining = Server.DoBuyOrder(username, amount, quote);
            /* Order is added to the database if it isn't fullfilled */
            if(remaining > 0)
            {
                dynamic res = Database.AddBuyOrder(username, remaining);
                if (res == null) return NULL;
            }
            return JsonConvert.SerializeObject(new { remaining = remaining });
        }
        public string AddSellOrder(string token, int amount)
        {
            if (!Server.clients.ContainsKey(token)) return NULL;
            string username = (string)Server.clients[token];

            Console.WriteLine("ADD_SELL_ORDER " + username + " " + amount);

            if (amount <= 0) return NULL;

            dynamic user = Database.GetUser(username);
            double quote = Database.GetQuotes(1)[0].value;

            if (user == null) return NULL;
            List<dynamic> sell_orders = Database.GetSellOrders(username);
            int diginotes = (int)Database.GetDiginotes(username).diginotes - amount;
            foreach (dynamic sell_order in sell_orders) diginotes -= amount;
            if (amount < 0) return NULL;

            int remaining = Server.DoSellOrder(username, amount, quote);
            if (remaining > 0)
            {
                dynamic res = Database.AddSellOrder(username, remaining);
                if (res == null) return NULL;
            }
            return JsonConvert.SerializeObject(new { remaining = remaining });
        }        
        public string ActivateBuyOrder(string token, int id, int amount)
        {
            if (!Server.clients.ContainsKey(token)) return NULL;
            string username = (string)Server.clients[token];

            Console.WriteLine("ACTIVATE_BUY_ORDER " + username + " " + id + " " + amount);

            double quote = Database.GetQuotes(1)[0].value;

            int remaining = Server.DoBuyOrder(username, amount, quote);
            /* Order is kept in the database if it isn't fullfilled */
            if (remaining > 0)
            {
                dynamic res = Database.EditBuyOrder(id, remaining,1);
                if (res == null) return NULL;
            }else if(remaining == 0)
            {
                dynamic res = Database.RemoveBuyOrder(id);
                if (res == null) return NULL;
            }
            return JsonConvert.SerializeObject(new { remaining = remaining });
        }
        public string ActivateSellOrder(string token, int id, int amount)
        {
            if (!Server.clients.ContainsKey(token)) return NULL;
            string username = (string)Server.clients[token];

            Console.WriteLine("ACTIVATE_SELL_ORDER " + username + " " + id + " " + amount);
            double quote = Database.GetQuotes(1)[0].value;

            int remaining = Server.DoSellOrder(username, amount, quote);
            /* Order is kept in the database if it isn't fullfilled */
            if (remaining > 0)
            {
                dynamic res = Database.EditSellOrder(id, remaining, 1);
                if (res == null) return NULL;
            }
            else if (remaining == 0)
            {
                dynamic res = Database.RemoveSellOrder(id);
                if (res == null) return NULL;
            }
            return JsonConvert.SerializeObject(new { remaining = remaining });
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
        public string DepositBalance(string token, double amount)
        {
            if (!Server.clients.ContainsKey(token)) return NULL;
            string username = (string)Server.clients[token];

            Console.WriteLine("DEPOSIT " + username + " " + amount);
            if (amount <= 0.0) return NULL;
            dynamic obj = Database.GetUser(username);
            if (obj == null) return NULL;
            double balance = obj.balance + amount;
            obj = Database.SetBalance(username, balance);
            if (obj == null) return NULL;
            return JsonConvert.SerializeObject(new { balance = balance });
        }
        public string WithdrawBalance(string token, double amount)
        {
            if (!Server.clients.ContainsKey(token)) return NULL;
            string username = (string)Server.clients[token];

            Console.WriteLine("WITHDRAW " + username + " " + amount);
            if (amount <= 0.0) return NULL;
            dynamic obj = Database.GetUser(username);
            if (obj == null) return NULL;
            if (obj.balance < amount) return NULL;
            double balance = obj.balance - amount;
            obj = Database.SetBalance(username, balance);
            if (obj == null) return NULL;
            return JsonConvert.SerializeObject(new { balance = balance });
        }
        public string GetSalt(string username)
        {
            Console.WriteLine("GET_SALT " + username);
            dynamic user = Database.GetUser(username);
            if (user == null) return NULL;
            return JsonConvert.SerializeObject(new { salt = user.salt });
        }       
    }
}
