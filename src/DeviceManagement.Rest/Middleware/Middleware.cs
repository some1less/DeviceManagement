
using System.Data;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using DeviceManagement.DAL.Context;
using DeviceManagement.Rest.Middleware.Helpers;
using DeviceManagement.Services.DTO;
using Microsoft.EntityFrameworkCore;

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
        var isPostOrPut   = context.Request.Method == HttpMethods.Post 
                            || context.Request.Method == HttpMethods.Put;
        var isDevicesRoute = context.Request.Path.StartsWithSegments("/api/devices");
        
        if (isPostOrPut && isDevicesRoute)
        {
            _logger.LogInformation("Middleware: Began processing request {Method} {Path}",
                context.Request.Method, context.Request.Path);
            
            var db = context.RequestServices.GetRequiredService<DevManagementContext>();

            var content = await File.ReadAllTextAsync("jsons/rules.json");
            var validations = JsonSerializer.Deserialize<Validation>(content);

            context.Request.EnableBuffering();
            string body;
            using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true))
            {
                body = await reader.ReadToEndAsync();
                context.Request.Body.Position = 0;
            }

            CreateDeviceDTO dto;
            try
            {
                dto = JsonSerializer.Deserialize<CreateDeviceDTO>(body,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Middleware: error parsing body: {Body}", body);
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Incorrect request format");
                return;
            }
            
            var type = await db.DeviceTypes
                .FirstOrDefaultAsync(t=>t.Id == dto.TypeId);
            if (type == null)
            {
                _logger.LogWarning("Middleware: DeviceType with type={typeId} not found",
                    dto.TypeId);
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await context.Response.WriteAsync($"DeviceType {dto.TypeId} not found");
                return;
            }
            
            
            var ruleItem = validations.Validations
                .FirstOrDefault(v=>v.Type.Equals(type.Name, StringComparison.OrdinalIgnoreCase));
            var errors = new List<string>();

            var activateValidation =
                ruleItem is not null
                && (
                    (ruleItem.PreRequestName == "isEnabled"
                     && dto.IsEnabled.ToString().Equals(ruleItem.PreRequestValue, StringComparison.OrdinalIgnoreCase))
                    || (dto.AdditionalProperties.HasValue 
                        && dto.AdditionalProperties.Value.TryGetProperty(ruleItem.PreRequestName, out var preProp) 
                        && preProp.GetRawText().Trim('"') == ruleItem.PreRequestValue)
                    );
            
            if (activateValidation)
            {
                foreach (var rule in ruleItem.Rules)
                {
                    if (!dto.AdditionalProperties.Value.TryGetProperty(rule.ParamName, out var ruleProp))
                    {
                        errors.Add($"{rule.ParamName} is missing");
                        continue;
                    }
                    
                    var val = ruleProp.GetRawText().Trim('"');

                    if (rule.Regex.ValueKind == JsonValueKind.String)
                    {
                        var rawPattern = rule.Regex.GetString()!;
                        var pattern = rawPattern.Trim('/');
                        
                        if (!Regex.IsMatch(val,pattern))
                        {
                            errors.Add($"{rule.ParamName} is differ from {pattern}");
                        }
                    } else if (rule.Regex.ValueKind == JsonValueKind.Array)
                    {
                        var allowed = rule.Regex
                            .EnumerateArray()
                            .Select(v => v.GetString())
                            .Where(v => v is not null)
                            .Cast<string>()
                            .ToList();
                        if (!allowed.Contains(val))
                        {
                            errors.Add($"{rule.ParamName} contains incorrect name: {val}");
                        }
                    }
                }
                
            }

            if (errors.Any())
            {
                _logger.LogWarning($"Middleware: validation failed for type {type.Name}: {string.Join("; ", errors)}");
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync(JsonSerializer.Serialize(new { Errors = errors }));
                return;
            }
            
            _logger.LogInformation("Middleware: validation was successful for type {Type}", type.Name);
            
        }
        else
        {
            _logger.LogInformation(
                "Middleware: skipping {Method} {Path}",
                context.Request.Method, context.Request.Path);
        }

        await _next(context);
    }
}