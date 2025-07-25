using System.ComponentModel.DataAnnotations;

namespace DeviceManagementSystem.Models
{
    /// <summary>
    /// Represents a device category in the system
    /// </summary>
    public class DeviceCategory
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Category name is required")]
        [StringLength(100, ErrorMessage = "Category name cannot exceed 100 characters")]
        [Display(Name = "Category Name")]
        public string Name { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string? Description { get; set; }

        // Navigation property
        public virtual ICollection<Device> Devices { get; set; } = new List<Device>();
    }
} 