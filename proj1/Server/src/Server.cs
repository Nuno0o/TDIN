using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels;


namespace Server {
  
    class Server
    {
        static void Main(string[] args)
        {
            Database.Init(true);         

            TcpChannel chan = new TcpChannel(9000);
            ChannelServices.RegisterChannel(chan, false);
            RemotingConfiguration.ApplicationName = "Server";
            RemotingConfiguration.RegisterWellKnownServiceType(
                typeof(Services),
                "Services",
                WellKnownObjectMode.Singleton
            );            

            Console.WriteLine("Press ENTER to exit ...");
            Console.ReadLine();           
        }
        public static int DoBuyOrder(string username, int amount, double quote)
        {
            //Remaining diginotes to buy, starts at amount in buy order
            int remainder = amount;
            //Tries to find a sell order that satisfies the buy order, either partially or fully
            dynamic order;
            while (((order = Database.GetBestSellOrder(username, remainder)) != null) && remainder > 0)
            {
                //Max amount of diginotes to be transfered, 
                //Equal to the minimum between order amount and amount in sell order
                int maxamount = System.Math.Min(remainder, order.amount);
                //Transfer diginotes from seller to buyer at seller's price(lower or equal than buyers price always)
                dynamic res = Database.TransferDiginotes(order.user, username, maxamount, quote);
                //If an error ocurred stop execution
                if (res == null) return remainder;
                if (res.rows == -1) return remainder;
                //If the sell order was fully satisfied, remove it from the db
                if (maxamount == order.amount)
                {
                    Database.RemoveSellOrder(order.id);
                }
                //If the sell order was only partially satisfied, reduce the amount it's selling
                else
                {
                    Database.EditSellOrder(order.id, order.amount - amount);
                }
                //Remaining diginotes to buy reduced by the amount sold in this iteration
                remainder -= maxamount;
            }
            //Returns number of diginotes that couldn't be satisfied
            return remainder;
        }
        public static int DoSellOrder(string username, int amount, double quote)
        {
            //Remaining diginotes to sell, starts at amount in sell order
            int remainder = amount;
            //Tries to find a buy order that satisfies the sell order, either partially or fully
            dynamic order;
            while (((order = Database.GetBestBuyOrder(username, remainder)) != null) && remainder > 0)
            {
                //Max amount of diginotes to be transfered, 
                //Equal to the minimum between order amount and amount in sell order
                int maxamount = System.Math.Min(remainder, order.amount);
                //Transfer diginotes from seller to buyer at buyer's price(lower or equal than seller price always)
                dynamic res = Database.TransferDiginotes(username, order.user, maxamount, quote);
                //If an error ocurred stop execution
                if (res == null) return remainder;
                if (res.rows == -1) return remainder;
                //If the buy order was fully satisfied, remove it from the db
                if (maxamount == order.amount)
                {
                    Database.RemoveBuyOrder(order.id);
                }
                //If the buy order was only partially satisfied, reduce the amount it's selling
                else
                {
                    Database.EditBuyOrder(order.id, order.amount - amount);
                }
                //Remaining diginotes to buy reduced by the amount sold in this iteration
                remainder -= maxamount;
            }
            //Returns number of diginotes that couldn't be satisfied
            return remainder;
        }
    }    
}
