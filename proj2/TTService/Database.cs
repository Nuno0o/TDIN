using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace TTService
{
    static class Database
    {
        public static string DB_PATH = AppDomain.CurrentDomain.GetData("DataDirectory") + "\\Database.db";
        public static string SQL_PATH = AppDomain.CurrentDomain.GetData("DataDirectory") + "\\Database.sqlite";

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
            SQLiteConnection conn;
            conn = new SQLiteConnection("Data Source=" + DB_PATH + ";Version=3;foreign keys=true;");
            conn.Open();

            /* creates sql command for future use */
            SQLiteCommand com = new SQLiteCommand(conn);
            SQLiteTransaction trans = null;

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

        public static dynamic AddDepartment (string name)
        {
            dynamic result = null;
            using (SQLiteConnection c = new SQLiteConnection(DB_PATH))
            {
                try
                {
                    c.Open();
                    string sql = "insert into Department(Name) values (@name)";
                    SQLiteCommand cmd = new SQLiteCommand(sql, c);
                    cmd.Parameters.AddWithValue("name", name);
                    result = cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {

                }
                finally
                {
                    c.Close();
                }
            }
            return result;
        }

        public static dynamic RemoveDepartment (int id)
        {
            dynamic result = -1;
            using (SQLiteConnection c = new SQLiteConnection(DB_PATH))
            {
                try
                {
                    c.Open();
                    string sql = "delete from Department where Department.id = @id";
                    SQLiteCommand cmd = new SQLiteCommand(sql, c);
                    cmd.Parameters.AddWithValue("id", id);
                    result = cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {

                }
                finally
                {
                    c.Close();
                }
            }
            return result;
        }

        public static dynamic AddUser(string name, string email, string hash, string salt, int department)
        {
            dynamic result = null;
            using (SQLiteConnection c = new SQLiteConnection(DB_PATH))
            {
                try
                {
                    c.Open();
                    string sql = 
                        @"insert into User(Name, Email, Password, Department)
                        values (@name, @email, @hash, @salt, @department)";
                    SQLiteCommand cmd = new SQLiteCommand(sql, c);
                    cmd.Parameters.AddWithValue("name", name);
                    cmd.Parameters.AddWithValue("email", email);
                    cmd.Parameters.AddWithValue("hash", hash);
                    cmd.Parameters.AddWithValue("salt", salt);
                    cmd.Parameters.AddWithValue("department", department);
                    result = cmd.ExecuteNonQuery();
                }
                catch(SqlException ex)
                {

                }
                finally
                {
                    c.Close();
                }
            }
            return result;
        }

        public static dynamic GetUser(int id)
        {
            dynamic result = null;
            using (SQLiteConnection c = new SQLiteConnection(DB_PATH))
            {
                try
                {
                    c.Open();
                    string sql = "select * from User where User.id = @id";
                    SQLiteCommand cmd = new SQLiteCommand(sql, c);
                    cmd.Parameters.AddWithValue("id", id);
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    result = new List<dynamic>();
                    while (reader.Read())
                    {
                        result.Add(new
                        {
                            id = reader["Id"],
                            name = reader["Name"],
                            email = reader["Email"],
                            password = reader["Password"],
                            department = reader["Department"]
                        });
                    }
                }
                catch (SqlException ex)
                {

                }
                finally
                {
                    c.Close();
                }
            }
            return result;
        }

        public static dynamic GetUser(string email)
        {
            dynamic result = null;
            using (SQLiteConnection c = new SQLiteConnection(DB_PATH))
            {
                try
                {
                    c.Open();
                    string sql = "select * from User where User.email = @email";
                    SQLiteCommand cmd = new SQLiteCommand(sql, c);
                    cmd.Parameters.AddWithValue("email", email);
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    result = new List<dynamic>();
                    while (reader.Read())
                    {
                        result.Add(new
                        {
                            id = reader["Id"],
                            name = reader["Name"],
                            email = reader["Email"],
                            password = reader["Password"],
                            department = reader["Department"]
                        });
                    }
                }
                catch (SqlException ex)
                {

                }
                finally
                {
                    c.Close();
                }
            }
            return result;
        }

        public static dynamic AddTicket (string title, string description, int author, int parent)
        {
            dynamic result = null;
            using (SQLiteConnection c = new SQLiteConnection(DB_PATH))
            {
                try
                {
                    c.Open();
                    string sql = "insert into Ticket(Title, Description, Author, CreatedAt, Parent) " +
                                  "values (@title, @description, @author, datetime(), @parentid)";
                    SQLiteCommand cmd = new SQLiteCommand(sql, c);
                    cmd.Parameters.AddWithValue("title", title);
                    cmd.Parameters.AddWithValue("description", description);
                    cmd.Parameters.AddWithValue("author", author);
                    cmd.Parameters.AddWithValue("parent", parent);
                    result = cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {

                }
                finally
                {
                    c.Close();
                }
            }
            return result;
        }
    }
}
