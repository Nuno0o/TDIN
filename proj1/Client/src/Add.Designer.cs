namespace Client
{
    partial class Add
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
            this.buy_radio = new System.Windows.Forms.RadioButton();
            this.sell_radio = new System.Windows.Forms.RadioButton();
            this.diginotes_input = new System.Windows.Forms.TextBox();
            this.confirm_button = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buy_radio
            // 
            this.buy_radio.AutoSize = true;
            this.buy_radio.Location = new System.Drawing.Point(12, 32);
            this.buy_radio.Name = "buy_radio";
            this.buy_radio.Size = new System.Drawing.Size(72, 17);
            this.buy_radio.TabIndex = 0;
            this.buy_radio.TabStop = true;
            this.buy_radio.Text = "Buy Order";
            this.buy_radio.UseVisualStyleBackColor = true;
            // 
            // sell_radio
            // 
            this.sell_radio.AutoSize = true;
            this.sell_radio.Location = new System.Drawing.Point(86, 32);
            this.sell_radio.Name = "sell_radio";
            this.sell_radio.Size = new System.Drawing.Size(71, 17);
            this.sell_radio.TabIndex = 1;
            this.sell_radio.TabStop = true;
            this.sell_radio.Text = "Sell Order";
            this.sell_radio.UseVisualStyleBackColor = true;
            // 
            // diginotes_input
            // 
            this.diginotes_input.Location = new System.Drawing.Point(69, 6);
            this.diginotes_input.Name = "diginotes_input";
            this.diginotes_input.Size = new System.Drawing.Size(88, 20);
            this.diginotes_input.TabIndex = 3;
            this.diginotes_input.Text = "0";
            // 
            // confirm_button
            // 
            this.confirm_button.Location = new System.Drawing.Point(12, 55);
            this.confirm_button.Name = "confirm_button";
            this.confirm_button.Size = new System.Drawing.Size(145, 24);
            this.confirm_button.TabIndex = 4;
            this.confirm_button.Text = "Confirm";
            this.confirm_button.UseVisualStyleBackColor = true;
            this.confirm_button.Click += new System.EventHandler(this.confirm_button_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Diginotes:";
            // 
            // Add
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(172, 85);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.confirm_button);
            this.Controls.Add(this.diginotes_input);
            this.Controls.Add(this.sell_radio);
            this.Controls.Add(this.buy_radio);
            this.Name = "Add";
            this.Text = "Add Order";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton buy_radio;
        private System.Windows.Forms.RadioButton sell_radio;
        private System.Windows.Forms.TextBox diginotes_input;
        private System.Windows.Forms.Button confirm_button;
        private System.Windows.Forms.Label label1;
    }
}