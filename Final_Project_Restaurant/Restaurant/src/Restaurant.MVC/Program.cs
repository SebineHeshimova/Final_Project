using Microsoft.EntityFrameworkCore;
using Restaurant.Data.DAL;
using Restaurant.Data;
using Restaurant.Business;
using Restaurant.MVC.ViewService;
using Microsoft.AspNetCore.Identity;
using Restaurant.Core.Entiity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<RestaurantDbContext>(option => { option.UseSqlServer(builder.Configuration.GetConnectionString("default")); });
builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddScoped<LayoutService>();
builder.Services.AddIdentity<AppUser, IdentityRole>(option =>
{
    option.Password.RequiredLength = 8;
    option.Password.RequireUppercase = true;
    option.Password.RequireLowercase = true;
    option.Password.RequireDigit = true;
    option.Password.RequiredUniqueChars = 1;

    //  option.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<RestaurantDbContext>().AddDefaultTokenProviders();
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
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
			name: "areas",
			pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
		  );
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
