using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VillaProject.Domain.Entities;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDBContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("VillaConnectionString"))
);
builder.Services.AddIdentity<ApplicationUser,IdentityRole>(options=>{
    options.Password.RequiredLength = 8;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireDigit = true;
})
.AddEntityFrameworkStores<ApplicationDBContext>().AddDefaultTokenProviders();
builder.Services.AddScoped<IUnitOfWork , UnitOfWork>();

//configuration the password 
// builder.Services.Configure<IdentityOptions>(options=>{
//  options.Password.RequiredLength = 12;
// });


/*
applicatons know the path of access denied and login from cookie
builder.Services.ConfigureApplicationCookie(options=>{
    options.AccessDeniedPath ="";
    options.LoginPath ="";
});
*/
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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
