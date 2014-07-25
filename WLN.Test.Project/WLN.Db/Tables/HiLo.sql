CREATE TABLE [dbo].[HiLo]
(
    [TableName]         NVARCHAR(64)    NOT NULL,
    [NextHi]            BIGINT          NOT NULL,

    CONSTRAINT [PK_HiLo] PRIMARY KEY CLUSTERED ([TableName] ASC)
)