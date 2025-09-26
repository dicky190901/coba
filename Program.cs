using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using coba.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<bapendaContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("bapendaContext")
        ?? throw new InvalidOperationException("Connection string 'bapendaContext' not found.")));

// Tambah services untuk session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // waktu session 30 menit
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Tambahkan session middleware sebelum Authorization
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");



app.Run();
