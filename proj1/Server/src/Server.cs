using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Security.Cryptography;

namespace Server {

  
    // TODO Declare static buy/sell order priority queue
    class Server
    {
        static void Main(string[] args)
        {
            Database.Init();

            // TODO Initialize buy/sell queues
            // TODO Fill them with orders from the database

            TcpChannel chan = new TcpChannel(9000);
            ChannelServices.RegisterChannel(chan, false);
            RemotingConfiguration.ApplicationName = "Server";
            RemotingConfiguration.RegisterWellKnownServiceType(
                typeof(Services),
                "Services",
                WellKnownObjectMode.Singleton
            );            

            Console.WriteLine("Press ENTER to exit ...");
            Console.ReadLine();
           
        }       
    }
}
