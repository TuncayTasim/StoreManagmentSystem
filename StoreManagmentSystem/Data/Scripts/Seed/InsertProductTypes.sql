PRINT 'Seeding Types...';

IF NOT EXISTS (SELECT 1 FROM [dbo].[Types] WHERE TypeId = 1)
    INSERT INTO [dbo].[Types] ([TypeId], [TypeName]) VALUES (1, 'Fruits');

IF NOT EXISTS (SELECT 1 FROM [dbo].[Types] WHERE TypeId = 2)
    INSERT INTO [dbo].[Types] ([TypeId], [TypeName]) VALUES (2, 'Vegetables');
GO