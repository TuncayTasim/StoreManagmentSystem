SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

PRINT 'Processing Table: Users...';

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Users')
BEGIN
    CREATE TABLE [dbo].[Users](
        [UserId] [uniqueidentifier] NOT NULL,
        [UserName] [nvarchar](500) NOT NULL,
        [FirstName] [nvarchar](500) NOT NULL,
        [LastName] [nvarchar](500) NOT NULL,
        [RoleId] [int] NOT NULL,
        [Email] [nvarchar](500) NOT NULL,
        [PhoneNumber] [nvarchar](500) NOT NULL,
        [Note] [ntext] NULL,
        [Password] [nvarchar](500) NOT NULL,
        [ActionToken] [nvarchar](500) NULL,
        [StatusId] [int] NOT NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([UserId] ASC)
    ) 
    
    ALTER TABLE [dbo].[Users] ADD CONSTRAINT [DF_Users_StatusId_1] DEFAULT ((1)) FOR [StatusId];
    
    PRINT ' -> Created table Users.';
END
ELSE PRINT ' -> Table Users already exists.';

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Users_Roles')
BEGIN
    ALTER TABLE [dbo].[Users] WITH CHECK ADD CONSTRAINT [FK_Users_Roles] 
    FOREIGN KEY([RoleId]) REFERENCES [dbo].[Roles] ([RoleId]);
    ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Roles];
    PRINT ' -> Added FK_Users_Roles.';
END

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Users_Status')
BEGIN
    ALTER TABLE [dbo].[Users] WITH CHECK ADD CONSTRAINT [FK_Users_Status] 
    FOREIGN KEY([StatusId]) REFERENCES [dbo].[Status] ([StatusId]);
    ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Status];
    PRINT ' -> Added FK_Users_Status.';
END
GO


