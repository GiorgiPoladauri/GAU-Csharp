using System;
using System.Collections.Generic;
using System.Linq;
using StudentCRUD.Service.Data;
using StudentCRUD.Service.Models;

namespace StudentCRUD.Service
{
    // StudentService = IStudentService-ის განხორციელება
    // აქ ვწერთ რეალურ ლოგიკას ბაზასთან
    public class StudentService : IStudentService
    {
        // ==================== READ ALL ====================
        public List<Student> GetAllStudents()
        {
            using (var db = new SchoolDbContext())
            {
                return db.Students.ToList();
            }
        }

        // ==================== READ ONE ====================
        public Student GetStudentById(int id)
        {
            using (var db = new SchoolDbContext())
            {
                return db.Students.Find(id);
            }
        }

        // ==================== CREATE ====================
        public bool AddStudent(Student student)
        {
            try
            {
                using (var db = new SchoolDbContext())
                {
                    db.Students.Add(student);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("AddStudent Error: " + ex.Message);
                return false;
            }
        }

        // ==================== UPDATE ====================
        public bool UpdateStudent(Student student)
        {
            try
            {
                using (var db = new SchoolDbContext())
                {
                    var existing = db.Students.Find(student.Id);
                    if (existing == null) return false;

                    existing.FirstName = student.FirstName;
                    existing.LastName  = student.LastName;
                    existing.Email     = student.Email;
                    existing.Age       = student.Age;
                    existing.Major     = student.Major;

                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("UpdateStudent Error: " + ex.Message);
                return false;
            }
        }

        // ==================== DELETE ====================
        public bool DeleteStudent(int id)
        {
            try
            {
                using (var db = new SchoolDbContext())
                {
                    var student = db.Students.Find(id);
                    if (student == null) return false;

                    db.Students.Remove(student);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("DeleteStudent Error: " + ex.Message);
                return false;
            }
        }
    }
}
