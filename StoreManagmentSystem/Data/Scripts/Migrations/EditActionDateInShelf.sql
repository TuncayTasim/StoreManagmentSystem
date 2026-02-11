USE [StoreManagmentSystem];
GO

BEGIN TRANSACTION;

ALTER TABLE [dbo].[Shelf]
ADD [ActionDate] datetime NOT NULL 
CONSTRAINT [DF_Shelf_ActionDate] DEFAULT GETDATE();

ALTER TABLE [dbo].[Shelf]
DROP CONSTRAINT [DF_Shelf_ActionDate];

COMMIT TRANSACTION;