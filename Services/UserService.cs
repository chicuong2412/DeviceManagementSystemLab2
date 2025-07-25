using Microsoft.EntityFrameworkCore;
using DeviceManagementSystem.Data;
using DeviceManagementSystem.Models;
using DeviceManagementSystem.Services.Interfaces;

namespace DeviceManagementSystem.Services
{
    /// <summary>
    /// Service implementation for user operations
    /// </summary>
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users
                .OrderBy(u => u.FullName)
                .ToListAsync();
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.Users
                .Include(u => u.AssignedDevices)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> CreateUserAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            // Check if email already exists
            if (await EmailExistsAsync(user.Email))
                throw new InvalidOperationException($"Email '{user.Email}' already exists");

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var existingUser = await _context.Users.FindAsync(user.Id);
            if (existingUser == null)
                throw new InvalidOperationException($"User with ID {user.Id} not found");

            // Check if email already exists (excluding current user)
            if (await EmailExistsAsync(user.Email, user.Id))
                throw new InvalidOperationException($"Email '{user.Email}' already exists");

            existingUser.FullName = user.FullName;
            existingUser.Email = user.Email;
            existingUser.PhoneNumber = user.PhoneNumber;

            await _context.SaveChangesAsync();
            return existingUser;
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                throw new InvalidOperationException($"User with ID {id} not found");

            // Check if user has assigned devices
            var hasAssignedDevices = await _context.Devices.AnyAsync(d => d.AssignedUser != null && d.AssignedUser.Id == id);
            if (hasAssignedDevices)
                throw new InvalidOperationException("Cannot delete user that has assigned devices");

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UserExistsAsync(int id)
        {
            return await _context.Users.AnyAsync(u => u.Id == id);
        }

        public async Task<bool> EmailExistsAsync(string email, int? excludeId = null)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            var query = _context.Users.Where(u => u.Email == email);
            
            if (excludeId.HasValue)
                query = query.Where(u => u.Id != excludeId.Value);

            return await query.AnyAsync();
        }
    }
} 