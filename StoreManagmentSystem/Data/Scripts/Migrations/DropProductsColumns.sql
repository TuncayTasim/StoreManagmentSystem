PRINT 'Applying migration: drop columns in Products...';
ALTER TABLE [dbo].[Products] DROP COLUMN Price;
ALTER TABLE [dbo].[Products] DROP COLUMN Quantity;
ALTER TABLE [dbo].[Products] DROP COLUMN DateAdded;
ALTER TABLE [dbo].[Products] DROP COLUMN ExpirationDate;

GO