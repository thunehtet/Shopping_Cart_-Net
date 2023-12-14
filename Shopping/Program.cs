using EFDemo.Models;
using Login_CA.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shopping.Constants;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddMvc(config => config.Filters.Add(typeof(LoginFilter))).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

// Configure the database context.
ConfigureDbContext(builder.Services);

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

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Browser}/{action=Index}/{id?}");

// Initialize our database before our web application starts
InitDB(app.Services);

app.Run();

void ConfigureDbContext(IServiceCollection services)
{
    //string connectionString = CommonConstants.connectionString;
    
    services.AddDbContext<MyDbContext>(options => {
        options.UseLazyLoadingProxies().UseSqlServer(CommonConstants.connectionString);
    });
}

void InitDB(IServiceProvider serviceProvider)
{
    using var scope = serviceProvider.CreateScope();
    MyDbContext db = scope.ServiceProvider.GetRequiredService<MyDbContext>();

    // for our debugging, we just start off by removing our old 
    // database (if there is one).
    //db.Database.EnsureDeleted();

    // create a new database.
    db.Database.EnsureCreated();
}
