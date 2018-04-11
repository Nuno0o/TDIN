using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Client
{
    public partial class Transactions : Form
    {
        public Transactions()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            dataGridView1.ColumnCount = 6;
            dataGridView1.Columns[0].Name = "ID";
            dataGridView1.Columns[1].Name = "Buyer";
            dataGridView1.Columns[2].Name = "Seller";
            dataGridView1.Columns[3].Name = "Quote";
            dataGridView1.Columns[4].Name = "Amount";
            dataGridView1.Columns[5].Name = "Date";

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.MultiSelect = false;

            try
            {
                string json = Client.stubs.GetTransactions(Client.username);
                Console.WriteLine(json + "xdd");
                List<dynamic> transactions = JsonConvert.DeserializeObject<List<dynamic>>(json);
                foreach (dynamic trans in transactions)
                {
                    dataGridView1.Rows.Add(new[] {
                        trans.id.ToString(),
                        trans.buyer.ToString(),
                        trans.seller.ToString(),
                        trans.quote.ToString(),
                        trans.amount.ToString(),
                        trans.date.ToString(),
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                //this.Dispose();
            }
        }
    }
}
