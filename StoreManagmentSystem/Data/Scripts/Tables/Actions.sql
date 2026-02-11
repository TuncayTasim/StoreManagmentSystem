SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

PRINT 'Processing Table: Actions...';
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Actions')
BEGIN
    CREATE TABLE [dbo].[Actions](
        [ActionId] [int] primary key NOT NULL,
        [ActionType] [nvarchar](500) NOT NULL,
    )
    PRINT ' -> Created table Actions.';
END
ELSE PRINT ' -> Table Actions already exists.';

GO