using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using Newtonsoft.Json;
using System.Collections;
using System.Threading;
using System.Diagnostics;

namespace TTService
{
    public class TTServ : ITTServ, IAuthServ
    {
        #region Operations
        public string HelloWorld(string name)
        {
            return "test"; 
        }
        public string InitDb(bool overwrite)
        {
            Database.Init(overwrite);
            return null;
        }
        public dynamic AddDepartment(string name)
        {
            return Database.AddDepartment(name);
        }
        public dynamic AddTicket(string title, string description, int author, int? parent)
        {
            return Database.AddTicket(title, description, author, parent);
        }
        public dynamic AssignTicket(int id, int assignee)
        {
            return Database.AssignTicket(id, assignee);
        }
        public dynamic AnswerTicket(int id, string answer)
        {
            return Database.AnswerTicket(id, answer);
        }
        public dynamic GetTicket(int id)
        {
            return Database.GetTicket(id);
        }
        public dynamic GetTicketChildren(int id)
        {
            return Database.GetTicketChildren(id);
        }
        public dynamic GetAuthorTickets(int id, string status)
        {
            return Database.GetAuthorTickets(id, status);
        }
        public dynamic GetSolverTickets(int id, string status)
        {
            return Database.GetSolverTickets(id, status);
        }
        #endregion

        #region Auth

        public string login(string email, string hash)
        {
            dynamic res = null;
            dynamic user = Database.GetUser(email);

            if (user == null)
            {
                res = new { error = "Invalid email!" };
            }
            else if (user.Hash != hash)
            {
                res = new { error = "Invalid hash!" };
            }
            else
            {
                string token = createToken();
                mut.WaitOne();
                sessions.Add(token, user.Id);
                mut.Close();
                res = new { token = token };
            }

            return JsonConvert.SerializeObject(res);
        }

        public string register(string name, string email, string hash, string salt, int department)
        {
            dynamic res = null;
            dynamic user = Database.AddUser(name, email, hash, salt, department);

            if (user == null)
            {
                res = new { error = "Email already in use!" };
            }
            else
            {
                res = new { success = "User created!" };
            }

            return JsonConvert.SerializeObject(res);
        }

        public string logout(string token)
        {
            dynamic res = null;

            if (!sessions.ContainsKey(token))
            {
                res = new { error = "Invalid token!" };
            }
            else
            {
                sessions.Remove(token);
                res = new { success = "Session ended!" };
            }

            return JsonConvert.SerializeObject(res);
        }

        public string getSalt(string email)
        {
            dynamic res = null;
            dynamic user = Database.GetUser(email);

            if (user == null)
            {
                res = new { error = "Invalid email!" };
            }
            else
            {
                res = new { salt = user.Salt };
            }

            return JsonConvert.SerializeObject(res);
        }

        /* session management stuff */
        private static Hashtable sessions = new Hashtable();
        private static Mutex mut = new Mutex();
        public static int tokenLifetime = 300; // in seconds

        public static bool isValidToken(int id, string token)
        {
            if (!sessions.ContainsKey(token)) return false;
            if (tokenIsExpired(token)) return false;
            token = createToken();
            mut.WaitOne();
            sessions.Add(token, id);
            mut.Close();
            return true;
        }

        private static string createToken()
        {
            byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            byte[] key = Guid.NewGuid().ToByteArray();
            string token = Convert.ToBase64String(time.Concat(key).ToArray());
            return token;
        }

        private static bool tokenIsExpired(string token)
        {
            byte[] data = Convert.FromBase64String(token);
            DateTime when = DateTime.FromBinary(BitConverter.ToInt64(data, 0));
            return when < DateTime.UtcNow.AddSeconds(-tokenLifetime);
        }

        #endregion
    }
}
