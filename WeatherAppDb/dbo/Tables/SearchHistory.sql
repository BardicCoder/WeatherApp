CREATE TABLE [dbo].[SearchHistory] (
    [MemberId] INT         NOT NULL,
    [ZipCode]  VARCHAR (5) NOT NULL,
    CONSTRAINT [PK_SearchHistory] PRIMARY KEY CLUSTERED ([MemberId] ASC, [ZipCode] ASC),
    CONSTRAINT [FK_Users_SearchHistory] FOREIGN KEY ([MemberId]) REFERENCES [dbo].[Users] ([MemberId])
);

