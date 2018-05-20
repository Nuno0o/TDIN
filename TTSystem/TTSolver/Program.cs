using System;
using TTSolver.TTSvc;

namespace TTSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            // create an instance of the WCF proxy
            TTServClient proxy = new TTServClient();

            // call the service operations
            string result = proxy.HelloWorld("John");
            Console.WriteLine("Answer from server: " + result);
            Console.WriteLine("Press <ENTER> to terminate service.");
            Console.WriteLine();
            Console.ReadLine();

            // close the connection and clean up resources  
            proxy.Close();
        }
    }
}
