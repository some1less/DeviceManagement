using System.Diagnostics;
using System.Text.Json;
using DeviceManagement.DAL.Context;
using DeviceManagement.DAL.Models;
using DeviceManagement.Services.DTO;
using DeviceManagement.Services.DTO.DeviceTypes;
using Microsoft.EntityFrameworkCore;

namespace DeviceManagement.Services.Services;

public class DeviceService : IDeviceService
{

    private readonly DevManagementContext _context;

    
    public DeviceService(DevManagementContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<DeviceAllDTO>> GetAllDevicesAsync()
    {
        var devices = await _context.Devices.ToListAsync();
        
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

    public async Task<IEnumerable<GetAllDeviceTypesDTO>> GetAllDeviceTypesAsync()
    {
        var deviceTypes = await _context.DeviceTypes.ToListAsync();
        var deviceTypesDtos = new List<GetAllDeviceTypesDTO>();
        foreach (var type in deviceTypes)
        {
            deviceTypesDtos.Add(new GetAllDeviceTypesDTO()
            {
                Id = type.Id,
                Name = type.Name,
            });
        }
        
        return deviceTypesDtos;
    }

    public async Task<DeviceByIdDTO?> GetDeviceIdAsync(int deviceId)
    {
        var device = await _context.Devices
            .Include(x => x.DeviceType)
            .Include(emp => emp.DeviceEmployees)
            .ThenInclude(emp => emp.Employee)
            .ThenInclude(p => p.Person)
            .FirstOrDefaultAsync(e => e.Id == deviceId);
        
        if (device == null) return null;
        
        return new DeviceByIdDTO()
        {
            Name = device.Name,
            IsEnabled = device.IsEnabled,
            AdditionalProperties = JsonDocument.Parse(device.AdditionalProperties).RootElement,
            Type = device.DeviceType?.Name ?? "Unknown",
        };
    }

    public async Task<CreateDeviceResponseDTO> CreateDeviceAsync(CreateDeviceDTO deviceDto)
    {
        
        // check if user wants to create device with type that does not exist
        var deviceType = await _context.DeviceTypes.FirstOrDefaultAsync(x => x.Id == deviceDto.TypeId);
        if (deviceType == null) throw new KeyNotFoundException($"Device type with id={deviceDto.TypeId} not found");

        var device = new Device()
        {
            Name = deviceDto.DeviceName,
            IsEnabled = deviceDto.IsEnabled,
            AdditionalProperties = deviceDto.AdditionalProperties?.GetRawText() ?? string.Empty,
            DeviceTypeId = deviceType.Id,
        };
        
        _context.Devices.Add(device);
        await _context.SaveChangesAsync();

        Debug.Assert(device.DeviceType != null, "device.DeviceType != null");
        return new CreateDeviceResponseDTO()
        {
            Id = device.Id,
            DeviceName = device.Name,
            DeviceTypeName = device.DeviceType.Name,
            IsEnabled = device.IsEnabled,
            AdditionalProperties = JsonDocument.Parse(device.AdditionalProperties).RootElement
        };
    }

    public async Task UpdateDeviceAsync(int id, UpdateDeviceDTO deviceDto)
    {
        var device = await _context.Devices
            .Include(x => x.DeviceType)
            .Include(emp => emp.DeviceEmployees)
            .ThenInclude(emp => emp.Employee)
            .ThenInclude(p => p.Person)
            .FirstOrDefaultAsync(e => e.Id == id);
        if (device == null) throw new KeyNotFoundException($"Device with id {id} not found");

        var deviceType = await _context.DeviceTypes.FirstOrDefaultAsync(x => x.Id == deviceDto.TypeId);
        if (deviceType == null) throw new KeyNotFoundException($"Device type id={deviceDto.TypeId} not found");
        
        device.Name = deviceDto.DeviceName;
        device.IsEnabled = deviceDto.IsEnabled;
        device.AdditionalProperties = deviceDto.AdditionalProperties?.GetRawText() ?? string.Empty;
        device.DeviceTypeId = deviceType.Id;
        
        _context.Entry(device).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteDeviceAsync(int deviceId)
    {
        var device = await _context.Devices.FindAsync(deviceId);
        if (device != null)
        {
            _context.Devices.Remove(device);
            await _context.SaveChangesAsync();
        }
    }
}