using System;
using System.Windows.Forms;
using System.Runtime.Remoting;
using Server;

namespace Client
{
    static class Client
    {
        public static RemObj stub;
        public static Login login;
        public static Menu menu;
       
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
            menu = new Menu();

            Application.Run(login);
            
        }
    }
}
