namespace GradeManagerProj
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
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabStudents = new System.Windows.Forms.TabPage();
            this.grpStudentList = new System.Windows.Forms.GroupBox();
            this.lblStudentPageInfo = new System.Windows.Forms.Label();
            this.btnNextPage = new System.Windows.Forms.Button();
            this.btnPrevPage = new System.Windows.Forms.Button();
            this.dgvStudents = new System.Windows.Forms.DataGridView();
            this.grpStudentForm = new System.Windows.Forms.GroupBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.btnDeleteStudent = new System.Windows.Forms.Button();
            this.btnEditStudent = new System.Windows.Forms.Button();
            this.btnAddStudent = new System.Windows.Forms.Button();
            this.dtpStudentBirth = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.txtStudentName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabTeachers = new System.Windows.Forms.TabPage();
            this.grpTeacherList = new System.Windows.Forms.GroupBox();
            this.dgvTeachers = new System.Windows.Forms.DataGridView();
            this.grpTeacherForm = new System.Windows.Forms.GroupBox();
            this.btnAddTeacher = new System.Windows.Forms.Button();
            this.txtTeacherName = new System.Windows.Forms.TextBox();
            this.lblTeacherName = new System.Windows.Forms.Label();
            this.tabSubjects = new System.Windows.Forms.TabPage();
            this.grpSubjectList = new System.Windows.Forms.GroupBox();
            this.dgvSubjects = new System.Windows.Forms.DataGridView();
            this.grpSubjectForm = new System.Windows.Forms.GroupBox();
            this.btnDeleteSubject = new System.Windows.Forms.Button();
            this.btnEditSubject = new System.Windows.Forms.Button();
            this.lblSubjectTeacher = new System.Windows.Forms.Label();
            this.btnAddSubject = new System.Windows.Forms.Button();
            this.cmbSubjectTeacher = new System.Windows.Forms.ComboBox();
            this.txtSubjectName = new System.Windows.Forms.TextBox();
            this.lblSubjectName = new System.Windows.Forms.Label();
            this.tabGrades = new System.Windows.Forms.TabPage();
            this.grpGradeFilter = new System.Windows.Forms.GroupBox();
            this.lblFilterSubject = new System.Windows.Forms.Label();
            this.cmbFilterSubject = new System.Windows.Forms.ComboBox();
            this.groGradeList = new System.Windows.Forms.GroupBox();
            this.dgvGrades = new System.Windows.Forms.DataGridView();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnCalculateAverage = new System.Windows.Forms.Button();
            this.cmbAverageStudent = new System.Windows.Forms.ComboBox();
            this.lblAverageStudent = new System.Windows.Forms.Label();
            this.grpGradeForm = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbGradeStudent = new System.Windows.Forms.ComboBox();
            this.lblGradeSubject = new System.Windows.Forms.Label();
            this.btnDeleteGrade = new System.Windows.Forms.Button();
            this.btnEditGrade = new System.Windows.Forms.Button();
            this.btnAddGrade = new System.Windows.Forms.Button();
            this.txtGradeValue = new System.Windows.Forms.TextBox();
            this.cmbGradeSubject = new System.Windows.Forms.ComboBox();
            this.lblGradeStudent = new System.Windows.Forms.Label();
            this.tabControlMain.SuspendLayout();
            this.tabStudents.SuspendLayout();
            this.grpStudentList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStudents)).BeginInit();
            this.grpStudentForm.SuspendLayout();
            this.tabTeachers.SuspendLayout();
            this.grpTeacherList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTeachers)).BeginInit();
            this.grpTeacherForm.SuspendLayout();
            this.tabSubjects.SuspendLayout();
            this.grpSubjectList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSubjects)).BeginInit();
            this.grpSubjectForm.SuspendLayout();
            this.tabGrades.SuspendLayout();
            this.grpGradeFilter.SuspendLayout();
            this.groGradeList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGrades)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.grpGradeForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabStudents);
            this.tabControlMain.Controls.Add(this.tabTeachers);
            this.tabControlMain.Controls.Add(this.tabSubjects);
            this.tabControlMain.Controls.Add(this.tabGrades);
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.Location = new System.Drawing.Point(0, 0);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(582, 473);
            this.tabControlMain.TabIndex = 0;
            // 
            // tabStudents
            // 
            this.tabStudents.Controls.Add(this.grpStudentList);
            this.tabStudents.Controls.Add(this.grpStudentForm);
            this.tabStudents.Location = new System.Drawing.Point(4, 25);
            this.tabStudents.Name = "tabStudents";
            this.tabStudents.Padding = new System.Windows.Forms.Padding(3);
            this.tabStudents.Size = new System.Drawing.Size(574, 444);
            this.tabStudents.TabIndex = 0;
            this.tabStudents.Text = "Students";
            this.tabStudents.UseVisualStyleBackColor = true;
            // 
            // grpStudentList
            // 
            this.grpStudentList.BackColor = System.Drawing.Color.RosyBrown;
            this.grpStudentList.Controls.Add(this.lblStudentPageInfo);
            this.grpStudentList.Controls.Add(this.btnNextPage);
            this.grpStudentList.Controls.Add(this.btnPrevPage);
            this.grpStudentList.Controls.Add(this.dgvStudents);
            this.grpStudentList.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grpStudentList.Location = new System.Drawing.Point(3, 225);
            this.grpStudentList.Name = "grpStudentList";
            this.grpStudentList.Size = new System.Drawing.Size(568, 216);
            this.grpStudentList.TabIndex = 1;
            this.grpStudentList.TabStop = false;
            this.grpStudentList.Text = "Student List";
            // 
            // lblStudentPageInfo
            // 
            this.lblStudentPageInfo.AutoSize = true;
            this.lblStudentPageInfo.Location = new System.Drawing.Point(235, 182);
            this.lblStudentPageInfo.Name = "lblStudentPageInfo";
            this.lblStudentPageInfo.Size = new System.Drawing.Size(70, 16);
            this.lblStudentPageInfo.TabIndex = 3;
            this.lblStudentPageInfo.Text = "Page X / Y";
            // 
            // btnNextPage
            // 
            this.btnNextPage.ForeColor = System.Drawing.Color.Coral;
            this.btnNextPage.Location = new System.Drawing.Point(487, 179);
            this.btnNextPage.Name = "btnNextPage";
            this.btnNextPage.Size = new System.Drawing.Size(75, 23);
            this.btnNextPage.TabIndex = 2;
            this.btnNextPage.Text = "Next";
            this.btnNextPage.UseVisualStyleBackColor = true;
            // 
            // btnPrevPage
            // 
            this.btnPrevPage.ForeColor = System.Drawing.Color.Coral;
            this.btnPrevPage.Location = new System.Drawing.Point(6, 179);
            this.btnPrevPage.Name = "btnPrevPage";
            this.btnPrevPage.Size = new System.Drawing.Size(75, 23);
            this.btnPrevPage.TabIndex = 1;
            this.btnPrevPage.Text = "Previous";
            this.btnPrevPage.UseVisualStyleBackColor = true;
            // 
            // dgvStudents
            // 
            this.dgvStudents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStudents.Location = new System.Drawing.Point(6, 30);
            this.dgvStudents.Name = "dgvStudents";
            this.dgvStudents.RowHeadersWidth = 51;
            this.dgvStudents.RowTemplate.Height = 24;
            this.dgvStudents.Size = new System.Drawing.Size(557, 143);
            this.dgvStudents.TabIndex = 0;
            // 
            // grpStudentForm
            // 
            this.grpStudentForm.BackColor = System.Drawing.Color.RosyBrown;
            this.grpStudentForm.Controls.Add(this.textBox2);
            this.grpStudentForm.Controls.Add(this.btnDeleteStudent);
            this.grpStudentForm.Controls.Add(this.btnEditStudent);
            this.grpStudentForm.Controls.Add(this.btnAddStudent);
            this.grpStudentForm.Controls.Add(this.dtpStudentBirth);
            this.grpStudentForm.Controls.Add(this.label2);
            this.grpStudentForm.Controls.Add(this.txtStudentName);
            this.grpStudentForm.Controls.Add(this.label1);
            this.grpStudentForm.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpStudentForm.Location = new System.Drawing.Point(3, 3);
            this.grpStudentForm.Name = "grpStudentForm";
            this.grpStudentForm.Size = new System.Drawing.Size(568, 216);
            this.grpStudentForm.TabIndex = 0;
            this.grpStudentForm.TabStop = false;
            this.grpStudentForm.Text = "Manage Student";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(261, 52);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(0, 22);
            this.textBox2.TabIndex = 7;
            // 
            // btnDeleteStudent
            // 
            this.btnDeleteStudent.ForeColor = System.Drawing.Color.Coral;
            this.btnDeleteStudent.Location = new System.Drawing.Point(270, 150);
            this.btnDeleteStudent.Name = "btnDeleteStudent";
            this.btnDeleteStudent.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteStudent.TabIndex = 6;
            this.btnDeleteStudent.Text = "Delete";
            this.btnDeleteStudent.UseVisualStyleBackColor = true;
            // 
            // btnEditStudent
            // 
            this.btnEditStudent.ForeColor = System.Drawing.Color.Coral;
            this.btnEditStudent.Location = new System.Drawing.Point(145, 150);
            this.btnEditStudent.Name = "btnEditStudent";
            this.btnEditStudent.Size = new System.Drawing.Size(75, 23);
            this.btnEditStudent.TabIndex = 5;
            this.btnEditStudent.Text = "Edit";
            this.btnEditStudent.UseVisualStyleBackColor = true;
            // 
            // btnAddStudent
            // 
            this.btnAddStudent.ForeColor = System.Drawing.Color.Coral;
            this.btnAddStudent.Location = new System.Drawing.Point(22, 150);
            this.btnAddStudent.Name = "btnAddStudent";
            this.btnAddStudent.Size = new System.Drawing.Size(75, 23);
            this.btnAddStudent.TabIndex = 4;
            this.btnAddStudent.Text = "Add";
            this.btnAddStudent.UseVisualStyleBackColor = true;
            // 
            // dtpStudentBirth
            // 
            this.dtpStudentBirth.Location = new System.Drawing.Point(145, 99);
            this.dtpStudentBirth.Name = "dtpStudentBirth";
            this.dtpStudentBirth.Size = new System.Drawing.Size(200, 22);
            this.dtpStudentBirth.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Birth Date";
            // 
            // txtStudentName
            // 
            this.txtStudentName.Location = new System.Drawing.Point(145, 39);
            this.txtStudentName.Name = "txtStudentName";
            this.txtStudentName.Size = new System.Drawing.Size(100, 22);
            this.txtStudentName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Full Name";
            // 
            // tabTeachers
            // 
            this.tabTeachers.Controls.Add(this.grpTeacherList);
            this.tabTeachers.Controls.Add(this.grpTeacherForm);
            this.tabTeachers.Location = new System.Drawing.Point(4, 25);
            this.tabTeachers.Name = "tabTeachers";
            this.tabTeachers.Padding = new System.Windows.Forms.Padding(3);
            this.tabTeachers.Size = new System.Drawing.Size(574, 444);
            this.tabTeachers.TabIndex = 1;
            this.tabTeachers.Text = "Teachers";
            this.tabTeachers.UseVisualStyleBackColor = true;
            // 
            // grpTeacherList
            // 
            this.grpTeacherList.Controls.Add(this.dgvTeachers);
            this.grpTeacherList.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grpTeacherList.Location = new System.Drawing.Point(3, 220);
            this.grpTeacherList.Name = "grpTeacherList";
            this.grpTeacherList.Size = new System.Drawing.Size(568, 221);
            this.grpTeacherList.TabIndex = 1;
            this.grpTeacherList.TabStop = false;
            this.grpTeacherList.Text = "Teacher List";
            // 
            // dgvTeachers
            // 
            this.dgvTeachers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTeachers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTeachers.Location = new System.Drawing.Point(3, 18);
            this.dgvTeachers.Name = "dgvTeachers";
            this.dgvTeachers.RowHeadersWidth = 51;
            this.dgvTeachers.RowTemplate.Height = 24;
            this.dgvTeachers.Size = new System.Drawing.Size(562, 200);
            this.dgvTeachers.TabIndex = 0;
            // 
            // grpTeacherForm
            // 
            this.grpTeacherForm.Controls.Add(this.btnAddTeacher);
            this.grpTeacherForm.Controls.Add(this.txtTeacherName);
            this.grpTeacherForm.Controls.Add(this.lblTeacherName);
            this.grpTeacherForm.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpTeacherForm.Location = new System.Drawing.Point(3, 3);
            this.grpTeacherForm.Name = "grpTeacherForm";
            this.grpTeacherForm.Size = new System.Drawing.Size(568, 211);
            this.grpTeacherForm.TabIndex = 0;
            this.grpTeacherForm.TabStop = false;
            this.grpTeacherForm.Text = "Manage Teacher";
            // 
            // btnAddTeacher
            // 
            this.btnAddTeacher.Location = new System.Drawing.Point(114, 78);
            this.btnAddTeacher.Name = "btnAddTeacher";
            this.btnAddTeacher.Size = new System.Drawing.Size(75, 23);
            this.btnAddTeacher.TabIndex = 2;
            this.btnAddTeacher.Text = "Add";
            this.btnAddTeacher.UseVisualStyleBackColor = true;
            // 
            // txtTeacherName
            // 
            this.txtTeacherName.Location = new System.Drawing.Point(114, 35);
            this.txtTeacherName.Name = "txtTeacherName";
            this.txtTeacherName.Size = new System.Drawing.Size(175, 22);
            this.txtTeacherName.TabIndex = 1;
            // 
            // lblTeacherName
            // 
            this.lblTeacherName.AutoSize = true;
            this.lblTeacherName.Location = new System.Drawing.Point(18, 35);
            this.lblTeacherName.Name = "lblTeacherName";
            this.lblTeacherName.Size = new System.Drawing.Size(68, 16);
            this.lblTeacherName.TabIndex = 0;
            this.lblTeacherName.Text = "Full Name";
            // 
            // tabSubjects
            // 
            this.tabSubjects.Controls.Add(this.grpSubjectList);
            this.tabSubjects.Controls.Add(this.grpSubjectForm);
            this.tabSubjects.Location = new System.Drawing.Point(4, 25);
            this.tabSubjects.Name = "tabSubjects";
            this.tabSubjects.Padding = new System.Windows.Forms.Padding(3);
            this.tabSubjects.Size = new System.Drawing.Size(574, 444);
            this.tabSubjects.TabIndex = 2;
            this.tabSubjects.Text = "Subjects";
            this.tabSubjects.UseVisualStyleBackColor = true;
            // 
            // grpSubjectList
            // 
            this.grpSubjectList.Controls.Add(this.dgvSubjects);
            this.grpSubjectList.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grpSubjectList.Location = new System.Drawing.Point(3, 243);
            this.grpSubjectList.Name = "grpSubjectList";
            this.grpSubjectList.Size = new System.Drawing.Size(568, 198);
            this.grpSubjectList.TabIndex = 1;
            this.grpSubjectList.TabStop = false;
            this.grpSubjectList.Text = "Subject List";
            // 
            // dgvSubjects
            // 
            this.dgvSubjects.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSubjects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSubjects.Location = new System.Drawing.Point(3, 18);
            this.dgvSubjects.Name = "dgvSubjects";
            this.dgvSubjects.RowHeadersWidth = 51;
            this.dgvSubjects.RowTemplate.Height = 24;
            this.dgvSubjects.Size = new System.Drawing.Size(562, 177);
            this.dgvSubjects.TabIndex = 0;
            // 
            // grpSubjectForm
            // 
            this.grpSubjectForm.Controls.Add(this.btnDeleteSubject);
            this.grpSubjectForm.Controls.Add(this.btnEditSubject);
            this.grpSubjectForm.Controls.Add(this.lblSubjectTeacher);
            this.grpSubjectForm.Controls.Add(this.btnAddSubject);
            this.grpSubjectForm.Controls.Add(this.cmbSubjectTeacher);
            this.grpSubjectForm.Controls.Add(this.txtSubjectName);
            this.grpSubjectForm.Controls.Add(this.lblSubjectName);
            this.grpSubjectForm.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpSubjectForm.Location = new System.Drawing.Point(3, 3);
            this.grpSubjectForm.Name = "grpSubjectForm";
            this.grpSubjectForm.Size = new System.Drawing.Size(568, 234);
            this.grpSubjectForm.TabIndex = 0;
            this.grpSubjectForm.TabStop = false;
            this.grpSubjectForm.Text = "Manage Subject";
            // 
            // btnDeleteSubject
            // 
            this.btnDeleteSubject.Location = new System.Drawing.Point(261, 149);
            this.btnDeleteSubject.Name = "btnDeleteSubject";
            this.btnDeleteSubject.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteSubject.TabIndex = 6;
            this.btnDeleteSubject.Text = "Delete";
            this.btnDeleteSubject.UseVisualStyleBackColor = true;
            // 
            // btnEditSubject
            // 
            this.btnEditSubject.Location = new System.Drawing.Point(156, 149);
            this.btnEditSubject.Name = "btnEditSubject";
            this.btnEditSubject.Size = new System.Drawing.Size(75, 23);
            this.btnEditSubject.TabIndex = 5;
            this.btnEditSubject.Text = "Edit";
            this.btnEditSubject.UseVisualStyleBackColor = true;
            // 
            // lblSubjectTeacher
            // 
            this.lblSubjectTeacher.AutoSize = true;
            this.lblSubjectTeacher.Location = new System.Drawing.Point(47, 81);
            this.lblSubjectTeacher.Name = "lblSubjectTeacher";
            this.lblSubjectTeacher.Size = new System.Drawing.Size(58, 16);
            this.lblSubjectTeacher.TabIndex = 4;
            this.lblSubjectTeacher.Text = "Teacher";
            // 
            // btnAddSubject
            // 
            this.btnAddSubject.Location = new System.Drawing.Point(50, 149);
            this.btnAddSubject.Name = "btnAddSubject";
            this.btnAddSubject.Size = new System.Drawing.Size(75, 23);
            this.btnAddSubject.TabIndex = 3;
            this.btnAddSubject.Text = "Add";
            this.btnAddSubject.UseVisualStyleBackColor = true;
            // 
            // cmbSubjectTeacher
            // 
            this.cmbSubjectTeacher.FormattingEnabled = true;
            this.cmbSubjectTeacher.Location = new System.Drawing.Point(131, 81);
            this.cmbSubjectTeacher.Name = "cmbSubjectTeacher";
            this.cmbSubjectTeacher.Size = new System.Drawing.Size(172, 24);
            this.cmbSubjectTeacher.TabIndex = 2;
            // 
            // txtSubjectName
            // 
            this.txtSubjectName.Location = new System.Drawing.Point(131, 40);
            this.txtSubjectName.Name = "txtSubjectName";
            this.txtSubjectName.Size = new System.Drawing.Size(172, 22);
            this.txtSubjectName.TabIndex = 1;
            // 
            // lblSubjectName
            // 
            this.lblSubjectName.AutoSize = true;
            this.lblSubjectName.Location = new System.Drawing.Point(47, 40);
            this.lblSubjectName.Name = "lblSubjectName";
            this.lblSubjectName.Size = new System.Drawing.Size(44, 16);
            this.lblSubjectName.TabIndex = 0;
            this.lblSubjectName.Text = "Name";
            // 
            // tabGrades
            // 
            this.tabGrades.Controls.Add(this.grpGradeFilter);
            this.tabGrades.Controls.Add(this.groGradeList);
            this.tabGrades.Controls.Add(this.groupBox4);
            this.tabGrades.Controls.Add(this.grpGradeForm);
            this.tabGrades.Location = new System.Drawing.Point(4, 25);
            this.tabGrades.Name = "tabGrades";
            this.tabGrades.Padding = new System.Windows.Forms.Padding(3);
            this.tabGrades.Size = new System.Drawing.Size(574, 444);
            this.tabGrades.TabIndex = 3;
            this.tabGrades.Text = "Grades";
            this.tabGrades.UseVisualStyleBackColor = true;
            // 
            // grpGradeFilter
            // 
            this.grpGradeFilter.Controls.Add(this.lblFilterSubject);
            this.grpGradeFilter.Controls.Add(this.cmbFilterSubject);
            this.grpGradeFilter.Location = new System.Drawing.Point(8, 118);
            this.grpGradeFilter.Name = "grpGradeFilter";
            this.grpGradeFilter.Size = new System.Drawing.Size(558, 100);
            this.grpGradeFilter.TabIndex = 1;
            this.grpGradeFilter.TabStop = false;
            this.grpGradeFilter.Text = "Filter";
            // 
            // lblFilterSubject
            // 
            this.lblFilterSubject.AutoSize = true;
            this.lblFilterSubject.Location = new System.Drawing.Point(38, 50);
            this.lblFilterSubject.Name = "lblFilterSubject";
            this.lblFilterSubject.Size = new System.Drawing.Size(71, 16);
            this.lblFilterSubject.TabIndex = 4;
            this.lblFilterSubject.Text = "By Subject";
            // 
            // cmbFilterSubject
            // 
            this.cmbFilterSubject.FormattingEnabled = true;
            this.cmbFilterSubject.Location = new System.Drawing.Point(137, 47);
            this.cmbFilterSubject.Name = "cmbFilterSubject";
            this.cmbFilterSubject.Size = new System.Drawing.Size(121, 24);
            this.cmbFilterSubject.TabIndex = 4;
            // 
            // groGradeList
            // 
            this.groGradeList.Controls.Add(this.dgvGrades);
            this.groGradeList.Location = new System.Drawing.Point(8, 224);
            this.groGradeList.Name = "groGradeList";
            this.groGradeList.Size = new System.Drawing.Size(558, 100);
            this.groGradeList.TabIndex = 1;
            this.groGradeList.TabStop = false;
            this.groGradeList.Text = "Grade List";
            // 
            // dgvGrades
            // 
            this.dgvGrades.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGrades.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvGrades.Location = new System.Drawing.Point(3, 18);
            this.dgvGrades.Name = "dgvGrades";
            this.dgvGrades.RowHeadersWidth = 51;
            this.dgvGrades.RowTemplate.Height = 24;
            this.dgvGrades.Size = new System.Drawing.Size(552, 79);
            this.dgvGrades.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnCalculateAverage);
            this.groupBox4.Controls.Add(this.cmbAverageStudent);
            this.groupBox4.Controls.Add(this.lblAverageStudent);
            this.groupBox4.Location = new System.Drawing.Point(8, 341);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(558, 100);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Calculate Average";
            // 
            // btnCalculateAverage
            // 
            this.btnCalculateAverage.Location = new System.Drawing.Point(240, 44);
            this.btnCalculateAverage.Name = "btnCalculateAverage";
            this.btnCalculateAverage.Size = new System.Drawing.Size(75, 23);
            this.btnCalculateAverage.TabIndex = 4;
            this.btnCalculateAverage.Text = "Calculate";
            this.btnCalculateAverage.UseVisualStyleBackColor = true;
            // 
            // cmbAverageStudent
            // 
            this.cmbAverageStudent.FormattingEnabled = true;
            this.cmbAverageStudent.Location = new System.Drawing.Point(98, 43);
            this.cmbAverageStudent.Name = "cmbAverageStudent";
            this.cmbAverageStudent.Size = new System.Drawing.Size(121, 24);
            this.cmbAverageStudent.TabIndex = 5;
            // 
            // lblAverageStudent
            // 
            this.lblAverageStudent.AutoSize = true;
            this.lblAverageStudent.Location = new System.Drawing.Point(29, 43);
            this.lblAverageStudent.Name = "lblAverageStudent";
            this.lblAverageStudent.Size = new System.Drawing.Size(63, 16);
            this.lblAverageStudent.TabIndex = 5;
            this.lblAverageStudent.Text = "Calculate";
            // 
            // grpGradeForm
            // 
            this.grpGradeForm.Controls.Add(this.label3);
            this.grpGradeForm.Controls.Add(this.cmbGradeStudent);
            this.grpGradeForm.Controls.Add(this.lblGradeSubject);
            this.grpGradeForm.Controls.Add(this.btnDeleteGrade);
            this.grpGradeForm.Controls.Add(this.btnEditGrade);
            this.grpGradeForm.Controls.Add(this.btnAddGrade);
            this.grpGradeForm.Controls.Add(this.txtGradeValue);
            this.grpGradeForm.Controls.Add(this.cmbGradeSubject);
            this.grpGradeForm.Controls.Add(this.lblGradeStudent);
            this.grpGradeForm.Location = new System.Drawing.Point(8, 6);
            this.grpGradeForm.Name = "grpGradeForm";
            this.grpGradeForm.Size = new System.Drawing.Size(558, 100);
            this.grpGradeForm.TabIndex = 0;
            this.grpGradeForm.TabStop = false;
            this.grpGradeForm.Text = "Manage Grade";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(373, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 16);
            this.label3.TabIndex = 8;
            this.label3.Text = "Grade Value :";
            // 
            // cmbGradeStudent
            // 
            this.cmbGradeStudent.FormattingEnabled = true;
            this.cmbGradeStudent.Location = new System.Drawing.Point(468, 28);
            this.cmbGradeStudent.Name = "cmbGradeStudent";
            this.cmbGradeStudent.Size = new System.Drawing.Size(84, 24);
            this.cmbGradeStudent.TabIndex = 7;
            // 
            // lblGradeSubject
            // 
            this.lblGradeSubject.AutoSize = true;
            this.lblGradeSubject.Location = new System.Drawing.Point(178, 28);
            this.lblGradeSubject.Name = "lblGradeSubject";
            this.lblGradeSubject.Size = new System.Drawing.Size(52, 16);
            this.lblGradeSubject.TabIndex = 6;
            this.lblGradeSubject.Text = "Subject";
            // 
            // btnDeleteGrade
            // 
            this.btnDeleteGrade.Location = new System.Drawing.Point(282, 69);
            this.btnDeleteGrade.Name = "btnDeleteGrade";
            this.btnDeleteGrade.Size = new System.Drawing.Size(75, 24);
            this.btnDeleteGrade.TabIndex = 5;
            this.btnDeleteGrade.Text = "Delete";
            this.btnDeleteGrade.UseVisualStyleBackColor = true;
            // 
            // btnEditGrade
            // 
            this.btnEditGrade.Location = new System.Drawing.Point(190, 70);
            this.btnEditGrade.Name = "btnEditGrade";
            this.btnEditGrade.Size = new System.Drawing.Size(75, 23);
            this.btnEditGrade.TabIndex = 4;
            this.btnEditGrade.Text = "Edit";
            this.btnEditGrade.UseVisualStyleBackColor = true;
            // 
            // btnAddGrade
            // 
            this.btnAddGrade.Location = new System.Drawing.Point(98, 70);
            this.btnAddGrade.Name = "btnAddGrade";
            this.btnAddGrade.Size = new System.Drawing.Size(75, 23);
            this.btnAddGrade.TabIndex = 3;
            this.btnAddGrade.Text = "Add";
            this.btnAddGrade.UseVisualStyleBackColor = true;
            // 
            // txtGradeValue
            // 
            this.txtGradeValue.Location = new System.Drawing.Point(64, 28);
            this.txtGradeValue.Name = "txtGradeValue";
            this.txtGradeValue.Size = new System.Drawing.Size(100, 22);
            this.txtGradeValue.TabIndex = 2;
            // 
            // cmbGradeSubject
            // 
            this.cmbGradeSubject.FormattingEnabled = true;
            this.cmbGradeSubject.Location = new System.Drawing.Point(236, 28);
            this.cmbGradeSubject.Name = "cmbGradeSubject";
            this.cmbGradeSubject.Size = new System.Drawing.Size(121, 24);
            this.cmbGradeSubject.TabIndex = 1;
            // 
            // lblGradeStudent
            // 
            this.lblGradeStudent.AutoSize = true;
            this.lblGradeStudent.Location = new System.Drawing.Point(6, 28);
            this.lblGradeStudent.Name = "lblGradeStudent";
            this.lblGradeStudent.Size = new System.Drawing.Size(52, 16);
            this.lblGradeStudent.TabIndex = 0;
            this.lblGradeStudent.Text = "Student";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(582, 473);
            this.Controls.Add(this.tabControlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            this.tabControlMain.ResumeLayout(false);
            this.tabStudents.ResumeLayout(false);
            this.grpStudentList.ResumeLayout(false);
            this.grpStudentList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStudents)).EndInit();
            this.grpStudentForm.ResumeLayout(false);
            this.grpStudentForm.PerformLayout();
            this.tabTeachers.ResumeLayout(false);
            this.grpTeacherList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTeachers)).EndInit();
            this.grpTeacherForm.ResumeLayout(false);
            this.grpTeacherForm.PerformLayout();
            this.tabSubjects.ResumeLayout(false);
            this.grpSubjectList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSubjects)).EndInit();
            this.grpSubjectForm.ResumeLayout(false);
            this.grpSubjectForm.PerformLayout();
            this.tabGrades.ResumeLayout(false);
            this.grpGradeFilter.ResumeLayout(false);
            this.grpGradeFilter.PerformLayout();
            this.groGradeList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGrades)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.grpGradeForm.ResumeLayout(false);
            this.grpGradeForm.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabStudents;
        private System.Windows.Forms.TabPage tabTeachers;
        private System.Windows.Forms.TabPage tabSubjects;
        private System.Windows.Forms.TabPage tabGrades;
        private System.Windows.Forms.GroupBox grpStudentForm;
        private System.Windows.Forms.Button btnDeleteStudent;
        private System.Windows.Forms.Button btnEditStudent;
        private System.Windows.Forms.Button btnAddStudent;
        private System.Windows.Forms.DateTimePicker dtpStudentBirth;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtStudentName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.GroupBox grpStudentList;
        private System.Windows.Forms.Button btnPrevPage;
        private System.Windows.Forms.DataGridView dgvStudents;
        private System.Windows.Forms.Button btnNextPage;
        private System.Windows.Forms.Label lblStudentPageInfo;
        private System.Windows.Forms.GroupBox grpTeacherList;
        private System.Windows.Forms.GroupBox grpTeacherForm;
        private System.Windows.Forms.Button btnAddTeacher;
        private System.Windows.Forms.TextBox txtTeacherName;
        private System.Windows.Forms.Label lblTeacherName;
        private System.Windows.Forms.DataGridView dgvTeachers;
        private System.Windows.Forms.GroupBox grpSubjectList;
        private System.Windows.Forms.GroupBox grpSubjectForm;
        private System.Windows.Forms.DataGridView dgvSubjects;
        private System.Windows.Forms.Button btnDeleteSubject;
        private System.Windows.Forms.Button btnEditSubject;
        private System.Windows.Forms.Label lblSubjectTeacher;
        private System.Windows.Forms.Button btnAddSubject;
        private System.Windows.Forms.ComboBox cmbSubjectTeacher;
        private System.Windows.Forms.TextBox txtSubjectName;
        private System.Windows.Forms.Label lblSubjectName;
        private System.Windows.Forms.GroupBox grpGradeFilter;
        private System.Windows.Forms.GroupBox groGradeList;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox grpGradeForm;
        private System.Windows.Forms.Button btnAddGrade;
        private System.Windows.Forms.TextBox txtGradeValue;
        private System.Windows.Forms.ComboBox cmbGradeSubject;
        private System.Windows.Forms.Label lblGradeStudent;
        private System.Windows.Forms.Label lblFilterSubject;
        private System.Windows.Forms.ComboBox cmbFilterSubject;
        private System.Windows.Forms.DataGridView dgvGrades;
        private System.Windows.Forms.Button btnCalculateAverage;
        private System.Windows.Forms.ComboBox cmbAverageStudent;
        private System.Windows.Forms.Label lblAverageStudent;
        private System.Windows.Forms.Button btnDeleteGrade;
        private System.Windows.Forms.Button btnEditGrade;
        private System.Windows.Forms.Label lblGradeSubject;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbGradeStudent;
    }
}