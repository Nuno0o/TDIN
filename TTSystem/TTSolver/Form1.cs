using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Messaging;
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
            Console.WriteLine("Answer from server: ");
            Console.WriteLine("Press <ENTER> to terminate service.");
            Console.WriteLine();
            //Operations.sendMessageToDepartment("IT2", "ola");

            
            proxy.Close();
        }
    }
}
