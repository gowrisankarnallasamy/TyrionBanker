CREATE TABLE [dbo].[UserAccount](
	[UserAccountId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[BankAccountId] [int] NOT NULL,
 CONSTRAINT [PK_UserAccount] PRIMARY KEY CLUSTERED 
(
	[UserAccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[UserAccount]  WITH CHECK ADD  CONSTRAINT [FK_UserAccount_BankAccount] FOREIGN KEY([BankAccountId])
REFERENCES [dbo].[BankAccount] ([BankAccountId])
GO

ALTER TABLE [dbo].[UserAccount] CHECK CONSTRAINT [FK_UserAccount_BankAccount]
GO

ALTER TABLE [dbo].[UserAccount]  WITH CHECK ADD  CONSTRAINT [FK_UserAccount_UserInfo] FOREIGN KEY([UserId])
REFERENCES [dbo].[UserInfo] ([UserId])
GO

ALTER TABLE [dbo].[UserAccount] CHECK CONSTRAINT [FK_UserAccount_UserInfo]
GO

ALTER TABLE [dbo].[UserAccount]
ADD UNIQUE ([BankAccountId]);
GO
