
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MvcCore_employeeProject.Data;
using MvcCore_employeeProject.Repositories;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("con")));
builder.Services.AddIdentity<IdentityUser, IdentityRole>(op =>
{
    op.Password.RequiredLength = 5;
    op.Password.RequireNonAlphanumeric = false;
    op.Password.RequireDigit = true;
    op.SignIn.RequireConfirmedAccount = false;

}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.ConfigureApplicationCookie(op =>
{
    op.LoginPath = "/Account/Login";
});
builder.Services.AddControllersWithViews();
var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "login",
    pattern: "login",
    defaults: new { controller = "Account", action = "Login" });

app.MapControllerRoute(
    name: "register",
    pattern: "register",
    defaults: new { controller = "Account", action = "Register" });

app.MapControllerRoute(
    name: "logout",
    pattern: "logout",
    defaults: new { controller = "Account", action = "Logout" });


app.MapControllerRoute(
    name: "employee-index",
    pattern: "employees/list",
    defaults: new { controller = "Employees", action = "Index" });


app.MapControllerRoute(
    name: "employee-create",
    pattern: "employees/create",
    defaults: new { controller = "Employees", action = "CreatePartial" });


app.MapControllerRoute(
    name: "employee-edit",
    pattern: "employees/edit/{id}",
    defaults: new { controller = "Employees", action = "EditPartial" });


app.MapControllerRoute(
    name: "employee-delete",
    pattern: "employees/delete/{id}",
    defaults: new { controller = "Employees", action = "DeleteEmployee" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
