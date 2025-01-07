using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementApp
{
    public class Student
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName {  get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime DateAdded { get; set; }
        private float _GPA { get; set; }

        public Student(int id, string firstName, string lastName, DateTime birthDate, float gpa)
        {
            ID = id;
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            _GPA = gpa;
            DateAdded = DateTime.Now;
        }

        public float GPA
        {
            get { return _GPA; }
            set
            {
                if (value < 0.0f || value > 4.0f)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "GPA must be between 0.0 and 4.0.");
                }
                _GPA = value;
            }
        }

        public int AgeCounter()
        {
            var today = DateTime.Today;
            int age = today.Year - BirthDate.Year;
            if (today.Month < BirthDate.Month || (today.Month == BirthDate.Month && today.Day < BirthDate.Day))
            {
                age--;
            }
            return age;
        }

        public string FullName()
        {
            return $"{FirstName} {LastName}";
        }
    };
}

