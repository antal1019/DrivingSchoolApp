using System.ComponentModel.DataAnnotations;

namespace DrivingSchoolApp.Models
{
    public class Student
    {
        public Student()
        {
            Lessons = new HashSet<Lesson>();
        }

        public int ID { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(50, ErrorMessage = "First Name cannot be longer than 50 characters.")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(50, ErrorMessage = "Last Name cannot be longer than 50 characters.")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Invalid phone number format")]
        [RegularExpression(@"^0[0-9]{3}[-. ]?[0-9]{3}[-. ]?[0-9]{3}$", ErrorMessage = "Phone must be in the format '0722-123-123'.")] 
        public string Phone { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "Invalid email address format")]
        public string? Email { get; set; }

        public virtual ICollection<Lesson> Lessons { get; set; }
    }
}