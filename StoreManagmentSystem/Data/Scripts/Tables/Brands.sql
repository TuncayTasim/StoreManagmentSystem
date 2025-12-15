SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

PRINT 'Processing Table: Brands...';
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Brands')
BEGIN
    CREATE TABLE [dbo].[Brands](
        [BrandId] int NOT NULL primary key,
        [BrandName] [nvarchar](500) NOT NULL
    )
    PRINT ' -> Created table Brands.';
END
ELSE PRINT ' -> Table Brands already exists.';

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Products_Brands')
BEGIN
    ALTER TABLE [dbo].[Products] WITH CHECK ADD CONSTRAINT [FK_Products_Brands] 
    FOREIGN KEY([BrandId]) REFERENCES [dbo].[Brands] ([BrandId]);
    
    ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Brands];
    PRINT ' -> Added FK_Products_Brands.';
END
GO


