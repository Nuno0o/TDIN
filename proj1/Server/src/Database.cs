﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace Server {

    class Database
    {

        private static SQLiteConnection conn;
        private static SQLiteCommand com;
        private static SQLiteTransaction trans;
        private static SQLiteDataReader reader;

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
            else if(overwrite)
            {
                File.Delete(DB_PATH);
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
                try {
                    trans = conn.BeginTransaction();
                    com.ExecuteNonQuery();
                    trans.Commit();
                }
                catch (SQLiteException e)
                {
                    trans.Rollback();
                    Console.WriteLine(e.StackTrace);
                }
            }            
        }        
        public static int AddUser(string username, string password, double balance = 0.0)
        {
            com.CommandText = "insert into User values (@user, @pass, @balance)";
            com.Parameters.Add(new SQLiteParameter("@user", username));
            com.Parameters.Add(new SQLiteParameter("@pass", password));
            com.Parameters.Add(new SQLiteParameter("@balance", balance.ToString()));

            int rows = -1;
            try
            {
                trans = conn.BeginTransaction();
                rows = com.ExecuteNonQuery();
                trans.Commit();
            }
            catch (SQLiteException e)
            {
                trans.Rollback();
                Console.WriteLine(e.StackTrace);
            }

            return rows;
        }
        public static Object GetBalance(string username)
        {

            com.CommandText =
                @"select balance from User
                where username = @username";
            com.Parameters.Add(new SQLiteParameter("@username", username));

            try
            {
                reader = com.ExecuteReader();
            }
            catch (SQLiteException e)
            {
                Console.WriteLine(e.StackTrace);
            }

            Object balance;
            try
            {
                reader.Read();
                balance = new { balance = reader["balance"] };
            }
            catch (InvalidOperationException e)
            {
                balance = null;
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                reader.Close();
            }

            return balance;
        }
        public static int SetBalance(string username, double balance = 0.0)
        {
                         
            com.CommandText =
                @"update User set balance = @balance
                where username = @username";
            com.Parameters.Add(new SQLiteParameter("@username", username));
            com.Parameters.Add(new SQLiteParameter("@balance", balance.ToString()));

            int rows = -1;
            try
            {
                trans = conn.BeginTransaction();
                com.ExecuteNonQuery();
                trans.Commit();
            }
            catch (SQLiteException e)
            {
                trans.Rollback();
                Console.WriteLine(e.StackTrace);
            }

            return rows;
        }
        public static Object GetUser(string username)
        {
            
            com.CommandText = "SELECT * FROM User WHERE username = @user";
            com.Parameters.Add(new SQLiteParameter("@user", username));
            
            try
            {
                reader = com.ExecuteReader();
            }
            catch (SQLiteException e)
            {
                Console.WriteLine(e.StackTrace);
            }

            Object user;
            try
            {
                reader.Read();
                user = new
                {
                    username = reader["username"],
                    password = reader["password"],
                    balance = reader["balance"]
                };
            }
            catch(InvalidOperationException e)
            {
                user = null;
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                reader.Close();
            }


            return user;
        }
        public static Object GetDiginotes(string username)
        {
            com.CommandText = "SELECT count(*) as diginotes FROM Diginote WHERE owner = @user";
            com.Parameters.Add(new SQLiteParameter("@user", username));

            try
            {
                reader = com.ExecuteReader();
            }
            catch (SQLiteException e)
            {
                Console.WriteLine(e.StackTrace);
            }

            Object obj;
            try
            {
                reader.Read();
                obj = new { diginotes = reader["diginotes"] };
            }
            catch (InvalidOperationException e)
            {
                obj = null;
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                reader.Close();
            }

            return obj;
        }
        public static int AddDiginotes(string username, int amount = 1)
        { 
            com.CommandText = "";
            for (int i = 0; i < amount; i++)
            {                   
                com.CommandText += "INSERT INTO Diginote(id,owner) VALUES(NULL,@user);";                                      
            }
            com.Parameters.Add(new SQLiteParameter("@user", username));

            int rows = -1;
            try
            {
                trans = conn.BeginTransaction();
                rows = com.ExecuteNonQuery();
                trans.Commit();
            }
            catch (SQLiteException e)
            {
                trans.Rollback();
                Console.WriteLine(e.StackTrace);
            }

            return rows;
        }
        public static int RemoveDiginotes(string user, int amount = 1)
        {    
            com.CommandText =
                @"delete from Diginote where id in
                (select id from Diginote where owner = @user limit @amount)";
            com.Parameters.Add(new SQLiteParameter("@user", user));
            com.Parameters.Add(new SQLiteParameter("@amount", amount.ToString()));

            int rows = -1;
            try
            {
                trans = conn.BeginTransaction();
                rows = com.ExecuteNonQuery();                
                trans.Commit();
            }
            catch (SQLiteException e)
            {
                trans.Rollback();
                Console.WriteLine(e.StackTrace);
            }

            return rows;
        }
        public static int TransferDiginotes(string from, string to, int amount = 1)
        {
            com.CommandText =
                @"update Diginote set owner = @to where id in
                (select id from Diginote where owner = @from limit @amount)";

            com.Parameters.Add(new SQLiteParameter("@from", from));
            com.Parameters.Add(new SQLiteParameter("@to", to));
            com.Parameters.Add(new SQLiteParameter("@amount", amount.ToString()));

            int rows = -1;
            try
            {
                trans = conn.BeginTransaction();
                rows = com.ExecuteNonQuery();
                trans.Commit();
            }
            catch (SQLiteException e)
            {
                trans.Rollback();
                Console.WriteLine(e.StackTrace);
            }

            return rows;
        }
        public static int AddBuyOrder(string user, int amount = 1, double price = 1.0)
        {
            com.CommandText =
                @"insert into BuyOrder(id,user,amount,price,date)
                values(NULL,@user,@amount,@price,datetime())";

            com.Parameters.Add(new SQLiteParameter("@user", user));
            com.Parameters.Add(new SQLiteParameter("@amount", amount.ToString()));
            com.Parameters.Add(new SQLiteParameter("@price", price.ToString()));

            int rows = -1;
            try
            {
                trans = conn.BeginTransaction();
                com.ExecuteNonQuery();
                trans.Commit();
            }
            catch (SQLiteException e)
            {
                trans.Rollback();
                Console.WriteLine(e.StackTrace);
            }

            return rows;
        }
        public static int AddSellOrder(string user, int amount = 1, double price = 1.0)
        {
            com.CommandText =
                @"insert into SellOrder(id,user,amount,price,date)
                values(NULL,@user,@amount,@price,datetime())";

            com.Parameters.Add(new SQLiteParameter("@user", user));
            com.Parameters.Add(new SQLiteParameter("@amount", amount.ToString()));
            com.Parameters.Add(new SQLiteParameter("@price", price.ToString()));

            int rows = -1;
            try
            {
                trans = conn.BeginTransaction();
                com.ExecuteNonQuery();
                trans.Commit();
            }
            catch (SQLiteException e)
            {
                trans.Rollback();
                Console.WriteLine(e.StackTrace);
            }

            return rows;
        }
        public static int RemoveBuyOrder(int id)
        {
            com.CommandText = @"delete from BuyOrder where id = @id";
            com.Parameters.Add(new SQLiteParameter("@id", id.ToString()));

            int rows = -1;
            try
            {

                trans = conn.BeginTransaction();
                rows = com.ExecuteNonQuery();
                trans.Commit();
            }
            catch (SQLiteException e)
            {
                trans.Rollback();
                Console.WriteLine(e.StackTrace);
            }

            return rows;
        }
        public static int RemoveSellOrder(int id)
        {
            com.CommandText = @"delete from SellOrder where id = @id";
            com.Parameters.Add(new SQLiteParameter("@id", id.ToString()));

            int rows = -1;
            try
            {
                trans = conn.BeginTransaction();
                rows = com.ExecuteNonQuery();
                trans.Commit();
            }
            catch (SQLiteException e)
            {
                trans.Rollback();
                Console.WriteLine(e.StackTrace);
            }

            return rows;
        }
        public static List<Object> GetBuyOrders(String user)
        {
            List<Object> orders = new List<Object>();
            com.CommandText = @"select * from BuyOrder where user = @user";
            com.Parameters.Add(new SQLiteParameter("@user", user));

            try
            {
                reader = com.ExecuteReader();
            }
            catch (SQLiteException e)
            {
                Console.WriteLine(e.StackTrace);
            }

            while (reader.Read())
            {
                orders.Add(new {
                    user = reader["user"],
                    amount = reader["amount"],
                    price = reader["price"],
                    date = reader["date"],
                    id = reader["id"],
                });
            }
            reader.Close();

            return orders;
        }
        public static List<Object> GetSellOrders(String user)
        {
            List<Object> orders = new List<Object>();
            com.CommandText = @"select * from SellOrder where user = @user";
            com.Parameters.Add(new SQLiteParameter("@user", user));

            try
            {
                reader = com.ExecuteReader();
            }
            catch (SQLiteException e)
            {
                Console.WriteLine(e.StackTrace);
            }

            while (reader.Read())
            {
                orders.Add(new
                {
                    user = reader["user"],
                    amount = reader["amount"],
                    price = reader["price"],
                    date = reader["date"],
                    id = reader["id"],
                });
            }
            reader.Close();

            return orders;
        }       
        public static int EditBuyOrder(int id, int amount, double price)
        {           
            com.CommandText =
                @"update BuyOrder
                set amount = @amount, price = @price
                where id = @id";

            com.Parameters.Add(new SQLiteParameter("@amount", amount.ToString()));
            com.Parameters.Add(new SQLiteParameter("@price", price.ToString()));
            com.Parameters.Add(new SQLiteParameter("@id", id.ToString()));

            int rows = -1;
            try
            {
                trans = conn.BeginTransaction();
                rows = com.ExecuteNonQuery();
                trans.Commit();
            }
            catch (SQLiteException e)
            {
                trans.Rollback();
                Console.WriteLine(e.StackTrace);
            }

            return rows;
        }
        public static int EditSellOrder(int id, int amount, double price)
        {
            com.CommandText =
                @"update SellOrder
                set amount = @amount, price = @price
                where id = @id";

            com.Parameters.Add(new SQLiteParameter("@amount", amount.ToString()));
            com.Parameters.Add(new SQLiteParameter("@price", price.ToString()));
            com.Parameters.Add(new SQLiteParameter("@id", id.ToString()));

            int rows = -1;
            try
            {
                trans = conn.BeginTransaction();
                rows = com.ExecuteNonQuery();
                trans.Commit();
            }
            catch (SQLiteException e)
            {
                trans.Rollback();
                Console.WriteLine(e.StackTrace);
            }

            return rows;
        }       
    }
}
