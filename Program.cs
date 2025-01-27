using DrivingSchoolApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using DrivingSchoolApp.Models;

var builder = WebApplication.CreateBuilder(args);

// Adaug? serviciile la container
builder.Services.AddDbContext<DrivingSchoolContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DrivingSchoolDb")));

// Configurare Identity
builder.Services.AddDefaultIdentity<ApplicationUser>(options => {
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 6;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<DrivingSchoolContext>();

builder.Services.AddRazorPages();

var app = builder.Build();

// Configurare pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
// Initializare roluri
await SeedRoles.Initialize(app.Services);
app.Run();