using System.Text;
using System.Text.Json.Serialization;
using DeviceManagement.DAL.Context;
using DeviceManagement.Rest.Middleware;
using DeviceManagement.Services.Helpers.Options;
using DeviceManagement.Services.Services;
using DeviceManagement.Services.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders().AddConsole().SetMinimumLevel(LogLevel.Debug);

var jwtConfig = builder.Configuration.GetSection("Jwt");

var connectionString = builder.Configuration.GetConnectionString("DeviceDatabase")
    ?? throw new Exception("DeviceDatabase connection string is not found");

builder.Services.AddDbContext<DevManagementContext>(o => o.UseSqlServer(connectionString));

builder.Services.Configure<JwtOptions>(jwtConfig);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtConfig["Issuer"],
            ValidAudience = jwtConfig["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig["Key"])),
            ClockSkew = TimeSpan.FromMinutes(10)
        };
    });
builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddTransient<IEmployeeService, EmployeeService>();
builder.Services.AddTransient<IDeviceService, DeviceService>();
builder.Services.AddControllers()
    
    .AddJsonOptions(opts => {
    opts.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    opts.JsonSerializerOptions.MaxDepth = 64;
    });

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<Middleware>();
app.MapControllers();

app.Run();
