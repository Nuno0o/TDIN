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
            unassigned_grid.Columns[0].Name = "Name";
            unassigned_grid.Columns[1].Name = "Email";
            unassigned_grid.Columns[2].Name = "Title";

            assigned_grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            assigned_grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            assigned_grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            assigned_grid.AllowUserToAddRows = false;
            assigned_grid.RowHeadersVisible = false;
            assigned_grid.ReadOnly = true;
            assigned_grid.MultiSelect = false;

            assigned_grid.ColumnCount = 4;
            assigned_grid.Columns[0].Name = "Name";
            assigned_grid.Columns[1].Name = "Email";
            assigned_grid.Columns[2].Name = "Title";
            assigned_grid.Columns[3].Name = "Status";

            depart_grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            depart_grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            depart_grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            depart_grid.AllowUserToAddRows = false;
            depart_grid.RowHeadersVisible = false;
            depart_grid.ReadOnly = true;
            depart_grid.MultiSelect = false;

            depart_grid.ColumnCount = 3;
            depart_grid.Columns[0].Name = "Name";
            depart_grid.Columns[1].Name = "Email";
            depart_grid.Columns[2].Name = "Title";

            FormClosing += Main_FormClosing;
            updateInterface();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public void updateInterface()
        {
            comboBox1.Items.Clear();
            foreach(dynamic department in Operations.departments)
            {
                comboBox1.Items.Add(department.name);
            }
            
        }

        public void Main_FormClosing(object sender, EventArgs e)
        {
            Operations.login.Show();
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }
    }
}
