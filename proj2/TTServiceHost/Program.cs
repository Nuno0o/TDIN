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
            // uri to serve as the base address.  
            Uri baseAddress = new Uri("http://localhost:8080/TTSystem/");

            // ServiceHost instance  
            ServiceHost host = new ServiceHost(typeof(TTService.TTServ), baseAddress);

            // Set folder containing database ( full path in app.config )
            AppDomain.CurrentDomain.SetData("DataDirectory", Directory.GetParent(Directory.GetParent(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)).ToString()).ToString());

            try
            {
                // start the service.  
                host.Open();
                Console.WriteLine("The service is ready.");
                Console.WriteLine("Press <ENTER> to terminate service.");
                Console.WriteLine();

                TTSvc.TTServClient proxy = new TTSvc.TTServClient();
                proxy.InitDb(true);
                //test scripts here
                proxy.AddTicket("a", "b", 1, null);
                proxy.Close();


                Console.ReadLine();

                // close the ServiceHostBase to shutdown the service.  
                host.Close();
            }
            catch (CommunicationException ce)
            {
                Console.WriteLine("An exception occurred: {0}", ce.Message);
                host.Abort();
            }
        }
    }
}
