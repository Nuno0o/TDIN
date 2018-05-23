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
            this.depart_grid = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.unassigned_grid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.assigned_grid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.depart_grid)).BeginInit();
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
            this.unassigned_grid.Size = new System.Drawing.Size(245, 305);
            this.unassigned_grid.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(77, 18);
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
            this.assigned_grid.Location = new System.Drawing.Point(263, 34);
            this.assigned_grid.Name = "assigned_grid";
            this.assigned_grid.ReadOnly = true;
            this.assigned_grid.Size = new System.Drawing.Size(225, 305);
            this.assigned_grid.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(349, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "My Tickets";
            // 
            // depart_grid
            // 
            this.depart_grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.depart_grid.Location = new System.Drawing.Point(495, 34);
            this.depart_grid.Name = "depart_grid";
            this.depart_grid.Size = new System.Drawing.Size(243, 150);
            this.depart_grid.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(551, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(127, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Questions to departments";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(576, 316);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Submit";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(495, 221);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(243, 89);
            this.textBox1.TabIndex = 7;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(617, 194);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(494, 197);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(117, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Question to department";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 352);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.depart_grid);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.assigned_grid);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.unassigned_grid);
            this.Name = "Main";
            this.Text = "Main";
            this.Load += new System.EventHandler(this.Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.unassigned_grid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.assigned_grid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.depart_grid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView unassigned_grid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView assigned_grid;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView depart_grid;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label4;
    }
}