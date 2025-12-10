SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

PRINT 'Processing Table: Products...';
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Products')
BEGIN
    CREATE TABLE [dbo].[Products](
        [ProductId] [uniqueidentifier] NOT NULL,
        [Name] [nvarchar](500) NOT NULL,
        [TypeId] [int] NOT NULL,
        [Price] [decimal](10, 2) NOT NULL,
        [Quantity] [decimal](10, 2) NOT NULL,
        [DateAdded] [date] NOT NULL,
        [ExpirationDate] [nchar](10) NOT NULL,
        [QRCode] [nvarchar](500) NOT NULL,
        [Note] [ntext] NULL,
        CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED ([ProductId] ASC)
    ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];
    PRINT ' -> Created table Products.';
END
ELSE PRINT ' -> Table Products already exists.';

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Products_Types')
BEGIN
    ALTER TABLE [dbo].[Products] WITH CHECK ADD CONSTRAINT [FK_Products_Types] 
    FOREIGN KEY([TypeId]) REFERENCES [dbo].[Types] ([TypeId]);
    
    ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Types];
    PRINT ' -> Added FK_Products_Types.';
END
GO


