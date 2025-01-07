using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace StudentManagementApp
{
    public class CRUDoperations
    {
        private List<Student> students;
        private int nextID;

        public CRUDoperations()
        {
            students = new List<Student>();
            LoadStudentsFromJson();
        }

        public List<Student> GetStudents()
        {
            return students;
        }


        public void CreateStudent(string firstName, string lastName, DateTime birthDate, float gpa)
        {
            int id = nextID++;
            DateTime dateAdded = DateTime.Now;

            Student newStudent = new Student(id, firstName, lastName, birthDate, gpa)
            {
                DateAdded = dateAdded 
            };

            students.Add(newStudent);
            SaveStudentsToJson();
        }


        public void DeleteStudent(int id)
        {
            Student student = students.Find(s => s.ID == id);

            if (student != null)
            {
                students.Remove(student);
                SaveStudentsToJson();
            }
            else
            {
                throw new Exception("Student with this ID does not exist!");
            }
        }

        private string GetFilePath()
        {
            string saveFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SaveFolder");
            Directory.CreateDirectory(saveFolder);
            return Path.Combine(saveFolder, "SaveFile.json");
        }

        public void SaveStudentsToJson()
        {
            string filePath = GetFilePath();
            string json = JsonSerializer.Serialize(students, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }

        public void LoadStudentsFromJson()
        {
            string filePath = GetFilePath();
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                students = JsonSerializer.Deserialize<List<Student>>(json) ?? new List<Student>();
                nextID = students.Any() ? students.Max(s => s.ID) + 1 : 0;
            }
            else
            {
                students = new List<Student>();
                nextID = 0;
            }
        }

    }
}
