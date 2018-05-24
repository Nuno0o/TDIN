using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TTSolver
{
    public partial class AssignedTicket : Form
    {
        int id;
        List<dynamic> secondary_questions = new List<dynamic>();
        public AssignedTicket(int id, string title, string description, string sender)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.MultiSelect = false;

            dataGridView1.ColumnCount = 4;
            dataGridView1.Columns[0].Name = "Id";
            dataGridView1.Columns[1].Name = "Name";
            dataGridView1.Columns[2].Name = "Description";
            dataGridView1.Columns[3].Name = "Answer";
            dataGridView1.Columns["Id"].Visible = false;

            this.textBox4.Text = title;
            this.id = id;
            this.textBox5.Text = description;
            this.label8.Text = sender;

            secondary_questions = new List<dynamic>();
            List<dynamic> secondary_tickets_ids = JsonConvert.DeserializeObject<List<dynamic>>(Operations.serv_proxy.GetTicketChildren(this.id));
            foreach (dynamic ticket in secondary_tickets_ids)
            {
                secondary_questions.Add(JsonConvert.DeserializeObject(Operations.serv_proxy.GetTicket((int)ticket.id)));
            }
            foreach(dynamic question in secondary_questions)
            {
                {
                    dataGridView1.Rows.Add(new[]
                    {
                        question.id,
                        sender,
                        question.title,
                        question.description
                    });
                    comboBox1.Items.Add(question.id);
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string message = textBox3.Text;
            int id = Convert.ToInt32(comboBox1.SelectedItem.ToString());
            try
            {
                Operations.serv_proxy.AnswerTicket(id, message);
                this.Dispose();
            }
            catch(Exception ex)
            {

            }  
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string message = textBox2.Text;
            int id = this.id;
            var cansend = true;
            foreach(DataGridViewRow row in dataGridView1.Rows)
            {
                if(row.Cells[2].Value.ToString() == "")
                {
                    cansend = false;
                }
            }
            if(cansend == false)
            {
                label9.Text = "Answer all questions first";
                return;
            }
            try
            {
                Operations.serv_proxy.AnswerTicket(id, message);
                Operations.serv_proxy.SendEmail(id);
                this.Dispose();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
