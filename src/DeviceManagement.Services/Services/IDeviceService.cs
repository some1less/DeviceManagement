using DeviceManagement.Services.DTO;

namespace DeviceManagement.Services.Services;

public interface IDeviceService
{
    Task<IEnumerable<DeviceAllDTO>> GetAllDevicesAsync();
    Task<DeviceByIdDTO?> GetDeviceIdAsync(int deviceId);
    Task<CreateDeviceDTO> CreateDeviceAsync(CreateDeviceDTO device);
    Task UpdateDeviceAsync(string id, UpdateDeviceDTO device);
    // Task DeleteDeviceAsync(int deviceId);
}