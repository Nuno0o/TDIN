namespace TTSolver
{
    partial class Register
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.name_input = new System.Windows.Forms.TextBox();
            this.email_input = new System.Windows.Forms.TextBox();
            this.password_input = new System.Windows.Forms.TextBox();
            this.repeat_password_input = new System.Windows.Forms.TextBox();
            this.register_button = new System.Windows.Forms.Button();
            this.back_button = new System.Windows.Forms.Button();
            this.status_display = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(68, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(71, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Email:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(50, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Password:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Repeat Password:";
            // 
            // name_input
            // 
            this.name_input.Location = new System.Drawing.Point(112, 6);
            this.name_input.Name = "name_input";
            this.name_input.Size = new System.Drawing.Size(121, 20);
            this.name_input.TabIndex = 4;
            // 
            // email_input
            // 
            this.email_input.Location = new System.Drawing.Point(112, 32);
            this.email_input.Name = "email_input";
            this.email_input.Size = new System.Drawing.Size(121, 20);
            this.email_input.TabIndex = 5;
            // 
            // password_input
            // 
            this.password_input.Location = new System.Drawing.Point(112, 58);
            this.password_input.Name = "password_input";
            this.password_input.PasswordChar = '*';
            this.password_input.Size = new System.Drawing.Size(121, 20);
            this.password_input.TabIndex = 6;
            // 
            // repeat_password_input
            // 
            this.repeat_password_input.Location = new System.Drawing.Point(112, 84);
            this.repeat_password_input.Name = "repeat_password_input";
            this.repeat_password_input.PasswordChar = '*';
            this.repeat_password_input.Size = new System.Drawing.Size(121, 20);
            this.repeat_password_input.TabIndex = 7;
            // 
            // register_button
            // 
            this.register_button.Location = new System.Drawing.Point(128, 110);
            this.register_button.Name = "register_button";
            this.register_button.Size = new System.Drawing.Size(105, 23);
            this.register_button.TabIndex = 8;
            this.register_button.Text = "Register";
            this.register_button.UseVisualStyleBackColor = true;
            this.register_button.Click += new System.EventHandler(this.register_button_Click);
            // 
            // back_button
            // 
            this.back_button.Location = new System.Drawing.Point(15, 110);
            this.back_button.Name = "back_button";
            this.back_button.Size = new System.Drawing.Size(107, 23);
            this.back_button.TabIndex = 11;
            this.back_button.Text = "Back";
            this.back_button.UseVisualStyleBackColor = true;
            this.back_button.Click += new System.EventHandler(this.back_button_Click);
            // 
            // status_display
            // 
            this.status_display.Location = new System.Drawing.Point(15, 139);
            this.status_display.Name = "status_display";
            this.status_display.ReadOnly = true;
            this.status_display.Size = new System.Drawing.Size(218, 20);
            this.status_display.TabIndex = 12;
            // 
            // Register
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(247, 168);
            this.Controls.Add(this.status_display);
            this.Controls.Add(this.back_button);
            this.Controls.Add(this.register_button);
            this.Controls.Add(this.repeat_password_input);
            this.Controls.Add(this.password_input);
            this.Controls.Add(this.email_input);
            this.Controls.Add(this.name_input);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Register";
            this.Text = "Solver Register";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox name_input;
        private System.Windows.Forms.TextBox email_input;
        private System.Windows.Forms.TextBox password_input;
        private System.Windows.Forms.TextBox repeat_password_input;
        private System.Windows.Forms.Button register_button;
        private System.Windows.Forms.Button back_button;
        private System.Windows.Forms.TextBox status_display;
    }
}