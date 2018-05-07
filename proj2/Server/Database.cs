using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Threading;

namespace Server
{

    class Database
    {

        private static SQLiteConnection conn;
        private static SQLiteCommand com;
        private static SQLiteTransaction trans;
        private static SQLiteDataReader reader;
        private static Mutex mut;

        public static readonly string DB_PATH = "../../db/db.sqlite";
        public static readonly string SQL_PATH = "../../db/db.sql";

        public static void Init(bool overwrite = false)
        {
            /* creates db file if it doesn't exist */
            if (!File.Exists(DB_PATH))
            {
                SQLiteConnection.CreateFile(DB_PATH);
                overwrite = true;
            }
            else if (overwrite)
            {
                File.Delete(DB_PATH);
            }

            /* created db connection*/
            conn = new SQLiteConnection("Data Source=" + DB_PATH + ";Version=3;foreign keys=true;");
            conn.Open();

            /* creates sql command for future use */
            com = new SQLiteCommand(conn);

            mut = new Mutex();

            /* if new db was created */
            if (overwrite)
            {

                /* executes sql script */
                try
                {
                    trans = conn.BeginTransaction();
                    /* reads sql script */
                    com.CommandText = File.ReadAllText(SQL_PATH);
                    com.ExecuteNonQuery();
                    trans.Commit();
                }
                catch (Exception e)
                {
                    trans.Rollback();
                    Console.WriteLine(e.ToString());
                }
            }
        }
        /*
         * Add new user to the database
         */
        public static dynamic AddUser(string username, string hash, string salt, double balance = 0.0)
        {
            mut.WaitOne();
            com.CommandText = "insert into User values (@user, @hash, @salt, @balance)";
            com.Parameters.Add(new SQLiteParameter("@user", username));
            com.Parameters.Add(new SQLiteParameter("@hash", hash));
            com.Parameters.Add(new SQLiteParameter("@salt", salt));
            com.Parameters.Add(new SQLiteParameter("@balance", balance));

            dynamic res;
            try
            {
                trans = conn.BeginTransaction();
                res = new { rows = com.ExecuteNonQuery() };
                trans.Commit();
            }
            catch (Exception e)
            {
                trans.Rollback();
                res = null;
                Console.WriteLine(e.ToString());
            }
            mut.ReleaseMutex();
            return res;
        }
        /*
         * Get balance of user
         */
        public static dynamic GetBalance(string username)
        {
            mut.WaitOne();

            com.CommandText =
                @"select balance from User
                where username = @username";
            com.Parameters.Add(new SQLiteParameter("@username", username));

            dynamic res;
            try
            {
                reader = com.ExecuteReader();
                reader.Read();
                res = new { balance = reader["balance"] };
            }
            catch (Exception e)
            {
                res = null;
                Console.WriteLine(e.ToString());
            }
            finally
            {
                if (reader != null) reader.Close();
            }
            mut.ReleaseMutex();
            return res;
        }
    }
}
