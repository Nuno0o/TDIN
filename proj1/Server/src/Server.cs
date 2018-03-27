using System;
using System.Runtime.Remoting;
using Server.Database;

namespace Server {

    class Server
    {
        static void Main(string[] args)
        {
            RemotingConfiguration.Configure("Server.exe.config",false);

            /* creates a new db */
            SQLiteDB.Init(true);

            SQLiteDB.AddUser("user1","1234",100.0);
            SQLiteDB.SetBalance("user1", 50.0);
            Console.WriteLine(SQLiteDB.GetUser("user1"));

            SQLiteDB.AddDiginotes("user1", 10);
            SQLiteDB.RemoveDiginotes("user1", 1);
            Console.WriteLine(SQLiteDB.GetDiginotes("user1"));

            SQLiteDB.AddUser("user2", "1234", 100.0);
            SQLiteDB.AddDiginotes("user2", 10);            

            SQLiteDB.AddBuyOrder("user2",3,30.0);
            SQLiteDB.AddBuyOrder("user2", 3, 40.0);
            SQLiteDB.RemoveBuyOrder(1);
            SQLiteDB.EditBuyOrder(2,2,50.0);

            SQLiteDB.AddSellOrder("user1", 3, 30.0);
            SQLiteDB.AddSellOrder("user1", 3, 40.0);
            SQLiteDB.RemoveSellOrder(1);
            SQLiteDB.EditSellOrder(2, 2, 50.0);

            Console.WriteLine(SQLiteDB.GetBuyOrders("user2"));
            Console.WriteLine(SQLiteDB.GetSellOrders("user1"));
            
            SQLiteDB.TransferDiginotes("user1","user2",3);
            Console.WriteLine(SQLiteDB.GetDiginotes("user2"));

            Console.WriteLine(SQLiteDB.ClearDatabase());

            Console.ReadLine();
        }
    }
}
