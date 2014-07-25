CREATE TABLE [Membership].[Role]
(
	[Id] INT NOT NULL, 
	[Name] NVARCHAR(128) NOT NULL,

    CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [AK_Role_Name] UNIQUE ([Name])
)
