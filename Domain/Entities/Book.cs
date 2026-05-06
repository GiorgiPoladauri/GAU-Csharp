using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Book
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "სათაური სავალდებულოა")] // პუნქტი 1
        public string Title { get; set; } = string.Empty;

        public string ISBN { get; set; } = string.Empty;

        [Range(0, 2026, ErrorMessage = "გამოცემის წელი არ უნდა აღემატებოდეს მიმდინარე წელს")] // პუნქტი 1
        public int PublishYear { get; set; }

        public string Category { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "სულ რაოდენობა უნდა იყოს მინიმუმ 1")] // პუნქტი 1
        public int TotalQty { get; set; }

        public int AvailableQty { get; set; }
    }
}