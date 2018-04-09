using Server;
using System;
using System.Windows.Forms;
using System.Runtime.Remoting;
using System.Collections.Generic;
using System.Threading;
using System.Text;
using System.Security.Cryptography;

namespace Client
{
    static class Client
    {
        public static Services stubs;
        public static Login login;
        public static Menu menu;

        public static Mutex mut;
        //Balance
        public static double balance;
        //Diginotes with sell orders included
        public static int diginotes;
        //Diginotes without sell orders included
        public static int realdiginotes;
        public static string username;

        public static List<Object> buy_orders;
        public static List<Object> sell_orders;
        public static List<Object> quotes;

        [STAThread]
        static void Main()
        {
            stubs = (Services)RemotingServices.Connect(
                typeof(Services),
                "tcp://localhost:9000/Server/Services"
            );

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            mut = new Mutex();
            login = new Login();
            Application.Run(login);
            
        }
        public static string Hash(string input)
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

        public static string Salt()
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

        public static double GetCurrentQuote()
        {
            List<double> qts = new List<double>();
            foreach (dynamic quote in Client.quotes)
            {
                qts.Add((double)quote.value);
            }
            return qts[0];
        }
    }
}
