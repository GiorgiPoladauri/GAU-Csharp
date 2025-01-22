namespace TraderApp
{
    partial class MainForm
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
            dataGridView1 = new DataGridView();
            label1 = new Label();
            textBox1 = new TextBox();
            button1 = new Button();
            textBox2 = new TextBox();
            numericUpDown1 = new NumericUpDown();
            dataGridView2 = new DataGridView();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            button2 = new Button();
            button3 = new Button();
            comboBox1 = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.BackgroundColor = SystemColors.Control;
            dataGridView1.BorderStyle = BorderStyle.Fixed3D;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 58);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(440, 160);
            dataGridView1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 28F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.Control;
            label1.Location = new Point(471, 32);
            label1.Name = "label1";
            label1.Size = new Size(317, 51);
            label1.TabIndex = 1;
            label1.Text = "Add Items to Sell";
            // 
            // textBox1
            // 
            textBox1.BackColor = SystemColors.ActiveCaption;
            textBox1.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point);
            textBox1.ForeColor = SystemColors.Control;
            textBox1.Location = new Point(559, 121);
            textBox1.Name = "textBox1";
            textBox1.PlaceholderText = "Name";
            textBox1.Size = new Size(142, 23);
            textBox1.TabIndex = 2;
            // 
            // button1
            // 
            button1.BackColor = SystemColors.MenuHighlight;
            button1.FlatAppearance.BorderColor = SystemColors.MenuHighlight;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.ForeColor = SystemColors.Control;
            button1.Location = new Point(626, 261);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 3;
            button1.Text = "Add";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // textBox2
            // 
            textBox2.BackColor = SystemColors.ActiveCaption;
            textBox2.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point);
            textBox2.ForeColor = SystemColors.Control;
            textBox2.Location = new Point(559, 215);
            textBox2.Name = "textBox2";
            textBox2.PlaceholderText = "Price";
            textBox2.Size = new Size(142, 23);
            textBox2.TabIndex = 4;
            // 
            // numericUpDown1
            // 
            numericUpDown1.BackColor = SystemColors.ActiveCaption;
            numericUpDown1.Location = new Point(559, 168);
            numericUpDown1.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(120, 23);
            numericUpDown1.TabIndex = 5;
            numericUpDown1.TextAlign = HorizontalAlignment.Center;
            numericUpDown1.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // dataGridView2
            // 
            dataGridView2.BackgroundColor = SystemColors.Control;
            dataGridView2.BorderStyle = BorderStyle.Fixed3D;
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Location = new Point(12, 285);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.RowTemplate.Height = 25;
            dataGridView2.Size = new Size(440, 160);
            dataGridView2.TabIndex = 6;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = SystemColors.Control;
            label2.Location = new Point(12, 4);
            label2.Name = "label2";
            label2.Size = new Size(218, 37);
            label2.TabIndex = 7;
            label2.Text = "Items on Market";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold, GraphicsUnit.Point);
            label3.ForeColor = SystemColors.Control;
            label3.Location = new Point(12, 231);
            label3.Name = "label3";
            label3.Size = new Size(272, 37);
            label3.TabIndex = 8;
            label3.Text = "Items You are Selling";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(481, 433);
            label4.Name = "label4";
            label4.Size = new Size(307, 15);
            label4.TabIndex = 9;
            label4.Text = "*HINT - Contact Seller via Email to Buy Your Desired Item";
            // 
            // button2
            // 
            button2.BackColor = SystemColors.MenuHighlight;
            button2.FlatAppearance.BorderColor = SystemColors.MenuHighlight;
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatStyle = FlatStyle.Flat;
            button2.ForeColor = SystemColors.Control;
            button2.Location = new Point(481, 360);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 10;
            button2.Text = "Delete";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.BackColor = SystemColors.MenuHighlight;
            button3.FlatAppearance.BorderColor = SystemColors.MenuHighlight;
            button3.FlatAppearance.BorderSize = 0;
            button3.FlatStyle = FlatStyle.Flat;
            button3.ForeColor = SystemColors.Control;
            button3.Location = new Point(626, 360);
            button3.Name = "button3";
            button3.Size = new Size(150, 23);
            button3.TabIndex = 11;
            button3.Text = "Go To First Page";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // comboBox1
            // 
            comboBox1.BackColor = SystemColors.ActiveCaption;
            comboBox1.FlatStyle = FlatStyle.Flat;
            comboBox1.ForeColor = SystemColors.Control;
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Price Asc", "Price Desc" });
            comboBox1.Location = new Point(481, 285);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(75, 23);
            comboBox1.TabIndex = 12;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(800, 457);
            Controls.Add(comboBox1);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(dataGridView2);
            Controls.Add(numericUpDown1);
            Controls.Add(textBox2);
            Controls.Add(button1);
            Controls.Add(textBox1);
            Controls.Add(label1);
            Controls.Add(dataGridView1);
            Cursor = Cursors.Cross;
            FormBorderStyle = FormBorderStyle.Fixed3D;
            MaximizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "TraderApp";
            TransparencyKey = SystemColors.ActiveBorder;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private Label label1;
        private TextBox textBox1;
        private Button button1;
        private TextBox textBox2;
        private NumericUpDown numericUpDown1;
        private DataGridView dataGridView2;
        private Label label2;
        private Label label3;
        private Label label4;
        private Button button2;
        private Button button3;
        private ComboBox comboBox1;
    }
}