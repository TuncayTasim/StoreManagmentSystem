PRINT 'Seeding Status...';

IF NOT EXISTS (SELECT 1 FROM [dbo].[Status] WHERE StatusId = 1)
    INSERT INTO [dbo].[Status] ([StatusId], [StatusName]) VALUES (1, 'Inactive');

IF NOT EXISTS (SELECT 1 FROM [dbo].[Status] WHERE StatusId = 2)
    INSERT INTO [dbo].[Status] ([StatusId], [StatusName]) VALUES (2, 'Active');
GO


