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

                /* executes sql script */
                try {
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
        public static dynamic AddUser(string username, string hash, string salt, double balance = 0.0)
        {
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
            catch (Exception e)
            {
                res = null;
                Console.WriteLine(e.ToString());
            }
            finally
            {
                if(reader != null) reader.Close();
            }

            return res;
        }
        public static dynamic SetBalance(string username, double balance = 0.0)
        {
                         
            com.CommandText =
                @"update User set balance = @balance
                where username = @username";
            com.Parameters.Add(new SQLiteParameter("@username", username));
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

            return res;
        }
        public static List<dynamic> GetQuotes(int limit = 1)
        {
            com.CommandText = "SELECT * FROM Quote ORDER BY date DESC LIMIT @limit";
            com.Parameters.Add(new SQLiteParameter("@limit", limit));

            List<dynamic> quotes = new List<dynamic>();
            try
            {
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    quotes.Add(new
                        {
                            value = reader["value"],
                            date = reader["date"]
                        }
                    );
                }
            }
            catch (Exception e)
            {
                quotes = null;
                Console.WriteLine(e.ToString());
            }
            finally
            {
                if(reader != null) reader.Close();
            }

            return quotes;
        }
        public static dynamic SetQuote(double value, string user)
        {
            
            dynamic quotes = Database.GetQuotes();
            if (quotes == null) return null;
            double quote = quotes[0].value;

            com.CommandText = @"INSERT INTO Quote(value,date) VALUES(@value,datetime());";
            if (value < quote)
                com.CommandText += @"UPDATE SellOrder SET active = 0 WHERE user <> @user;";
            else if (value > quote)
                com.CommandText += @"UPDATE BuyOrder SET active = 0 WHERE user <> @user;";           

            com.Parameters.Add(new SQLiteParameter("@user", user.ToString()));
            com.Parameters.Add(new SQLiteParameter("@value", value));

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

            return res;
        }
        public static dynamic GetUser(string username)
        {
            
            com.CommandText = "SELECT * FROM User WHERE username = @user";
            com.Parameters.Add(new SQLiteParameter("@user", username));

            dynamic user;
            try
            {
                reader = com.ExecuteReader();
                reader.Read();
                user = new
                {
                    username = reader["username"],
                    hash = reader["hash"],
                    balance = reader["balance"],
                    salt = reader["salt"]
                };
            }
            catch(Exception e)
            {
                user = null;
                //Console.WriteLine(e.ToString());
            }
            finally
            {
                if(reader != null) reader.Close();
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
                Console.WriteLine(e.ToString());
            }
            finally
            {
                if(reader != null) reader.Close();
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
            catch (Exception e)
            {
                trans.Rollback();
                res = null;
                Console.WriteLine(e.ToString());
            }

            return res;
        }
        public static dynamic RemoveDiginotes(string user, int amount = 1)
        {    
            com.CommandText =
                @"delete from Diginote where id in
                (select id from Diginote where owner = @user limit @amount)";
            com.Parameters.Add(new SQLiteParameter("@user", user));
            com.Parameters.Add(new SQLiteParameter("@amount", amount.ToString()));

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

            return res;
        }
        public static dynamic TransferDiginotes(string seller, string buyer, int amount, double quote)
        {
            com.CommandText =
               @"update Diginote set owner = @buyer where id in
                (select id from Diginote where owner = @seller limit @amount);
                update User set balance = balance - @total where username = @buyer;
                update User set balance = balance + @total where username = @seller;
                insert into _Transaction values (NULL,@buyer,@seller,@quote,@amount,datetime());";

            com.Parameters.Add(new SQLiteParameter("@buyer", buyer));
            com.Parameters.Add(new SQLiteParameter("@seller", seller));
            com.Parameters.Add(new SQLiteParameter("@quote", quote));
            com.Parameters.Add(new SQLiteParameter("@amount", amount));           
            com.Parameters.Add(new SQLiteParameter("@total", (quote * amount)));

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

            return res;
        }
        public static dynamic GetTransactions(string user)
        {
            List<dynamic> trans = new List<dynamic>();
            com.CommandText = @"select * from _Transaction where buyer = @user1 or seller = @user2";
            com.Parameters.Add(new SQLiteParameter("@user1", user));
            com.Parameters.Add(new SQLiteParameter("@user2", user));

            try
            {
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    trans.Add(new
                    {
                        buyer = reader["buyer"],
                        seller = reader["seller"],
                        quote = reader["quote"],
                        amount = reader["amount"],
                        date = reader["date"],
                        id = reader["id"]
                    });
                }
            }
            catch (Exception e)
            {
                trans = null;
                Console.WriteLine(e.ToString());
            }
            finally
            {
                if (reader != null) reader.Close();
            }

            return trans;
        }
        public static dynamic GetDiginotesList(string user)
        {
            List<dynamic> dgn = new List<dynamic>();
            com.CommandText = @"select * from Diginote where owner = @user";
            com.Parameters.Add(new SQLiteParameter("@user", user));

            try
            {
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    dgn.Add(new
                    {
                        id = reader["id"]
                    });
                }
            }
            catch (Exception e)
            {
                dgn = null;
                Console.WriteLine(e.ToString());
            }
            finally
            {
                if (reader != null) reader.Close();
            }

            return dgn;
        }
        public static dynamic AddBuyOrder(string user, int amount = 1)
        {
            com.CommandText =
                @"insert into BuyOrder(id,user,amount,date)
                values(NULL,@user,@amount,datetime())";

            com.Parameters.Add(new SQLiteParameter("@user", user));
            com.Parameters.Add(new SQLiteParameter("@amount", amount.ToString()));

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

            return res;
        }
        public static dynamic AddSellOrder(string user, int amount = 1)
        {
            com.CommandText =
                @"insert into SellOrder(id,user,amount,date)
                values(NULL,@user,@amount,datetime())";

            com.Parameters.Add(new SQLiteParameter("@user", user));
            com.Parameters.Add(new SQLiteParameter("@amount", amount.ToString()));

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
            catch (Exception e)
            {
                trans.Rollback();
                res = null;
                Console.WriteLine(e.ToString());
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
            catch (Exception e)
            {
                trans.Rollback();
                res = null;
                Console.WriteLine(e.ToString());
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
                        date = reader["date"],
                        active = reader["active"],
                        id = reader["id"],
                    });
                }
            }
            catch (Exception e)
            {
                orders = null;
                Console.WriteLine(e.ToString());
            }
            finally
            {
                if(reader != null) reader.Close();
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
                        date = reader["date"],
                        active = reader["active"],
                        id = reader["id"],
                    });
                }
                if(reader != null) reader.Close();
            }
            catch (Exception e)
            {                
                orders = null;
                Console.WriteLine(e.ToString());
            }            

            return orders;
        }
        public static dynamic EditBuyOrder(int id, int amount, int active = 1)
        {           
            com.CommandText =
                @"update BuyOrder
                set amount = @amount, active = @active
                where id = @id";

            com.Parameters.Add(new SQLiteParameter("@amount", amount.ToString()));
            com.Parameters.Add(new SQLiteParameter("@id", id.ToString()));
            com.Parameters.Add(new SQLiteParameter("@active", active.ToString()));

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

            return res;
        }
        public static dynamic EditSellOrder(int id, int amount, int active = 1)
        {
            com.CommandText =
                @"update SellOrder
                set amount = @amount, active = @active
                where id = @id";

            com.Parameters.Add(new SQLiteParameter("@amount", amount.ToString()));
            com.Parameters.Add(new SQLiteParameter("@id", id.ToString()));
            com.Parameters.Add(new SQLiteParameter("@active", active.ToString()));

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

            return res;
        }  
        
        public static dynamic GetBestSellOrder(string user,int amount)
        {
            com.CommandText =
                @"select * from SellOrder 
                where user <> @user and active = 1
                order by date asc limit 1";
            com.Parameters.Add(new SQLiteParameter("@amount", amount.ToString()));
            com.Parameters.Add(new SQLiteParameter("@user", user.ToString()));

            dynamic res;
            try
            {
                reader = com.ExecuteReader();
                reader.Read();
                res = new
                {
                    id = System.Convert.ToInt32(reader["id"]),
                    amount = System.Convert.ToInt32(reader["amount"]),
                    user = reader["user"]
                };
            }
            catch (Exception e)
            {
                res = null;
                Console.WriteLine(e.ToString());
            }
            finally
            {
                if(reader != null) reader.Close();
            }

            return res;
        }

        public static dynamic GetBestBuyOrder(string user, int amount)
        {
            com.CommandText =
                @"select * from BuyOrder where user <> @user
                and active = 1 order by date asc limit 1";
            com.Parameters.Add(new SQLiteParameter("@amount", amount.ToString()));
            com.Parameters.Add(new SQLiteParameter("@user", user));

            dynamic res;
            try
            {
                reader = com.ExecuteReader();
                reader.Read();
                res = new
                {
                    id = System.Convert.ToInt32(reader["id"]),
                    amount = System.Convert.ToInt32(reader["amount"]),
                    user = reader["user"]
                };
            }
            catch (Exception e)
            {
                res = null;
                Console.WriteLine(e.ToString());
            }
            finally
            {
                if(reader != null) reader.Close();
            }

            return res;
        }       
    }
}
