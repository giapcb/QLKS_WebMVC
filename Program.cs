using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QLKS_WebMVC.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<QLKS_WebMVCContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("QLKS_WebMVCContext") ?? throw new InvalidOperationException("Connection string 'QLKS_WebMVCContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
