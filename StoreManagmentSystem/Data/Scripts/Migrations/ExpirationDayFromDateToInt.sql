USE [StoreManagmentSystem];
GO

BEGIN TRANSACTION;

ALTER TABLE [dbo].[Stocks]
ADD [Temp_ExpirationDate] INT NULL;
GO

UPDATE [dbo].[Stocks]
SET [Temp_ExpirationDate] = CAST(CONVERT(VARCHAR(8), ExpirationDate, 112) AS INT);
GO

ALTER TABLE [dbo].[Stocks]
ALTER COLUMN [Temp_ExpirationDate] INT NOT NULL;
GO

ALTER TABLE [dbo].[Stocks]
DROP COLUMN [ExpirationDate];
GO

EXEC sp_rename '[dbo].[Stocks].[Temp_ExpirationDate]', 'DaysToExpire', 'COLUMN';
GO

COMMIT TRANSACTION;