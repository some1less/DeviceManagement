using System.Text.Json;
using DeviceManagement.DAL.Models;
using DeviceManagement.DAL.Repositories;
using DeviceManagement.Services.DTO;

namespace DeviceManagement.Services.Services;

public class DeviceService : IDeviceService
{
    private readonly IDeviceRepository _deviceRepository;

    public DeviceService(IDeviceRepository deviceRepository)
    {
        _deviceRepository = deviceRepository;
    }
    
    public async Task<IEnumerable<DeviceAllDTO>> GetAllDevicesAsync()
    {
        var devices = await _deviceRepository.GetAllDevicesAsync();
        var devicesDto = new List<DeviceAllDTO>();
        // mapping :>
        foreach (var device in devices)
        {
            devicesDto.Add(new DeviceAllDTO()
            {
                Id = device.Id,
                Name = device.Name,
            });
        }
        
        return devicesDto;
    }

    public async Task<DeviceByIdDTO?> GetDeviceIdAsync(int deviceId)
    {
        var device = await _deviceRepository.GetDeviceIdAsync(deviceId);
        if (device == null) return null;
        var curr = device.DeviceEmployees.FirstOrDefault(e => e.ReturnDate == null);

        return new DeviceByIdDTO()
        {
            DeviceTypeName = device.DeviceType?.Name ?? "Unknown",
            IsEnabled = device.IsEnabled,
            AdditionalProperties = JsonDocument.Parse(device.AdditionalProperties).RootElement,
            Employee = curr is null
                ? null
                : new EmployeeDTO()
                {
                    Id = curr.Employee.Id,
                    Name =
                        $"{curr.Employee.Person.FirstName} {curr.Employee.Person.MiddleName} {curr.Employee.Person.LastName}"
                }
        };
    }

    public async Task<CreateDeviceDTO> CreateDeviceAsync(CreateDeviceDTO deviceDto)
    {
        
        // check if user wants to create device with type that does not exist
        var deviceType = await _deviceRepository.GetDeviceName(deviceDto.DeviceTypeName);
        if (deviceType == null) throw new KeyNotFoundException($"Device type {deviceDto.DeviceTypeName} not found");

        var device = new Device()
        {
            Name = deviceDto.DeviceTypeName,
            IsEnabled = deviceDto.IsEnabled,
            AdditionalProperties = deviceDto.AdditionalProperties?.GetRawText() ?? string.Empty,
            DeviceTypeId = deviceType.Id,
        };
        
        var result = await _deviceRepository.CreateDeviceAsync(device);

        return new CreateDeviceDTO()
        {
            DeviceTypeName = deviceType.Name,
            IsEnabled = result.IsEnabled,
            AdditionalProperties = JsonDocument.Parse(result.AdditionalProperties).RootElement
        };
    }

    public async Task UpdateDeviceAsync(string id, UpdateDeviceDTO device)
    {
        
    }
}