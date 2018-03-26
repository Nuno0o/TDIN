using System;
using System.Runtime.Remoting;
using Server.Database;

namespace Server {

    class Server
    {
        static void Main(string[] args)
        {
            RemotingConfiguration.Configure("Server.exe.config",false);

            SQLiteDB.init();
            SQLiteDB.ClearDatabase();
            SQLiteDB.AddUser("joaquim", "batata");
            SQLiteDB.AddUser("joaquims", "batata");
            SQLiteDB.AddDiginotes("joaquim", 3);
            SQLiteDB.AddDiginotes("joaquims", 3);
            SQLiteDB.TransferDiginotes("joaquim", "joaquims", 3);
            Console.WriteLine(SQLiteDB.GetUserDiginotes("joaquims"));
            Console.ReadLine();
        }
    }
}
