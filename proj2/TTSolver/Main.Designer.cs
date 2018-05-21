namespace TTSolver
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.unassigned_grid = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.assigned_grid = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.unassigned_grid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.assigned_grid)).BeginInit();
            this.SuspendLayout();
            // 
            // unassigned_grid
            // 
            this.unassigned_grid.AllowUserToAddRows = false;
            this.unassigned_grid.AllowUserToDeleteRows = false;
            this.unassigned_grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.unassigned_grid.Location = new System.Drawing.Point(12, 34);
            this.unassigned_grid.Name = "unassigned_grid";
            this.unassigned_grid.ReadOnly = true;
            this.unassigned_grid.Size = new System.Drawing.Size(354, 305);
            this.unassigned_grid.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(136, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Unassigned tickets";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // assigned_grid
            // 
            this.assigned_grid.AllowUserToAddRows = false;
            this.assigned_grid.AllowUserToDeleteRows = false;
            this.assigned_grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.assigned_grid.Location = new System.Drawing.Point(382, 34);
            this.assigned_grid.Name = "assigned_grid";
            this.assigned_grid.ReadOnly = true;
            this.assigned_grid.Size = new System.Drawing.Size(354, 305);
            this.assigned_grid.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(528, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Assigned Tickets";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 355);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.assigned_grid);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.unassigned_grid);
            this.Name = "Main";
            this.Text = "Main";
            ((System.ComponentModel.ISupportInitialize)(this.unassigned_grid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.assigned_grid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView unassigned_grid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView assigned_grid;
        private System.Windows.Forms.Label label2;
    }
}