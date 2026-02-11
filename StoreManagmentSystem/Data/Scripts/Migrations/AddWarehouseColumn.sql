BEGIN
PRINT 'Applying migration: add columns in Warehouse...';
ALTER TABLE [dbo].Warehouse add CurrentQuantity decimal(10,2);
END

GO