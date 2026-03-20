using System;
using System.ServiceModel;
using System.Windows.Forms;
using StudentCRUD.Service;
using StudentCRUD.Service.Models;

namespace StudentCRUD.Client
{
    public partial class Form1 : Form
    {
        // WCF proxy - ChannelFactory ქმნის რეალურ WCF კავშირს სერვისთან
        private IStudentService _client;
        private ChannelFactory<IStudentService> _factory;

        // რედაქტირების რეჟიმი
        private int _editingStudentId = -1;

        public Form1()
        {
            InitializeComponent();

            // WCF ChannelFactory - App.config-ში მითითებული endpoint-ის გამოყენებით
            _factory = new ChannelFactory<IStudentService>("BasicHttpBinding_IStudentService");
            _client  = _factory.CreateChannel();
        }

        // ==================== FORM LOAD ====================
        private void Form1_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            LoadStudents();
            SetFormMode(isEditing: false);
        }

        // ==================== DataGridView კონფიგურაცია ====================
        private void SetupDataGridView()
        {
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AllowUserToAddRows = false;
        }

        // ==================== READ ====================
        private void LoadStudents()
        {
            try
            {
                var students = _client.GetAllStudents();
                dataGridView1.DataSource = students;

                if (dataGridView1.Columns.Count > 0)
                {
                    if (dataGridView1.Columns["Id"]        != null) dataGridView1.Columns["Id"].HeaderText        = "ID";
                    if (dataGridView1.Columns["FirstName"] != null) dataGridView1.Columns["FirstName"].HeaderText = "სახელი";
                    if (dataGridView1.Columns["LastName"]  != null) dataGridView1.Columns["LastName"].HeaderText  = "გვარი";
                    if (dataGridView1.Columns["Email"]     != null) dataGridView1.Columns["Email"].HeaderText     = "ელ-ფოსტა";
                    if (dataGridView1.Columns["Age"]       != null) dataGridView1.Columns["Age"].HeaderText       = "ასაკი";
                    if (dataGridView1.Columns["Major"]     != null) dataGridView1.Columns["Major"].HeaderText     = "სპეციალობა";
                }

                lblStatus.Text = $"სულ {students.Count} სტუდენტი";
            }
            catch (Exception ex)
            {
                MessageBox.Show("სერვისთან კავშირის შეცდომა:\n" + ex.Message,
                    "შეცდომა", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==================== CREATE ====================
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;
            try
            {
                bool success = _client.AddStudent(BuildStudentFromForm());
                if (success)
                {
                    MessageBox.Show("სტუდენტი წარმატებით დაემატა!", "შედეგი",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    LoadStudents();
                }
                else
                    MessageBox.Show("დამატება ვერ მოხერხდა.", "შეცდომა",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("შეცდომა: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==================== UPDATE ====================
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_editingStudentId < 0)
            {
                MessageBox.Show("გთხოვთ ჯერ შეარჩიოთ სტუდენტი სიიდან.",
                    "გაფრთხილება", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!ValidateInputs()) return;
            try
            {
                var student = BuildStudentFromForm();
                student.Id = _editingStudentId;
                bool success = _client.UpdateStudent(student);
                if (success)
                {
                    MessageBox.Show("სტუდენტი წარმატებით განახლდა!", "შედეგი",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    LoadStudents();
                    SetFormMode(isEditing: false);
                }
                else
                    MessageBox.Show("განახლება ვერ მოხერხდა.", "შეცდომა",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("შეცდომა: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==================== DELETE ====================
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("გთხოვთ ჯერ შეარჩიოთ სტუდენტი სიიდან.",
                    "გაფრთხილება", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string name = dataGridView1.SelectedRows[0].Cells["FirstName"].Value?.ToString()
                        + " " + dataGridView1.SelectedRows[0].Cells["LastName"].Value?.ToString();

            var confirm = MessageBox.Show(
                $"ნამდვილად გსურთ წაშალოთ სტუდენტი: {name}?",
                "წაშლის დადასტურება", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value);
                bool success = _client.DeleteStudent(id);
                if (success)
                {
                    MessageBox.Show("სტუდენტი წარმატებით წაიშალა!", "შედეგი",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    LoadStudents();
                    SetFormMode(isEditing: false);
                }
                else
                    MessageBox.Show("წაშლა ვერ მოხერხდა.", "შეცდომა",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("შეცდომა: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==================== DataGridView კლიკი ====================
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) return;
            var row = dataGridView1.SelectedRows[0];
            txtFirstName.Text = row.Cells["FirstName"].Value?.ToString() ?? "";
            txtLastName.Text  = row.Cells["LastName"].Value?.ToString()  ?? "";
            txtEmail.Text     = row.Cells["Email"].Value?.ToString()     ?? "";
            txtAge.Text       = row.Cells["Age"].Value?.ToString()       ?? "";
            txtMajor.Text     = row.Cells["Major"].Value?.ToString()     ?? "";
            _editingStudentId = Convert.ToInt32(row.Cells["Id"].Value);
            SetFormMode(isEditing: true);
        }

        // ==================== გასუფთავება ====================
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
            SetFormMode(isEditing: false);
        }

        // ==================== დამხმარე მეთოდები ====================
        private Student BuildStudentFromForm()
        {
            return new Student
            {
                FirstName = txtFirstName.Text.Trim(),
                LastName  = txtLastName.Text.Trim(),
                Email     = txtEmail.Text.Trim(),
                Age       = int.Parse(txtAge.Text.Trim()),
                Major     = txtMajor.Text.Trim()
            };
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            { MessageBox.Show("სახელი სავალდებულოა!"); txtFirstName.Focus(); return false; }
            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            { MessageBox.Show("გვარი სავალდებულოა!"); txtLastName.Focus(); return false; }
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            { MessageBox.Show("ელ-ფოსტა სავალდებულოა!"); txtEmail.Focus(); return false; }
            if (!int.TryParse(txtAge.Text, out int age) || age < 16 || age > 100)
            { MessageBox.Show("ასაკი უნდა იყოს 16-100 შორის!"); txtAge.Focus(); return false; }
            if (string.IsNullOrWhiteSpace(txtMajor.Text))
            { MessageBox.Show("სპეციალობა სავალდებულოა!"); txtMajor.Focus(); return false; }
            return true;
        }

        private void ClearForm()
        {
            txtFirstName.Text = "";
            txtLastName.Text  = "";
            txtEmail.Text     = "";
            txtAge.Text       = "";
            txtMajor.Text     = "";
            _editingStudentId = -1;
            dataGridView1.ClearSelection();
        }

        private void SetFormMode(bool isEditing)
        {
            btnAdd.Enabled    = !isEditing;
            btnUpdate.Enabled = isEditing;
            btnDelete.Enabled = isEditing;
            lblFormTitle.Text = isEditing ? "✏️ სტუდენტის რედაქტირება" : "➕ ახალი სტუდენტი";
        }

        // ფანჯრის დახურვისას WCF კავშირი სწორად ვხუროთ
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try   { ((IClientChannel)_client).Close(); _factory.Close(); }
            catch { ((IClientChannel)_client).Abort(); _factory.Abort(); }
        }
    }
}
