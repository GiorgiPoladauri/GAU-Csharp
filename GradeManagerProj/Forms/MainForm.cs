using System;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;
using GradeManagerProj.Data;
using GradeManagerProj.Models;

namespace GradeManagerProj
{
    public partial class MainForm : Form
    {
        private readonly ExamContext _context = new ExamContext();

        private const int PageSize = 10;
        private int CurrentStudentPage = 1;
        private int TotalStudentPages = 1;

        public MainForm()
        {
            InitializeComponent();

            this.Load += MainForm_Load;

            btnAddStudent.Click += btnAddStudent_Click;
            btnEditStudent.Click += btnEditStudent_Click;
            btnDeleteStudent.Click += btnDeleteStudent_Click;
            btnPrevPage.Click += btnPrevPage_Click;
            btnNextPage.Click += btnNextPage_Click;
            dgvStudents.SelectionChanged += dgvStudents_SelectionChanged;

            btnAddTeacher.Click += btnAddTeacher_Click;

            btnAddSubject.Click += btnAddSubject_Click;
            btnEditSubject.Click += btnEditSubject_Click;
            btnDeleteSubject.Click += btnDeleteSubject_Click;
            dgvSubjects.SelectionChanged += dgvSubjects_SelectionChanged;

            cmbFilterSubject.SelectedIndexChanged += cmbFilterSubject_SelectedIndexChanged;
            dgvGrades.SelectionChanged += dgvGrades_SelectionChanged;
            btnAddGrade.Click += btnAddGrade_Click;
            btnEditGrade.Click += btnEditGrade_Click;
            btnDeleteGrade.Click += btnDeleteGrade_Click;
            btnCalculateAverage.Click += btnCalculateAverage_Click;

            LoadTeachers();
            LoadSubjects();
            LoadStudentsPage();
            LoadComboBoxes();
            LoadGrades(null);
        }

        #region Students (CRUD + Pagination)

        private void LoadStudentsPage()
        {
            try
            {
                int totalCount = _context.Students.Count();
                TotalStudentPages = (int)Math.Ceiling(totalCount / (double)PageSize);
                if (TotalStudentPages == 0) TotalStudentPages = 1;

                if (CurrentStudentPage < 1) CurrentStudentPage = 1;
                if (CurrentStudentPage > TotalStudentPages) CurrentStudentPage = TotalStudentPages;

                var pageItems = _context.Students
                    .OrderBy(s => s.Id)
                    .Skip((CurrentStudentPage - 1) * PageSize)
                    .Take(PageSize)
                    .ToList();

                dgvStudents.DataSource = pageItems.Select(s => new
                {
                    s.Id,
                    s.FullName,
                    BirthDate = s.BirthDate.ToShortDateString()
                }).ToList();

                lblStudentPageInfo.Text = $"Page {CurrentStudentPage} / {TotalStudentPages}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading students: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPrevPage_Click(object sender, EventArgs e)
        {
            if (CurrentStudentPage > 1)
            {
                CurrentStudentPage--;
                LoadStudentsPage();
            }
        }

        private void btnNextPage_Click(object sender, EventArgs e)
        {
            if (CurrentStudentPage < TotalStudentPages)
            {
                CurrentStudentPage++;
                LoadStudentsPage();
            }
        }

        private void btnAddStudent_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtStudentName.Text))
                {
                    MessageBox.Show("Please enter the student’s full name.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var newStudent = new Students
                {
                    FullName = txtStudentName.Text.Trim(),
                    BirthDate = dtpStudentBirth.Value.Date
                };

                _context.Students.Add(newStudent);
                _context.SaveChanges();

                int totalCount = _context.Students.Count();
                TotalStudentPages = (int)Math.Ceiling(totalCount / (double)PageSize);
                CurrentStudentPage = TotalStudentPages;

                LoadStudentsPage();
                txtStudentName.Clear();
                dtpStudentBirth.Value = DateTime.Today;
                LoadComboBoxes();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding student: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvStudents_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvStudents.SelectedRows.Count == 0) return;

            int selectedId = (int)dgvStudents.SelectedRows[0].Cells["Id"].Value;
            var student = _context.Students.Find(selectedId);
            if (student == null) return;

            txtStudentName.Text = student.FullName;
            dtpStudentBirth.Value = student.BirthDate;
        }

        private void btnEditStudent_Click(object sender, EventArgs e)
        {
            if (dgvStudents.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select a student to edit.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int selectedId = (int)dgvStudents.SelectedRows[0].Cells["Id"].Value;
            var student = _context.Students.Find(selectedId);
            if (student == null) return;

            try
            {
                if (string.IsNullOrWhiteSpace(txtStudentName.Text))
                {
                    MessageBox.Show("Please enter the student’s full name.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                student.FullName = txtStudentName.Text.Trim();
                student.BirthDate = dtpStudentBirth.Value.Date;
                _context.SaveChanges();

                LoadStudentsPage();
                LoadComboBoxes();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error editing student: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteStudent_Click(object sender, EventArgs e)
        {
            if (dgvStudents.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select a student to delete.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int selectedId = (int)dgvStudents.SelectedRows[0].Cells["Id"].Value;
            var student = _context.Students.Include(s => s.Grades).FirstOrDefault(s => s.Id == selectedId);
            if (student == null) return;

            var confirm = MessageBox.Show(
                $"Are you sure you want to delete \"{student.FullName}\" and all their grades?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            try
            {
                if (student.Grades.Any())
                    _context.Grades.RemoveRange(student.Grades);

                _context.Students.Remove(student);
                _context.SaveChanges();

                int totalCount = _context.Students.Count();
                TotalStudentPages = (int)Math.Ceiling(totalCount / (double)PageSize);
                if (CurrentStudentPage > TotalStudentPages) CurrentStudentPage = TotalStudentPages;
                if (CurrentStudentPage < 1) CurrentStudentPage = 1;

                LoadStudentsPage();
                txtStudentName.Clear();
                dtpStudentBirth.Value = DateTime.Today;
                LoadComboBoxes();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting student: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Teachers (Add + List)

        private void LoadTeachers()
        {
            try
            {
                var list = _context.Teachers
                    .Select(t => new { t.Id, t.FullName })
                    .ToList();
                dgvTeachers.DataSource = list;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading teachers: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddTeacher_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtTeacherName.Text))
                {
                    MessageBox.Show("Please enter the teacher’s full name.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var teacher = new Teachers
                {
                    FullName = txtTeacherName.Text.Trim()
                };
                _context.Teachers.Add(teacher);
                _context.SaveChanges();

                LoadTeachers();
                txtTeacherName.Clear();
                LoadSubjects(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding teacher: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Subjects (CRUD)

        private void LoadSubjects()
        {
            try
            {
                var list = _context.Subjects
                    .Include(s => s.Teacher)
                    .Select(s => new
                    {
                        s.Id,
                        s.Name,
                        TeacherName = s.Teacher.FullName
                    })
                    .ToList();
                dgvSubjects.DataSource = list;

                var teachersList = _context.Teachers
                    .Select(t => new { t.Id, t.FullName })
                    .ToList();

                cmbSubjectTeacher.DataSource = teachersList;
                cmbSubjectTeacher.DisplayMember = "FullName";
                cmbSubjectTeacher.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading subjects: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvSubjects_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSubjects.SelectedRows.Count == 0) return;

            int selectedId = (int)dgvSubjects.SelectedRows[0].Cells["Id"].Value;
            var subj = _context.Subjects.Find(selectedId);
            if (subj == null) return;

            txtSubjectName.Text = subj.Name;
            cmbSubjectTeacher.SelectedValue = subj.TeacherId;
        }

        private void btnAddSubject_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtSubjectName.Text))
                {
                    MessageBox.Show("Please enter the subject’s name.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var newSubj = new Subjects
                {
                    Name = txtSubjectName.Text.Trim(),
                    TeacherId = (int)cmbSubjectTeacher.SelectedValue
                };

                _context.Subjects.Add(newSubj);
                _context.SaveChanges();

                LoadSubjects();
                txtSubjectName.Clear();
                LoadComboBoxes();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding subject: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditSubject_Click(object sender, EventArgs e)
        {
            if (dgvSubjects.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select a subject to edit.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int selectedId = (int)dgvSubjects.SelectedRows[0].Cells["Id"].Value;
            var subj = _context.Subjects.Find(selectedId);
            if (subj == null) return;

            try
            {
                if (string.IsNullOrWhiteSpace(txtSubjectName.Text))
                {
                    MessageBox.Show("Please enter the subject’s name.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                subj.Name = txtSubjectName.Text.Trim();
                subj.TeacherId = (int)cmbSubjectTeacher.SelectedValue;
                _context.SaveChanges();

                LoadSubjects();
                LoadComboBoxes();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error editing subject: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteSubject_Click(object sender, EventArgs e)
        {
            if (dgvSubjects.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select a subject to delete.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int selectedId = (int)dgvSubjects.SelectedRows[0].Cells["Id"].Value;
            var subj = _context.Subjects.Include(s => s.Grades).FirstOrDefault(s => s.Id == selectedId);
            if (subj == null) return;

            var confirm = MessageBox.Show(
                $"Are you sure you want to delete \"{subj.Name}\" and its grades?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            try
            {
                if (subj.Grades.Any())
                    _context.Grades.RemoveRange(subj.Grades);

                _context.Subjects.Remove(subj);
                _context.SaveChanges();

                LoadSubjects();
                LoadComboBoxes();
                txtSubjectName.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting subject: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Grades (CRUD + Filter + Highlight + Average)

        private void LoadComboBoxes()
        {
            var studentsList = _context.Students
                .Select(s => new { s.Id, s.FullName })
                .ToList();

            cmbGradeStudent.DataSource = new BindingSource(studentsList, null);
            cmbGradeStudent.DisplayMember = "FullName";
            cmbGradeStudent.ValueMember = "Id";

            cmbAverageStudent.DataSource = new BindingSource(studentsList, null);
            cmbAverageStudent.DisplayMember = "FullName";
            cmbAverageStudent.ValueMember = "Id";

            var subjectsList = _context.Subjects
                .Select(s => new { s.Id, s.Name })
                .ToList();

            cmbGradeSubject.DataSource = new BindingSource(subjectsList, null);
            cmbGradeSubject.DisplayMember = "Name";
            cmbGradeSubject.ValueMember = "Id";

            var filterList = subjectsList
                .Select(s => new { s.Id, s.Name })
                .ToList();
            filterList.Insert(0, new { Id = 0, Name = "All Subjects" });

            cmbFilterSubject.DataSource = new BindingSource(filterList, null);
            cmbFilterSubject.DisplayMember = "Name";
            cmbFilterSubject.ValueMember = "Id";
        }

        private void LoadGrades(int? filterSubjectId)
        {
            try
            {
                var query = _context.Grades
                    .Include(g => g.Student)
                    .Include(g => g.Subject)
                    .Include(g => g.Subject.Teacher)
                    .AsQueryable();

                if (filterSubjectId.HasValue && filterSubjectId.Value != 0)
                {
                    query = query.Where(g => g.SubjectId == filterSubjectId.Value);
                }

                var gradeItems = query
                    .Select(g => new
                    {
                        g.Id,
                        StudentName = g.Student.FullName,
                        SubjectName = g.Subject.Name,
                        TeacherName = g.Subject.Teacher.FullName,
                        g.GradeValue
                    })
                    .ToList();

                dgvGrades.DataSource = gradeItems;
                HighlightLowGrades();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading grades: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HighlightLowGrades()
        {
            foreach (DataGridViewRow row in dgvGrades.Rows)
            {
                if (row.Cells["GradeValue"].Value == null) continue;
                if (!double.TryParse(row.Cells["GradeValue"].Value.ToString(), out double val)) continue;

                row.DefaultCellStyle.BackColor = (val < 6)
                    ? System.Drawing.Color.LightCoral
                    : System.Drawing.Color.White;
            }
        }

        private void cmbFilterSubject_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFilterSubject.SelectedValue is int subjectId)
            {
                int? filterId = (subjectId == 0) ? (int?)null : subjectId;
                LoadGrades(filterId);
            }
        }

        private void dgvGrades_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvGrades.SelectedRows.Count == 0) return;

            int selectedId = (int)dgvGrades.SelectedRows[0].Cells["Id"].Value;
            var grade = _context.Grades
                .Include(g => g.Student)
                .Include(g => g.Subject)
                .FirstOrDefault(g => g.Id == selectedId);
            if (grade == null) return;

            cmbGradeStudent.SelectedValue = grade.StudentId;
            cmbGradeSubject.SelectedValue = grade.SubjectId;
            txtGradeValue.Text = grade.GradeValue.ToString("F1");
        }

        private void btnAddGrade_Click(object sender, EventArgs e)
        {
            try
            {
                if (!double.TryParse(txtGradeValue.Text.Trim(), out double val) || val < 0 || val > 10)
                {
                    MessageBox.Show("Please enter a valid grade between 0 and 10.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var newGrade = new Grades
                {
                    StudentId = (int)cmbGradeStudent.SelectedValue,
                    SubjectId = (int)cmbGradeSubject.SelectedValue,
                    GradeValue = val
                };

                _context.Grades.Add(newGrade);
                _context.SaveChanges();

                LoadGrades((int?)cmbFilterSubject.SelectedValue);
                txtGradeValue.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding grade: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditGrade_Click(object sender, EventArgs e)
        {
            if (dgvGrades.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select a grade to edit.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int selectedId = (int)dgvGrades.SelectedRows[0].Cells["Id"].Value;
            var grade = _context.Grades.Find(selectedId);
            if (grade == null) return;

            try
            {
                if (!double.TryParse(txtGradeValue.Text.Trim(), out double val) || val < 0 || val > 10)
                {
                    MessageBox.Show("Please enter a valid grade between 0 and 10.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                grade.StudentId = (int)cmbGradeStudent.SelectedValue;
                grade.SubjectId = (int)cmbGradeSubject.SelectedValue;
                grade.GradeValue = val;
                _context.SaveChanges();

                LoadGrades((int?)cmbFilterSubject.SelectedValue);
                txtGradeValue.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error editing grade: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteGrade_Click(object sender, EventArgs e)
        {
            if (dgvGrades.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select a grade to delete.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int selectedId = (int)dgvGrades.SelectedRows[0].Cells["Id"].Value;
            var grade = _context.Grades.Find(selectedId);
            if (grade == null) return;

            var confirm = MessageBox.Show(
                "Are you sure you want to delete this grade?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            try
            {
                _context.Grades.Remove(grade);
                _context.SaveChanges();

                LoadGrades((int?)cmbFilterSubject.SelectedValue);
                txtGradeValue.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting grade: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCalculateAverage_Click(object sender, EventArgs e)
        {
            if (!(cmbAverageStudent.SelectedValue is int studentId))
            {
                MessageBox.Show("Select a student to calculate average.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var grades = _context.Grades
                .Where(g => g.StudentId == studentId)
                .Select(g => g.GradeValue)
                .ToList();

            if (!grades.Any())
            {
                MessageBox.Show("This student has no grades yet.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            double avg = grades.Average();
            MessageBox.Show($"Average grade for this student: {avg:F2}", "Average",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadStudentsPage();
            LoadTeachers();
            LoadSubjects();
            LoadComboBoxes();
            LoadGrades(null);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _context.Dispose();
            base.OnFormClosing(e);
        }
    }
}
