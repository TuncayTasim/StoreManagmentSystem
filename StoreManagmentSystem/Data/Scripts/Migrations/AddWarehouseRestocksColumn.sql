BEGIN
PRINT 'Applying migration: add columns in WarehouseRestocks...';
ALTER TABLE [dbo].WarehouseRestocks add WarehouseId int;
END

GO
