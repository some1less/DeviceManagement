using DeviceManagement.DAL.Models;

namespace DeviceManagement.DAL.Repositories;

public interface IDeviceRepository
{
    Task<IEnumerable<Device>> GetAllDevicesAsync();
    Task<Device?> GetDeviceIdAsync(int deviceId);
    Task<Device> CreateDeviceAsync(Device device);
    Task UpdateDeviceAsync(Device device);
    Task DeleteDeviceAsync(int deviceId);
    
    Task<DeviceType?> GetDeviceName(string name);
}