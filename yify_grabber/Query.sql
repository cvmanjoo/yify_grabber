--CREATE DATABASE yify;

USE yify;

--DROP TABLE movies;

CREATE TABLE movies (
MovieID INT DEFAULT 0,
MovieUrl VARCHAR(1024),
DateUploaded DATETIME,
Uploader VARCHAR(20),
UploaderUID INT,
UploaderNotes TEXT,
Quality VARCHAR(10),
Resolution VARCHAR(10),
FrameRate VARCHAR(10),
Language VARCHAR(20),
Subtitles VARCHAR(20),
LargeCover TEXT,
MediumCover TEXT,
LargeScreenshot1 VARCHAR(1024),
LargeScreenshot2 VARCHAR(1024),
LargeScreenshot3 VARCHAR(1024),
MediumScreenshot1 VARCHAR(1024),
MediumScreenshot2 VARCHAR(1024),
MediumScreenshot3 VARCHAR(1024),
ImdbCode VARCHAR(20),
ImdbLink VARCHAR(1024),
MovieTitle VARCHAR(512),
MovieTitleClean VARCHAR(512),
MovieYear VARCHAR(5),
MovieRating VARCHAR(5),
MovieRuntime VARCHAR(20),
YoutubeTrailerID VARCHAR(20),
YoutubeTrailerUrl VARCHAR(1024),
AgeRating VARCHAR(10),
Genre1 VARCHAR(25),
Genre2 VARCHAR(25),
ShortDescription TEXT,
LongDescription TEXT,
Downloaded INT,
TorrentUrl  VARCHAR(1024),
TorrentHash VARCHAR(40),
TorrentMagnetUrl VARCHAR(1024),
TorrentSeeds INT,
TorrentPeers INT,
Size VARCHAR(25),
SizeByte BIGINT
);

--TRUNCATE table Movies;


Select * from Movies;