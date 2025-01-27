using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DrivingSchoolApp.Data;
using DrivingSchoolApp.Models;

namespace DrivingSchoolApp.Pages.Vehicles
{
    public class IndexModel : PageModel
    {
        private readonly DrivingSchoolContext _context;

        public IndexModel(DrivingSchoolContext context)
        {
            _context = context;
        }

        public IList<Vehicle> Vehicle { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchType { get; set; } // Proprietatea pentru tipul selectat

        public async Task OnGetAsync()
        {
            // Creează o interogare de bază pentru vehicule
            var query = _context.Vehicles.AsQueryable();

            // Aplică filtrul dacă SearchType nu este null sau gol
            if (!string.IsNullOrEmpty(SearchType))
            {
                query = query.Where(v => v.Type == SearchType);
            }

            // Execută interogarea și obține rezultatele
            Vehicle = await query.ToListAsync();
        }
    }
}
