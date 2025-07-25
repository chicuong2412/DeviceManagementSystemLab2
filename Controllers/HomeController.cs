using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DeviceManagementSystem.Models;
using DeviceManagementSystem.Services.Interfaces;

namespace DeviceManagementSystem.Controllers
{
    /// <summary>
    /// Controller for the home page and dashboard
    /// </summary>
    public class HomeController : Controller
    {
        private readonly IDeviceService _deviceService;
        private readonly IDeviceCategoryService _categoryService;
        private readonly IUserService _userService;

        public HomeController(IDeviceService deviceService, IDeviceCategoryService categoryService, IUserService userService)
        {
            _deviceService = deviceService ?? throw new ArgumentNullException(nameof(deviceService));
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var devices = await _deviceService.GetAllDevicesAsync();
                var categories = await _categoryService.GetAllCategoriesAsync();
                var users = await _userService.GetAllUsersAsync();

                ViewBag.TotalDevices = devices.Count();
                ViewBag.TotalCategories = categories.Count();
                ViewBag.TotalUsers = users.Count();
                ViewBag.InUseDevices = devices.Count(d => d.Status == Models.DeviceStatus.InUse);
                ViewBag.BrokenDevices = devices.Count(d => d.Status == Models.DeviceStatus.Broken);
                ViewBag.MaintenanceDevices = devices.Count(d => d.Status == Models.DeviceStatus.UnderMaintenance);

                return View();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error loading dashboard: {ex.Message}";
                return View();
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
