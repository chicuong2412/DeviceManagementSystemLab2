using Microsoft.EntityFrameworkCore;
using DeviceManagementSystem.Data;
using DeviceManagementSystem.Models;
using DeviceManagementSystem.Services.Interfaces;

namespace DeviceManagementSystem.Services
{
    /// <summary>
    /// Service implementation for device operations
    /// </summary>
    public class DeviceService : IDeviceService
    {
        private readonly ApplicationDbContext _context;

        public DeviceService(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Device>> GetAllDevicesAsync()
        {
            return await _context.Devices
                .Include(d => d.DeviceCategory)
                .Include(d => d.AssignedUser)
                .OrderBy(d => d.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<Device>> GetDevicesByCategoryAsync(int categoryId)
        {
            return await _context.Devices
                .Include(d => d.DeviceCategory)
                .Include(d => d.AssignedUser)
                .Where(d => d.DeviceCategoryId == categoryId)
                .OrderBy(d => d.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<Device>> SearchDevicesAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return await GetAllDevicesAsync();

            var normalizedSearchTerm = searchTerm.ToLower().Trim();

            return await _context.Devices
                .Include(d => d.DeviceCategory)
                .Include(d => d.AssignedUser)
                .Where(d => d.Name.ToLower().Contains(normalizedSearchTerm) ||
                           d.DeviceCode.ToLower().Contains(normalizedSearchTerm))
                .OrderBy(d => d.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<Device>> FilterDevicesByStatusAsync(DeviceStatus status)
        {
            return await _context.Devices
                .Include(d => d.DeviceCategory)
                .Include(d => d.AssignedUser)
                .Where(d => d.Status == status)
                .OrderBy(d => d.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<Device>> FilterDevicesByCategoryAsync(int categoryId)
        {
            return await GetDevicesByCategoryAsync(categoryId);
        }

        public async Task<Device?> GetDeviceByIdAsync(int id)
        {
            return await _context.Devices
                .Include(d => d.DeviceCategory)
                .Include(d => d.AssignedUser)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<Device> CreateDeviceAsync(Device device)
        {
            if (device == null)
                throw new ArgumentNullException(nameof(device));

            // Check if device code already exists
            if (await DeviceCodeExistsAsync(device.DeviceCode))
                throw new InvalidOperationException($"Device code '{device.DeviceCode}' already exists");

            _context.Devices.Add(device);
            await _context.SaveChangesAsync();
            return device;
        }

        public async Task<Device> UpdateDeviceAsync(Device device)
        {
            if (device == null)
                throw new ArgumentNullException(nameof(device));

            var existingDevice = await _context.Devices.FindAsync(device.Id);
            if (existingDevice == null)
                throw new InvalidOperationException($"Device with ID {device.Id} not found");

            // Check if device code already exists (excluding current device)
            if (await DeviceCodeExistsAsync(device.DeviceCode, device.Id))
                throw new InvalidOperationException($"Device code '{device.DeviceCode}' already exists");

            existingDevice.Name = device.Name;
            existingDevice.DeviceCode = device.DeviceCode;
            existingDevice.DeviceCategoryId = device.DeviceCategoryId;
            existingDevice.Status = device.Status;
            existingDevice.DateOfEntry = device.DateOfEntry;

            await _context.SaveChangesAsync();
            return existingDevice;
        }

        public async Task DeleteDeviceAsync(int id)
        {
            var device = await _context.Devices.FindAsync(id);
            if (device == null)
                throw new InvalidOperationException($"Device with ID {id} not found");

            _context.Devices.Remove(device);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeviceExistsAsync(int id)
        {
            return await _context.Devices.AnyAsync(d => d.Id == id);
        }

        public async Task<bool> DeviceCodeExistsAsync(string deviceCode, int? excludeId = null)
        {
            if (string.IsNullOrWhiteSpace(deviceCode))
                return false;

            var query = _context.Devices.Where(d => d.DeviceCode == deviceCode);
            
            if (excludeId.HasValue)
                query = query.Where(d => d.Id != excludeId.Value);

            return await query.AnyAsync();
        }
    }
} 