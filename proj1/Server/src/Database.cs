using System;
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
        public static dynamic AddUser(string username, string hash, string salt, double balance = 0.0)
        {
            com.CommandText = "insert into User values (@user, @hash, @salt, @balance)";
            com.Parameters.Add(new SQLiteParameter("@user", username));
            com.Parameters.Add(new SQLiteParameter("@hash", hash));
            com.Parameters.Add(new SQLiteParameter("@salt", salt));
            com.Parameters.Add(new SQLiteParameter("@balance", balance.ToString()));

            dynamic res;
            try
            {
                trans = conn.BeginTransaction();
                res = new { rows = com.ExecuteNonQuery() };
                trans.Commit();
            }
            catch (SQLiteException e)
            {
                trans.Rollback();
                res = null;
                Console.WriteLine(e.StackTrace);
            }

            return res;
        }
        public static dynamic GetBalance(string username)
        {

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
            catch (InvalidOperationException e)
            {
                res = null;
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                reader.Close();
            }

            return res;
        }
        public static dynamic SetBalance(string username, double balance = 0.0)
        {
                         
            com.CommandText =
                @"update User set balance = @balance
                where username = @username";
            com.Parameters.Add(new SQLiteParameter("@username", username));
            com.Parameters.Add(new SQLiteParameter("@balance", balance.ToString()));

            dynamic res;
            try
            {
                trans = conn.BeginTransaction();
                res = new { rows = com.ExecuteNonQuery() };
                trans.Commit();
            }
            catch (SQLiteException e)
            {
                trans.Rollback();
                res = null;
                Console.WriteLine(e.StackTrace);
            }

            return res;
        }
        public static dynamic GetUser(string username)
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

            dynamic user;
            try
            {
                reader.Read();
                user = new
                {
                    username = reader["username"],
                    hash = reader["hash"],
                    balance = reader["balance"],
                    salt = reader["salt"]
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
        public static dynamic GetDiginotes(string username)
        {
            com.CommandText = "SELECT count(*) as diginotes FROM Diginote WHERE owner = @user";
            com.Parameters.Add(new SQLiteParameter("@user", username));

            dynamic res;
            try
            {
                reader = com.ExecuteReader();
                reader.Read();
                res = new { diginotes = reader["diginotes"] };
            }
            catch (InvalidOperationException e)
            {
                res = null;
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                reader.Close();
            }

            return res;
        }
        public static dynamic AddDiginotes(string username, int amount = 1)
        { 
            com.CommandText = "";
            for (int i = 0; i < amount; i++)
            {                   
                com.CommandText += "INSERT INTO Diginote(id,owner) VALUES(NULL,@user);";                                      
            }
            com.Parameters.Add(new SQLiteParameter("@user", username));

            dynamic res;
            try
            {
                trans = conn.BeginTransaction();
                res = new { rows = com.ExecuteNonQuery() };
                trans.Commit();
            }
            catch (SQLiteException e)
            {
                trans.Rollback();
                res = null;
                Console.WriteLine(e.StackTrace);
            }

            return res;
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
        public static dynamic AddBuyOrder(string user, int amount = 1, double price = 1.0)
        {
            com.CommandText =
                @"insert into BuyOrder(id,user,amount,price,date)
                values(NULL,@user,@amount,@price,datetime())";

            com.Parameters.Add(new SQLiteParameter("@user", user));
            com.Parameters.Add(new SQLiteParameter("@amount", amount.ToString()));
            com.Parameters.Add(new SQLiteParameter("@price", price.ToString()));

            dynamic res;
            try
            {
                trans = conn.BeginTransaction();
                res = new { rows = com.ExecuteNonQuery() };
                trans.Commit();
            }
            catch (SQLiteException e)
            {
                trans.Rollback();
                res = null;
                Console.WriteLine(e.StackTrace);
            }

            return res;
        }
        public static dynamic AddSellOrder(string user, int amount = 1, double price = 1.0)
        {
            com.CommandText =
                @"insert into SellOrder(id,user,amount,price,date)
                values(NULL,@user,@amount,@price,datetime())";

            com.Parameters.Add(new SQLiteParameter("@user", user));
            com.Parameters.Add(new SQLiteParameter("@amount", amount.ToString()));
            com.Parameters.Add(new SQLiteParameter("@price", price.ToString()));

            dynamic res;
            try
            {
                trans = conn.BeginTransaction();
                res = new { rows = com.ExecuteNonQuery() };
                trans.Commit();
            }
            catch (SQLiteException e)
            {
                trans.Rollback();
                res = null;
                Console.WriteLine(e.StackTrace);
            }

            return res;
        }
        public static dynamic RemoveBuyOrder(int id)
        {
            com.CommandText = @"delete from BuyOrder where id = @id";
            com.Parameters.Add(new SQLiteParameter("@id", id.ToString()));

            dynamic res;
            try
            {

                trans = conn.BeginTransaction();
                res = new { rows = com.ExecuteNonQuery() };
                trans.Commit();
            }
            catch (SQLiteException e)
            {
                trans.Rollback();
                res = null;
                Console.WriteLine(e.StackTrace);
            }

            return res;
        }
        public static dynamic RemoveSellOrder(int id)
        {
            com.CommandText = @"delete from SellOrder where id = @id";
            com.Parameters.Add(new SQLiteParameter("@id", id.ToString()));

            dynamic res;
            try
            {
                trans = conn.BeginTransaction();
                res = new { rows = com.ExecuteNonQuery() };
                trans.Commit();
            }
            catch (SQLiteException e)
            {
                trans.Rollback();
                res = null;
                Console.WriteLine(e.StackTrace);
            }

            return res;
        }
        public static List<dynamic> GetBuyOrders(String user)
        {
            List<dynamic> orders = new List<dynamic>();
            com.CommandText = @"select * from BuyOrder where user = @user";
            com.Parameters.Add(new SQLiteParameter("@user", user));

            try
            {
                reader = com.ExecuteReader();
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
            }
            catch (SQLiteException e)
            {
                Console.WriteLine(e.StackTrace);
                orders = null;
            }            

            return orders;
        }
        public static List<dynamic> GetSellOrders(String user)
        {
            List<dynamic> orders = new List<dynamic>();
            com.CommandText = @"select * from SellOrder where user = @user";
            com.Parameters.Add(new SQLiteParameter("@user", user));

            try
            {
                reader = com.ExecuteReader();
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
            }
            catch (SQLiteException e)
            {
                Console.WriteLine(e.StackTrace);
                orders = null;
            }            

            return orders;
        }
        public static dynamic GetBuyOrder(int id)
        {
            com.CommandText =
                @"select * from BuyOrder                
                where id = @id";
            com.Parameters.Add(new SQLiteParameter("@id", id.ToString()));
            dynamic res;
            try
            {
                reader = com.ExecuteReader();
                reader.Read();
                res = new {
                    id = reader["id"],
                    amount = reader["amount"],
                    price = reader["price"],
                    user = reader["user"]
                };
            }
            catch (SQLiteException e)
            {
                res = null;
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                reader.Close();
            }

            return res;

        }
        public static dynamic GetSellOrder(int id)
        {
            com.CommandText =
                @"select * from SellOrder                
                where id = @id";
            com.Parameters.Add(new SQLiteParameter("@id", id.ToString()));
            dynamic res;
            try
            {
                reader = com.ExecuteReader();
                reader.Read();
                res = new
                {
                    id = reader["id"],
                    amount = reader["amount"],
                    price = reader["price"],
                    user = reader["user"]
                };
            }
            catch (SQLiteException e)
            {
                res = null;
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                reader.Close();
            }

            return res;
        }
        public static dynamic EditBuyOrder(int id, int amount, double price)
        {           
            com.CommandText =
                @"update BuyOrder
                set amount = @amount, price = @price
                where id = @id";

            com.Parameters.Add(new SQLiteParameter("@amount", amount.ToString()));
            com.Parameters.Add(new SQLiteParameter("@price", price.ToString()));
            com.Parameters.Add(new SQLiteParameter("@id", id.ToString()));

            dynamic res;
            try
            {
                trans = conn.BeginTransaction();
                res = new { rows = com.ExecuteNonQuery() };
                trans.Commit();
            }
            catch (SQLiteException e)
            {
                trans.Rollback();
                res = null;
                Console.WriteLine(e.StackTrace);
            }

            return res;
        }
        public static dynamic EditSellOrder(int id, int amount, double price)
        {
            com.CommandText =
                @"update SellOrder
                set amount = @amount, price = @price
                where id = @id";

            com.Parameters.Add(new SQLiteParameter("@amount", amount.ToString()));
            com.Parameters.Add(new SQLiteParameter("@price", price.ToString()));
            com.Parameters.Add(new SQLiteParameter("@id", id.ToString()));

            dynamic res;
            try
            {
                trans = conn.BeginTransaction();
                res = new { rows = com.ExecuteNonQuery() };
                trans.Commit();
            }
            catch (SQLiteException e)
            {
                trans.Rollback();
                res = null;
                Console.WriteLine(e.StackTrace);
            }

            return res;
        }       
    }
}
