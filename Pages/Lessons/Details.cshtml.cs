using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DrivingSchoolApp.Data;
using DrivingSchoolApp.Models;

namespace DrivingSchoolApp.Pages.Lessons
{
    public class DetailsModel : PageModel
    {
        private readonly DrivingSchoolApp.Data.DrivingSchoolContext _context;

        public DetailsModel(DrivingSchoolApp.Data.DrivingSchoolContext context)
        {
            _context = context;
        }

        public Lesson Lesson { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lesson = await _context.Lessons.FirstOrDefaultAsync(m => m.ID == id);
            if (lesson == null)
            {
                return NotFound();
            }
            else
            {
                Lesson = lesson;
            }
            return Page();
        }
    }
}
