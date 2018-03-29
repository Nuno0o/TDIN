using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels;

namespace Server {

    class Server
    {
        static void Main(string[] args)
        {
            Database.Init();

            TcpChannel chan = new TcpChannel(9000);
            ChannelServices.RegisterChannel(chan, false);
            RemotingConfiguration.ApplicationName = "Server";
            RemotingConfiguration.RegisterWellKnownServiceType(
                typeof(RemObj),
                "RemObj",
                WellKnownObjectMode.Singleton
            );            

            Console.WriteLine("Press ENTER to exit ...");
            Console.ReadLine();
           
        }
    }
}
