CREATE TABLE [dbo].[Permissions]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [EmployeeForename] TEXT NULL, 
    [EmployeeSurname] TEXT NULL, 
    [PermissionType] INT NULL, 
    [PermissionDate] DATE NULL, 
    CONSTRAINT [FK_Permissions_PermisionTypes] FOREIGN KEY ([PermissionType]) REFERENCES [PermissionTypes]([Id])
)
