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
--Role
if not exists (select * from [Membership].[Role] where [Name] =  N'Administrator')
    INSERT [Membership].[Role] ([Name]) VALUES ( N'Administrator')

if not exists (select * from [Membership].[Role] where [Name] = N'User')
    INSERT [Membership].[Role] ([Name]) VALUES ( N'User')