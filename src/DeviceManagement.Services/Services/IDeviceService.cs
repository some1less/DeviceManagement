using DeviceManagement.Services.DTO;
using DeviceManagement.Services.DTO.DeviceTypes;

namespace DeviceManagement.Services.Services;

public interface IDeviceService
{
    Task<IEnumerable<DeviceAllDTO>> GetAllDevicesAsync();
    Task<IEnumerable<GetAllDeviceTypesDTO>> GetAllDeviceTypesAsync();

    Task<DeviceByIdDTO?> GetDeviceIdAsync(int deviceId);
    Task<CreateDeviceResponseDTO> CreateDeviceAsync(CreateDeviceDTO device);
    Task UpdateDeviceAsync(int id, UpdateDeviceDTO device);
    Task DeleteDeviceAsync(int deviceId);
}