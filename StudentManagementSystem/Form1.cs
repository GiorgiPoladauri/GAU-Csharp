using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace StudentManagementSystem
{
    public partial class Form1 : Form
    {
        private string jsonFilePath = "DataFiles\\Data.json";
        private List<Student> students = new List<Student>();
        private int nextId = 1;

        public Form1()
        {
            InitializeComponent();
            LoadStudentsFromFile();
            dataGridViewStudents.DataSource = new BindingSource { DataSource = students };
        }

        private void LoadStudentsFromFile()
        {
            if (File.Exists(jsonFilePath))
            {
                var json = File.ReadAllText(jsonFilePath);
                students = JsonConvert.DeserializeObject<List<Student>>(json) ?? new List<Student>();
                if (students.Count > 0)
                {
                    nextId = students[^1].ID + 1;
                }
            }
        }

        private void SaveStudentsToFile()
        {
            var json = JsonConvert.SerializeObject(students, Formatting.Indented);
            File.WriteAllText(jsonFilePath, json);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var student = new Student
            {
                ID = nextId++,
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text,
                BirthDate = dateTimePickerBirthDate.Value,
                GPA = float.Parse(txtGPA.Text)
            };
            students.Add(student);
            RefreshGrid();
            SaveStudentsToFile();
            ClearFields();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridViewStudents.CurrentRow == null) return;
            var selectedStudent = (Student)dataGridViewStudents.CurrentRow.DataBoundItem;

            selectedStudent.FirstName = txtFirstName.Text;
            selectedStudent.LastName = txtLastName.Text;
            selectedStudent.BirthDate = dateTimePickerBirthDate.Value;
            selectedStudent.GPA = float.Parse(txtGPA.Text);

            RefreshGrid();
            SaveStudentsToFile();
            ClearFields();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewStudents.CurrentRow == null) return;
            var selectedStudent = (Student)dataGridViewStudents.CurrentRow.DataBoundItem;
            students.Remove(selectedStudent);

            RefreshGrid();
            SaveStudentsToFile();
        }

        private void RefreshGrid()
        {
            dataGridViewStudents.DataSource = null;
            dataGridViewStudents.DataSource = students;
        }

        private void ClearFields()
        {
            txtFirstName.Clear();
            txtLastName.Clear();
            txtGPA.Clear();
        }
    }
}
