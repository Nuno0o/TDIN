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
        private SQLiteConnection conn;
        public Database(string path)
        {
            conn = new SQLiteConnection("Data Source="+ path +";Version=3;");
            conn.Open();
        }
        public bool ClearDatabase()
        {
            try
            {
                using (SQLiteTransaction trans = conn.BeginTransaction())
                {

                    using (SQLiteCommand insertUser = new SQLiteCommand(conn))
                    {
                        insertUser.CommandText = "DELETE FROM User";
                        insertUser.ExecuteNonQuery();
                    }
                    using (SQLiteCommand insertUser = new SQLiteCommand(conn))
                    {
                        insertUser.CommandText = "DELETE FROM Diginote";
                        insertUser.ExecuteNonQuery();
                    }

                    trans.Commit();
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }
        public bool AddUser(string username, string password)
        {
            try
            {
                using (SQLiteCommand insertUser = new SQLiteCommand(conn))
                {
                    insertUser.CommandText = "INSERT INTO User(username,password,currency) VALUES (@user,@pass,0)";
                    insertUser.Parameters.Add(new SQLiteParameter("@user", username));
                    insertUser.Parameters.Add(new SQLiteParameter("@pass", password));
                    return insertUser.ExecuteNonQuery() == 1;
                }
            }catch(Exception e)
            {
                return false;
            }
        }
        public bool AddCurrency(string username, int ammount)
        {
            try
            {
                using (SQLiteCommand insertUser = new SQLiteCommand(conn))
                {
                    insertUser.CommandText = "UPDATE User SET currency = currency + @amm WHERE User.username = @user";
                    insertUser.Parameters.Add(new SQLiteParameter("@user", username));
                    insertUser.Parameters.Add(new SQLiteParameter("@amm", ammount));
                    return insertUser.ExecuteNonQuery() == 1;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool AddDiginotes(string username, int ammount)
        {
            try
            {
                int minId = 0;
                using (SQLiteCommand getBiggestId = new SQLiteCommand(conn))
                {
                    getBiggestId.CommandText = "SELECT id FROM Diginote ORDER BY id DESC LIMIT 1";
                    SQLiteDataReader reader = getBiggestId.ExecuteReader();
                    if (reader.Read())
                    {
                        minId = reader.GetInt32(0);
                    }
                }

                int currentId = minId+1;
                using (SQLiteTransaction trans = conn.BeginTransaction())
                {
                    for(int i = 0; i < ammount; i++)
                    {
                        using (SQLiteCommand insertUser = new SQLiteCommand(conn))
                        {
                            insertUser.CommandText = "INSERT INTO Diginote(id,owner) VALUES(@id,@user)";
                            insertUser.Parameters.Add(new SQLiteParameter("@id", currentId));
                            insertUser.Parameters.Add(new SQLiteParameter("@user", username));
                            insertUser.ExecuteNonQuery();
                        }
                        currentId++;
                    }
                    trans.Commit();
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }

}
