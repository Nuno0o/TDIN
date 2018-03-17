using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Data.SQLite;

namespace TDIN.src.database
{
    class Database
    {
        public static void createDatabase()
        {
            SQLiteConnection.CreateFile("ola.sqlite");
        }
    }
}
