using AspNetCoreWebApplication.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Entity Framework
builder.Services.AddDbContext<DatabaseContext>(o => o.UseSqlServer());  //o => o. t�r�ndeki yaz�m tarz�na "lambda expression" denir. 

// Oturum a�ma
builder.Services.AddAuthentication().AddCookie(o =>
{
    o.LoginPath = "/Admin/Login";
    o.Cookie.Name = "AdminLogin";  //Cookienin ismi AdminLogin olsun
});
// Projeye admin paneli eklemek i�in Projeye sa� t�k a��lan men�den Add > New Scaffolded Item diyerek a��lan pencereden Area y� se�ip Admin ismini verip ok diyerek alan� olu�turuyoruz. 

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

// Oturum a�may� aktif et. 
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(  // Bu alan� area k�sm�nda admin area y� kullanabilmek i�in ekledik. E�er ba�ka arealar eklersek yine bu alana eklemeliyiz. 
            name: "admin",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
          );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
