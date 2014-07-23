CREATE TABLE [Membership].[UserRole]
(
    [UserId]            BIGINT          NOT NULL,
    [RoleId]            BIGINT             NOT NULL,

    CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED ([UserId], [RoleId]),
    CONSTRAINT [FK_UserRole_To_User] FOREIGN KEY ([UserId]) REFERENCES [Membership].[User]([Id]),
    CONSTRAINT [FK_UserRole_To_Role] FOREIGN KEY ([RoleId]) REFERENCES [Membership].[Role]([Id]),
)

GO

CREATE NONCLUSTERED INDEX [NCI_UserRole_RoleId] ON [Membership].[UserRole] ([RoleId] ASC)
