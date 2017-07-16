USE [test]
GO

/****** Object:  Table [dbo].[Rents]    Script Date: 2017-07-16 15:24:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Rents](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[cars_ID] [int] NOT NULL,
	[clients_ID] [int] NOT NULL,
	[dateRent] [datetime] NOT NULL,
	[dateReturn] [datetime] NOT NULL,
	[comment] [text] NULL,
 CONSTRAINT [PK_Rents] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[Rents]  WITH CHECK ADD  CONSTRAINT [FK_Rents_Cars] FOREIGN KEY([cars_ID])
REFERENCES [dbo].[Cars] ([id])
GO

ALTER TABLE [dbo].[Rents] CHECK CONSTRAINT [FK_Rents_Cars]
GO

ALTER TABLE [dbo].[Rents]  WITH CHECK ADD  CONSTRAINT [FK_Rents_Clients] FOREIGN KEY([clients_ID])
REFERENCES [dbo].[Clients] ([id])
GO

ALTER TABLE [dbo].[Rents] CHECK CONSTRAINT [FK_Rents_Clients]
GO


