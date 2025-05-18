using DeviceManagement.DAL.Context;
using DeviceManagement.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DeviceManagement.DAL.Repositories;

public class DeviceRepository : IDeviceRepository
{
    
    private readonly DevManagementContext _context;

    public DeviceRepository(DevManagementContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Device>> GetAllDevicesAsync()
    {
        return await _context.Devices.ToListAsync();
    }

    public async Task<Device?> GetDeviceIdAsync(int deviceId)
    {
        
        return await _context.Devices
            .Include(x => x.DeviceType)
            .Include(emp => emp.DeviceEmployees)
            .ThenInclude(emp => emp.Employee)
            .ThenInclude(p => p.Person)
            .FirstOrDefaultAsync(e => e.Id == deviceId);
        
    }

    public async Task<Device> CreateDeviceAsync(Device device)
    {
        
        _context.Devices.Add(device);
        await _context.SaveChangesAsync();
        return device;
        
    }

    public Task UpdateDeviceAsync(Device device)
    {
        _context.Entry(device).State = EntityState.Modified;
        return _context.SaveChangesAsync();
    }

    public Task DeleteDeviceAsync(int deviceId)
    {
        throw new NotImplementedException();
    }

    public async Task<DeviceType?> GetDeviceName(string name)
    {
        return await _context.DeviceTypes.FirstOrDefaultAsync(x => x.Name == name);
    }
}