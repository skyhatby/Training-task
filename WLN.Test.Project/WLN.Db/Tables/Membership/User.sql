CREATE TABLE [Membership].[User]
(
	[Id] BIGINT NOT NULL,
	[Name] NVARCHAR(128) NOT NULL,
    [Password] NVARCHAR(128) NOT NULL, 
    [PasswordSalt] NVARCHAR(128) NOT NULL,

    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([Id] ASC),
)
GO

CREATE NONCLUSTERED INDEX [NCI_User_Name] ON [Membership].[User]([Name] ASC)