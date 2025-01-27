using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DrivingSchoolApp.Data;
using DrivingSchoolApp.Models;
using Microsoft.AspNetCore.Authorization;


namespace DrivingSchoolApp.Pages.Lessons
{
    [Authorize(Roles = "Instructor")]

    public class CreateModel : PageModel
    {
        private readonly DrivingSchoolApp.Data.DrivingSchoolContext _context;

        public CreateModel(DrivingSchoolApp.Data.DrivingSchoolContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["InstructorID"] = new SelectList(_context.Instructors, "ID", "FirstName");
        ViewData["StudentID"] = new SelectList(_context.Students, "ID", "FirstName");
        ViewData["VehicleID"] = new SelectList(_context.Vehicles, "ID", "LicensePlate");
            return Page();
        }

        [BindProperty]
        public Lesson Lesson { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Lessons.Add(Lesson);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
