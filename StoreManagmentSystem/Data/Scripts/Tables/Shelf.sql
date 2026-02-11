SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

PRINT 'Processing Table: Shelf...';
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Shelf')
BEGIN
    CREATE TABLE [dbo].[Shelf](
        [ShelfId] [int] NOT NULL identity,
        [ActionId] [int] NOT NULL,
        [ProductId] [uniqueidentifier] NOT NULL,
        [Quantity] [decimal](10, 2) NOT NULL,
        [ActionDate] [date] NOT NULL,
        [CurrentQuantity] [decimal](10, 2) NOT NULL,
		CONSTRAINT [PK_Shelf] PRIMARY KEY CLUSTERED ([ShelfId] ASC)
    )
    PRINT ' -> Created table Shelf.';
END
ELSE PRINT ' -> Table Shelf already exists.';

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_ActionId')
BEGIN
    ALTER TABLE [dbo].[Shelf] WITH CHECK ADD CONSTRAINT [FK_ActionId]
FOREIGN KEY([ActionId]) REFERENCES [dbo].[Actions]([ActionId]);
    
    ALTER TABLE [dbo].[Shelf] CHECK CONSTRAINT [FK_ActionId];
    PRINT ' -> Added FK_ActionId.';
END
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Shelf_ProductId')
BEGIN
    ALTER TABLE [dbo].[Shelf] WITH CHECK ADD CONSTRAINT [FK_Shelf_ProductId]
FOREIGN KEY([ProductId]) REFERENCES [dbo].[Products]([ProductId]);
    
    ALTER TABLE [dbo].[Shelf] CHECK CONSTRAINT [FK_Shelf_ProductId];
    PRINT ' -> Added FK_Shelf_ProductId.';
END
GO