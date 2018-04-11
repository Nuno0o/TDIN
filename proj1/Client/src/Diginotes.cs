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
    public partial class Diginotes : Form
    {
        /*
         * Show list of diginotes in datagridview
         */
        public Diginotes()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            dataGridView1.ColumnCount = 1;
            dataGridView1.Columns[0].Name = "ID";

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.MultiSelect = false;

            try
            {
                string json = Client.stubs.GetDiginotesList(Client.username);
                List<dynamic> diginotes = JsonConvert.DeserializeObject<List<dynamic>>(json);
                foreach(dynamic diginote in diginotes)
                {
                    dataGridView1.Rows.Add(new[] {
                        diginote.id.ToString()
                    });
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                //this.Dispose();
            }
            
        }
    }
}
