CREATE OR ALTER VIEW IdentityUsersV
AS 
SELECT Email, UserName, PhoneNumber, IsActive, IsLocked
FROM [LonG_Identity].dbo.Users