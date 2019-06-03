CREATE TABLE [dbo].[Users] (
    [MemberId] INT          IDENTITY (1, 1) NOT NULL,
    [UserName] VARCHAR (50) NOT NULL,
    [Password] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([MemberId] ASC)
);

