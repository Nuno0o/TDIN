using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using System.Diagnostics;
using System.Reflection;

namespace TTService
{
    static class Database
    {
        public static string DB_PATH = Directory.GetParent(Directory.GetParent(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)).ToString()).ToString() + "\\Database.db";
        public static string SQL_PATH = Directory.GetParent(Directory.GetParent(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)).ToString()).ToString() + "\\Database.sqlite";

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
            SQLiteConnection conn;
            /* created db connection*/
            conn = new SQLiteConnection("Data Source=" + DB_PATH + ";Version=3;foreign keys=true;");
            conn.Open();

            /* creates sql command for future use */
            SQLiteCommand com;
            com = new SQLiteCommand(conn);

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
                    Debug.WriteLine(e.ToString());
                }
            }
            conn.Close();
        }

        #region Ticket

        public static dynamic AddTicket(string title, string description, int author, int? parent)
        {            
            dynamic result = null;
            using (SQLiteConnection c = new SQLiteConnection("Data Source=" + DB_PATH + ";Version=3;foreign keys=true;"))
            {
                try
                {
                    c.Open();

                    string sql = @"
                        INSERT INTO Ticket (Title, Description, Author, Parent, CreatedAt)
                        VALUES (@title, @description, @author, @parent, datetime())
                    ";                

                    SQLiteCommand cmd = new SQLiteCommand(sql, c);

                    cmd.Parameters.AddWithValue("title", title);
                    cmd.Parameters.AddWithValue("description", description);
                    cmd.Parameters.AddWithValue("author", author);
                    cmd.Parameters.AddWithValue("parent", parent);

                    result = cmd.ExecuteNonQuery();
                    
                    // hold parent ticket - waiting for answers
                    if (parent != null)
                    {
                        HoldTicket((int) parent);
                    }       
                }
                catch (SQLiteException ex)
                {
                    Debug.WriteLine(ex);
                }
                finally
                {
                    c.Close();
                }
            }
            return result;
        }

        public static dynamic AssignTicket(int id, int assignee)
        {
            dynamic result = null;
            using (SQLiteConnection c = new SQLiteConnection("Data Source=" + DB_PATH + ";Version=3;foreign keys=true;"))
            {
                try
                {
                    c.Open();

                    string sql = @"
                        UPDATE Ticket
                        SET Status = 'assigned', Assignee = @assignee
                        WHERE Id = @id
                    ";

                    SQLiteCommand cmd = new SQLiteCommand(sql, c);
                    
                    cmd.Parameters.AddWithValue("assignee", assignee);
                    cmd.Parameters.AddWithValue("id", id);

                    result = cmd.ExecuteNonQuery();
                }
                catch (SQLiteException ex)
                {
                    Debug.WriteLine(ex);
                }
                finally
                {
                    c.Close();
                }
            }
            return result;
        }

        public static dynamic AnswerTicket(int id, string answer)
        {
            dynamic result = null;
            using (SQLiteConnection c = new SQLiteConnection("Data Source=" + DB_PATH + ";Version=3;foreign keys=true;"))
            {
                try
                {
                    c.Open();

                    string sql = @"
                        UPDATE Ticket
                        SET Status = 'solved', Answer = @answer
                        WHERE Id = @id
                    ";

                    SQLiteCommand cmd = new SQLiteCommand(sql, c);

                    cmd.Parameters.AddWithValue("id", id);
                    cmd.Parameters.AddWithValue("answer", answer);

                    result = cmd.ExecuteNonQuery();
                }
                catch (SQLiteException ex)
                {
                    Debug.WriteLine(ex);
                }
                finally
                {
                    c.Close();
                }
            }
            return result;
        }

        private static dynamic HoldTicket(int id)
        {
            dynamic result = null;
            using (SQLiteConnection c = new SQLiteConnection("Data Source=" + DB_PATH + ";Version=3;foreign keys=true;"))
            {
                try
                {
                    c.Open();

                    string sql = @"
                        UPDATE Ticket
                        SET Status = 'waiting for answers'
                        WHERE Id = @id
                    ";

                    SQLiteCommand cmd = new SQLiteCommand(sql, c);

                    cmd.Parameters.AddWithValue("id", id);

                    result = cmd.ExecuteNonQuery();
                }
                catch (SQLiteException ex)
                {
                    Debug.WriteLine(ex);
                }
                finally
                {
                    c.Close();
                }
            }
            return result;
        }

        public static dynamic GetTicket(int id)
        {
            dynamic result = null;
            using (SQLiteConnection c = new SQLiteConnection("Data Source=" + DB_PATH + ";Version=3;foreign keys=true;"))
            {
                try
                {
                    c.Open();

                    string sql = @"
                        SELECT *
                        FROM Ticket
                        WHERE Id = @id
                    ";

                    SQLiteCommand cmd = new SQLiteCommand(sql, c);

                    cmd.Parameters.AddWithValue("id", id);

                    SQLiteDataReader reader = cmd.ExecuteReader();

                    result = new List<dynamic>();

                    while (reader.Read())
                    {
                        result.Add(new
                        {
                            id = reader["Id"],
                            title = reader["Title"],
                            description = reader["Description"],
                            author = reader["Author"],
                            createdAt = reader["CreatedAt"],
                            status = reader["Status"],
                            parent = reader["Parent"],
                            answer = reader["Answer"],
                            assignee = reader["Assignee"]
                        });
                    }
                }
                catch (SQLiteException ex)
                {
                    Debug.WriteLine(ex);
                }
                finally
                {
                    c.Close();
                }
            }
            return result;
        }

        public static dynamic GetTicketChildren(int id)
        {
            dynamic result = null;
            using (SQLiteConnection c = new SQLiteConnection("Data Source=" + DB_PATH + ";Version=3;foreign keys=true;"))
            {
                try
                {
                    c.Open();

                    string sql = @"
                        SELECT Id
                        FROM Ticket
                        WHERE Parent = @id
                    ";

                    SQLiteCommand cmd = new SQLiteCommand(sql, c);

                    cmd.Parameters.AddWithValue("id", id);

                    SQLiteDataReader reader = cmd.ExecuteReader();

                    result = new List<dynamic>();

                    while (reader.Read())
                    {
                        result.Add(new
                        {
                            id = reader["Id"]
                        });
                    }
                }
                catch (SQLiteException ex)
                {
                    Debug.WriteLine(ex);
                }
                finally
                {
                    c.Close();
                }
            }
            return result;
        }

        public static dynamic GetAuthorTickets(int id, string status)
        {
            dynamic result = null;
            using (SQLiteConnection c = new SQLiteConnection("Data Source=" + DB_PATH + ";Version=3;foreign keys=true;"))
            {
                try
                {
                    c.Open();

                    string sql;
                    if (status != null)
                    {
                        sql = @"
                            SELECT Id
                            FROM Ticket
                            WHERE Author = @id
                            AND Status = @status
                        ";
                    }
                    else
                    {
                        sql = @"
                            SELECT Id
                            FROM Ticket
                            WHERE Author = @id
                        ";
                    }

                    SQLiteCommand cmd = new SQLiteCommand(sql, c);

                    cmd.Parameters.AddWithValue("id", id);

                    if (status != null)
                    {
                        cmd.Parameters.AddWithValue("status", status);
                    }

                    SQLiteDataReader reader = cmd.ExecuteReader();

                    result = new List<dynamic>();

                    while (reader.Read())
                    {
                        result.Add(new
                        {
                            id = reader["Id"]
                        });
                    }
                }
                catch (SQLiteException ex)
                {
                    Debug.WriteLine(ex);
                }
                finally
                {
                    c.Close();
                }
            }
            return result;
        }

        public static dynamic GetSolverTickets(int id, string status)
        {
            dynamic result = null;
            using (SQLiteConnection c = new SQLiteConnection("Data Source=" + DB_PATH + ";Version=3;foreign keys=true;"))
            {
                try
                {
                    c.Open();

                    string sql;
                    if (status != null)
                    {
                        sql = @"
                            SELECT Id
                            FROM Ticket
                            WHERE Assignee = @id
                            AND Status = @status
                        ";
                    }
                    else
                    {
                        sql = @"
                            SELECT Id
                            FROM Ticket
                            WHERE Assignee = @id
                        ";
                    }

                    SQLiteCommand cmd = new SQLiteCommand(sql, c);

                    cmd.Parameters.AddWithValue("id", id);

                    if (status != null)
                    {
                        cmd.Parameters.AddWithValue("status", status);
                    }

                    SQLiteDataReader reader = cmd.ExecuteReader();

                    result = new List<dynamic>();

                    while (reader.Read())
                    {
                        result.Add(new
                        {
                            id = reader["Id"]
                        });
                    }
                }
                catch (SQLiteException ex)
                {
                    Debug.WriteLine(ex);
                }
                finally
                {
                    c.Close();
                }
            }
            return result;
        }

        public static dynamic GetUnassignedTickets()
        {
            dynamic result = null;
            using (SQLiteConnection c = new SQLiteConnection("Data Source=" + DB_PATH + ";Version=3;foreign keys=true;"))
            {
                try
                {
                    c.Open();

                    string sql = @"
                        SELECT Id
                        FROM Ticket
                        WHERE Status = 'unassigned'
                    ";

                    SQLiteCommand cmd = new SQLiteCommand(sql, c);

                    SQLiteDataReader reader = cmd.ExecuteReader();

                    result = new List<dynamic>();

                    while (reader.Read())
                    {
                        result.Add(new
                        {
                            id = reader["Id"]
                        });
                    }
                }
                catch (SQLiteException ex)
                {
                    Debug.WriteLine(ex);
                }
                finally
                {
                    c.Close();
                }
            }
            return result;
        }

        #endregion

        #region Department

        public static dynamic AddDepartment (string name)
        {
            dynamic result = null;
            using (SQLiteConnection c = new SQLiteConnection("Data Source=" + DB_PATH + ";Version=3;foreign keys=true;"))
            {
                try
                {
                    c.Open();
                    string sql = "insert into Department(Name) values (@name)";
                    SQLiteCommand cmd = new SQLiteCommand(sql, c);
                    cmd.Parameters.AddWithValue("name", name);
                    result = cmd.ExecuteNonQuery();
                }
                catch (SQLiteException ex)
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
            using (SQLiteConnection c = new SQLiteConnection("Data Source=" + DB_PATH + ";Version=3;foreign keys=true;"))
            {
                try
                {
                    c.Open();
                    string sql = "delete from Department where Department.id = @id";
                    SQLiteCommand cmd = new SQLiteCommand(sql, c);
                    cmd.Parameters.AddWithValue("id", id);
                    result = cmd.ExecuteNonQuery();
                }
                catch (SQLiteException ex)
                {

                }
                finally
                {
                    c.Close();
                }
            }
            return result;
        }

        #endregion

        #region User

        public static dynamic AddUser(string name, string email, string hash, string salt, int department)
        {
            dynamic result;
            using (SQLiteConnection c = new SQLiteConnection("Data Source=" + DB_PATH + ";Version=3;foreign keys=true;"))
            {
                try
                {
                    c.Open();
                    string sql = 
                        @"insert into User(Name, Email, Hash, Salt, Department)
                        values (@name, @email, @hash, @salt, @department)";
                    SQLiteCommand cmd = new SQLiteCommand(sql, c);
                    cmd.Parameters.AddWithValue("name", name);
                    cmd.Parameters.AddWithValue("email", email);
                    cmd.Parameters.AddWithValue("hash", hash);
                    cmd.Parameters.AddWithValue("salt", salt);
                    cmd.Parameters.AddWithValue("department", department);
                    result = cmd.ExecuteNonQuery();
                }
                catch(SQLiteException ex)
                {
                    Debug.WriteLine(ex);
                    result = null;
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
            dynamic result;
            using (SQLiteConnection c = new SQLiteConnection("Data Source=" + DB_PATH + ";Version=3;foreign keys=true;"))
            {
                try
                {
                    c.Open();
                    string sql = "select * from User where User.id = @id";
                    SQLiteCommand cmd = new SQLiteCommand(sql, c);
                    cmd.Parameters.AddWithValue("id", id);
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    result = new
                    {
                        id = reader["Id"],
                        name = reader["Name"],
                        email = reader["Email"],
                        hash = reader["Hash"],
                        salt = reader["Salt"],
                        department = reader["Department"]
                    };
                    
                }
                catch (SQLiteException ex)
                {
                    result = null;
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
            dynamic result;
            using (SQLiteConnection c = new SQLiteConnection("Data Source=" + DB_PATH + ";Version=3;foreign keys=true;"))
            {
                try
                {
                    c.Open();
                    string sql = "select * from User where User.email = @email";
                    SQLiteCommand cmd = new SQLiteCommand(sql, c);
                    cmd.Parameters.AddWithValue("email", email);
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    reader.Read();                    
                    result = new
                    {
                        id = reader["Id"],
                        name = reader["Name"],
                        email = reader["Email"],
                        hash = reader["Hash"],
                        salt = reader["Salt"],
                        department = reader["Department"]
                    };                    
                }
                catch (SQLiteException ex)
                {
                    result = null;
                }
                finally
                {
                    c.Close();
                }
            }
            return result;
        }

        #endregion
    }
}
