IF EXISTS (
    SELECT 1
    FROM sys.columns
    WHERE Name = 'QuantityInWarehouse'
)
BEGIN
    PRINT 'Column exists';
END

ELSE
BEGIN
PRINT 'Applying migration: add columns in Products...';
ALTER TABLE [dbo].[Products] add QuantityInWarehouse decimal(10,2);
END

GO

IF EXISTS (
    SELECT 1
    FROM sys.columns
    WHERE Name = 'QuantityInShelf'
)
BEGIN
    PRINT 'Column exists';
END

ELSE
BEGIN
PRINT 'Applying migration: add columns in Products...';
ALTER TABLE [dbo].[Products] add QuantityInShelf decimal(10,2);
END

GO
