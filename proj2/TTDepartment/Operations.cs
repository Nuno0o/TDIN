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
        //auth
        public static string token;
        public static TTServClient serv_proxy = new TTServClient();
        public static AuthServClient auth_proxy = new AuthServClient();
        //interface
        public static Login login = null;
        public static Main main = null;
        //all information
        public static int userid = -1;
        public static string name = "";
        public static string useremail = "";
        public static int department = -1;
        public static Message[] messages = new Message[0];

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
                if(m.Length != messages.Length)
                {
                    messages = m;
                    updateInterface();
                }
                    
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
        public static void removeMessageFromQueue(int id)
        {
            String toremoveid = null;
            foreach(Message m in messages)
            {
                dynamic ticket = JsonConvert.DeserializeObject(m.Body.ToString());
                if (ticket.id == id.ToString())
                {
                    toremoveid = m.Id;
                }
            }
            if(toremoveid == null)
            {
                return;
            }
            try
            {
                string path = ".\\private$\\dep" + department;
                if (!MessageQueue.Exists(path))
                {
                    MessageQueue.Create(path);
                }
                MessageQueue q = new MessageQueue(path);
                q.Formatter = new BinaryMessageFormatter();
                Message m = q.ReceiveById(toremoveid);
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
        public static void getInformation()
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

            receiveMessageDepartment();

            beginAsyncReceiveMessages();
        }

        public static void updateInterface()
        {
            if (main == null)
                return;
            main.updateInterface();
        }
        

        #region Auth

        

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
                auth_proxy.Open();

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
    }
}
