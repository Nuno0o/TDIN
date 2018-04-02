using Server;
using System;
using System.Windows.Forms;
using System.Runtime.Remoting;
using System.Collections.Generic;

namespace Client
{
    static class Client
    {
        public static RemObj stub;
        public static Login login;
        public static Menu menu;

        public static double balance;
        public static int diginotes;
        public static string username;

        public static List<Object> buy_orders;
        public static List<Object> sell_orders;

        [STAThread]
        static void Main()
        {
            stub = (RemObj)RemotingServices.Connect(
                typeof(RemObj),
                "tcp://localhost:9000/Server/RemObj"
            );            

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            login = new Login();
            Application.Run(login);
            
        }
    }
}
