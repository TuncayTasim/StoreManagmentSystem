BEGIN
PRINT 'Applying migration: add columns in ShelfRestocks...';
ALTER TABLE [dbo].ShelfRestocks add ShelfId int;
END

GO
