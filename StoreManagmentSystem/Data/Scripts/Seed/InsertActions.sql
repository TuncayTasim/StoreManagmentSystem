PRINT 'Seeding Actions...';

IF NOT EXISTS (SELECT 1 FROM [dbo].[Actions] WHERE ActionId = 1)
    INSERT INTO [dbo].[Actions] ([ActionId], [ActionType]) VALUES (1, 'RestockWarehouse');
    IF NOT EXISTS (SELECT 1 FROM [dbo].[Actions] WHERE ActionId = 2)
    INSERT INTO [dbo].[Actions] ([ActionId], [ActionType]) VALUES (2, 'RejectStock');
      IF NOT EXISTS (SELECT 1 FROM [dbo].[Actions] WHERE ActionId = 3)
    INSERT INTO [dbo].[Actions] ([ActionId], [ActionType]) VALUES (3, 'MoveToShelf');
      IF NOT EXISTS (SELECT 1 FROM [dbo].[Actions] WHERE ActionId = 4)
    INSERT INTO [dbo].[Actions] ([ActionId], [ActionType]) VALUES (4, 'RejectItem');
    IF NOT EXISTS (SELECT 1 FROM [dbo].[Actions] WHERE ActionId = 5)
    INSERT INTO [dbo].[Actions] ([ActionId], [ActionType]) VALUES (5, 'Sell');

GO


