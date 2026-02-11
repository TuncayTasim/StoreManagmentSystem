PRINT 'Applying migration: drop columns in WarehouseRestocks...';

ALTER TABLE [dbo].WarehouseRestocks DROP COLUMN ProductId;
ALTER TABLE [dbo].WarehouseRestocks DROP COLUMN QuantityRestocked;
ALTER TABLE [dbo].WarehouseRestocks DROP COLUMN RestockDate;

GO