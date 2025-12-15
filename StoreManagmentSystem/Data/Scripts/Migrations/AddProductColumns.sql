IF EXISTS (
    SELECT 1
    FROM sys.columns
    WHERE Name = 'BrandId'
)
BEGIN
    PRINT 'Column exists';
END

ELSE
BEGIN
PRINT 'Applying migration: add columns in Products...';
ALTER TABLE [dbo].[Products] add BrandId int;
END

GO
