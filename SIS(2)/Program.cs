using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<SIS_2_.Data.StudentDataContext>(
    o => o.UseNpgsql(builder.Configuration.GetConnectionString("StudentDb")));

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");


app.MapControllerRoute(
        name: "Create",
        pattern: "{controller=Student}/{action=Create}/{id?}");


app.MapControllerRoute(
        name: "Index",
        pattern: "{controller=Student}/{action=List}/{id?}");


app.MapControllerRoute(
        name: "List2",
        pattern: "{controller=Student}/{action=List2}/{id?}");



app.Run();
