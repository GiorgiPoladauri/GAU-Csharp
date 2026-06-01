using GPWebAPI.Application.DTOs;
using GPWebAPI.Application.Services;
using GPWebAPI.Domain.Entities;

namespace GPWebAPI.Infrastructure.Services
{
    public class StudentService : IStudentService
    {
        private readonly AppDbContext _context;

        public StudentService(AppDbContext context)
        {
            _context = context;
        }

        public List<StudentDTO> GetAllStudents()
        {
            return _context.Students.Select(x => new StudentDTO
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Age = x.Age
            }).ToList();
        }

        public StudentDTO? GetStudentById(int id)
        {
            var student = _context.Students.FirstOrDefault(x => x.Id == id);

            if (student == null)
                return null;

            return new StudentDTO
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Age = student.Age
            };
        }

        public void AddStudent(CreateStudentDTO studentDto)
        {
            var student = new Student
            {
                FirstName = studentDto.FirstName,
                LastName = studentDto.LastName,
                Age = studentDto.Age
            };

            _context.Students.Add(student);
            _context.SaveChanges();
        }

        public void UpdateStudent(int id, CreateStudentDTO studentDto)
        {
            var student = _context.Students.FirstOrDefault(x => x.Id == id);

            if (student != null)
            {
                student.FirstName = studentDto.FirstName;
                student.LastName = studentDto.LastName;
                student.Age = studentDto.Age;
                _context.SaveChanges();
            }
        }

        public void DeleteStudent(int id)
        {
            var student = _context.Students.FirstOrDefault(x => x.Id == id);

            if (student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
            }
        }
    }
}