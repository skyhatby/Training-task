CREATE TABLE [Membership].[User]
(
	[Id] BIGINT NOT NULL  IDENTITY,
    [Password] NVARCHAR(128) NOT NULL, 
    [PasswordSalt] NVARCHAR(128) NOT NULL,

    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([Id] ASC)
)
