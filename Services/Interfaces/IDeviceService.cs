using DeviceManagementSystem.Models;

namespace DeviceManagementSystem.Services.Interfaces
{
    /// <summary>
    /// Service interface for device operations
    /// </summary>
    public interface IDeviceService
    {
        Task<IEnumerable<Device>> GetAllDevicesAsync();
        Task<IEnumerable<Device>> GetDevicesByCategoryAsync(int categoryId);
        Task<IEnumerable<Device>> SearchDevicesAsync(string searchTerm);
        Task<IEnumerable<Device>> FilterDevicesByStatusAsync(DeviceStatus status);
        Task<IEnumerable<Device>> FilterDevicesByCategoryAsync(int categoryId);
        Task<Device?> GetDeviceByIdAsync(int id);
        Task<Device> CreateDeviceAsync(Device device);
        Task<Device> UpdateDeviceAsync(Device device);
        Task DeleteDeviceAsync(int id);
        Task<bool> DeviceExistsAsync(int id);
        Task<bool> DeviceCodeExistsAsync(string deviceCode, int? excludeId = null);
    }
} 