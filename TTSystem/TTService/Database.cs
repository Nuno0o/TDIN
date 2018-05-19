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

        public static dynamic AddTicket()
        {
            using (SqlConnection c = new SqlConnection(DBPATH))
            {
                try
                {
                    c.Open();
                    //string sql = "insert into TTickets(Author, Problem, Answer, Status) values (" + author + ", '" + problem + "', '', 1)";
                    //SqlCommand cmd = new SqlCommand(sql, c);
                    //cmd.ExecuteNonQuery();
                    //cmd.CommandText = "select max(Id) from TTickets";
                    //id = (int)cmd.ExecuteScalar();
                }
                catch (SqlException)
                {
                }
                finally
                {
                    c.Close();
                }
            }
            return 1;
        }
    }
}
