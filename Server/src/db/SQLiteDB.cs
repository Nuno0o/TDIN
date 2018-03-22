using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace Server.Database {

    class SQLiteDB {

        private static SQLiteConnection conn;
        private static SQLiteCommand com;
        private static SQLiteTransaction trans;
        private static SQLiteDataReader reader;

        public static readonly string DB_PATH = "../../db/db.sqlite";
        public static readonly string SQL_PATH = "../../db/db.sql";

        public static void init(bool overwrite = false)
        {
            /* creates db file if it doesn't exist */
            if (!File.Exists(DB_PATH))
            {
                SQLiteConnection.CreateFile(DB_PATH);
                overwrite = true;
            }

            /* created db connection*/
            conn = new SQLiteConnection("Data Source=" + DB_PATH + ";Version=3;foreign keys=true;");
            conn.Open();

            /* creates sql command for future use */
            com = new SQLiteCommand(conn);

            /* if new db was created */
            if (overwrite)
            {
                /* reads sql script */
                com.CommandText = File.ReadAllText(SQL_PATH);

                /* executes sql script */
                try { com.ExecuteNonQuery(); }
                catch (SQLiteException e) { Console.WriteLine(e.StackTrace); }
            }            
        }
        public static bool ClearDatabase()
        {
            try
            {               
                trans = conn.BeginTransaction();

                com.CommandText = "DELETE FROM User; DELETE FROM Diginote;";
                com.ExecuteNonQuery();

                trans.Commit();
               
                return true;
            }
            catch (Exception e){ return false; }

        }
        public static bool AddUser(string username, string password)
        {
            try
            {                
                com.CommandText = "INSERT INTO User(username,password,currency) VALUES (@user,@pass,0)";
                com.Parameters.Add(new SQLiteParameter("@user", username));
                com.Parameters.Add(new SQLiteParameter("@pass", password));
                return com.ExecuteNonQuery() == 1;                
            }catch(Exception e)
            {
                return false;
            }
        }
        public static bool AddCurrency(string username, int amount)
        {
            try
            {               
                com.CommandText = "UPDATE User SET currency = currency + @amm WHERE User.username = @user";
                com.Parameters.Add(new SQLiteParameter("@user", username));
                com.Parameters.Add(new SQLiteParameter("@amm", amount));
                return com.ExecuteNonQuery() == 1;                
            }
            catch (Exception e) { return false; }
        }
        public static bool AddDiginotes(string username, int amount)
        {
            try {
                int minId = 0;
               
                com.CommandText = "SELECT id FROM Diginote ORDER BY id DESC LIMIT 1";
                reader = com.ExecuteReader();
                if (reader.Read()) { minId = reader.GetInt32(0); }
                

                int currentId = minId+1;
                trans = conn.BeginTransaction();

                for(int i = 0; i < amount; i++)
                {
                   
                    com.CommandText = "INSERT INTO Diginote(id,owner) VALUES(@id,@user)";
                    com.Parameters.Add(new SQLiteParameter("@id", currentId));
                    com.Parameters.Add(new SQLiteParameter("@user", username));
                    com.ExecuteNonQuery();
                    
                    currentId++;
                }
                trans.Commit();
                
                return true;
            }
            catch (Exception e) { return false; }
        }
        public static bool TransferDiginotes(string source, string destiny, int amount)
        {
            try {
                List<int> diginotes = new List<int>();

                com.CommandText = "SELECT id FROM Diginote WHERE owner = @user ORDER BY id DESC LIMIT @amount";
                com.Parameters.Add(new SQLiteParameter("@user", source));
                com.Parameters.Add(new SQLiteParameter("@amount", amount));
                reader = com.ExecuteReader();
                while (reader.Read()) { diginotes.Add(reader.GetInt32(0)); }

                if (diginotes.Count < amount) return false;

                trans = conn.BeginTransaction();
                foreach (int diginote in diginotes)
                {
                    com.CommandText = "UPDATE Diginote SET owner=@user WHERE Diginote.id=@diginote";
                    com.Parameters.Add(new SQLiteParameter("@user", destiny));
                    com.Parameters.Add(new SQLiteParameter("@diginote", diginote));
                    com.ExecuteNonQuery();
                }
                trans.Commit();

                return true;
            }
            catch (Exception e) { return false; }
        }
        public static int GetUserDiginotes(string username) {
            try {                
                com.CommandText = "SELECT COUNT(*) FROM Diginote WHERE Diginote.owner = @username";
                com.Parameters.Add(new SQLiteParameter("@username", username));
                reader = com.ExecuteReader();
                if (reader.Read()) { return reader.GetInt32(0); }
                return 0;                
            }
            catch (Exception e) { return 0; }
        }
    }

}
