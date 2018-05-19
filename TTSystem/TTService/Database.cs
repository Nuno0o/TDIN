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
            dynamic result = -1;
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

        public static dynamic AddUser ()
        {
            using (SqlConnection c = new SqlConnection(DBPATH))
            {
                try
                {
                    c.Open();

                }
                catch(SqlException ex)
                {

                }
                finally
                {
                    c.Close();
                }
            }
            return -1;
        }

        public static dynamic AddTicket ()
        {
            using (SqlConnection c = new SqlConnection(DBPATH))
            {
                try
                {
                    c.Open();
                }
                catch (SqlException)
                {
                }
                finally
                {
                    c.Close();
                }
            }
            return -1;
        }
    }
}
