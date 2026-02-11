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


