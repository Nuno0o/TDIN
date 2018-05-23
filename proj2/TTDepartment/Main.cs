using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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

            //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.MultiSelect = false;

            dataGridView1.ColumnCount = 2;
            dataGridView1.Columns[0].Name = "Id";
            dataGridView1.Columns[1].Name = "Description";

            updateInterface();
        }

        public void updateInterface()
        {
            try
            {
                dataGridView1.Rows.Clear();
                comboBox1.Items.Clear();
                foreach(System.Messaging.Message m in Operations.messages)
                {
                    dynamic info = JsonConvert.DeserializeObject(m.Body.ToString());
                    dataGridView1.Rows.Add(new[]
                    {
                        info.id,
                        info.description
                    });
                    comboBox1.Items.Add(info.id);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(!(comboBox1.SelectedIndex > -1))
            {
                return;
            }
            string answer = textBox1.Text;
            int id = Convert.ToInt32(comboBox1.SelectedItem.ToString());
            Debug.WriteLine(id + answer);
            try
            {
                Operations.serv_proxy.AnswerTicketDepartment(id, answer);
                Operations.removeMessageFromQueue(id);
            }catch(Exception ex)
            {

            }
        }
    }
}
