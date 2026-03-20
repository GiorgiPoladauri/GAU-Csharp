namespace StudentCRUD.Client
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            // ==================== CONTROLS ====================
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.lblFormTitle  = new System.Windows.Forms.Label();
            this.lblFirstName  = new System.Windows.Forms.Label();
            this.lblLastName   = new System.Windows.Forms.Label();
            this.lblEmail      = new System.Windows.Forms.Label();
            this.lblAge        = new System.Windows.Forms.Label();
            this.lblMajor      = new System.Windows.Forms.Label();
            this.txtFirstName  = new System.Windows.Forms.TextBox();
            this.txtLastName   = new System.Windows.Forms.TextBox();
            this.txtEmail      = new System.Windows.Forms.TextBox();
            this.txtAge        = new System.Windows.Forms.TextBox();
            this.txtMajor      = new System.Windows.Forms.TextBox();
            this.btnAdd        = new System.Windows.Forms.Button();
            this.btnUpdate     = new System.Windows.Forms.Button();
            this.btnDelete     = new System.Windows.Forms.Button();
            this.btnClear      = new System.Windows.Forms.Button();
            this.lblStatus     = new System.Windows.Forms.Label();
            this.panelForm     = new System.Windows.Forms.Panel();
            this.lblTitle      = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panelForm.SuspendLayout();
            this.SuspendLayout();

            // ==================== FORM ====================
            this.Text          = "Student CRUD - WCF + Entity Framework";
            this.Size          = new System.Drawing.Size(900, 620);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Font          = new System.Drawing.Font("Segoe UI", 9.5f);
            this.BackColor     = System.Drawing.Color.FromArgb(240, 242, 245);
            this.Load          += new System.EventHandler(this.Form1_Load);
            this.FormClosing   += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);

            // ==================== TITLE LABEL ====================
            this.lblTitle.Text      = "სტუდენტების მართვის სისტემა";
            this.lblTitle.Font      = new System.Drawing.Font("Segoe UI", 14f, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(30, 60, 120);
            this.lblTitle.Location  = new System.Drawing.Point(12, 10);
            this.lblTitle.Size      = new System.Drawing.Size(500, 30);

            // ==================== DATAGRIDVIEW ====================
            this.dataGridView1.Location         = new System.Drawing.Point(12, 50);
            this.dataGridView1.Size             = new System.Drawing.Size(560, 510);
            this.dataGridView1.BackgroundColor  = System.Drawing.Color.White;
            this.dataGridView1.BorderStyle      = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(30, 60, 120);
            this.dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersDefaultCellStyle.Font      = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold);
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.GridColor         = System.Drawing.Color.FromArgb(220, 220, 220);
            this.dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(245, 248, 255);
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);

            // ==================== RIGHT PANEL ====================
            this.panelForm.Location  = new System.Drawing.Point(585, 50);
            this.panelForm.Size      = new System.Drawing.Size(295, 510);
            this.panelForm.BackColor = System.Drawing.Color.White;
            this.panelForm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // ==================== FORM TITLE ====================
            this.lblFormTitle.Text      = "➕ ახალი სტუდენტი";
            this.lblFormTitle.Font      = new System.Drawing.Font("Segoe UI", 10f, System.Drawing.FontStyle.Bold);
            this.lblFormTitle.ForeColor = System.Drawing.Color.FromArgb(30, 60, 120);
            this.lblFormTitle.Location  = new System.Drawing.Point(10, 10);
            this.lblFormTitle.Size      = new System.Drawing.Size(275, 25);

            // ==================== LABELS & TEXTBOXES ====================
            int labelX = 10, inputX = 10, startY = 50, gap = 65;

            // სახელი
            this.lblFirstName.Text     = "სახელი:";
            this.lblFirstName.Location = new System.Drawing.Point(labelX, startY);
            this.lblFirstName.Size     = new System.Drawing.Size(275, 18);
            this.txtFirstName.Location = new System.Drawing.Point(inputX, startY + 20);
            this.txtFirstName.Size     = new System.Drawing.Size(275, 28);
            this.txtFirstName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // გვარი
            this.lblLastName.Text     = "გვარი:";
            this.lblLastName.Location = new System.Drawing.Point(labelX, startY + gap);
            this.lblLastName.Size     = new System.Drawing.Size(275, 18);
            this.txtLastName.Location = new System.Drawing.Point(inputX, startY + gap + 20);
            this.txtLastName.Size     = new System.Drawing.Size(275, 28);
            this.txtLastName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // ელ-ფოსტა
            this.lblEmail.Text     = "ელ-ფოსტა:";
            this.lblEmail.Location = new System.Drawing.Point(labelX, startY + gap * 2);
            this.lblEmail.Size     = new System.Drawing.Size(275, 18);
            this.txtEmail.Location = new System.Drawing.Point(inputX, startY + gap * 2 + 20);
            this.txtEmail.Size     = new System.Drawing.Size(275, 28);
            this.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // ასაკი
            this.lblAge.Text     = "ასაკი:";
            this.lblAge.Location = new System.Drawing.Point(labelX, startY + gap * 3);
            this.lblAge.Size     = new System.Drawing.Size(275, 18);
            this.txtAge.Location = new System.Drawing.Point(inputX, startY + gap * 3 + 20);
            this.txtAge.Size     = new System.Drawing.Size(275, 28);
            this.txtAge.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // სპეციალობა
            this.lblMajor.Text     = "სპეციალობა:";
            this.lblMajor.Location = new System.Drawing.Point(labelX, startY + gap * 4);
            this.lblMajor.Size     = new System.Drawing.Size(275, 18);
            this.txtMajor.Location = new System.Drawing.Point(inputX, startY + gap * 4 + 20);
            this.txtMajor.Size     = new System.Drawing.Size(275, 28);
            this.txtMajor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // ==================== BUTTONS ====================
            int btnY = startY + gap * 5 + 10, btnW = 130, btnH = 36;

            // დამატება (მწვანე)
            this.btnAdd.Text      = "✚  დამატება";
            this.btnAdd.Location  = new System.Drawing.Point(10, btnY);
            this.btnAdd.Size      = new System.Drawing.Size(btnW, btnH);
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(40, 167, 69);
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.Font      = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold);
            this.btnAdd.Click    += new System.EventHandler(this.btnAdd_Click);

            // განახლება (ლურჯი)
            this.btnUpdate.Text      = "✎  განახლება";
            this.btnUpdate.Location  = new System.Drawing.Point(155, btnY);
            this.btnUpdate.Size      = new System.Drawing.Size(btnW, btnH);
            this.btnUpdate.BackColor = System.Drawing.Color.FromArgb(0, 123, 255);
            this.btnUpdate.ForeColor = System.Drawing.Color.White;
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.FlatAppearance.BorderSize = 0;
            this.btnUpdate.Font      = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold);
            this.btnUpdate.Click    += new System.EventHandler(this.btnUpdate_Click);

            // წაშლა (წითელი)
            this.btnDelete.Text      = "✖  წაშლა";
            this.btnDelete.Location  = new System.Drawing.Point(10, btnY + 44);
            this.btnDelete.Size      = new System.Drawing.Size(btnW, btnH);
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(220, 53, 69);
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.Font      = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold);
            this.btnDelete.Click    += new System.EventHandler(this.btnDelete_Click);

            // გასუფთავება (ნაცრისფერი)
            this.btnClear.Text      = "↺  გასუფთავება";
            this.btnClear.Location  = new System.Drawing.Point(155, btnY + 44);
            this.btnClear.Size      = new System.Drawing.Size(btnW, btnH);
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(108, 117, 125);
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.FlatAppearance.BorderSize = 0;
            this.btnClear.Font      = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold);
            this.btnClear.Click    += new System.EventHandler(this.btnClear_Click);

            // ==================== STATUS LABEL ====================
            this.lblStatus.Text      = "";
            this.lblStatus.Location  = new System.Drawing.Point(12, 568);
            this.lblStatus.Size      = new System.Drawing.Size(300, 20);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(100, 100, 100);

            // ==================== ADD CONTROLS TO PANEL ====================
            this.panelForm.Controls.Add(this.lblFormTitle);
            this.panelForm.Controls.Add(this.lblFirstName);
            this.panelForm.Controls.Add(this.txtFirstName);
            this.panelForm.Controls.Add(this.lblLastName);
            this.panelForm.Controls.Add(this.txtLastName);
            this.panelForm.Controls.Add(this.lblEmail);
            this.panelForm.Controls.Add(this.txtEmail);
            this.panelForm.Controls.Add(this.lblAge);
            this.panelForm.Controls.Add(this.txtAge);
            this.panelForm.Controls.Add(this.lblMajor);
            this.panelForm.Controls.Add(this.txtMajor);
            this.panelForm.Controls.Add(this.btnAdd);
            this.panelForm.Controls.Add(this.btnUpdate);
            this.panelForm.Controls.Add(this.btnDelete);
            this.panelForm.Controls.Add(this.btnClear);

            // ==================== ADD CONTROLS TO FORM ====================
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panelForm);
            this.Controls.Add(this.lblStatus);

            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panelForm.ResumeLayout(false);
            this.panelForm.PerformLayout();
            this.ResumeLayout(false);
        }

        // ==================== CONTROL DECLARATIONS ====================
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblFormTitle;
        private System.Windows.Forms.Label lblFirstName;
        private System.Windows.Forms.Label lblLastName;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblAge;
        private System.Windows.Forms.Label lblMajor;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtAge;
        private System.Windows.Forms.TextBox txtMajor;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Panel panelForm;
    }
}
