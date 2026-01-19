SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

PRINT 'Processing Table: ShelfRestocks...';
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'ShelfRestocks')
BEGIN
    CREATE TABLE [dbo].[ShelfRestocks](
        [ShelfId] [int] NOT NULL identity,
        [ProductId] [uniqueidentifier] NOT NULL,
        [PriceSell] [decimal](10, 2) NOT NULL,
        [QuantityInShelf] [decimal](10, 2) NOT NULL,
        [RestockDate] [date] NOT NULL,
		CONSTRAINT [PK_ShelfRestocks] PRIMARY KEY CLUSTERED ([ShelfId] ASC)
    )
    PRINT ' -> Created table ShelfRestocks.';
END
ELSE PRINT ' -> Table ShelfRestocks already exists.';

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Products_Id')
BEGIN
    ALTER TABLE [dbo].[ShelfRestocks] WITH CHECK ADD CONSTRAINT [FK_Products_Id] 
    FOREIGN KEY([ProductId]) REFERENCES [dbo].[Products]([ProductId]);
    
    ALTER TABLE [dbo].[ShelfRestocks] CHECK CONSTRAINT [FK_Products_Id];
    PRINT ' -> Added FK_Products_Id.';
END
GO



