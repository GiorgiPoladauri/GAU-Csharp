using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Reader
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PersonalNumber { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "ელ. ფოსტა არასწორი ფორმატისაა")] // პუნქტი 2
        public string Email { get; set; } = string.Empty;

        public DateTime RegistrationDate { get; set; }
        public Domain.Enums.ReaderStatus Status { get; set; }
    }
}