SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

PRINT 'Processing Table: Types...';
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Types')
BEGIN
    CREATE TABLE [dbo].[Types](
        [TypeId] [int] NOT NULL,
        [TypeName] [nvarchar](500) NOT NULL,
        CONSTRAINT [PK_Types] PRIMARY KEY CLUSTERED ([TypeId] ASC)
    ) ON [PRIMARY];
    PRINT ' -> Created table Types.';
END
ELSE PRINT ' -> Table Types already exists.';
GO


