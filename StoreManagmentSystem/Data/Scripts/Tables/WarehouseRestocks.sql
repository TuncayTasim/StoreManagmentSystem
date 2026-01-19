SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

PRINT 'Processing Table: WarehouseRestocks...';
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'WarehouseRestocks')
BEGIN
    CREATE TABLE [dbo].[WarehouseRestocks](
        [WarehouseId] [int] NOT NULL identity,
        [ProductId] [uniqueidentifier] NOT NULL,
        [PriceBought] [decimal](10, 2) NOT NULL,
        [QuantityRestocked] [decimal](10, 2) NOT NULL,
        [RestockDate] [date] NOT NULL,
        [DaysToExpire] [int] NOT NULL,
		CONSTRAINT [PK_WarehouseRestocks] PRIMARY KEY CLUSTERED ([WarehouseId] ASC)
    )
    PRINT ' -> Created table WarehouseRestocks.';
END
ELSE PRINT ' -> Table WarehouseRestocks already exists.';

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Products_Id')
BEGIN
    ALTER TABLE [dbo].[WarehouseRestocks] WITH CHECK ADD CONSTRAINT [FK_Products_Id] 
    FOREIGN KEY([ProductId]) REFERENCES [dbo].[Products]([ProductId]);
    
    ALTER TABLE [dbo].[WarehouseRestocks] CHECK CONSTRAINT [FK_Products_Id];
    PRINT ' -> Added FK_Products_Id.';
END
GO



