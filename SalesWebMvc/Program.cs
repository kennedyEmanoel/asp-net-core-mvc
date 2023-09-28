using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System;

// consider this section as ConfigureServices()
var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddScoped<SeedingService>();
}

builder.Services.AddDbContext<SalesWebMvcContext>
    (options => options.UseMySql(
        "server=localhost;initial catalog=SALES_WEB_MVC;uid=root;pwd=1234567",
        Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.34-mysql")));

//builder.Services.AddTransient<SeedingService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

//consider this section as Configure()
var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{
//    app.UseDeveloperExceptionPage();
//}
//else
//{
//    app.UseExceptionHandler("/Home/Error");
//    app.UseHsts();
//}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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
