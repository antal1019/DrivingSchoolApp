using System.ComponentModel.DataAnnotations;

namespace DrivingSchoolApp.Models
{
    public class Lesson
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Date and time are required")]
        [Display(Name = "Date/Time")]
        public DateTime DateTime { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Student is required")]
        public int StudentID { get; set; }
        public virtual Student Student { get; set; } = null!;

        [Required(ErrorMessage = "Instructor is required")]
        public int InstructorID { get; set; }
        public virtual Instructor Instructor { get; set; } = null!;

        [Required(ErrorMessage = "Vehicle is required")]
        public int VehicleID { get; set; }
        public virtual Vehicle Vehicle { get; set; } = null!;
    }
}
