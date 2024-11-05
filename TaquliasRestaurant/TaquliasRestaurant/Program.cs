using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaquliasRestaurant.Data;
using TaquliasRestaurant.Interface;
using TaquliasRestaurant.Models;
using TaquliasRestaurant.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddMemoryCache();
builder.Services.AddSession(option => 
{
    option.IdleTimeout = TimeSpan.FromMinutes(30);
});
//injections
builder.Services.AddScoped<IingredentInterface,IngredintsRepository>();
builder.Services.AddScoped<IproductInterface,ProductRepository>();
builder.Services.AddScoped<IcategoryInterface, CategoryRepository>();
builder.Services.AddScoped<IOrderInterface, OrderRepository>();





var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Ingredient}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
