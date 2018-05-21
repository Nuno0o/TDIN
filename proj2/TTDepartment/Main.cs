using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TTDepartment.TTSvc;

namespace TTDepartment
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            /* --- TESTING --- */

            Console.WriteLine("Testing started!");

            TTServClient proxy = new TTServClient();

            proxy.AddTicket("titulo", "texto", 1, null);

            Console.WriteLine("Testing finished!");
        }
    }
}
