namespace Client
{
    partial class Funds
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
            this.deposit_radio = new System.Windows.Forms.RadioButton();
            this.withdraw_radio = new System.Windows.Forms.RadioButton();
            this.amount_input = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.funds_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // deposit_radio
            // 
            this.deposit_radio.AutoSize = true;
            this.deposit_radio.Location = new System.Drawing.Point(12, 32);
            this.deposit_radio.Name = "deposit_radio";
            this.deposit_radio.Size = new System.Drawing.Size(61, 17);
            this.deposit_radio.TabIndex = 0;
            this.deposit_radio.TabStop = true;
            this.deposit_radio.Text = "Deposit";
            this.deposit_radio.UseVisualStyleBackColor = true;
            // 
            // withdraw_radio
            // 
            this.withdraw_radio.AutoSize = true;
            this.withdraw_radio.Location = new System.Drawing.Point(79, 32);
            this.withdraw_radio.Name = "withdraw_radio";
            this.withdraw_radio.Size = new System.Drawing.Size(70, 17);
            this.withdraw_radio.TabIndex = 1;
            this.withdraw_radio.TabStop = true;
            this.withdraw_radio.Text = "Withdraw";
            this.withdraw_radio.UseVisualStyleBackColor = true;
            // 
            // amount_input
            // 
            this.amount_input.Location = new System.Drawing.Point(64, 6);
            this.amount_input.Name = "amount_input";
            this.amount_input.Size = new System.Drawing.Size(85, 20);
            this.amount_input.TabIndex = 2;
            this.amount_input.Text = "0.0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Amount:";
            // 
            // funds_button
            // 
            this.funds_button.Location = new System.Drawing.Point(12, 55);
            this.funds_button.Name = "funds_button";
            this.funds_button.Size = new System.Drawing.Size(137, 23);
            this.funds_button.TabIndex = 4;
            this.funds_button.Text = "Confirm";
            this.funds_button.UseVisualStyleBackColor = true;
            this.funds_button.Click += new System.EventHandler(this.funds_button_Click);
            // 
            // Funds
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(160, 87);
            this.Controls.Add(this.funds_button);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.amount_input);
            this.Controls.Add(this.withdraw_radio);
            this.Controls.Add(this.deposit_radio);
            this.Name = "Funds";
            this.Text = "Funds";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton deposit_radio;
        private System.Windows.Forms.RadioButton withdraw_radio;
        private System.Windows.Forms.TextBox amount_input;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button funds_button;
    }
}