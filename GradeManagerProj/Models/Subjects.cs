using GradeManagerProj.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GradeManagerProj.Models
{
    public class Subjects
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int TeacherId { get; set; }
        public virtual Teachers Teacher { get; set; }

        public virtual ICollection<Grades> Grades { get; set; }
    }
}
