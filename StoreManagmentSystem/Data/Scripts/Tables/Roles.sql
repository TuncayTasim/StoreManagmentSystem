SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

PRINT 'Processing Table: Roles...';
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Roles')
BEGIN
    CREATE TABLE [dbo].[Roles](
        [RoleId] [int] NOT NULL,
        [RoleName] [nvarchar](500) NOT NULL,
        CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED ([RoleId] ASC)
    ) ON [PRIMARY];
    PRINT ' -> Created table Roles.';
END
ELSE PRINT ' -> Table Roles already exists.';
GO


