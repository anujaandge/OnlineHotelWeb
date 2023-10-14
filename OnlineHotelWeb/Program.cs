using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineHotelBill;
using onlineHotelutility;
using System;

/*
 System.AggregateException: 'Some services are not able to be constructed
(Error while validating the service descriptor 'ServiceType: onlineHotelutility.
IDbInitializer Lifetime: Scoped ImplementationType: onlineHotelutility.DbInitializer':
Unable to resolve service for type 'Microsoft.AspNetCore.Identity.UserManager`1[OnlineHotelModels.ApplicationUser]' while attempting to activate 'onlineHotelutility.DbInitializer'.)'

 */

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContextConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddDefaultTokenProviders()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddScoped<IDbInitializer, DbInitializer>();
builder.Services.AddRazorPages();
// Add services to the container.
builder.Services.AddControllersWithViews();
//

//
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
app.UseAuthentication();;

app.UseAuthorization();
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
void DataSedding()
{
    using(var scope = app.Services.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        dbInitializer.Initialize();
    }
}
DataSedding();
