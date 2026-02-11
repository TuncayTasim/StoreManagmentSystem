SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

PRINT 'Processing Table: Sales...';
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Sales')
BEGIN
    CREATE TABLE [dbo].[Sales](
        [SaleId] [int] NOT NULL identity,
        [ShelfId] int NOT NULL,
        [PriceSold] [decimal](10, 2) NOT NULL,
        [QuantitySold] [decimal](10, 2) NOT NULL
		CONSTRAINT [PK_Sales] PRIMARY KEY CLUSTERED ([SaleId] ASC)
    )
    PRINT ' -> Created table Sales.';
END
ELSE PRINT ' -> Table Sales already exists.';

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Sales_ShelfId')
BEGIN
    ALTER TABLE [dbo].[Sales] WITH CHECK ADD CONSTRAINT [FK_Sales_ShelfId]
FOREIGN KEY([ShelfId]) REFERENCES [dbo].[Shelf]([ShelfId]);
    
    ALTER TABLE [dbo].[Sales] CHECK CONSTRAINT [FK_Sales_ShelfId];
    PRINT ' -> Added FK_Sales_ShelfId.';
END
GO