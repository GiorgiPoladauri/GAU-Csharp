using System;

namespace StudentManagementSystem
{
    public class Student
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }

        private float gpa;
        public float GPA
        {
            get { return gpa; }
            set
            {
                if (value < 0 || value > 4.3)
                {
                    throw new Exception("Yoo, something's wrong with your GPA, CHECK!");
                }
                gpa = value;
            }
        }

        public int CalculateAge()
        {
            int age = DateTime.Now.Year - BirthDate.Year;
            if (DateTime.Now < BirthDate.AddYears(age))
            {
                age--;
            }
            if (age < 0)
            {
                throw new Exception("Are you from the future?");
            }
            return age;
        }

        public string FullNameOfStudent()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
