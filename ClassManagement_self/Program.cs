using BAL.Implementation;
using BAL.Interface;
using DAO;
using DAO.Implementation;
using DAO.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Initialize AppServicesHelper with the environment
AppServicesHelper.Initialize(builder.Environment);

// Add services to the container
builder.Services.AddControllersWithViews()
    .AddRazorRuntimeCompilation();

// Add Razor Pages
builder.Services.AddRazorPages();

// Example of adding DB context (adjust to your setup)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Dependency injection for your services
builder.Services.AddScoped<IUserGroupRepo, UserGroupRepo>();
builder.Services.AddScoped<IUserGroupService, UserGroupService>();
builder.Services.AddScoped<ICourseGroupRepo, CourseGroupRepo>();
builder.Services.AddScoped<ICourseGroupService, CourseGroupService>();
builder.Services.AddScoped<ICourseInfoRepo, CourseInfoRepo>();
builder.Services.AddScoped<ICourseInfoService, CourseInfoService>();
builder.Services.AddScoped<IDashboardSliderRepo, DashboardSliderRepo>();
builder.Services.AddScoped<IDashboardSliderService, DashboardSliderService>();
builder.Services.AddScoped<INoticeInfoRepo, NoticeInfoRepo>();
builder.Services.AddScoped<INoticeInfoService, NoticeInfoService>();
builder.Services.AddScoped<ITrainerRepo, TrainerRepo>();
builder.Services.AddScoped<IStudentRepo, StudentRepo>();
builder.Services.AddScoped<ITrainerService, TrainerService>();
builder.Services.AddScoped<IStudentService, StudentService>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStaticFiles(); // Serve static files from wwwroot
app.UseRouting();

// Map Razor Pages routes
app.MapRazorPages();

// Map MVC routes (for controllers)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

// Define the AppServicesHelper class
public static class AppServicesHelper
{
    public static void Initialize(IHostEnvironment environment)
    {
        // Example initialization logic based on environment
        if (environment.IsDevelopment())
        {
            // Set up development-specific services or configurations
            Console.WriteLine("Development environment detected.");
        }
        else
        {
            // Set up production or staging-specific services
            Console.WriteLine("Production/Staging environment detected.");
        }

        // You can also initialize services like logging or other configurations here
    }
}
