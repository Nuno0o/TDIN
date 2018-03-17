using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDIN
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = System.IO.Directory.GetCurrentDirectory().Replace("\\bin\\Debug", "") + "/db/database.db";
            src.database.Database db = new src.database.Database(path);
            db.ClearDatabase();
            db.AddUser("joaquim", "batata");
            db.AddUser("joaquims", "batata");
            db.AddDiginotes("joaquim", 3);
            db.AddDiginotes("joaquims", 3);
            db.TransferDiginotes("joaquim", "joaquims", 3);
            Console.WriteLine(db.GetUserDiginotes("joaquims"));
            Console.ReadLine();
        }
    }
}
