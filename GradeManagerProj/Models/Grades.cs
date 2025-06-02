using GradeManagerProj.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GradeManagerProj.Models
{
    public class Grades
    {
        public int Id { get; set; }

        [Required]
        public int StudentId { get; set; }
        public virtual Students Student { get; set; }

        [Required]
        public int SubjectId { get; set; }
        public virtual Subjects Subject { get; set; }

        [Required]
        public double GradeValue { get; set; }
    }
}
