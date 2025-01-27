using Microsoft.AspNetCore.Identity;

namespace DrivingSchoolApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public int? StudentId { get; set; }
        public virtual Student? Student { get; set; }
        public int? InstructorId { get; set; }
        public virtual Instructor? Instructor { get; set; }
    }
}