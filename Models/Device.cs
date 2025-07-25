using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeviceManagementSystem.Models
{
    /// <summary>
    /// Represents a device in the system
    /// </summary>
    public class Device
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Device name is required")]
        [StringLength(200, ErrorMessage = "Device name cannot exceed 200 characters")]
        [Display(Name = "Device Name")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Device code is required")]
        [StringLength(50, ErrorMessage = "Device code cannot exceed 50 characters")]
        [Display(Name = "Device Code")]
        public string DeviceCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Device category is required")]
        [Display(Name = "Device Category")]
        public int DeviceCategoryId { get; set; }

        [Required(ErrorMessage = "Device status is required")]
        [Display(Name = "Status")]
        public DeviceStatus Status { get; set; }

        [Required(ErrorMessage = "Date of entry is required")]
        [Display(Name = "Date of Entry")]
        [DataType(DataType.Date)]
        public DateTime DateOfEntry { get; set; } = DateTime.Today;

        // Navigation properties
        public virtual DeviceCategory? DeviceCategory { get; set; }
        public virtual User? AssignedUser { get; set; }
    }

    /// <summary>
    /// Enum representing the possible device statuses
    /// </summary>
    public enum DeviceStatus
    {
        [Display(Name = "In Use")]
        InUse,
        
        [Display(Name = "Broken")]
        Broken,
        
        [Display(Name = "Under Maintenance")]
        UnderMaintenance
    }
} 