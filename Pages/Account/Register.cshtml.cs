using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DrivingSchoolApp.Models;
using DrivingSchoolApp.Models.ViewModels;
using DrivingSchoolApp.Data;

namespace DrivingSchoolApp.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly DrivingSchoolContext _context;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            DrivingSchoolContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        [BindProperty]
        public RegisterViewModel RegisterInput { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = RegisterInput.Email,
                    Email = RegisterInput.Email,
                    FirstName = RegisterInput.FirstName,
                    LastName = RegisterInput.LastName,
                    Phone = RegisterInput.Phone
                };

                var result = await _userManager.CreateAsync(user, RegisterInput.Password);

                if (result.Succeeded)
                {
                    // Ad?ug?m utilizatorul în rolul selectat
                    await _userManager.AddToRoleAsync(user, RegisterInput.Role);

                    // Cre?m ?i entitatea corespunz?toare (Student sau Instructor)
                    if (RegisterInput.Role == "Student")
                    {
                        var student = new Student
                        {
                            FirstName = RegisterInput.FirstName,
                            LastName = RegisterInput.LastName,
                            Email = RegisterInput.Email,
                            Phone = RegisterInput.Phone
                        };
                        _context.Students.Add(student);
                        await _context.SaveChangesAsync();

                        user.StudentId = student.ID;
                        await _userManager.UpdateAsync(user);
                    }
                    else if (RegisterInput.Role == "Instructor")
                    {
                        var instructor = new Instructor
                        {
                            FirstName = RegisterInput.FirstName,
                            LastName = RegisterInput.LastName,
                            Email = RegisterInput.Email,
                            Phone = RegisterInput.Phone
                        };
                        _context.Instructors.Add(instructor);
                        await _context.SaveChangesAsync();

                        user.InstructorId = instructor.ID;
                        await _userManager.UpdateAsync(user);
                    }

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToPage("/Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return Page();
        }
    }
}