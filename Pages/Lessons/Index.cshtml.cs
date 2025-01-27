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
    public class IndexModel : PageModel
    {
        private readonly DrivingSchoolApp.Data.DrivingSchoolContext _context;

        public IndexModel(DrivingSchoolApp.Data.DrivingSchoolContext context)
        {
            _context = context;
        }

        public IList<Lesson> Lesson { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Lesson = await _context.Lessons
        .Include(l => l.Student)
        .Include(l => l.Instructor)
        .Include(l => l.Vehicle)
        .ToListAsync();
        }
    }
}
