using Microsoft.EntityFrameworkCore;
using DeviceManagementSystem.Data;
using DeviceManagementSystem.Models;
using DeviceManagementSystem.Services.Interfaces;

namespace DeviceManagementSystem.Services
{
    /// <summary>
    /// Service implementation for device category operations
    /// </summary>
    public class DeviceCategoryService : IDeviceCategoryService
    {
        private readonly ApplicationDbContext _context;

        public DeviceCategoryService(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<DeviceCategory>> GetAllCategoriesAsync()
        {
            return await _context.DeviceCategories
                .Include(c => c.Devices)
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        public async Task<DeviceCategory?> GetCategoryByIdAsync(int id)
        {
            return await _context.DeviceCategories
                .Include(c => c.Devices)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<DeviceCategory> CreateCategoryAsync(DeviceCategory category)
        {
            if (category == null)
                throw new ArgumentNullException(nameof(category));

            _context.DeviceCategories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<DeviceCategory> UpdateCategoryAsync(DeviceCategory category)
        {
            if (category == null)
                throw new ArgumentNullException(nameof(category));

            var existingCategory = await _context.DeviceCategories.FindAsync(category.Id);
            if (existingCategory == null)
                throw new InvalidOperationException($"Category with ID {category.Id} not found");

            existingCategory.Name = category.Name;
            existingCategory.Description = category.Description;

            await _context.SaveChangesAsync();
            return existingCategory;
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _context.DeviceCategories.FindAsync(id);
            if (category == null)
                throw new InvalidOperationException($"Category with ID {id} not found");

            // Check if category has associated devices
            var hasDevices = await _context.Devices.AnyAsync(d => d.DeviceCategoryId == id);
            if (hasDevices)
                throw new InvalidOperationException("Cannot delete category that has associated devices");

            _context.DeviceCategories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CategoryExistsAsync(int id)
        {
            return await _context.DeviceCategories.AnyAsync(c => c.Id == id);
        }
    }
} 