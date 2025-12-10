USE master;
GO

IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'StoreManagmentSystem')
BEGIN
    EXEC ('CREATE DATABASE [' + '[StoreManagmentSystem]' + ']');
    PRINT 'Database created successfully.';
END
ELSE
BEGIN
    PRINT 'Database already exists.';
END
GO

USE StoreManagmentSystem;
GO

PRINT '--- EXECUTING TABLE SCRIPTS ---';

:r .\Tables\Roles.sql
:r .\Tables\Statuses.sql
:r .\Tables\Types.sql
:r .\Tables\Users.sql
:r .\Tables\Products.sql

PRINT '--- EXECUTING SEED SCRIPTS ---';

:r .\Seed\InsertRoles.sql
:r .\Seed\InsertTypes.sql

PRINT '--- INSTALLATION COMPLETED SUCCESSFULLY ---';
