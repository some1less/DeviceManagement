using System.Text;
using DeviceManagement.DAL.Context;
using DeviceManagement.Services.DTO;
using DeviceManagement.Services.Helpers.Options;
using DeviceManagement.Services.Services;
using DeviceManagement.Services.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

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
builder.Services.AddControllers();

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

app.MapControllers();

/*app.MapGet("api/devices", async (IDeviceService service) =>
{
    try
    {
        var devices = await service.GetAllDevicesAsync();
        return Results.Ok(devices);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

app.MapGet("api/devices/{id}", async (IDeviceService service, int id) =>
{
    try
    {
        var device = await service.GetDeviceIdAsync(id);
        if (device == null) return Results.NotFound();
        return Results.Ok(device);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

app.MapPost("api/devices", async (IDeviceService service, CreateDeviceDTO deviceDto) =>
{
    try
    {
        var result = await service.CreateDeviceAsync(deviceDto);
        return Results.Created($"/api/devices", result);

    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

app.MapPut("api/devices/{id}", async (IDeviceService service, UpdateDeviceDTO deviceDto, int id) =>
{
    try
    {
        var device = await service.GetDeviceIdAsync(id);
        if (device == null) return Results.NotFound();

        await service.UpdateDeviceAsync(id, deviceDto);
        return Results.NoContent();

    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

app.MapDelete("api/devices/{id}", async (IDeviceService service, int id) =>
{
    try
    {
        var device = await service.GetDeviceIdAsync(id);
        if (device == null) return Results.NotFound();

        await service.DeleteDeviceAsync(id);
        return Results.NoContent();
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

app.MapGet("api/employees", async (IEmployeeService service) =>
{
    try
    {
        var employees = await service.GetAllEmployeesAsync();
        return Results.Ok(employees);

    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

app.MapGet("api/employees/{id}", async (IEmployeeService service, int id) =>
{
    try
    {
        var employee = await service.GetEmployeeIdAsync(id);
        if (employee == null) return Results.NotFound();
        return Results.Ok(employee);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});*/

app.Run();
