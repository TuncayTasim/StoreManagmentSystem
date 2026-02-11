PRINT 'Applying migration: drop columns in ShelfRestocks...';

ALTER TABLE [dbo].ShelfRestocks DROP COLUMN ProductId;
ALTER TABLE [dbo].ShelfRestocks DROP COLUMN QuantityRestocked;
ALTER TABLE [dbo].ShelfRestocks DROP COLUMN RestockDate;

GO