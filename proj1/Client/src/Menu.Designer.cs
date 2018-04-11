namespace Client
{
    partial class Menu
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
            this.diginotes_label = new System.Windows.Forms.Label();
            this.balance_label = new System.Windows.Forms.Label();
            this.orders_grid = new System.Windows.Forms.DataGridView();
            this.add_button = new System.Windows.Forms.Button();
            this.remove_button = new System.Windows.Forms.Button();
            this.diginotes_display = new System.Windows.Forms.TextBox();
            this.balance_display = new System.Windows.Forms.TextBox();
            this.logout_button = new System.Windows.Forms.Button();
            this.funds_button = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.orders_grid)).BeginInit();
            this.SuspendLayout();
            // 
            // diginotes_label
            // 
            this.diginotes_label.AutoSize = true;
            this.diginotes_label.Location = new System.Drawing.Point(12, 9);
            this.diginotes_label.Name = "diginotes_label";
            this.diginotes_label.Size = new System.Drawing.Size(51, 13);
            this.diginotes_label.TabIndex = 0;
            this.diginotes_label.Text = "Diginotes";
            // 
            // balance_label
            // 
            this.balance_label.AutoSize = true;
            this.balance_label.Location = new System.Drawing.Point(12, 38);
            this.balance_label.Name = "balance_label";
            this.balance_label.Size = new System.Drawing.Size(46, 13);
            this.balance_label.TabIndex = 1;
            this.balance_label.Text = "Balance";
            // 
            // orders_grid
            // 
            this.orders_grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.orders_grid.Location = new System.Drawing.Point(12, 62);
            this.orders_grid.Name = "orders_grid";
            this.orders_grid.Size = new System.Drawing.Size(305, 306);
            this.orders_grid.TabIndex = 3;
            // 
            // add_button
            // 
            this.add_button.Location = new System.Drawing.Point(12, 374);
            this.add_button.Name = "add_button";
            this.add_button.Size = new System.Drawing.Size(154, 23);
            this.add_button.TabIndex = 4;
            this.add_button.Text = "Add";
            this.add_button.UseVisualStyleBackColor = true;
            this.add_button.Click += new System.EventHandler(this.add_button_Click);
            // 
            // remove_button
            // 
            this.remove_button.Location = new System.Drawing.Point(172, 374);
            this.remove_button.Name = "remove_button";
            this.remove_button.Size = new System.Drawing.Size(146, 23);
            this.remove_button.TabIndex = 5;
            this.remove_button.Text = "Remove";
            this.remove_button.UseVisualStyleBackColor = true;
            this.remove_button.Click += new System.EventHandler(this.remove_button_Click);
            // 
            // diginotes_display
            // 
            this.diginotes_display.Location = new System.Drawing.Point(64, 6);
            this.diginotes_display.Name = "diginotes_display";
            this.diginotes_display.ReadOnly = true;
            this.diginotes_display.Size = new System.Drawing.Size(82, 20);
            this.diginotes_display.TabIndex = 7;
            // 
            // balance_display
            // 
            this.balance_display.Location = new System.Drawing.Point(64, 35);
            this.balance_display.Name = "balance_display";
            this.balance_display.ReadOnly = true;
            this.balance_display.Size = new System.Drawing.Size(82, 20);
            this.balance_display.TabIndex = 8;
            // 
            // logout_button
            // 
            this.logout_button.Location = new System.Drawing.Point(239, 4);
            this.logout_button.Name = "logout_button";
            this.logout_button.Size = new System.Drawing.Size(79, 23);
            this.logout_button.TabIndex = 9;
            this.logout_button.Text = "Logout";
            this.logout_button.UseVisualStyleBackColor = true;
            this.logout_button.Click += new System.EventHandler(this.logout_button_Click);
            // 
            // funds_button
            // 
            this.funds_button.Location = new System.Drawing.Point(239, 33);
            this.funds_button.Name = "funds_button";
            this.funds_button.Size = new System.Drawing.Size(79, 23);
            this.funds_button.TabIndex = 10;
            this.funds_button.Text = "Funds";
            this.funds_button.UseVisualStyleBackColor = true;
            this.funds_button.Click += new System.EventHandler(this.funds_button_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(172, 402);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(145, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "Change Quote";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 402);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(154, 23);
            this.button2.TabIndex = 12;
            this.button2.Text = "Activate";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(152, 4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(81, 23);
            this.button3.TabIndex = 13;
            this.button3.Text = "Serials";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(153, 35);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(80, 21);
            this.button4.TabIndex = 14;
            this.button4.Text = "Transactions";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(329, 437);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.funds_button);
            this.Controls.Add(this.logout_button);
            this.Controls.Add(this.balance_display);
            this.Controls.Add(this.diginotes_display);
            this.Controls.Add(this.remove_button);
            this.Controls.Add(this.add_button);
            this.Controls.Add(this.orders_grid);
            this.Controls.Add(this.balance_label);
            this.Controls.Add(this.diginotes_label);
            this.Name = "Menu";
            this.Text = "Menu";
            ((System.ComponentModel.ISupportInitialize)(this.orders_grid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label diginotes_label;
        private System.Windows.Forms.Label balance_label;
        private System.Windows.Forms.DataGridView orders_grid;
        private System.Windows.Forms.Button add_button;
        private System.Windows.Forms.Button remove_button;
        private System.Windows.Forms.TextBox diginotes_display;
        private System.Windows.Forms.TextBox balance_display;
        private System.Windows.Forms.Button logout_button;
        private System.Windows.Forms.Button funds_button;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
    }
}