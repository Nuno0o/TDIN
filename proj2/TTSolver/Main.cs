using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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

            unassigned_grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            unassigned_grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            unassigned_grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            unassigned_grid.AllowUserToAddRows = false;
            unassigned_grid.RowHeadersVisible = false;
            unassigned_grid.ReadOnly = true;
            unassigned_grid.MultiSelect = false;

            unassigned_grid.ColumnCount = 3;
            unassigned_grid.Columns[0].Name = "Id";
            unassigned_grid.Columns[1].Name = "Name";
            unassigned_grid.Columns[2].Name = "Title";
            unassigned_grid.Columns["Id"].Visible = false;

            assigned_grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            assigned_grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            assigned_grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            assigned_grid.AllowUserToAddRows = false;
            assigned_grid.RowHeadersVisible = false;
            assigned_grid.ReadOnly = true;
            assigned_grid.MultiSelect = false;

            assigned_grid.ColumnCount = 4;
            assigned_grid.Columns[0].Name = "Id";
            assigned_grid.Columns[1].Name = "Name";
            assigned_grid.Columns[2].Name = "Title";
            assigned_grid.Columns[3].Name = "Status";

            //depart_grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            depart_grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            depart_grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            depart_grid.AllowUserToResizeColumns = true;
            depart_grid.AllowUserToAddRows = false;
            depart_grid.RowHeadersVisible = false;
            depart_grid.ReadOnly = true;
            depart_grid.MultiSelect = false;

            depart_grid.ColumnCount = 3;
            depart_grid.Columns[0].Name = "Department";
            depart_grid.Columns[1].Name = "Description";
            depart_grid.Columns[2].Name = "Answer";

            FormClosing += Main_FormClosing;
            HandleCreated += oncreated;
        }
        public void oncreated(object sender, EventArgs e)
        {
            updateInterface();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public void updateInterface()
        {
            Invoke(new Action(() =>
            {
                try
                {
                    //update departments
                    comboBox1.Items.Clear();
                    foreach (dynamic department in Operations.departments)
                    {
                        comboBox1.Items.Add(department.name);
                    }
                    //update unassigned tickets
                    unassigned_grid.Rows.Clear();
                    foreach (dynamic ticket in Operations.unassigned_tickets)
                    {
                        dynamic user = JsonConvert.DeserializeObject(Operations.serv_proxy.GetUserById((int)ticket.author));
                        unassigned_grid.Rows.Add(new[]
                        {
                        ticket.id,
                        user.name,
                        ticket.title
                    });
                    }
                    //assigned tickets
                    assigned_grid.Rows.Clear();
                    foreach (dynamic ticket in Operations.assigned_tickets)
                    {
                        dynamic user = JsonConvert.DeserializeObject(Operations.serv_proxy.GetUserById((int)ticket.author));
                        assigned_grid.Rows.Add(new[]
                        {
                        ticket.id,
                        user.name,
                        ticket.title,
                        ticket.status
                    });
                    }
                    //department tickets
                    depart_grid.Rows.Clear();
                    foreach (dynamic ticket in Operations.department_tickets)
                    {
                        string depname = "";
                        foreach (dynamic department in Operations.departments)
                        {
                            if ((int)department.id == (int)ticket.department)
                                depname = department.name;
                        }
                        depart_grid.Rows.Add(new[]
                        {
                        depname,
                        ticket.description,
                        ticket.answer
                    }
                        );
                    }

                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }));


        }

        public void Main_FormClosing(object sender, EventArgs e)
        {
            Operations.login.Show();
            Operations.useremail = null;
            Operations.main = null;
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (unassigned_grid.SelectedRows.Count != 1) return;
            var row = unassigned_grid.SelectedRows[0];
            
            foreach(dynamic ticket in Operations.unassigned_tickets)
            {
                if (ticket.id == Convert.ToInt32(row.Cells[0].Value))
                {
                    UnassignedTicket a = new UnassignedTicket((int)ticket.id, (string)ticket.title,(string)ticket.description,row.Cells[1].Value.ToString());
                    a.ShowDialog();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (assigned_grid.SelectedRows.Count != 1) return;
            var row = assigned_grid.SelectedRows[0];
            foreach (dynamic ticket in Operations.assigned_tickets)
            {
                if (ticket.id == Convert.ToInt32(row.Cells[0].Value))
                {
                    AssignedTicket a = new AssignedTicket(ticket.id, ticket.title, ticket.description, row.Cells[1].Value.ToString());
                    a.ShowDialog();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(!(comboBox1.SelectedIndex > -1))
            {
                return;
            }
            string selected = comboBox1.SelectedItem.ToString();
            int selectedid = -1;
            foreach(dynamic department in Operations.departments)
            {
                if (department.name == selected)
                    selectedid = department.id;
            }
            string text = textBox1.Text;
            try{

                Operations.serv_proxy.AddTicketDepartment(text, Operations.userid, selectedid);
                int biggestid = -1;
                List<dynamic> depticks = JsonConvert.DeserializeObject<List<dynamic>>(Operations.serv_proxy.GetAuthorTicketsDepartment(Operations.userid));

                foreach(dynamic ticket in depticks)
                {
                    if((int)ticket.id > biggestid)
                    {
                        biggestid = (int)ticket.id;
                    }
                }
                var message = new
                {
                    id = biggestid,
                    description = text
                };
                Operations.sendMessageToDepartment(selectedid.ToString(), JsonConvert.SerializeObject(message));
                label5.Text = "Success";
            }catch(Exception ex)
            {
                label5.Text = "Error";
            }
        }
    }
}
