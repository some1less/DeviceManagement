using DeviceManagement.Services.DTO;

namespace DeviceManagement.Services.Services;

public interface IDeviceService
{
    Task<IEnumerable<DeviceAllDTO>> GetAllDevicesAsync();
    Task<DeviceByIdDTO?> GetDeviceIdAsync(int deviceId);
    Task<CreateDeviceResponseDTO> CreateDeviceAsync(CreateDeviceDTO device);
    Task UpdateDeviceAsync(int id, UpdateDeviceDTO device);
    Task DeleteDeviceAsync(int deviceId);
}