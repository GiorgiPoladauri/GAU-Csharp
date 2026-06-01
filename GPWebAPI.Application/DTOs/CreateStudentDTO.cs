namespace GPWebAPI.Application.DTOs
{
    public class CreateStudentDTO
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public int Age { get; set; }
    }
}