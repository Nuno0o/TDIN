using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TTSolver
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            TTCallback cb = new TTCallback();
            InstanceContext icntxt = new InstanceContext(cb);

            TTCallbacks.TTServContractsClient c = new TTCallbacks.TTServContractsClient(icntxt);
            c.Subscribe();

            TTSvc.TTServClient c2 = new TTSvc.TTServClient();

            c2.HelloWorld("me");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
