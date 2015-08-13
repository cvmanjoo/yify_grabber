USE [yify]
GO

/****** Object:  Table [dbo].[movie_torrents]    Script Date: 13-Aug-2015 9:32:28 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[movie_torrents](
	[id] [int] NULL,
	[url] [varchar](512) NULL,
	[hash] [varchar](50) NULL,
	[quality] [varchar](5) NULL,
	[resolution] [varchar](20) NULL,
	[framerate] [float] NULL,
	[seeds] [int] NULL,
	[peers] [int] NULL,
	[size] [varchar](20) NULL,
	[size_bytes] [bigint] NULL,
	[download_count] [bigint] NULL,
	[date_uploaded] [date] NULL,
	[date_uploaded_unix] [nchar](10) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


