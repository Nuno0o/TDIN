using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.IO;
using TTService;
using System.Reflection;
using System.Messaging;

namespace TTServHost
{
    class Program
    {
        static void Main(string[] args)
        {

            // ServiceHost instance  
            ServiceHost host = new ServiceHost(typeof(TTService.TTServ));
            ServiceHost host2 = new ServiceHost(typeof(TTService.TTCallbacks));

            try
            {
                // start the services.  
                //host.Open();
                //host2.Open();

                TTSvc.TTServClient proxy = new TTSvc.TTServClient();
                proxy.InitDb(true);

                Console.WriteLine("The services are ready.");
                Console.WriteLine("Press <ENTER> to terminate services.");
                Console.ReadLine();

                

                // close the ServiceHostBase to shutdown the service.  
                //host.Close();
                //host2.Close();
            }
            catch (CommunicationException ce)
            {
                Console.WriteLine("An exception occurred: {0}", ce.Message);
                //host.Abort();
            }
        }
    }
}
