CREATE TABLE [dbo].[RolePrivilage](
	[RolePrivilageId] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [int] NOT NULL,
	[PrivilageId] [int] NOT NULL,
 CONSTRAINT [PK_RolePrivilage] PRIMARY KEY CLUSTERED 
(
	[RolePrivilageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[RolePrivilage]  WITH CHECK ADD  CONSTRAINT [FK_RolePrivilage_Privilages] FOREIGN KEY([PrivilageId])
REFERENCES [dbo].[Privilages] ([PrivilageId])
GO

ALTER TABLE [dbo].[RolePrivilage] CHECK CONSTRAINT [FK_RolePrivilage_Privilages]
GO

ALTER TABLE [dbo].[RolePrivilage]  WITH CHECK ADD  CONSTRAINT [FK_RolePrivilage_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([RoleId])
GO

ALTER TABLE [dbo].[RolePrivilage] CHECK CONSTRAINT [FK_RolePrivilage_Roles]
GO


