using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DrivingSchoolApp.Data;
using DrivingSchoolApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace DrivingSchoolApp.Pages.Lessons
{
    [Authorize(Roles = "Instructor")]
    public class EditModel : PageModel
    {
        private readonly DrivingSchoolApp.Data.DrivingSchoolContext _context;

        public EditModel(DrivingSchoolApp.Data.DrivingSchoolContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Lesson Lesson { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Fetch the lesson based on the provided ID
            var lesson = await _context.Lessons.FirstOrDefaultAsync(m => m.ID == id);
            if (lesson == null)
            {
                return NotFound();
            }

            Lesson = lesson;

            // Populate dropdown lists for related entities
            ViewData["InstructorID"] = new SelectList(_context.Instructors, "ID", "FirstName", Lesson.InstructorID);
            ViewData["StudentID"] = new SelectList(_context.Students, "ID", "FirstName", Lesson.StudentID);
            ViewData["VehicleID"] = new SelectList(_context.Vehicles, "ID", "LicensePlate", Lesson.VehicleID);

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Repopulate dropdown lists if validation fails
                ViewData["InstructorID"] = new SelectList(_context.Instructors, "ID", "FirstName", Lesson.InstructorID);
                ViewData["StudentID"] = new SelectList(_context.Students, "ID", "FirstName", Lesson.StudentID);
                ViewData["VehicleID"] = new SelectList(_context.Vehicles, "ID", "LicensePlate", Lesson.VehicleID);

                return Page();
            }

            _context.Attach(Lesson).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LessonExists(Lesson.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool LessonExists(int id)
        {
            return _context.Lessons.Any(e => e.ID == id);
        }
    }
}
