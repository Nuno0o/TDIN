using System;
using System.Windows.Forms;
using System.Runtime.Remoting;

namespace Client
{
    static class Client
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            RemotingConfiguration.Configure("Client.exe.config", false);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Login());
        }
    }
}
