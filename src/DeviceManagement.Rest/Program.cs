var builder = WebApplication.CreateBuilder(args);

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

app.UseAuthorization();

app.MapGet("api/devices", () =>
{
    
});

app.MapGet("api/devices/{id}", () =>
{
    
});

app.MapPost("api/devices", () =>
{
    
});

app.MapPut("api/devices/{id}", () =>
{
    
});

app.MapDelete("api/devices/{id}", () =>
{
    
});

app.MapGet("api/employees", () =>
{
    
});

app.MapGet("api/employees/{id}", () =>
{
    
});

app.Run();
