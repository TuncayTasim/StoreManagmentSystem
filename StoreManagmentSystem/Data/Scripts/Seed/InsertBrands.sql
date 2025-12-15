PRINT 'Seeding Brands...';

IF NOT EXISTS (SELECT 1 FROM [dbo].[Brands] WHERE BrandId = 1)
    INSERT INTO [dbo].[Brands] ([BrandId], [BrandName]) VALUES (1, 'No brand');

GO


