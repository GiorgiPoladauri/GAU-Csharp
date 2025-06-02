using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GradeManagerProj.Models
{
    public class Teachers
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        public virtual ICollection<Subjects> Subjects { get; set; }
    }
}
