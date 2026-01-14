PRINT 'Seeding Roles...';

IF NOT EXISTS (SELECT 1 FROM [dbo].[Roles] WHERE RoleId = 1)
    INSERT INTO [dbo].[Roles] ([RoleId], [RoleName]) VALUES (1, 'Admin');

IF NOT EXISTS (SELECT 1 FROM [dbo].[Roles] WHERE RoleId = 2)
    INSERT INTO [dbo].[Roles] ([RoleId], [RoleName]) VALUES (2, 'Salesman');

IF NOT EXISTS (SELECT 1 FROM [dbo].[Roles] WHERE RoleId = 3)
    INSERT INTO [dbo].[Roles] ([RoleId], [RoleName]) VALUES (3, 'InventoryManager');

IF NOT EXISTS (SELECT 1 FROM [dbo].[Roles] WHERE RoleId = 4)
    INSERT INTO [dbo].[Roles] ([RoleId], [RoleName]) VALUES (4, 'ShelfManager');
GO


