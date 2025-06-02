// Models/Subject.cs
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

        // Subject დაკავშირებულია Teacher-თან (დოსტრინგი TeacherId-ზე და ნავიგაციურ თვისებაზე)
        [Required]
        public int TeacherId { get; set; }
        public virtual Teachers Teacher { get; set; }

        // ნავიგაციური თვისება Grades-თან
        public virtual ICollection<Grades> Grades { get; set; }
    }
}
