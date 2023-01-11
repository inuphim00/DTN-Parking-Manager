using DtnParkingSystem.Interface;
using DtnParkingSystem.Services;
using Google.Cloud.Firestore;
using System.Text.Json;
using TestParkingSystem.Interface;
using TestParkingSystem.Models;
using TestParkingSystem.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var firebaseJson = JsonSerializer.Serialize(new FirebaseSettings());
var firebaseSettings = new FirebaseSettings();

builder.Services.AddSingleton(_ => new ParkingSpaceDAO(
	new FirestoreDbBuilder
	{
		ProjectId = firebaseSettings.ProjectId,
		JsonCredentials = firebaseJson
	}.Build()
));


builder.Services.AddSingleton(_ => new OccupantsDAO(
	new FirestoreDbBuilder
	{
		ProjectId = firebaseSettings.ProjectId,
		JsonCredentials = firebaseJson
	}.Build()
));
builder.Services.AddScoped<IManageParkingSpaces, ManageParkingSpace>();
builder.Services.AddScoped<IGetParkingDetails, GetParkingDetails>();
builder.Services.AddScoped<IManageOccupants, ManageOccupants>();







builder.Services.AddScoped<ParkingDetails, ParkingDetails>();



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
