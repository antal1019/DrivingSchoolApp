using System.ComponentModel.DataAnnotations;

namespace DrivingSchoolApp.Models
{
    public class Instructor
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression(@"^0[0-9]{3}[-. ]?[0-9]{3}[-. ]?[0-9]{3}$", ErrorMessage = "Phone must be in the format '0722-123-123'.")]
        public string Phone { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "Email must be valid")]
        public string? Email { get; set; }

        public ICollection<Lesson>? Lessons { get; set; }
    }
}
