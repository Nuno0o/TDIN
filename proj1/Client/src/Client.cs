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
        //Current orders and quote
        public static List<dynamic> buy_orders;
        public static List<dynamic> sell_orders;
        public static List<dynamic> quotes;
        //Used to keep track of the time any order went inactive, and after DIFFERENCE the order will go active again, and the order removed from the dictionary
        public const long DIFFERENCE = 60000;
        public static Dictionary<int, long> b_activateTimers = new Dictionary<int, long>();
        public static Dictionary<int, long> s_activateTimers = new Dictionary<int, long>();

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
            if (Client.quotes.Count == 0) return 1.0;
            return Client.quotes[0].value;
        }

        public static dynamic BuyOrderById(int id)
        {
            foreach(dynamic buy_order in buy_orders)
            {
                if(buy_order.id == id)
                {
                    return buy_order;
                }
            }
            return null;
        }

        public static dynamic SellOrderById(int id)
        {
            foreach (dynamic sell_order in sell_orders)
            {
                if (sell_order.id == id)
                {
                    return sell_order;
                }
            }
            return null;
        }
    }
}
