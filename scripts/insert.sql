INSERT INTO DeviceType (Name) VALUES
('PC'),
('Smartwatch'),
('Embedded'),
('Monitor'),
('Printer');

INSERT INTO Position (Name, MinExpYears) VALUES
('Software Engineer', 2),
('System Administrator', 3),
('Project Manager', 5),
('IT Support', 1),
('HR Manager', 4);

INSERT INTO Person (PassportNumber, FirstName, MiddleName, LastName, PhoneNumber, Email) VALUES
('AA123456', 'John', 'Michael', 'Doe', '+1234567890', 'john.doe@example.com'),
('BB654321', 'Jane', NULL, 'Smith', '+1987654321', 'jane.smith@example.com'),
('CC987654', 'Alice', 'Marie', 'Johnson', '+1123456789', 'alice.johnson@example.com'),
('DD246810', 'Bob', NULL, 'Brown', '+1222333444', 'bob.brown@example.com'),
('EE135791', 'Eve', 'Grace', 'Davis', '+1333555777', 'eve.davis@example.com');

INSERT INTO Device (Name, IsEnabled, AdditionalProperties, DeviceTypeId) VALUES
                                                                             
('HP ProDesk 600 G6', 0, '{"operationSystem": null}', 1),

('ThinkCentre M75q Gen 5 Tiny', 1, '{"operationSystem": "Windows 11 Pro 23H2"}', 1),

('Apple Watch Ultra 2', 1, '{"battery": "24%"}', 2),

('Raspberry Pi 4', 0, '{"ipAddress": "192.168.0.1", "network": "example"}', 3),

('Dell UltraSharp', 1, '{"ports": [{"type": "HDMI", "version": "2.0"}]}', 4),

('HP LaserJet', 0, '{"colors": "black and white"}', 5);

INSERT INTO Employee (Salary, PositionId, PersonId, HireDate) VALUES
(75000.00, 1, 1, '2021-03-01'),
(65000.00, 2, 2, '2022-06-15'),
(90000.00, 3, 3, '2020-01-20'),
(55000.00, 4, 4, '2023-09-10'),
(80000.00, 5, 5, '2019-12-01');

INSERT INTO DeviceEmployee (DeviceId, EmployeeId, IssueDate, ReturnDate) VALUES
(1, 1, '2023-01-10', NULL),
(2, 2, '2023-02-20', '2024-01-15'),
(3, 3, '2022-08-05', NULL),
(4, 4, '2023-11-01', NULL),
(5, 5, '2021-05-25', '2023-05-25');
