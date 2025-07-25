using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DeviceManagementSystem.Models;
using DeviceManagementSystem.Services.Interfaces;

namespace DeviceManagementSystem.Controllers
{
    /// <summary>
    /// Controller for managing devices
    /// </summary>
    public class DevicesController : Controller
    {
        private readonly IDeviceService _deviceService;
        private readonly IDeviceCategoryService _categoryService;

        public DevicesController(IDeviceService deviceService, IDeviceCategoryService categoryService)
        {
            _deviceService = deviceService ?? throw new ArgumentNullException(nameof(deviceService));
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
        }

        // GET: Devices
        public async Task<IActionResult> Index(string searchTerm, int? categoryFilter, DeviceStatus? statusFilter)
        {
            ViewBag.SearchTerm = searchTerm;
            ViewBag.CategoryFilter = categoryFilter;
            ViewBag.StatusFilter = statusFilter;

            // Populate filter dropdowns
            await PopulateFilterDropdowns();

            IEnumerable<Device> devices;

            // Apply filters
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                devices = await _deviceService.SearchDevicesAsync(searchTerm);
            }
            else if (categoryFilter.HasValue)
            {
                devices = await _deviceService.FilterDevicesByCategoryAsync(categoryFilter.Value);
            }
            else if (statusFilter.HasValue)
            {
                devices = await _deviceService.FilterDevicesByStatusAsync(statusFilter.Value);
            }
            else
            {
                devices = await _deviceService.GetAllDevicesAsync();
            }

            return View(devices);
        }

        // GET: Devices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var device = await _deviceService.GetDeviceByIdAsync(id.Value);
            if (device == null)
                return NotFound();

            return View(device);
        }

        // GET: Devices/Create
        public async Task<IActionResult> Create()
        {
            await PopulateCategoryDropdown();
            return View();
        }

        // POST: Devices/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,DeviceCode,DeviceCategoryId,Status,DateOfEntry")] Device device)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _deviceService.CreateDeviceAsync(device);
                    TempData["SuccessMessage"] = "Device created successfully.";
                    return RedirectToAction(nameof(Index));
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error creating device: {ex.Message}");
                }
            }

            await PopulateCategoryDropdown();
            return View(device);
        }

        // GET: Devices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var device = await _deviceService.GetDeviceByIdAsync(id.Value);
            if (device == null)
                return NotFound();

            await PopulateCategoryDropdown();
            return View(device);
        }

        // POST: Devices/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,DeviceCode,DeviceCategoryId,Status,DateOfEntry")] Device device)
        {
            if (id != device.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _deviceService.UpdateDeviceAsync(device);
                    TempData["SuccessMessage"] = "Device updated successfully.";
                    return RedirectToAction(nameof(Index));
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error updating device: {ex.Message}");
                }
            }

            await PopulateCategoryDropdown();
            return View(device);
        }

        // GET: Devices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var device = await _deviceService.GetDeviceByIdAsync(id.Value);
            if (device == null)
                return NotFound();

            return View(device);
        }

        // POST: Devices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _deviceService.DeleteDeviceAsync(id);
                TempData["SuccessMessage"] = "Device deleted successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error deleting device: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        private async Task PopulateCategoryDropdown()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            ViewBag.DeviceCategoryId = new SelectList(categories, "Id", "Name");
        }

        private async Task PopulateFilterDropdowns()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            
            var statuses = Enum.GetValues<DeviceStatus>()
                .Select(s => new { Id = (int)s, Name = s.GetType().GetField(s.ToString())?.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.DisplayAttribute), false).FirstOrDefault() is System.ComponentModel.DataAnnotations.DisplayAttribute attr ? attr.Name : s.ToString() });
            ViewBag.Statuses = new SelectList(statuses, "Id", "Name");
        }
    }
} 