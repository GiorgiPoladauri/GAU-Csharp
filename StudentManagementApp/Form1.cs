namespace StudentManagementApp
{
    public partial class Form1 : Form
    {
        private CRUDoperations crudOperations;
        public Form1()
        {
            InitializeComponent();
            crudOperations = new CRUDoperations();
        }

        private void RefreshStudentList()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = crudOperations.GetStudents();
        }

        private void DataGridViewRefresh(List<Student> sortedStudents)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = sortedStudents;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if ((float.Parse(textBox3.Text) <= 4.0 && float.Parse(textBox3.Text) >= 0.0) && (textBox1.TextLength < 20 && textBox1.TextLength > 0))
                {
                    string firstName = textBox1.Text;
                    string lastName = textBox2.Text;
                    DateTime birthDate = dateTimePicker1.Value;
                    float gpa = float.Parse(textBox3.Text);

                    crudOperations.CreateStudent(firstName, lastName, birthDate, gpa);
                    RefreshStudentList();
                    MessageBox.Show("Student added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Check : Either your name or surname is too long or gpa is not in range...", "Something Went Wrong", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    int selectedRowIndex = dataGridView1.SelectedRows[0].Index;
                    int id = (int)dataGridView1.Rows[selectedRowIndex].Cells["ID"].Value;

                    crudOperations.DeleteStudent(id);
                    RefreshStudentList();

                    MessageBox.Show("Student deleted successfully!", "Success", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("Please select a student to delete.", "Error", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string selectedOption = comboBox1.SelectedItem.ToString();
                List<Student> sortedStudents = new List<Student>();

                switch (selectedOption)
                {
                    case "Sort By Date Added":
                        sortedStudents = crudOperations.GetStudents().OrderByDescending(s => s.DateAdded).ToList();
                        break;
                    case "Sort By GPA":
                        sortedStudents = crudOperations.GetStudents().OrderByDescending(s => s.GPA).ToList();
                        break;
                    case "Sort By Name":
                        sortedStudents = crudOperations.GetStudents().OrderBy(s => s.FirstName).ThenBy(s => s.LastName).ToList();
                        break;
                    default:
                        MessageBox.Show("Unknown sorting option selected.", "Error", MessageBoxButtons.OK);
                        return;
                }

                DataGridViewRefresh(sortedStudents);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK);
            }
        }
    }
}
