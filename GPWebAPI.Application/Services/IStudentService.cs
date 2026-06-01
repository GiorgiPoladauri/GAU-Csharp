using GPWebAPI.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPWebAPI.Application.Services
{
    public interface IStudentService
    {
        List<StudentDTO> GetAllStudents();

        StudentDTO GetStudentById(int id);

        void AddStudent(CreateStudentDTO studentDto);

        void UpdateStudent(int id, CreateStudentDTO studentDto);

        void DeleteStudent(int id);
    }
}
