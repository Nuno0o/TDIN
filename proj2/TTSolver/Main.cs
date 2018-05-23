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

            depart_grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            depart_grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            depart_grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            depart_grid.AllowUserToAddRows = false;
            depart_grid.RowHeadersVisible = false;
            depart_grid.ReadOnly = true;
            depart_grid.MultiSelect = false;

            depart_grid.ColumnCount = 2;
            depart_grid.Columns[0].Name = "Title";
            depart_grid.Columns[1].Name = "Answer";

            FormClosing += Main_FormClosing;
            updateInterface();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public void updateInterface()
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
                    dynamic user = Operations.serv_proxy.GetUserById(ticket.author);
                    unassigned_grid.Rows.Add(new[]
                    {
                        ticket.id,
                        user.id,
                        ticket.title
                    });
                }
                //assigned tickets
                assigned_grid.Rows.Clear();
                foreach (dynamic ticket in Operations.assigned_tickets)
                {
                    dynamic user = Operations.serv_proxy.GetUserById(ticket.author);
                    assigned_grid.Rows.Add(new[]
                    {
                        ticket.id,
                        user.id,
                        ticket.title,
                        ticket.status
                    });
                }

            }
            catch(Exception e)
            {

            }
            
        }

        public void Main_FormClosing(object sender, EventArgs e)
        {
            Operations.login.Show();
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
                    UnassignedTicket a = new UnassignedTicket(ticket.id, ticket.title,ticket.description,row.Cells[1].Value.ToString());
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
    }
}
