using DeviceManagementSystem.Models;

namespace DeviceManagementSystem.Services.Interfaces
{
    /// <summary>
    /// Service interface for device category operations
    /// </summary>
    public interface IDeviceCategoryService
    {
        Task<IEnumerable<DeviceCategory>> GetAllCategoriesAsync();
        Task<DeviceCategory?> GetCategoryByIdAsync(int id);
        Task<DeviceCategory> CreateCategoryAsync(DeviceCategory category);
        Task<DeviceCategory> UpdateCategoryAsync(DeviceCategory category);
        Task DeleteCategoryAsync(int id);
        Task<bool> CategoryExistsAsync(int id);
    }
} 