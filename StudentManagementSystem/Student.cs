using System;

namespace StudentManagementSystem
{
    public class Student
    {
        private string FirstName { get; set; }
        private int ID { get; set; }
        private string LastName { get; set; }
        private DateTime BirthDate { get; set; }
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
            int Age = DateTime.Now.Year - BirthDate.Year;
            if (DateTime.Now < BirthDate.AddYears(Age))
            {
                Age--;
            }

            if (Age < 0)
            {
                throw new Exception("Are you from the future?");
            }

            return Age;
        }

        public string FullNameOfStudent()
        {
            return FirstName + " " + LastName;
        }
    }
}
