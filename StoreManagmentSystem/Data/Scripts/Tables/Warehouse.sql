SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

PRINT 'Processing Table: Warehouse...';
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Warehouse')
BEGIN
    CREATE TABLE [dbo].[Warehouse](
        [WarehouseId] [int] NOT NULL identity,
        [ActionId] [int] NOT NULL,
        [ProductId] [uniqueidentifier] NOT NULL,
        [Quantity] [decimal](10, 2) NOT NULL,
        [ActionDate] [datetime] NOT NULL,
        [CurrentQuantity] [decimal](10, 2) NOT NULL,
		CONSTRAINT [PK_Warehouse] PRIMARY KEY CLUSTERED ([WarehouseId] ASC)
    )
    PRINT ' -> Created table Warehouse.';
END
ELSE PRINT ' -> Table Warehouse already exists.';

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Warehouse_ActionId')
BEGIN
    ALTER TABLE [dbo].[Warehouse] WITH CHECK ADD CONSTRAINT [FK_Warehouse_ActionId]
FOREIGN KEY([ActionId]) REFERENCES [dbo].[Actions]([ActionId]);
    
    ALTER TABLE [dbo].[Warehouse] CHECK CONSTRAINT [FK_Warehouse_ActionId];
    PRINT ' -> Added FK_Warehouse_ActionId.';
END
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Warehouse_ProductId')
BEGIN
    ALTER TABLE [dbo].[Warehouse] WITH CHECK ADD CONSTRAINT [FK_Warehouse_ProductId]
FOREIGN KEY([ProductId]) REFERENCES [dbo].[Products]([ProductId]);
    
    ALTER TABLE [dbo].[Warehouse] CHECK CONSTRAINT [FK_Warehouse_ProductId];
    PRINT ' -> Added FK_Warehouse_ProductId.';
END
GO