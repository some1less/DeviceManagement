
using System.Data;
using System.Text;
using System.Text.Json;
using DeviceManagement.Rest.Middleware.Helpers;

namespace DeviceManagement.Rest.Middleware;

public class Middleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<Middleware> _logger;

    public Middleware(RequestDelegate next, ILogger<Middleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path.StartsWithSegments("/api"))
        {
            _logger.LogInformation("Beginning to process request");

            var path = "jsons/rules.json";
            var content = await File.ReadAllTextAsync(path);
            var validations = JsonSerializer.Deserialize<Validation>(content);

            string types = "";
            StringBuilder sb = new StringBuilder();
            foreach (var validation in validations.Validations)
            {
                sb.Append(validation.Type + ", ");
                types = sb.ToString();
            }
            
            _logger.LogInformation($"Found {validations.Validations.Count} validations from json file for types: {types}");
            
                
                
            await _next(context);
        }
    }
}