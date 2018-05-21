using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTService
{
    static class Database
    {
        public static string DBPATH = ConfigurationManager.ConnectionStrings["TTs"].ConnectionString;

        #region Ticket

        public static dynamic AddTicket(string title, string description, int author, int? parent)
        {            
            dynamic result = null;
            using (SqlConnection c = new SqlConnection(DBPATH))
            {
                try
                {
                    c.Open();

                    string sql;
                    if (parent != null)
                    {
                        sql = @"
                            INSERT INTO Ticket (Title, Description, Author, Parent)
                            VALUES (@title, @description, @author, @parent)
                        ";
                    }
                    else
                    {
                        sql = @"
                            INSERT INTO Ticket (Title, Description, Author)
                            VALUES (@title, @description, @author)
                        ";
                    }                    

                    SqlCommand cmd = new SqlCommand(sql, c);

                    cmd.Parameters.AddWithValue("title", title);
                    cmd.Parameters.AddWithValue("description", description);
                    cmd.Parameters.AddWithValue("author", author);

                    if (parent != null)
                    {
                        cmd.Parameters.AddWithValue("parent", parent);
                    }

                    result = cmd.ExecuteNonQuery();
                    
                    // hold parent ticket - waiting for answers
                    if (parent != null)
                    {
                        HoldTicket((int) parent);
                    }       
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex);
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
            using (SqlConnection c = new SqlConnection(DBPATH))
            {
                try
                {
                    c.Open();

                    string sql = @"
                        UPDATE Ticket
                        SET Status = 'assigned', Assignee = @assignee
                        WHERE Id = @id
                    ";

                    SqlCommand cmd = new SqlCommand(sql, c);
                    
                    cmd.Parameters.AddWithValue("assignee", assignee);
                    cmd.Parameters.AddWithValue("id", id);

                    result = cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex);
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
            using (SqlConnection c = new SqlConnection(DBPATH))
            {
                try
                {
                    c.Open();

                    string sql = @"
                        SELECT *
                        FROM Ticket
                        WHERE Id = @id
                    ";

                    SqlCommand cmd = new SqlCommand(sql, c);

                    cmd.Parameters.AddWithValue("id", id);

                    SqlDataReader reader = cmd.ExecuteReader();

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
                catch (SqlException ex)
                {
                    Console.WriteLine(ex);
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
            using (SqlConnection c = new SqlConnection(DBPATH))
            {
                try
                {
                    c.Open();

                    string sql = @"
                        SELECT Id
                        FROM Ticket
                        WHERE parent = @id
                    ";

                    SqlCommand cmd = new SqlCommand(sql, c);

                    cmd.Parameters.AddWithValue("id", id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    result = new List<dynamic>();

                    while (reader.Read())
                    {
                        result.Add(new
                        {
                            id = reader["Id"]
                        });
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex);
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
            using (SqlConnection c = new SqlConnection(DBPATH))
            {
                try
                {
                    c.Open();

                    string sql = @"
                        UPDATE Ticket
                        SET Status = 'solved', Answer = @answer
                        WHERE Id = @id
                    ";

                    SqlCommand cmd = new SqlCommand(sql, c);

                    cmd.Parameters.AddWithValue("id", id);
                    cmd.Parameters.AddWithValue("answer", answer);

                    result = cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex);
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
            using (SqlConnection c = new SqlConnection(DBPATH))
            {
                try
                {
                    c.Open();

                    string sql = @"
                        UPDATE Ticket
                        SET Status = 'waiting for answers'
                        WHERE Id = @id
                    ";

                    SqlCommand cmd = new SqlCommand(sql, c);

                    cmd.Parameters.AddWithValue("id", id);

                    result = cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex);
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
            using (SqlConnection c = new SqlConnection(DBPATH))
            {
                try
                {
                    c.Open();
                    string sql = "insert into Department(Name) values (@name)";
                    SqlCommand cmd = new SqlCommand(sql, c);
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
            using (SqlConnection c = new SqlConnection(DBPATH))
            {
                try
                {
                    c.Open();
                    string sql = "delete from Department where Department.id = @id";
                    SqlCommand cmd = new SqlCommand(sql, c);
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

        #endregion

        #region User

        public static dynamic AddUser(string name, string email, string hash, string salt, int department)
        {
            dynamic result = null;
            using (SqlConnection c = new SqlConnection(DBPATH))
            {
                try
                {
                    c.Open();
                    string sql = 
                        @"insert into User(Name, Email, Password, Department)
                        values (@name, @email, @hash, @salt, @department)";
                    SqlCommand cmd = new SqlCommand(sql, c);
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
            using (SqlConnection c = new SqlConnection(DBPATH))
            {
                try
                {
                    c.Open();
                    string sql = "select * from User where User.id = @id";
                    SqlCommand cmd = new SqlCommand(sql, c);
                    cmd.Parameters.AddWithValue("id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
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
            using (SqlConnection c = new SqlConnection(DBPATH))
            {
                try
                {
                    c.Open();
                    string sql = "select * from User where User.email = @email";
                    SqlCommand cmd = new SqlCommand(sql, c);
                    cmd.Parameters.AddWithValue("email", email);
                    SqlDataReader reader = cmd.ExecuteReader();
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

        #endregion
    }
}
