SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

PRINT 'Processing Table: Status...';
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Status')
BEGIN
    CREATE TABLE [dbo].[Status](
        [StatusId] [int] NOT NULL,
        [StatusName] [nvarchar](500) NOT NULL,
        CONSTRAINT [PK_Status] PRIMARY KEY CLUSTERED ([StatusId] ASC)
    ) ON [PRIMARY];
    PRINT ' -> Created table Status.';
END
ELSE PRINT ' -> Table Status already exists.';
GO

