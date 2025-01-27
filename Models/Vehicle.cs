using System.ComponentModel.DataAnnotations;

namespace DrivingSchoolApp.Models
{
    public class Vehicle
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Vehicle producer is required.")]
        [StringLength(50, ErrorMessage = "Producer cannot be longer than 50 characters.")]
        [Display(Name = "Producer")]
        public string Make { get; set; } = string.Empty; // Inițializare implicită

        [Required(ErrorMessage = "Vehicle model is required")]
        [StringLength(50, ErrorMessage = "Model cannot be longer than 50 characters")]
        [Display(Name = "Model")]
        public string Model { get; set; } = string.Empty; // Inițializare implicită

        [Required(ErrorMessage = "License plate is required")]
        [StringLength(20, ErrorMessage = "Licenste plate cannot be longer than 20 characters")]
        [Display(Name = "License Plate")]
        public string LicensePlate { get; set; } = string.Empty; // Inițializare implicită

        [StringLength(10, ErrorMessage = "Driver's license type cannot be longer than 10 characters")]
        [Display(Name = "Type")]
        public string? Type { get; set; } // Opțional

        // Relația: Un vehicul poate fi folosit în mai multe lecții
        [Display(Name = "Lessons")]
        public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>(); // Inițializare implicită pentru a preveni erorile
    }
}
