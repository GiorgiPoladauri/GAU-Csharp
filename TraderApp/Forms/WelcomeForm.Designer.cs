namespace TraderApp
{
    partial class WelcomeForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button1 = new Button();
            label1 = new Label();
            button2 = new Button();
            label2 = new Label();
            SuspendLayout();
            // 
            // button1
            // 
            button1.BackColor = SystemColors.MenuHighlight;
            button1.FlatAppearance.BorderColor = SystemColors.MenuHighlight;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.ForeColor = SystemColors.Control;
            button1.Location = new Point(264, 217);
            button1.Name = "button1";
            button1.Size = new Size(254, 57);
            button1.TabIndex = 0;
            button1.Text = "Login";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 48F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.Control;
            label1.Location = new Point(30, 30);
            label1.Name = "label1";
            label1.Size = new Size(746, 86);
            label1.TabIndex = 1;
            label1.Text = "Welcome to TraderApp !";
            // 
            // button2
            // 
            button2.BackColor = SystemColors.MenuHighlight;
            button2.FlatAppearance.BorderColor = SystemColors.MenuHighlight;
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatStyle = FlatStyle.Flat;
            button2.ForeColor = SystemColors.Control;
            button2.Location = new Point(264, 300);
            button2.Name = "button2";
            button2.Size = new Size(254, 57);
            button2.TabIndex = 2;
            button2.Text = "Register";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            label2.ForeColor = SystemColors.ActiveBorder;
            label2.Location = new Point(264, 149);
            label2.Name = "label2";
            label2.Size = new Size(254, 30);
            label2.TabIndex = 3;
            label2.Text = "Please, Login or Register";
            // 
            // WelcomeForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(800, 457);
            Controls.Add(label2);
            Controls.Add(button2);
            Controls.Add(label1);
            Controls.Add(button1);
            Cursor = Cursors.Cross;
            ForeColor = SystemColors.ControlText;
            FormBorderStyle = FormBorderStyle.Fixed3D;
            HelpButton = true;
            MaximizeBox = false;
            Name = "WelcomeForm";
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "TraderApp";
            TransparencyKey = SystemColors.ActiveBorder;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Label label1;
        private Button button2;
        private Label label2;
    }
}
