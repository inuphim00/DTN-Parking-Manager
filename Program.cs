using DtnParkingSystem.Interface;
using DtnParkingSystem.Models;
using DtnParkingSystem.Services;
using Google.Cloud.Firestore;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var firebaseJson = JsonSerializer.Serialize(new FirebaseSettings());
var firebaseSettings = new FirebaseSettings();

builder.Services.AddScoped<IManageParkingSpaces, ManageParkingSpace>();
builder.Services.AddScoped<IGetParkingDetails, GetParkingDetails>();
builder.Services.AddScoped<IManageOccupants, ManageOccupants>();
builder.Services.AddTransient<IParkingDetails, ParkingDetails>();
builder.Services.AddScoped<IOccupantsDAO, OccupantsDAO>();
builder.Services.AddScoped<IParkingSpaceDAO, ParkingSpaceDAO>();

string path = AppDomain.CurrentDomain.BaseDirectory + @"dtnparkingmanager.json";
Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);


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
