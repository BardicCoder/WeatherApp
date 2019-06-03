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

CREATE TABLE #tmp_Users
(
	[MemberId] [int] NOT NULL,
	[UserName] [VarChar](50) NOT NULL,
	[Password] [VarChar](50) NOT NULL
)

INSERT INTO #tmp_Users VALUES (1, 'Tester1', 'Password')
INSERT INTO #tmp_Users VALUES (2, 'Tester1', 'Password')
INSERT INTO #tmp_Users VALUES (3, 'Tester1', 'Password')

SET IDENTITY_INSERT Users ON
INSERT Users (MemberId, UserName, Password)
SELECT missing.*
FROM #tmp_users missing
	LEFT OUTER JOIN Users
		ON missing.MemberId = Users.MemberId
WHERE Users.MemberId IS NULL

SET IDENTITY_INSERT Users OFF
DROP TABLE #tmp_Users

CREATE TABLE #tmp_History
(
	[MemberId] [int] NOT NULL,
	[ZipCode] [VarChar](5) NOT NULL
)

INSERT INTO #tmp_History VALUES (1, '98503')
INSERT INTO #tmp_History VALUES (1, '99029')
INSERT INTO #tmp_History VALUES (1, '99123')

SET IDENTITY_INSERT SearchHistory ON
INSERT SearchHistory (MemberId, ZipCode)
SELECT missing.*
FROM #tmp_History missing
	LEFT OUTER JOIN SearchHistory
		ON missing.MemberId = SearchHistory.MemberId
			AND missing.ZipCode = SearchHistory.ZipCode
WHERE SearchHistory.MemberId IS NULL

SET IDENTITY_INSERT SearchHistory OFF
DROP TABLE #tmp_History