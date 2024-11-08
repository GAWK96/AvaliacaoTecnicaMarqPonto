using Microsoft.EntityFrameworkCore;
using Prova.MarQ.API;
using Prova.MarQ.Infra;
using Prova.MarQ.Infra.Loader.Clocking_Loader;
using Prova.MarQ.Infra.Loader.Company_Loader;
using Prova.MarQ.Infra.Loader.Employee_Loader;
using Prova.MarQ.Infra.Service.Clocking_Service;
using Prova.MarQ.Infra.Service.Company_Service;
using Prova.MarQ.Infra.Service.Employee_Service;

var builder = WebApplication.CreateBuilder(args);

string dbPath = "D:\\MarqPonto\\prova-marq-backend\\Prova.MarQ\\Infrastructure\\Prova.MarQ.Infra\\provaMarqDB.db";
if (!File.Exists(dbPath))
{
    Console.WriteLine("Database file not found!");
}
else
{
    Console.WriteLine("Database file found and accessible.");
}
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ProvaMarqDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
Console.WriteLine($"Using connection string: {connectionString}");
builder.Services.AddDbContext<ProvaMarqDbContext>(options => options.UseSqlite(connectionString));
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<ICompanyLoader, CompanyLoader>();
builder.Services.AddScoped<IEmployeeLoader, EmployeeLoader>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IClockingLoader, ClockingLoader>();
builder.Services.AddScoped<IClockingService, ClockingService>();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
