using System;
using System.Messaging;
using System.Diagnostics;
using TTDepartment.TTSvc;
using Newtonsoft.Json;
using System.Text;
using System.Security.Cryptography;

namespace TTDepartment
{
    static class Operations
    {
        public static string department = "";
        public static Message[] messages;

        static Operations()
        {
            auth_proxy.Open();
            serv_proxy.Open();
        }

        public static bool receiveMessageDepartment()
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
                Message[] m = q.GetAllMessages();
                messages = m;
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;


        }
        public static void beginAsyncReceiveMessages()
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
                q.PeekCompleted += QueuePeeker;
                q.BeginPeek();
            }
            catch (Exception ex)
            {

            }
        }
        public static void removeMessageFromQueue(string id)
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
                Message m = q.ReceiveById(id);
                Debug.WriteLine(m.Body);
            }
            catch (Exception ex)
            {

            }
        }
        public static void QueuePeeker(object src, PeekCompletedEventArgs rcea)
        {
            MessageQueue q = (MessageQueue)src;
            Message m = q.EndReceive(rcea.AsyncResult);
            receiveMessageDepartment();
            q.BeginPeek();
        }

        #region Auth

        private static string token;
        private static TTServClient serv_proxy = new TTServClient();
        private static AuthServClient auth_proxy = new AuthServClient();

        public static string GetToken()
        {
            return token;
        }

        public static bool Login(string email, string password)
        {
            bool ret;

            try
            {
                //auth_proxy.Open();

                string json;
                dynamic res;

                json = auth_proxy.getSalt(email);
                res = JsonConvert.DeserializeObject(json);

                string salt = res.salt;
                string hash = Hash(password + salt);

                json = auth_proxy.login(email, hash);
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
                //auth_proxy.Close();
            }

            return ret;
        }

        public static bool Register(string name, string email, string password, int department)
        {
            bool ret;

            try
            {
                //auth_proxy.Open();

                string salt = Salt();
                string hash = Hash(password + salt);

                string json = auth_proxy.register(name, email, hash, salt, department);

                ret = json.Contains("success");
            }
            catch (Exception ex)
            {
                ret = false;
            }
            finally
            {
                //auth_proxy.Close();
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
