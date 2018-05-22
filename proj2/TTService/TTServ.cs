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

        public string AddDepartment(string name)
        {
            dynamic res = null;
            dynamic rows = Database.AddDepartment(name);

            if (rows == null || rows <= 0)
            {
                res = new { error = "Couldn't add a new department!" };
            }
            else
            {
                res = new { success = "Department added!" };
            }

            return JsonConvert.SerializeObject(res);
        }

        public string AddTicket(string title, string description, int author, int? parent)
        {
            dynamic res = null;
            dynamic rows = Database.AddTicket(title, description, author, parent);

            if (rows == null || rows <= 0)
            {
                res = new { error = "Couldn't add a new ticket!" };
            }
            else
            {
                res = new { success = "Ticket added!" };
            }

            return JsonConvert.SerializeObject(res);
        }

        public string AssignTicket(int id, int assignee)
        {
            dynamic res = null;
            dynamic rows = Database.AssignTicket(id, assignee);

            if (rows == null || rows <= 0)
            {
                res = new { error = "Couldn't assign ticket!" };
            }
            else
            {
                res = new { success = "Ticket assigned!" };
            }

            return JsonConvert.SerializeObject(res);            
        }

        public string AnswerTicket(int id, string answer)
        {
            dynamic res = null;
            dynamic rows = Database.AnswerTicket(id, answer);

            if (rows == null || rows <= 0)
            {
                res = new { error = "Couldn't answer ticket!" };
            }
            else
            {
                res = new { success = "Ticket answered!" };
            }

            return JsonConvert.SerializeObject(res);
        }

        public string GetTicket(int id)
        {
            dynamic res = null;
            dynamic ticket = Database.GetTicket(id);

            if (ticket == null)
            {
                res = new { error = "Couldn't retrieve ticket!" };
            }
            else
            {
                res = ticket;
            }

            return JsonConvert.SerializeObject(res);
        }

        public string GetTicketChildren(int id)
        {
            dynamic res = null;
            dynamic children = Database.GetTicketChildren(id);

            if (children == null)
            {
                res = new { error = "Couldn't retrieve ticket's children!" };
            }
            else
            {
                res = children;
            }

            return JsonConvert.SerializeObject(res);
        }

        public string GetAuthorTickets(int id, string status)
        {
            dynamic res = null;
            dynamic tickets = Database.GetAuthorTickets(id, status);

            if (tickets == null)
            {
                res = new { error = "Couldn't retrieve tickets!" };
            }
            else
            {
                res = tickets;
            }

            return JsonConvert.SerializeObject(res);
        }

        public string GetSolverTickets(int id, string status)
        {
            dynamic res = null;
            dynamic tickets = Database.GetSolverTickets(id, status);

            if (tickets == null)
            {
                res = new { error = "Couldn't retrieve tickets!" };
            }
            else
            {
                res = tickets;
            }

            return JsonConvert.SerializeObject(res);
        }

        public string GetUnassignedTickets()
        {
            dynamic res = null;
            dynamic tickets = Database.GetUnassignedTickets();

            if (tickets == null)
            {
                res = new { error = "Couldn't retrieve tickets!" };
            }
            else
            {
                res = tickets;
            }

            return JsonConvert.SerializeObject(res);
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
