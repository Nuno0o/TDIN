using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TTSolver.TTSvc;

namespace TTSolver
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // create an instance of the WCF proxy
            TTServClient proxy = new TTServClient();

            // call the service operations
            string result = proxy.HelloWorld("John");
            Console.WriteLine("Answer from server: " + result);
            Console.WriteLine("Press <ENTER> to terminate service.");
            Console.WriteLine();
            //Console.ReadLine();

            // close the connection and clean up resources  
            proxy.Close();
        }
    }
}
