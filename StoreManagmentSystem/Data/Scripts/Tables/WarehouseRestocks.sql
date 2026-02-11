SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

PRINT 'Processing Table: WarehouseRestocks...';
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'WarehouseRestocks')
BEGIN
    CREATE TABLE [dbo].[WarehouseRestocks](
        [StockId] [int] NOT NULL identity,
        [PriceBought] [decimal](10, 2) NOT NULL,
        [DaysToExpire] [int] NOT NULL,
        [WarehouseId] [int] NOT NULL,
		CONSTRAINT [PK_WarehouseRestocks] PRIMARY KEY CLUSTERED ([StockId] ASC)
    )
    PRINT ' -> Created table WarehouseRestocks.';
END
ELSE PRINT ' -> Table WarehouseRestocks already exists.';

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_WarehouseRestocks_WarehouseId')
BEGIN
    ALTER TABLE [dbo].[WarehouseRestocks] WITH CHECK ADD CONSTRAINT [FK_WarehouseRestocks_WarehouseId]
FOREIGN KEY([WarehouseId]) REFERENCES [dbo].[Warehouse]([WarehouseId]);
    
    ALTER TABLE [dbo].[WarehouseRestocks] CHECK CONSTRAINT [FK_WarehouseRestocks_WarehouseId];
    PRINT ' -> Added FK_WarehouseRestocks_WarehouseId.';
END
GO



