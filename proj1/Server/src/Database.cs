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
                catch (SQLiteException e)
                {
                    trans.Rollback();
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
                
            }

            return res;
        }
        public static dynamic GetQuotes(int n)
        {
            com.CommandText = "SELECT * FROM Quote ORDER BY date DESC LIMIT @n";

            com.Parameters.Add(new SQLiteParameter("@n", n));

            List<dynamic> quotes = new List<dynamic>();
            try
            {
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    quotes.Add(new
                        {
                            value = System.Convert.ToDouble(reader["value"]),
                            date = reader["date"]
                        }
                    );
                }
            }
            catch (Exception e)
            {
                quotes = null;
            }
            finally
            {
                reader.Close();
            }

            return quotes;
        }
        public static dynamic SetQuote(double value,string user = "")
        {
            double currentQuote = 0;
            try
            {
                dynamic qts = Database.GetQuotes(1);
                if (qts == null)
                    return null;
                currentQuote = qts[0].value;
            }catch(Exception ex)
            {
                return null;
            }
            dynamic res;
            try
            {

                trans = conn.BeginTransaction();

                com.CommandText =
                @"INSERT INTO Quote(value,date) VALUES(@value,datetime())";
                com.Parameters.Add(new SQLiteParameter("@value", value.ToString()));

                res = new { rows = com.ExecuteNonQuery() };
                
                if(value < currentQuote)
                {
                    com.CommandText =
                    @"UPDATE SellOrder SET active = 0 WHERE user <> @user";
                    com.Parameters.Add(new SQLiteParameter("@user", user.ToString()));
                    com.ExecuteNonQuery();
                }
                else if(value > currentQuote)
                {
                    com.CommandText =
                    @"UPDATE BuyOrder SET active = 0 WHERE user <> @user";
                    com.Parameters.Add(new SQLiteParameter("@user", user.ToString()));
                    com.ExecuteNonQuery();
                }

                trans.Commit();
            }
            catch (SQLiteException e)
            {
                trans.Rollback();
                res = null;

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
                
            }

            return rows;
        }
        public static int TransferDiginotes(string from, string to, int amount = 0, double price = 0)
        {
            try
            {
                trans = conn.BeginTransaction();
                //Transfer diginotes
                com.CommandText =
                @"update Diginote set owner = @to where id in
                (select id from Diginote where owner = @from limit @amount)";

                com.Parameters.Add(new SQLiteParameter("@from", from));
                com.Parameters.Add(new SQLiteParameter("@to", to));
                com.Parameters.Add(new SQLiteParameter("@amount", amount.ToString()));

                com.ExecuteNonQuery();
                //Remove balance from buyer
                com.CommandText =
                @"update User set balance = balance - @total where username = @user";

                com.Parameters.Add(new SQLiteParameter("@total", price*amount));
                com.Parameters.Add(new SQLiteParameter("@user", to));

                com.ExecuteNonQuery();
                //Add balance to seller
                com.CommandText =
                @"update User set balance = balance + @total where username = @user";

                com.Parameters.Add(new SQLiteParameter("@total", price*amount));
                com.Parameters.Add(new SQLiteParameter("@user", from));

                com.ExecuteNonQuery();

                trans.Commit();
            }
            catch (SQLiteException e)
            {
                trans.Rollback();
                
                return 0;
            }

            return 1;
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
            catch (SQLiteException e)
            {
                trans.Rollback();
                res = null;
                
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
            catch (SQLiteException e)
            {
                trans.Rollback();
                res = null;
                
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
                reader.Close();
            }
            catch (SQLiteException e)
            {
                
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
                        date = reader["date"],
                        active = reader["active"],
                        id = reader["id"],
                    });
                }
                reader.Close();
            }
            catch (SQLiteException e)
            {
                
                orders = null;
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
            catch (SQLiteException e)
            {
                trans.Rollback();
                res = null;
                
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
            catch (SQLiteException e)
            {
                trans.Rollback();
                res = null;
                
            }

            return res;
        }  
        
        public static dynamic GetBestSellOrder(string user,int amount)
        {
            com.CommandText =
                @"select * 
                  from SellOrder 
                  where user <> @user and active = 1 order by date asc limit 1";
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
                
            }
            finally
            {
                reader.Close();
            }

            return res;
        }

        public static dynamic GetBestBuyOrder(string user, int amount)
        {
            com.CommandText =
                @"select *
                  from BuyOrder 
                  where user <> @user and active = 1 order by date asc limit 1";
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
                
            }
            finally
            {
                reader.Close();
            }

            return res;
        }
    }
}
