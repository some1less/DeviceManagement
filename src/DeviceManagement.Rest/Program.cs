using DeviceManagement.DAL.Context;
using DeviceManagement.Services.DTO;
using DeviceManagement.Services.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DeviceDatabase");
builder.Services.AddDbContext<DevManagementContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddTransient<IEmployeeService, EmployeeService>();
builder.Services.AddTransient<IDeviceService, DeviceService>();

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

app.MapGet("api/devices", async (IDeviceService service) =>
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
});

app.Run();
