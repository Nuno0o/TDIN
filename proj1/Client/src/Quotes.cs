using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Quotes : Form
    {
        public Quotes()
        {
            InitializeComponent();

            FillChart();
        }

        private void FillChart()
        {
            for (int i = Client.quotes.Count-1; i >= 0;i--)
            {
                chart1.Series["Quotes"].Points.AddXY(Client.quotes[i].date.ToString(), (double)Client.quotes[i].value);
            }
        }
    }
}
