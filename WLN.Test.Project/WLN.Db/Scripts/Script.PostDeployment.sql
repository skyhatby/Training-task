/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
-- HiLo Table Data
if not exists (select * from [dbo].[HiLo] where [TableName] = N'User')
    INSERT [dbo].[HiLo] ([TableName], [NextHi]) VALUES (N'User', 1)

--Role
if not exists (select * from [Membership].[Role] where [Id] = 1)
    INSERT [Membership].[Role] ([Id], [Name]) VALUES (1, N'Administrator')

if not exists (select * from [Membership].[Role] where [Id] = 2)
    INSERT [Membership].[Role] ([Id], [Name]) VALUES (2, N'User')