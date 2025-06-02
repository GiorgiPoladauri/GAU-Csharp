using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GradeManagerProj.Models
{
    public class Students
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        public virtual ICollection<Grades> Grades { get; set; }
    }
}
