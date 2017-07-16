USE [test]
GO

/****** Object:  Table [dbo].[Cars]    Script Date: 2017-07-16 15:24:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Cars](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[model] [varchar](50) NOT NULL,
	[status] [int] NOT NULL,
	[regNum] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Cars] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

