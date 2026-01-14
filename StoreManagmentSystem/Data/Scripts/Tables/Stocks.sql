SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

PRINT 'Processing Table: Stocks...';
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Stocks')
BEGIN
    CREATE TABLE [dbo].[Stocks](
        [StockId] [int] NOT NULL identity,
        [ProductId] [uniqueidentifier] NOT NULL,
        [PriceBought] [decimal](10, 2) NOT NULL,
        [QuantityRestocked] [decimal](10, 2) NOT NULL,
        [RestockDate] [date] NOT NULL,
        [ExpirationDate] [date] NOT NULL,
		CONSTRAINT [PK_Stocks] PRIMARY KEY CLUSTERED ([StockId] ASC)
    )
    PRINT ' -> Created table Stocks.';
END
ELSE PRINT ' -> Table Stocks already exists.';

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Products_Id')
BEGIN
    ALTER TABLE [dbo].[Stocks] WITH CHECK ADD CONSTRAINT [FK_Products_Id] 
    FOREIGN KEY([ProductId]) REFERENCES [dbo].[Products]([ProductId]);
    
    ALTER TABLE [dbo].[Stocks] CHECK CONSTRAINT [FK_Products_Id];
    PRINT ' -> Added FK_Products_Id.';
END
GO



