using System;
using System.Text;
using System.Messaging;
using TTSolver.TTSvc;
using System.Security.Cryptography;
using Newtonsoft.Json;
using System.ServiceModel;

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
            AuthServClient proxy = new AuthServClient();

            try
            {
                proxy.Open();

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
                proxy.Close();
            }

            return ret;
        }

        public static bool Register(string name, string email, string password, int department)
        {
            bool ret;
            AuthServClient proxy = new AuthServClient();

            try
            {
                proxy.Open();

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
                proxy.Close();
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



        #endregion
    }
}
