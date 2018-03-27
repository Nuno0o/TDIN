using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace Server.Database {

    class SQLiteDB
    {

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

                com.CommandText =
                    @"DELETE FROM User;
                    DELETE FROM Diginote;
                    DELETE FROM BuyOrder;
                    DELETE FROM SellOrder;";
                com.ExecuteNonQuery();

                trans.Commit();               
                return true;
            }
            catch (Exception e){ return false; }

        }
        public static bool AddUser(string username, string password, double balance = 0.0)
        {
            try
            {                
                com.CommandText =
                    @"INSERT INTO User(username,password,currency)
                    VALUES (@user,@pass,@balance)";
                com.Parameters.Add(new SQLiteParameter("@user", username));
                com.Parameters.Add(new SQLiteParameter("@pass", password));
                com.Parameters.Add(new SQLiteParameter("@pass", balance.ToString()));
                return com.ExecuteNonQuery() == 1;                
            }catch(Exception e)
            {
                return false;
            }
        }
        public static bool SetBalance(string username, double balance = 0.0)
        {
            try
            {               
                com.CommandText =
                    @"UPDATE User SET balance = @amount
                    WHERE User.username = @user";
                com.Parameters.Add(new SQLiteParameter("@user", username));
                com.Parameters.Add(new SQLiteParameter("@balance", balance.ToString()));
                return com.ExecuteNonQuery() == 1;                
            }
            catch (Exception e) { return false; }
        }
        public static bool GetBalance(string username)
        {
            try
            {
                com.CommandText = "SELECT balance FROM User WHERE username = @user";
                com.Parameters.Add(new SQLiteParameter("@user", username));
                return com.ExecuteNonQuery() == 1;
            }
            catch (Exception e) { return false; }
        }
        public static bool AddDiginotes(string username, int amount = 1)
        {
            try {                
                trans = conn.BeginTransaction();
                com.CommandText = "";
                for (int i = 0; i < amount; i++)
                {                   
                    com.CommandText += "INSERT INTO Diginote(id,owner) VALUES(NULL,@user);";                                      
                }
                com.Parameters.Add(new SQLiteParameter("@user", username));
                com.ExecuteNonQuery();
                trans.Commit();
                
                return true;
            } catch (Exception e) { return false; }
        }
        public static bool RemoveDiginotes(string user, int amount = 1)
        {
            try {
                trans = conn.BeginTransaction();

                com.CommandText =
                    @"delete from Diginote where id in
                    (select id from Diginote where owner = @user limit @amount)";
                com.Parameters.Add(new SQLiteParameter("@user", user));
                com.Parameters.Add(new SQLiteParameter("@amount", amount.ToString()));
                com.ExecuteNonQuery();
                
                trans.Commit();
                return true;
            } catch (Exception e) { return false; }
        }
        public static bool TransferDiginotes(string from, string to, int amount = 1)
        {
            try {

                trans = conn.BeginTransaction();

                com.CommandText =
                    @"update Diginote set owner = @to where id in
                    (select id from Diginote where owner = @from limit @amount)";

                com.Parameters.Add(new SQLiteParameter("@from", from));
                com.Parameters.Add(new SQLiteParameter("@to", to));
                com.Parameters.Add(new SQLiteParameter("@amount", amount.ToString()));
                com.ExecuteNonQuery();

                trans = conn.BeginTransaction();
                
                return true;
            }
            catch (Exception e) { return false; }
        }
        public static bool AddBuyOrder(string user, int amount = 1, double price = 1.0)
        {
            try
            {

                trans = conn.BeginTransaction();

                com.CommandText =
                    @"insert into BuyOrder(id,user,amount,price,date)
                    values(NULL,@user,@amount,@price,datetime())";

                com.Parameters.Add(new SQLiteParameter("@user", user));
                com.Parameters.Add(new SQLiteParameter("@amount", amount.ToString()));
                com.Parameters.Add(new SQLiteParameter("@price", price.ToString()));
                com.ExecuteNonQuery();

                trans = conn.BeginTransaction();

                return true;
            }
            catch (Exception e) { return false; }
        }
        public static bool AddSellOrder(string user, int amount = 1, double price = 1.0)
        {
            try
            {

                trans = conn.BeginTransaction();

                com.CommandText =
                    @"insert into SellOrder(id,user,amount,price,date)
                    values(NULL,@user,@amount,@price,datetime())";

                com.Parameters.Add(new SQLiteParameter("@user", user));
                com.Parameters.Add(new SQLiteParameter("@amount", amount.ToString()));
                com.Parameters.Add(new SQLiteParameter("@price", price.ToString()));
                com.ExecuteNonQuery();

                trans = conn.BeginTransaction();

                return true;
            }
            catch (Exception e) { return false; }
        }
        public static bool RemoveBuyOrder(int id)
        {
            try
            {

                trans = conn.BeginTransaction();

                com.CommandText = @"delete from BuyOrder where id = @id";

                com.Parameters.Add(new SQLiteParameter("@id", id.ToString()));
                com.ExecuteNonQuery();

                trans = conn.BeginTransaction();

                return true;
            }
            catch (Exception e) { return false; }
        }
        public static bool RemoveSellOrder(int id)
        {
            try
            {

                trans = conn.BeginTransaction();

                com.CommandText = @"delete from SellOrder where id = @id";

                com.Parameters.Add(new SQLiteParameter("@id", id.ToString()));
                com.ExecuteNonQuery();

                trans = conn.BeginTransaction();

                return true;
            }
            catch (Exception e) { return false; }
        }
        public static bool GetBuyOrders(String user)
        {
            try
            {

                trans = conn.BeginTransaction();

                com.CommandText = @"select * from BuyOrder where user = @user";

                com.Parameters.Add(new SQLiteParameter("@user", user));
                com.ExecuteNonQuery();

                trans = conn.BeginTransaction();

                return true;
            }
            catch (Exception e) { return false; }
        }
        public static bool GetSellOrders(String user)
        {
            try
            {

                trans = conn.BeginTransaction();

                com.CommandText = @"select * from SellOrder where user = @user";

                com.Parameters.Add(new SQLiteParameter("@user", user));
                com.ExecuteNonQuery();

                trans = conn.BeginTransaction();

                return true;
            }
            catch (Exception e) { return false; }
        }
        public static bool EditBuyOrder(int id, int amount, double price)
        {
            try
            {

                trans = conn.BeginTransaction();

                com.CommandText =
                    @"update BuyOrder
                    set amount = @amount, price = @price
                    where id = @id";

                com.Parameters.Add(new SQLiteParameter("@amount", amount.ToString()));
                com.Parameters.Add(new SQLiteParameter("@price", price.ToString()));
                com.Parameters.Add(new SQLiteParameter("@id", id.ToString()));
                com.ExecuteNonQuery();

                trans = conn.BeginTransaction();

                return true;
            }
            catch (Exception e) { return false; }
        }
        public static bool EditSellOrder(int id, int amount, double price)
        {
            try
            {

                trans = conn.BeginTransaction();

                com.CommandText =
                    @"update SellOrder
                    set amount = @amount, price = @price
                    where id = @id";

                com.Parameters.Add(new SQLiteParameter("@amount", amount.ToString()));
                com.Parameters.Add(new SQLiteParameter("@price", price.ToString()));
                com.Parameters.Add(new SQLiteParameter("@id", id.ToString()));
                com.ExecuteNonQuery();

                trans = conn.BeginTransaction();

                return true;
            }
            catch (Exception e) { return false; }
        }
    }
}
