using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Prova.MarQ.Infra.Loader.Clocking_Loader;
using Prova.MarQ.Infra.Loader.Company_Loader;
using Prova.MarQ.Infra.Loader.Employee_Loader;
using Prova.MarQ.Infra.Loader.User_Loader;
using Prova.MarQ.Infra.Service.Clocking_Service;
using Prova.MarQ.Infra.Service.Company_Service;
using Prova.MarQ.Infra.Service.Employee_Service;
using Prova.MarQ.Infra.Service.User_Service;
using Prova.MarQ.Infra;

var builder = WebApplication.CreateBuilder(args);

// Database file check
string dbPath = "D:\\MarqPonto\\prova-marq-backend\\Prova.MarQ\\Infrastructure\\Prova.MarQ.Infra\\provaMarqDB.db";
if (!File.Exists(dbPath))
{
    Console.WriteLine("Database file not found!");
}
else
{
    Console.WriteLine("Database file found and accessible.");
}

// JWT settings
var key = Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]);
var issuer = builder.Configuration["JwtSettings:Issuer"];
var audience = builder.Configuration["JwtSettings:Audience"];

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: 'Bearer {token}'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});
builder.Services.AddSingleton<JwtTokenGenerator>(provider =>
{
    var key = builder.Configuration["JwtSettings:SecretKey"];
    var issuer = builder.Configuration["JwtSettings:Issuer"];
    var audience = builder.Configuration["JwtSettings:Audience"];
    return new JwtTokenGenerator(key, issuer, audience);
});
builder.Services.AddDbContext<ProvaMarqDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<ICompanyLoader, CompanyLoader>();
builder.Services.AddScoped<IEmployeeLoader, EmployeeLoader>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IClockingLoader, ClockingLoader>();
builder.Services.AddScoped<IClockingService, ClockingService>();
builder.Services.AddScoped<IUserLoader, UserLoader>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ProvaMarqDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = issuer,
        ValidAudience = audience,
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.DocumentTitle = "My API Documentation";
        c.EnableValidator();
        c.OAuthClientId("your-client-id");
        c.OAuthAppName("Swagger UI");
    });
}

app.UseHttpsRedirection();
app.UseAuthentication(); // Add authentication middleware
app.UseAuthorization();
app.MapControllers();

app.Run();
