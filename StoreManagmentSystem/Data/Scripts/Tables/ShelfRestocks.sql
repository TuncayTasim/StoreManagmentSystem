SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

PRINT 'Processing Table: ShelfRestocks...';
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'ShelfRestocks')
BEGIN
    CREATE TABLE [dbo].[ShelfRestocks](
        [ItemId] [int] NOT NULL identity,
        [PriceSell] [decimal](10, 2) NOT NULL,
        [ShelfId] [int] NOT NULL,
		CONSTRAINT [PK_ShelfRestocks] PRIMARY KEY CLUSTERED ([ItemId] ASC)
    )
    PRINT ' -> Created table ShelfRestocks.';
END
ELSE PRINT ' -> Table ShelfRestocks already exists.';

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_ShelfRestocks_ShelfId')
BEGIN
    ALTER TABLE [dbo].[ShelfRestocks] WITH CHECK ADD CONSTRAINT [FK_ShelfRestocks_ShelfId]
FOREIGN KEY([ShelfId]) REFERENCES [dbo].[Shelf]([ShelfId]);
    
    ALTER TABLE [dbo].[ShelfRestocks] CHECK CONSTRAINT [FK_ShelfRestocks_ShelfId];
    PRINT ' -> Added FK_ShelfRestocks_ShelfId.';
END
GO



