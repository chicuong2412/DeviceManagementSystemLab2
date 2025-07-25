-- Device Management System - Advanced Sample Data Script
-- This script provides comprehensive sample data for testing all features

-- =============================================
-- SAMPLE USERS (Employees)
-- =============================================
INSERT INTO Users (FullName, Email, PhoneNumber) VALUES
-- IT Department
('Alex Thompson', 'alex.thompson@company.com', '+1-555-0201'),
('Maria Rodriguez', 'maria.rodriguez@company.com', '+1-555-0202'),
('James Wilson', 'james.wilson@company.com', '+1-555-0203'),

-- Sales Department
('Rachel Green', 'rachel.green@company.com', '+1-555-0204'),
('Tom Anderson', 'tom.anderson@company.com', '+1-555-0205'),
('Lisa Park', 'lisa.park@company.com', '+1-555-0206'),

-- Marketing Department
('David Chen', 'david.chen@company.com', '+1-555-0207'),
('Sarah Williams', 'sarah.williams@company.com', '+1-555-0208'),
('Mike Johnson', 'mike.johnson@company.com', '+1-555-0209'),

-- HR Department
('Emma Davis', 'emma.davis@company.com', '+1-555-0210'),
('Chris Brown', 'chris.brown@company.com', '+1-555-0211'),
('Anna Lee', 'anna.lee@company.com', '+1-555-0212'),

-- Finance Department
('Kevin Martinez', 'kevin.martinez@company.com', '+1-555-0213'),
('Jennifer Taylor', 'jennifer.taylor@company.com', '+1-555-0214'),
('Ryan Garcia', 'ryan.garcia@company.com', '+1-555-0215');

-- =============================================
-- SAMPLE DEVICES BY CATEGORY
-- =============================================

-- Category 1: Computer (Laptops and Desktops)
INSERT INTO Devices (Name, DeviceCode, DeviceCategoryId, Status, DateOfEntry) VALUES
-- High-end laptops for executives
('MacBook Pro 16" M3 Max', 'EXEC001', 1, 0, '2024-01-05'),
('Dell XPS 15 9530', 'EXEC002', 1, 0, '2024-01-10'),
('HP Elite Dragonfly', 'EXEC003', 1, 0, '2024-01-15'),

-- Standard laptops for employees
('Dell Latitude 5540', 'LAP101', 1, 0, '2024-01-20'),
('HP EliteBook 840 G10', 'LAP102', 1, 0, '2024-01-25'),
('Lenovo ThinkPad T14', 'LAP103', 1, 0, '2024-02-01'),
('Dell Latitude 5520', 'LAP104', 1, 1, '2024-02-05'), -- Broken
('HP ProBook 450 G10', 'LAP105', 1, 0, '2024-02-10'),
('Lenovo ThinkPad E14', 'LAP106', 1, 2, '2024-02-15'), -- Under Maintenance
('MacBook Air M2', 'LAP107', 1, 0, '2024-02-20'),

-- Desktop computers
('Dell OptiPlex 7010', 'DESK101', 1, 0, '2024-01-08'),
('HP EliteDesk 800 G6', 'DESK102', 1, 0, '2024-01-12'),
('Lenovo ThinkCentre M90t', 'DESK103', 1, 0, '2024-01-18'),
('iMac 24" M3', 'DESK104', 1, 0, '2024-01-22'),
('Dell Precision 3660', 'DESK105', 1, 0, '2024-01-28'),
('HP Z2 Mini G9', 'DESK106', 1, 1, '2024-02-08'), -- Broken

-- Category 2: Printers
INSERT INTO Devices (Name, DeviceCode, DeviceCategoryId, Status, DateOfEntry) VALUES
-- Office printers
('HP LaserJet Pro M404n', 'PRN101', 2, 0, '2024-01-05'),
('Canon imageRUNNER 2630', 'PRN102', 2, 0, '2024-01-10'),
('Brother MFC-L8900CDW', 'PRN103', 2, 0, '2024-01-15'),
('Epson WorkForce WF-3720', 'PRN104', 2, 2, '2024-01-20'), -- Under Maintenance
('HP OfficeJet Pro 9015', 'PRN105', 2, 0, '2024-01-25'),
('Canon PIXMA TS8320', 'PRN106', 2, 1, '2024-02-01'), -- Broken
('Brother HL-L2350DW', 'PRN107', 2, 0, '2024-02-05'),
('HP LaserJet Pro M15w', 'PRN108', 2, 0, '2024-02-10'),

-- Category 3: Phones
INSERT INTO Devices (Name, DeviceCode, DeviceCategoryId, Status, DateOfEntry) VALUES
-- Mobile phones
('iPhone 15 Pro Max', 'PHN101', 3, 0, '2024-01-05'),
('Samsung Galaxy S24 Ultra', 'PHN102', 3, 0, '2024-01-10'),
('Google Pixel 8 Pro', 'PHN103', 3, 0, '2024-01-15'),
('iPhone 14', 'PHN104', 3, 1, '2024-01-20'), -- Broken
('Samsung Galaxy A54', 'PHN105', 3, 0, '2024-01-25'),
('Google Pixel 7a', 'PHN106', 3, 2, '2024-02-01'), -- Under Maintenance

-- IP Phones
('Cisco IP Phone 8845', 'IPPHN101', 3, 0, '2024-01-08'),
('Polycom VVX 411', 'IPPHN102', 3, 0, '2024-01-12'),
('Yealink T46S', 'IPPHN103', 3, 0, '2024-01-18'),
('Cisco IP Phone 8861', 'IPPHN104', 3, 0, '2024-01-22'),
('Polycom VVX 601', 'IPPHN105', 3, 1, '2024-01-28'), -- Broken

-- Category 4: Network Devices
INSERT INTO Devices (Name, DeviceCode, DeviceCategoryId, Status, DateOfEntry) VALUES
-- Switches
('Cisco Catalyst 2960-X', 'SW101', 4, 0, '2024-01-05'),
('HP ProCurve 2520-24G', 'SW102', 4, 0, '2024-01-10'),
('Ubiquiti UniFi Switch 24', 'SW103', 4, 0, '2024-01-15'),
('Netgear GS724T', 'SW104', 4, 1, '2024-01-20'), -- Broken
('Cisco Catalyst 3560', 'SW105', 4, 0, '2024-01-25'),

-- Routers
('Cisco RV340 Router', 'RT101', 4, 0, '2024-01-08'),
('TP-Link Archer C7', 'RT102', 4, 0, '2024-01-12'),
('Netgear Nighthawk R7000', 'RT103', 4, 0, '2024-01-18'),
('Ubiquiti EdgeRouter X', 'RT104', 4, 2, '2024-01-22'), -- Under Maintenance

-- Access Points
('Cisco Aironet 1815i', 'AP101', 4, 0, '2024-01-05'),
('Ubiquiti UniFi AP-AC-Pro', 'AP102', 4, 0, '2024-01-10'),
('TP-Link EAP225', 'AP103', 4, 0, '2024-01-15'),

-- Category 5: Other Devices
INSERT INTO Devices (Name, DeviceCode, DeviceCategoryId, Status, DateOfEntry) VALUES
-- Tablets
('iPad Pro 12.9" M2', 'TAB101', 5, 0, '2024-01-05'),
('Samsung Galaxy Tab S9 Ultra', 'TAB102', 5, 0, '2024-01-10'),
('Microsoft Surface Pro 9', 'TAB103', 5, 0, '2024-01-15'),
('iPad Air 5th Gen', 'TAB104', 5, 1, '2024-01-20'), -- Broken

-- Workstations
('Dell Precision 5560', 'WS101', 5, 0, '2024-01-08'),
('HP ZBook Studio G9', 'WS102', 5, 0, '2024-01-12'),
('Lenovo ThinkPad P16', 'WS103', 5, 0, '2024-01-18'),
('Mac Studio M2 Ultra', 'WS104', 5, 0, '2024-01-22'),

-- Peripherals
('Wacom Intuos Pro Large', 'TABLET101', 5, 0, '2024-01-05'),
('Logitech MX Master 3S', 'MOUSE101', 5, 0, '2024-01-10'),
('Apple Magic Keyboard', 'KB101', 5, 0, '2024-01-15'),
('Dell Ultrasharp U2723QE', 'MON101', 5, 0, '2024-01-20'),
('HP E27 G4 Monitor', 'MON102', 5, 2, '2024-01-25'), -- Under Maintenance

-- Wearables
('Apple Watch Series 9', 'WATCH101', 5, 0, '2024-01-08'),
('Samsung Galaxy Watch 6', 'WATCH102', 5, 0, '2024-01-12'),
('Fitbit Sense 2', 'WATCH103', 5, 1, '2024-01-18'); -- Broken

-- =============================================
-- VERIFICATION QUERIES
-- =============================================

-- Check total counts
-- SELECT 'Users' as TableName, COUNT(*) as Count FROM Users
-- UNION ALL
-- SELECT 'Devices' as TableName, COUNT(*) as Count FROM Devices;

-- Check devices by category
-- SELECT 
--     c.Name as Category,
--     COUNT(d.Id) as TotalDevices,
--     SUM(CASE WHEN d.Status = 0 THEN 1 ELSE 0 END) as InUse,
--     SUM(CASE WHEN d.Status = 1 THEN 1 ELSE 0 END) as Broken,
--     SUM(CASE WHEN d.Status = 2 THEN 1 ELSE 0 END) as UnderMaintenance
-- FROM DeviceCategories c 
-- LEFT JOIN Devices d ON c.Id = d.DeviceCategoryId 
-- GROUP BY c.Name, c.Id 
-- ORDER BY c.Id;

-- Check devices by status
-- SELECT 
--     CASE d.Status 
--         WHEN 0 THEN 'In Use'
--         WHEN 1 THEN 'Broken'
--         WHEN 2 THEN 'Under Maintenance'
--     END as Status,
--     COUNT(*) as Count
-- FROM Devices d
-- GROUP BY d.Status
-- ORDER BY d.Status;

-- Sample search queries to test functionality
-- SELECT * FROM Devices WHERE Name LIKE '%Dell%' OR DeviceCode LIKE '%LAP%';
-- SELECT * FROM Devices WHERE Status = 0 AND DeviceCategoryId = 1;
-- SELECT * FROM Devices WHERE DateOfEntry >= '2024-02-01'; 