CREATE TABLE [dbo].[BankAccount](
	[BankAccountId] [int] IDENTITY(1,1) NOT NULL,
	[BankAccountNo] [nvarchar](50) NOT NULL,
	[Balanca] [decimal](18, 3) NOT NULL,
	[BankAccountType] [int] NOT NULL,
 CONSTRAINT [PK_BankAccount] PRIMARY KEY CLUSTERED 
(
	[BankAccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[BankAccount]  WITH CHECK ADD  CONSTRAINT [FK_BankAccount_BankAccountTypes] FOREIGN KEY([BankAccountType])
REFERENCES [dbo].[BankAccountTypes] ([BankAccountTypeId])
GO

ALTER TABLE [dbo].[BankAccount] CHECK CONSTRAINT [FK_BankAccount_BankAccountTypes]
GO