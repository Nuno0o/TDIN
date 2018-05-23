using System;
using System.Text;
using System.Messaging;
using TTSolver.TTSvc;
using System.Security.Cryptography;
using Newtonsoft.Json;
using System.ServiceModel;
using System.Diagnostics;
using System.Collections.Generic;

namespace TTSolver
{
    static class Operations
    {
        //auth
        public static TTServClient serv_proxy = new TTServClient();
        public static AuthServClient auth_proxy = new AuthServClient();
        public static string token;
        //callbacks
        public static TTCallback cb = new TTCallback();
        public static InstanceContext icntxt = new InstanceContext(cb);
        public static TTCallbacks.TTServContractsClient c = new TTCallbacks.TTServContractsClient(icntxt);
        //interface
        public static Login login = null;
        public static Main main = null;
        //all information
        public static int userid = -1;
        public static string name = "";
        public static string useremail = "";
        public static int department = -1;
        public static List<dynamic> departments;
        public static List<dynamic> unassigned_tickets;
        public static List<dynamic> assigned_tickets;
        public static List<dynamic> department_tickets;


        public static void init()
        {
            //subscribe to callbacks from server
            c.Subscribe();
        }

        #region auth

        public static string GetToken()
        {
            return token;
        }

        public static bool Login(string email, string password)
        {
            bool ret;

            try
            {

                string json;
                dynamic res;

                json = auth_proxy.GetSalt(email);
                res = JsonConvert.DeserializeObject(json);

                string salt = res.salt;
                string hash = Hash(password + salt);

                json = auth_proxy.Login(email, hash);
                if (json.Contains("token"))
                {
                    res = JsonConvert.DeserializeObject(json);
                    token = res.token;
                    useremail = email;
                    getInformation();
                    ret = true;
                }
                else
                {
                    ret = false;
                }
            }
            catch (Exception ex)
            {
                ret = false;
            }
            finally
            {
            }

            return ret;
        }

        public static bool Register(string name, string email, string password, int department)
        {
            bool ret;

            try
            {

                string salt = Salt();
                string hash = Hash(password + salt);

                string json = auth_proxy.Register(name, email, hash, salt, department);

                ret = json.Contains("success");
            }
            catch (Exception ex)
            {
                ret = false;
            }
            finally
            {
            }

            return ret;
        }

        private static string Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            SHA256 algorithm = SHA256.Create();

            byte[] bytes = algorithm.ComputeHash(
                new UTF8Encoding().GetBytes(input)
            );

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }

            return hash.ToString();
        }

        private static string Salt()
        {
            Random rng = new Random();
            string alphabet =
                "qwertyuiopasdfghjklzxcvbnm" +
                "QWERTYUIOPASDFGHJKLZXCVBNM" +
                "1234567890";
            string salt = "";

            while (salt.Length != 16)
            {
                int index = rng.Next() % alphabet.Length;
                salt += alphabet.Substring(index, 1);
            }

            return salt;
        }
        #endregion

        #region departments
        public static bool sendMessageToDepartment(string department, string message)
        {
            try
            {
                string path = ".\\private$\\dep" + department;
                if (!MessageQueue.Exists(path))
                {
                    MessageQueue.Create(path);
                }
                MessageQueue q = new MessageQueue(path);
                q.Formatter = new BinaryMessageFormatter();
                q.Send(message);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        #endregion

        #region solver

        public static void updateInterface()
        {
            main.updateInterface();
        }

        public static void getInformation()
        {
            try
            {
                if (useremail == "")
                    return;
                dynamic userinfo = JsonConvert.DeserializeObject(serv_proxy.GetUserByEmail(useremail));
                if (userinfo == null)
                    return;
                //user info

                name = userinfo.name;
                userid = userinfo.id;
                department = userinfo.department;
                //unassigned and assigned tickets
                departments = JsonConvert.DeserializeObject<List<dynamic>>(serv_proxy.GetDepartments());
                unassigned_tickets = new List<dynamic>();
                List<dynamic> unassigned_tickets_ids = JsonConvert.DeserializeObject<List<dynamic>>(serv_proxy.GetUnassignedTickets());
                foreach(dynamic ticket in unassigned_tickets_ids)
                {
                    unassigned_tickets.Add(JsonConvert.DeserializeObject(serv_proxy.GetTicket((int)ticket.id)));
                }
                assigned_tickets = new List<dynamic>();
                List<dynamic> assigned_tickets_ids = JsonConvert.DeserializeObject<List<dynamic>>(serv_proxy.GetSolverTickets(userid, null));
                foreach (dynamic ticket in assigned_tickets_ids)
                {
                    assigned_tickets.Add(JsonConvert.DeserializeObject(serv_proxy.GetTicket((int)ticket.id)));
                }
                department_tickets = new List<dynamic>();
                List<dynamic> department_tickets_ids = JsonConvert.DeserializeObject<List<dynamic>>(serv_proxy.GetAuthorTicketsDepartment(userid));
                foreach(dynamic ticket in department_tickets_ids)
                {
                    department_tickets.Add(JsonConvert.DeserializeObject(serv_proxy.GetTicketDepartment((int)ticket.id)));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public static void assignTicket(int id, int assignee)
        {
            try
            {
                serv_proxy.AssignTicket(id, assignee);
            }
            catch (Exception e)
            {

            }
        }

        #endregion
    }
}
