using System;
using TTClient.TTSvc;
using Newtonsoft.Json;
using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;

namespace TTClient
{
    static class Operations
    {
        #region Auth

        public static TTServClient serv_proxy = new TTServClient();
        public static AuthServClient auth_proxy = new AuthServClient();
        private static string token = null;

        public static dynamic GetDepartments()
        {
            dynamic departments;
            
            try
            {

                string json = serv_proxy.GetDepartments();

                if (json.Contains("error"))
                {
                    return null;
                }
                else
                {
                    departments = JsonConvert.DeserializeObject(json);

                }
            }
            catch (Exception e)
            {
                departments = null;
            }

            return departments;
        }

        public static List<dynamic> GetQuestions(int id)
        {
            List<dynamic> tickets = new List<dynamic>();

            try
            {
                string json = serv_proxy.GetTicketChildren(id);

                if (json.Contains("error"))
                {
                    return null;
                }
                else
                {
                    dynamic res = JsonConvert.DeserializeObject(json);

                    foreach (dynamic ticket in res)
                    {
                        json = serv_proxy.GetTicket((int)ticket.id.Value);
                        if (json.Contains("error")) continue;

                        res = JsonConvert.DeserializeObject(json);
                        tickets.Add(new
                        {                            
                            question = res.description,
                            answer = res.answer
                        });
                    }
                }
            }
            catch (Exception e)
            {
                tickets = null;
            }

            return tickets;
        }

        public static dynamic GetTicket(int id)
        {
            dynamic res;
            try
            {
                string json = serv_proxy.GetTicket(id);
                res = JsonConvert.DeserializeObject(json);

                if (res == null)
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                res = null;
            }

            return res;
        }

        public static List<dynamic> GetTickets()
        {
            List<dynamic> tickets = new List<dynamic>();

            try
            {
                string json = serv_proxy.GetAuthorTickets(token, null);                

                if (json.Contains("error"))
                {
                    return null;
                }
                else
                {
                    dynamic res = JsonConvert.DeserializeObject(json);

                    foreach (dynamic ticket in res)
                    {
                        json = serv_proxy.GetTicket((int)ticket.id.Value);                        
                        if (json.Contains("error")) continue;
                        res = JsonConvert.DeserializeObject(json);

                        tickets.Add(new
                        {
                            id = res.id,
                            title = res.title,
                            date = res.createdAt,
                            status = res.status
                        });
                    }
                }
            }
            catch(Exception e)
            {
                tickets = null;
            }           

            return tickets;
        }

        public static bool SendTicket(string title, string description)
        {
            bool ret;

            try
            {
                string json = serv_proxy.AddTicket(title, description, token, null);
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

        public static bool SendQuestion(string question, int parent)
        {
            bool ret;

            try
            {
                string json = serv_proxy.AddTicket("", question, token, parent);
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

        public static string GetToken()
        {
            return token;
        }

        public static bool Logout()
        {
            bool ret;

            try
            {
                string json = auth_proxy.Logout(token);
                ret = json.Contains("success");
                if (ret) token = null;
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
    }
}
