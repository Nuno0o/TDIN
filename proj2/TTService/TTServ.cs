using System;
using System.Linq;
using Newtonsoft.Json;
using System.Collections;

namespace TTService
{
    public class TTServ : ITTServ, IAuthServ
    {
        #region Operations

        public string HelloWorld(string name)
        {
            TTCallbacks.m_event();
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

        public string AddTicket(string title, string description, string token, int? parent)
        {
            dynamic res = null;
            int id = isValidToken(token);

            if (id < 0)
            {
                res = new { error = "Invalid Token!" };
            }
            else
            {
                dynamic rows = Database.AddTicket(title, description, id, parent);

                if (rows == null || rows <= 0)
                {
                    res = new { error = "Couldn't add a new ticket!" };
                }
                else
                {
                    res = new { success = "Ticket added!" };
                }
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

        public string GetAuthorTickets(string token, string status)
        {
            dynamic res = null;
            int id = isValidToken(token);

            if (id < 0)
            {
                res = new { error = "Invalid Token!" };
            }
            else
            {
                dynamic tickets = Database.GetAuthorTickets(id, status);

                if (tickets == null)
                {
                    res = new { error = "Couldn't retrieve tickets!" };
                }
                else
                {
                    res = tickets;
                }
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

        public string GetUserByEmail(string email)
        {
            dynamic res = null;
            dynamic user = Database.GetUser(email);
            if (user == null)
            {
                res = new { error = "Couldn't retrieve tickets!" };
            }
            else
            {
                res = user;
            }
            res = new { id = user.id, name = user.name, email = user.email, department = user.department };

            return JsonConvert.SerializeObject(res);
        }

        public string GetUserById(int id)
        {
            dynamic res = null;
            dynamic user = Database.GetUser(id);
            if (user == null)
            {
                res = new { error = "Couldn't retrieve tickets!" };
            }
            else
            {
                res = user;
            }
            res = new { id = user.id, name = user.name, email = user.email, department = user.department };

            return JsonConvert.SerializeObject(res);
        }

        #endregion

        #region Auth

        public string Login(string email, string hash)
        {
            dynamic res = null;
            dynamic user = Database.GetUser(email);

            if (user == null)
            {
                res = new { error = "Invalid email!" };
            }
            else if (user.hash != hash)
            {
                res = new { error = "Invalid hash!" };
            }
            else
            {
                string token = createToken();
                //mut.WaitOne();
                sessions[token] = new {
                    id = user.id,
                    time = DateTime.Now
                };
                //mut.Close();
                res = new { token = token };
            }

            return JsonConvert.SerializeObject(res);
        }

        public string Register(string name, string email, string hash, string salt, int department)
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

            string json = JsonConvert.SerializeObject(res);

            return json;
        }

        public string Logout(string token)
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

        public string GetSalt(string email)
        {
            dynamic res = null;
            dynamic user = Database.GetUser(email);

            if (user == null)
            {
                res = new { error = "Invalid email!" };
            }
            else
            {
                res = new { salt = user.salt };
            }

            return JsonConvert.SerializeObject(res);
        }

        /* session management stuff */
        private static Hashtable sessions = new Hashtable();
        //private static Mutex mut = new Mutex();
        private static int tokenLifetime = 300; // in seconds

        private static int isValidToken(string token)
        {
            if (!sessions.ContainsKey(token)) return -1;
            dynamic session = sessions[token];
            DateTime before = session.time;
            DateTime now = DateTime.Now;
            int id = (int)session.id;
            if (before.AddSeconds(tokenLifetime) < now) return -1;
            //mut.WaitOne();
            sessions[token] = new {
                id = id,
                time = now
            };
            //mut.Close();
            return id;
        }

        private static string createToken()
        {            
            byte[] key = Guid.NewGuid().ToByteArray();
            string token = Convert.ToBase64String(key);
            return token;
        }

        #endregion
    }
}
