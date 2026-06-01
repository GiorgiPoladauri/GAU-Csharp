using GPWebAPI.Application.DTOs;
using GPWebAPI.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace GPWebAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public IActionResult GetAllStudents()
        {
            return Ok(_studentService.GetAllStudents());
        }

        [HttpGet("{id}")]
        public IActionResult GetStudentById(int id)
        {
            var student = _studentService.GetStudentById(id);

            if (student == null)
                return NotFound();

            return Ok(student);
        }

        [HttpPost]
        public IActionResult AddStudent(CreateStudentDTO dto)
        {
            _studentService.AddStudent(dto);
            return Ok("Student Added");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStudent(int id, CreateStudentDTO dto)
        {
            _studentService.UpdateStudent(id, dto);
            return Ok("Student Updated");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            _studentService.DeleteStudent(id);
            return Ok("Student Deleted");
        }
    }
}