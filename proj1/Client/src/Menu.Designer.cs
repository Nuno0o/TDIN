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
            this.digicoins_label = new System.Windows.Forms.Label();
            this.balance_label = new System.Windows.Forms.Label();
            this.sellorders_grid = new System.Windows.Forms.DataGridView();
            this.buyorders_grid = new System.Windows.Forms.DataGridView();
            this.add_button = new System.Windows.Forms.Button();
            this.remove_button = new System.Windows.Forms.Button();
            this.edit_button = new System.Windows.Forms.Button();
            this.digicoins_display = new System.Windows.Forms.TextBox();
            this.balance_display = new System.Windows.Forms.TextBox();
            this.logout_button = new System.Windows.Forms.Button();
            this.funds_button = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.sellorders_grid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buyorders_grid)).BeginInit();
            this.SuspendLayout();
            // 
            // digicoins_label
            // 
            this.digicoins_label.AutoSize = true;
            this.digicoins_label.Location = new System.Drawing.Point(12, 9);
            this.digicoins_label.Name = "digicoins_label";
            this.digicoins_label.Size = new System.Drawing.Size(50, 13);
            this.digicoins_label.TabIndex = 0;
            this.digicoins_label.Text = "Digicoins";
            this.digicoins_label.Click += new System.EventHandler(this.label1_Click);
            // 
            // balance_label
            // 
            this.balance_label.AutoSize = true;
            this.balance_label.Location = new System.Drawing.Point(12, 38);
            this.balance_label.Name = "balance_label";
            this.balance_label.Size = new System.Drawing.Size(46, 13);
            this.balance_label.TabIndex = 1;
            this.balance_label.Text = "Balance";
            this.balance_label.Click += new System.EventHandler(this.label2_Click);
            // 
            // sellorders_grid
            // 
            this.sellorders_grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.sellorders_grid.Location = new System.Drawing.Point(12, 218);
            this.sellorders_grid.Name = "sellorders_grid";
            this.sellorders_grid.Size = new System.Drawing.Size(240, 150);
            this.sellorders_grid.TabIndex = 2;
            // 
            // buyorders_grid
            // 
            this.buyorders_grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.buyorders_grid.Location = new System.Drawing.Point(12, 62);
            this.buyorders_grid.Name = "buyorders_grid";
            this.buyorders_grid.Size = new System.Drawing.Size(240, 150);
            this.buyorders_grid.TabIndex = 3;
            // 
            // add_button
            // 
            this.add_button.Location = new System.Drawing.Point(12, 374);
            this.add_button.Name = "add_button";
            this.add_button.Size = new System.Drawing.Size(75, 23);
            this.add_button.TabIndex = 4;
            this.add_button.Text = "Add";
            this.add_button.UseVisualStyleBackColor = true;
            // 
            // remove_button
            // 
            this.remove_button.Location = new System.Drawing.Point(96, 374);
            this.remove_button.Name = "remove_button";
            this.remove_button.Size = new System.Drawing.Size(75, 23);
            this.remove_button.TabIndex = 5;
            this.remove_button.Text = "Remove";
            this.remove_button.UseVisualStyleBackColor = true;
            // 
            // edit_button
            // 
            this.edit_button.Location = new System.Drawing.Point(177, 374);
            this.edit_button.Name = "edit_button";
            this.edit_button.Size = new System.Drawing.Size(75, 23);
            this.edit_button.TabIndex = 6;
            this.edit_button.Text = "Edit";
            this.edit_button.UseVisualStyleBackColor = true;
            // 
            // digicoins_display
            // 
            this.digicoins_display.Location = new System.Drawing.Point(64, 6);
            this.digicoins_display.Name = "digicoins_display";
            this.digicoins_display.ReadOnly = true;
            this.digicoins_display.Size = new System.Drawing.Size(100, 20);
            this.digicoins_display.TabIndex = 7;
            // 
            // balance_display
            // 
            this.balance_display.Location = new System.Drawing.Point(64, 35);
            this.balance_display.Name = "balance_display";
            this.balance_display.ReadOnly = true;
            this.balance_display.Size = new System.Drawing.Size(100, 20);
            this.balance_display.TabIndex = 8;
            // 
            // logout_button
            // 
            this.logout_button.Location = new System.Drawing.Point(177, 4);
            this.logout_button.Name = "logout_button";
            this.logout_button.Size = new System.Drawing.Size(75, 23);
            this.logout_button.TabIndex = 9;
            this.logout_button.Text = "Logout";
            this.logout_button.UseVisualStyleBackColor = true;
            // 
            // funds_button
            // 
            this.funds_button.Location = new System.Drawing.Point(177, 33);
            this.funds_button.Name = "funds_button";
            this.funds_button.Size = new System.Drawing.Size(75, 23);
            this.funds_button.TabIndex = 10;
            this.funds_button.Text = "Funds";
            this.funds_button.UseVisualStyleBackColor = true;
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(264, 400);
            this.Controls.Add(this.funds_button);
            this.Controls.Add(this.logout_button);
            this.Controls.Add(this.balance_display);
            this.Controls.Add(this.digicoins_display);
            this.Controls.Add(this.edit_button);
            this.Controls.Add(this.remove_button);
            this.Controls.Add(this.add_button);
            this.Controls.Add(this.buyorders_grid);
            this.Controls.Add(this.sellorders_grid);
            this.Controls.Add(this.balance_label);
            this.Controls.Add(this.digicoins_label);
            this.Name = "Menu";
            this.Text = "Menu";
            ((System.ComponentModel.ISupportInitialize)(this.sellorders_grid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buyorders_grid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label digicoins_label;
        private System.Windows.Forms.Label balance_label;
        private System.Windows.Forms.DataGridView sellorders_grid;
        private System.Windows.Forms.DataGridView buyorders_grid;
        private System.Windows.Forms.Button add_button;
        private System.Windows.Forms.Button remove_button;
        private System.Windows.Forms.Button edit_button;
        private System.Windows.Forms.TextBox digicoins_display;
        private System.Windows.Forms.TextBox balance_display;
        private System.Windows.Forms.Button logout_button;
        private System.Windows.Forms.Button funds_button;
    }
}