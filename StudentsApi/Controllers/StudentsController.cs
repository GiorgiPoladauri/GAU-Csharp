using Microsoft.AspNetCore.Mvc;
using StudentsApi.Models;

namespace StudentsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        // in-memory database
        private static List<Student> _students = new()
        {
            new Student { Id = 1, FirstName = "Giorgi", LastName = "Lomidze", GroupId = 1 },
            new Student { Id = 2, FirstName = "Nino", LastName = "Gelashvili", GroupId = 1 },
            new Student { Id = 3, FirstName = "Luka", LastName = "Beridze", GroupId = 2 }
        };

        // GET /api/students
        [HttpGet]
        public ActionResult<List<Student>> GetAll()
        {
            return _students;
        }

        // GET /api/students/{id:int}
        [HttpGet("{id:int}")]
        public ActionResult<Student> GetById(int id)
        {
            var student = _students.FirstOrDefault(x => x.Id == id);
            if (student == null)
                return NotFound();

            return student;
        }

        // GET /api/students/group/{groupId:int}
        [HttpGet("group/{groupId:int}")]
        public ActionResult<List<Student>> GetByGroup(int groupId)
        {
            var result = _students.Where(x => x.GroupId == groupId).ToList();
            return result;
        }

        // GET /api/students/search?name=...
        [HttpGet("search")]
        public ActionResult<List<Student>> Search([FromQuery] string name)
        {
            var result = _students
                .Where(x =>
                    x.FirstName.Contains(name, StringComparison.OrdinalIgnoreCase) ||
                    x.LastName.Contains(name, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return result;
        }

        // POST /api/students
        [HttpPost]
        public ActionResult Create(Student student)
        {
            student.Id = _students.Any() ? _students.Max(x => x.Id) + 1 : 1;
            _students.Add(student);

            return Ok(student);
        }

        // PUT /api/students/{id:int}
        [HttpPut("{id:int}")]
        public ActionResult Update(int id, Student updatedStudent)
        {
            var student = _students.FirstOrDefault(x => x.Id == id);
            if (student == null)
                return NotFound();

            student.FirstName = updatedStudent.FirstName;
            student.LastName = updatedStudent.LastName;
            student.GroupId = updatedStudent.GroupId;

            return Ok(student);
        }

        // DELETE /api/students/{id:int}
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var student = _students.FirstOrDefault(x => x.Id == id);
            if (student == null)
                return NotFound();

            _students.Remove(student);
            return Ok();
        }
    }
}