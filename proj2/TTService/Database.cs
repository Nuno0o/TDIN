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

        public static dynamic AddTicket (string title, string description, int author, int parent)
        {
            dynamic result = null;
            using (SqlConnection c = new SqlConnection(DBPATH))
            {
                try
                {
                    c.Open();
                    string sql = "insert into Ticket(Title, Description, Author, CreatedAt, Parent) " +
                                  "values (@title, @description, @author, datetime(), @parentid)";
                    SqlCommand cmd = new SqlCommand(sql, c);
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
