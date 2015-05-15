--CREATE DATABASE yify;

USE [yify]
GO

/****** Object:  Table [dbo].[movie_details]    Script Date: 15-May-2015 10:15:15 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[movie_details](
	[id] [int] NOT NULL CONSTRAINT [DF__movie_detail__id__0F975522]  DEFAULT ((0)),
	[url] [varchar](1024) NULL,
	[imdb_code] [varchar](20) NULL,
	[title] [varchar](512) NULL,
	[title_long] [varchar](512) NULL,
	[slug] [varchar](512) NULL,
	[year] [varchar](5) NULL,
	[rating] [varchar](5) NULL,
	[runtime] [varchar](20) NULL,
	[language] [varchar](20) NULL,
	[mpa_rating] [varchar](10) NULL,
	[download_count] [int] NULL,
	[like_count] [int] NULL,
	[rt_critics_score] [int] NULL,
	[rt_critics_rating] [varchar](25) NULL,
	[rt_audience_score] [int] NULL,
	[rt_audience_rating] [varchar](25) NULL,
	[description_intro] [text] NULL,
	[description_full] [text] NULL,
	[yt_trailer_code] [varchar](20) NULL,
	[date_uploaded] [date] NULL,
	[date_uploaded_unix] [float] NULL,
 CONSTRAINT [PK_movie_details] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

SELECT MAX(id) FROM movie_details;

SELECT * FROM movie_details;

--truncate table movie_details;