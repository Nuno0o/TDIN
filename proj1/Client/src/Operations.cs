using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Client
{
    static class Operations
    {
        public static void login(string username, string password)
        {
            try
            {
                string json = Client.stub.Login(username, password);
                dynamic obj = JsonConvert.DeserializeObject(json);
                if (!obj) throw new System.Exception("Login failed");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return;
        }

        public static void register(string username, string password)
        {
            try
            {
                string json = Client.stub.Register(username, password);
                dynamic obj = JsonConvert.DeserializeObject(json);
                if (!obj) throw new System.Exception("Register failed");
            }catch(Exception ex)
            {
                throw ex;
            }
            return;
        }
    }
}
