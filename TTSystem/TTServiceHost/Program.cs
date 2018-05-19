using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.IO;
using TTService;
using System.Reflection;

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
                // add a service endpoint.  
                host.AddServiceEndpoint(typeof(ITTServ), new WSHttpBinding(), "TTService");

                // enable metadata exchange.  
                ServiceMetadataBehavior smb = new ServiceMetadataBehavior
                {
                    HttpGetEnabled = true
                };
                host.Description.Behaviors.Add(smb);

                // start the service.  
                host.Open();
                Console.WriteLine("The service is ready.");
                Console.WriteLine("Press <ENTER> to terminate service.");
                Console.WriteLine();
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
