namespace TraderApp
{
    partial class RegisterForm
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
            label1 = new Label();
            textBox1 = new TextBox();
            button1 = new Button();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            label2 = new Label();
            button2 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 30F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.Control;
            label1.Location = new Point(30, 44);
            label1.Name = "label1";
            label1.Size = new Size(416, 54);
            label1.TabIndex = 0;
            label1.Text = "Register to continue...";
            // 
            // textBox1
            // 
            textBox1.BackColor = SystemColors.ActiveCaption;
            textBox1.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point);
            textBox1.ForeColor = SystemColors.Control;
            textBox1.Location = new Point(45, 143);
            textBox1.Name = "textBox1";
            textBox1.PlaceholderText = "Email";
            textBox1.Size = new Size(142, 23);
            textBox1.TabIndex = 1;
            // 
            // button1
            // 
            button1.BackColor = SystemColors.MenuHighlight;
            button1.FlatAppearance.BorderColor = SystemColors.MenuHighlight;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.ForeColor = SystemColors.Control;
            button1.Location = new Point(45, 273);
            button1.Name = "button1";
            button1.Size = new Size(142, 23);
            button1.TabIndex = 2;
            button1.Text = "Register";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // textBox2
            // 
            textBox2.BackColor = SystemColors.ActiveCaption;
            textBox2.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point);
            textBox2.ForeColor = SystemColors.Control;
            textBox2.Location = new Point(45, 186);
            textBox2.Name = "textBox2";
            textBox2.PasswordChar = '*';
            textBox2.PlaceholderText = "Password";
            textBox2.Size = new Size(142, 23);
            textBox2.TabIndex = 3;
            textBox2.UseSystemPasswordChar = true;
            // 
            // textBox3
            // 
            textBox3.BackColor = SystemColors.ActiveCaption;
            textBox3.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point);
            textBox3.ForeColor = SystemColors.Control;
            textBox3.Location = new Point(45, 230);
            textBox3.Name = "textBox3";
            textBox3.PasswordChar = '*';
            textBox3.PlaceholderText = "Confirm Password";
            textBox3.Size = new Size(142, 23);
            textBox3.TabIndex = 4;
            textBox3.UseSystemPasswordChar = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point);
            label2.Location = new Point(96, 433);
            label2.Name = "label2";
            label2.Size = new Size(692, 15);
            label2.TabIndex = 5;
            label2.Text = "We are constantly developing our app. For now it is still under development and it runs on beta version ! DATA LOSS MAY OCCUR !";
            // 
            // button2
            // 
            button2.BackColor = SystemColors.MenuHighlight;
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatStyle = FlatStyle.Flat;
            button2.ForeColor = SystemColors.Control;
            button2.Location = new Point(64, 346);
            button2.Name = "button2";
            button2.Size = new Size(107, 23);
            button2.TabIndex = 6;
            button2.Text = "Go To First Page";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // RegisterForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(800, 457);
            Controls.Add(button2);
            Controls.Add(label2);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(button1);
            Controls.Add(textBox1);
            Controls.Add(label1);
            Cursor = Cursors.Cross;
            FormBorderStyle = FormBorderStyle.Fixed3D;
            MaximizeBox = false;
            Name = "RegisterForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "TraderApp";
            TransparencyKey = SystemColors.ActiveBorder;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox textBox1;
        private Button button1;
        private TextBox textBox2;
        private TextBox textBox3;
        private Label label2;
        private Button button2;
    }
}