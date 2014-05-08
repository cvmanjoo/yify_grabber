using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace yify_grabber
{
    class Program
    {
        static void Main(string[] args)
        {
            Int32 ID;
            string connectionString = "Data Source=MONSTER-PC\\SQLEXPRESS;Initial Catalog=yify;Integrated Security=True";

            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            //Get Maxiumum MovieID from Database!
            String CountQuery = "SELECT MAX(MovieID) FROM Movies;";
            SqlCommand CountCommand = new SqlCommand(CountQuery, connection);

            try
            {
                ID = (int)CountCommand.ExecuteScalar();
            }
            catch
            {
                ID = 0;
            }
            Console.WriteLine("Local Database has "+ID+" Records!");

            //Get Maximum MovieID from Yify API
            XmlDocument list = new XmlDocument();
            list.Load("https://yts.re/api/list.xml");

            XmlNodeList elemlist = list.GetElementsByTagName("MovieID");
            String MaxMovieID = elemlist[0].InnerText;

            Console.WriteLine("Yify Database has "+ MaxMovieID + " Movies!");
            Int32 MovieCount = Convert.ToInt32(MaxMovieID);

            //Collect and insert into database!
            for (ID = ID + 1; ID <= MovieCount; ID++)
            {
                String XmlUrl = "http://yts.re/api/movie.xml?id=" + ID.ToString();

                XmlDocument doc = new XmlDocument();
                doc.Load(XmlUrl);

                try
                {
                    XmlNodeList elemList = doc.GetElementsByTagName("MovieID");
                    String MovieID = elemList[0].InnerText;

                    elemList = doc.GetElementsByTagName("MovieUrl");
                    String MovieUrl = elemList[0].InnerText;

                    elemList = doc.GetElementsByTagName("DateUploaded");
                    String DateUploaded = elemList[0].InnerText;

                    elemList = doc.GetElementsByTagName("Uploader");
                    String Uploader = elemList[0].InnerText;

                    elemList = doc.GetElementsByTagName("UploaderUID");
                    String UploaderUID = elemList[0].InnerText;

                    elemList = doc.GetElementsByTagName("UploaderNotes");
                    String UploaderNotes = elemList[0].InnerText;

                    elemList = doc.GetElementsByTagName("Quality");
                    String Quality = elemList[0].InnerText;

                    elemList = doc.GetElementsByTagName("Resolution");
                    String Resolution = elemList[0].InnerText;

                    elemList = doc.GetElementsByTagName("FrameRate");
                    String FrameRate = elemList[0].InnerText;

                    elemList = doc.GetElementsByTagName("Language");
                    String Language = elemList[0].InnerText;

                    elemList = doc.GetElementsByTagName("Subtitles");
                    String Subtitles = elemList[0].InnerText;

                    elemList = doc.GetElementsByTagName("LargeCover");
                    String LargeCover = elemList[0].InnerText;

                    elemList = doc.GetElementsByTagName("MediumCover");
                    String MediumCover = elemList[0].InnerText;

                    elemList = doc.GetElementsByTagName("LargeScreenshot1");
                    String LargeScreenshot1 = elemList[0].InnerText;

                    elemList = doc.GetElementsByTagName("LargeScreenshot2");
                    String LargeScreenshot2 = elemList[0].InnerText;

                    elemList = doc.GetElementsByTagName("LargeScreenshot3");
                    String LargeScreenshot3 = elemList[0].InnerText;

                    elemList = doc.GetElementsByTagName("MediumScreenshot1");
                    String MediumScreenshot1 = elemList[0].InnerText;

                    elemList = doc.GetElementsByTagName("MediumScreenshot2");
                    String MediumScreenshot2 = elemList[0].InnerText;

                    elemList = doc.GetElementsByTagName("MediumScreenshot3");
                    String MediumScreenshot3 = elemList[0].InnerText;

                    elemList = doc.GetElementsByTagName("ImdbCode");
                    String ImdbCode = elemList[0].InnerText;

                    elemList = doc.GetElementsByTagName("ImdbLink");
                    String ImdbLink = elemList[0].InnerText;

                    elemList = doc.GetElementsByTagName("MovieTitle");
                    String MovieTitle = elemList[0].InnerText;

                    elemList = doc.GetElementsByTagName("MovieTitleClean");
                    String MovieTitleClean = elemList[0].InnerText;

                    elemList = doc.GetElementsByTagName("MovieYear");
                    String MovieYear = elemList[0].InnerText;

                    elemList = doc.GetElementsByTagName("MovieRating");
                    String MovieRating = elemList[0].InnerText;

                    elemList = doc.GetElementsByTagName("MovieRuntime");
                    String MovieRuntime = elemList[0].InnerText;

                    elemList = doc.GetElementsByTagName("YoutubeTrailerID");
                    String YoutubeTrailerID = elemList[0].InnerText;

                    elemList = doc.GetElementsByTagName("YoutubeTrailerUrl");
                    String YoutubeTrailerUrl = elemList[0].InnerText;

                    elemList = doc.GetElementsByTagName("AgeRating");
                    String AgeRating = elemList[0].InnerText;

                    elemList = doc.GetElementsByTagName("Genre1");
                    String Genre1 = elemList[0].InnerText;

                    elemList = doc.GetElementsByTagName("Genre2");
                    String Genre2 = elemList[0].InnerText;

                    elemList = doc.GetElementsByTagName("ShortDescription");
                    String ShortDescription = elemList[0].InnerText;

                    elemList = doc.GetElementsByTagName("LongDescription");
                    String LongDescription = elemList[0].InnerText;

                    elemList = doc.GetElementsByTagName("Downloaded");
                    String Downloaded = elemList[0].InnerText;

                    elemList = doc.GetElementsByTagName("TorrentUrl");
                    String TorrentUrl = elemList[0].InnerText;

                    elemList = doc.GetElementsByTagName("TorrentHash");
                    String TorrentHash = elemList[0].InnerText;

                    elemList = doc.GetElementsByTagName("TorrentMagnetUrl");
                    String TorrentMagnetUrl = elemList[0].InnerText;

                    elemList = doc.GetElementsByTagName("TorrentSeeds");
                    String TorrentSeeds = elemList[0].InnerText;

                    elemList = doc.GetElementsByTagName("TorrentPeers");
                    String TorrentPeers = elemList[0].InnerText;

                    elemList = doc.GetElementsByTagName("Size");
                    String Size = elemList[0].InnerText;

                    elemList = doc.GetElementsByTagName("SizeByte");
                    String SizeByte = elemList[0].InnerText;

                    Console.WriteLine(MovieID + " - " + MovieTitle);

                    String InsertQuery = "INSERT INTO Movies VALUES(@MovieID,@MovieUrl,@DateUploaded,@Uploader,@UploaderUID,@UploaderNotes,@Quality,@Resolution,@FrameRate,@Language,@Subtitles,@LargeCover,@MediumCover,@LargeScreenshot1,@LargeScreenshot2,@LargeScreenshot3,@MediumScreenshot1,@MediumScreenshot2,@MediumScreenshot3,@ImdbCode,@ImdbLink,@MovieTitle,@MovieTitleClean,@MovieYear,@MovieRating,@MovieRuntime,@YoutubeTrailerID,@YoutubeTrailerUrl,@AgeRating,@Genre1,@Genre2,@ShortDescription,@LongDescription,@Downloaded,@TorrentUrl,@TorrentHash,@TorrentMagnetUrl,@TorrentSeeds,@TorrentPeers,@Size,@SizeByte)";

                    //Console.WriteLine(InsertQuery);

                    SqlCommand command = new SqlCommand(InsertQuery, connection);

                    command.Parameters.Add("@MovieID", SqlDbType.Int);
                    command.Parameters["@MovieID"].Value = MovieID;

                    command.Parameters.Add("@MovieUrl", SqlDbType.VarChar);
                    command.Parameters["@MovieUrl"].Value = MovieUrl;

                    command.Parameters.Add("@DateUploaded", SqlDbType.DateTime);
                    command.Parameters["@DateUploaded"].Value = DateUploaded;

                    command.Parameters.Add("@Uploader", SqlDbType.VarChar);
                    command.Parameters["@Uploader"].Value = Uploader;

                    command.Parameters.Add("@UploaderUID", SqlDbType.Int);
                    command.Parameters["@UploaderUID"].Value = UploaderUID;

                    command.Parameters.Add("@UploaderNotes", SqlDbType.Text);
                    command.Parameters["@UploaderNotes"].Value = UploaderNotes;

                    command.Parameters.Add("@Quality", SqlDbType.VarChar);
                    command.Parameters["@Quality"].Value = Quality;

                    command.Parameters.Add("@Resolution", SqlDbType.VarChar);
                    command.Parameters["@Resolution"].Value = Resolution;

                    command.Parameters.Add("@FrameRate", SqlDbType.VarChar);
                    command.Parameters["@FrameRate"].Value = FrameRate;

                    command.Parameters.Add("@Language", SqlDbType.VarChar);
                    command.Parameters["@Language"].Value = Language;

                    command.Parameters.Add("@Subtitles", SqlDbType.VarChar);
                    command.Parameters["@Subtitles"].Value = Subtitles;

                    command.Parameters.Add("@LargeCover", SqlDbType.VarChar);
                    command.Parameters["@LargeCover"].Value = LargeCover;

                    command.Parameters.Add("@MediumCover", SqlDbType.VarChar);
                    command.Parameters["@MediumCover"].Value = MediumCover;

                    command.Parameters.Add("@LargeScreenshot1", SqlDbType.VarChar);
                    command.Parameters["@LargeScreenshot1"].Value = LargeScreenshot1;

                    command.Parameters.Add("@LargeScreenshot2", SqlDbType.VarChar);
                    command.Parameters["@LargeScreenshot2"].Value = LargeScreenshot2;

                    command.Parameters.Add("@LargeScreenshot3", SqlDbType.VarChar);
                    command.Parameters["@LargeScreenshot3"].Value = LargeScreenshot3;

                    command.Parameters.Add("@MediumScreenshot1", SqlDbType.VarChar);
                    command.Parameters["@MediumScreenshot1"].Value = MediumScreenshot1;

                    command.Parameters.Add("@MediumScreenshot2", SqlDbType.VarChar);
                    command.Parameters["@MediumScreenshot2"].Value = MediumScreenshot2;

                    command.Parameters.Add("@MediumScreenshot3", SqlDbType.VarChar);
                    command.Parameters["@MediumScreenshot3"].Value = MediumScreenshot3;

                    command.Parameters.Add("@ImdbCode", SqlDbType.VarChar);
                    command.Parameters["@ImdbCode"].Value = ImdbCode;

                    command.Parameters.Add("@ImdbLink", SqlDbType.VarChar);
                    command.Parameters["@ImdbLink"].Value = ImdbLink;

                    command.Parameters.Add("@MovieTitle", SqlDbType.VarChar);
                    command.Parameters["@MovieTitle"].Value = MovieTitle;

                    command.Parameters.Add("@MovieTitleClean", SqlDbType.VarChar);
                    command.Parameters["@MovieTitleClean"].Value = MovieTitleClean;

                    command.Parameters.Add("@MovieYear", SqlDbType.VarChar);
                    command.Parameters["@MovieYear"].Value = MovieYear;

                    command.Parameters.Add("@MovieRating", SqlDbType.VarChar);
                    command.Parameters["@MovieRating"].Value = MovieRating;

                    command.Parameters.Add("@MovieRuntime", SqlDbType.VarChar);
                    command.Parameters["@MovieRuntime"].Value = MovieRuntime;

                    command.Parameters.Add("@YoutubeTrailerID", SqlDbType.VarChar);
                    command.Parameters["@YoutubeTrailerID"].Value = YoutubeTrailerID;

                    command.Parameters.Add("@YoutubeTrailerUrl", SqlDbType.VarChar);
                    command.Parameters["@YoutubeTrailerUrl"].Value = YoutubeTrailerUrl;

                    command.Parameters.Add("@AgeRating", SqlDbType.VarChar);
                    command.Parameters["@AgeRating"].Value = AgeRating;

                    command.Parameters.Add("@Genre1", SqlDbType.VarChar);
                    command.Parameters["@Genre1"].Value = Genre1;

                    command.Parameters.Add("@Genre2", SqlDbType.VarChar);
                    command.Parameters["@Genre2"].Value = Genre2;

                    command.Parameters.Add("@ShortDescription", SqlDbType.Text);
                    command.Parameters["@ShortDescription"].Value = ShortDescription;

                    command.Parameters.Add("@LongDescription", SqlDbType.Text);
                    command.Parameters["@LongDescription"].Value = LongDescription;

                    command.Parameters.Add("@Downloaded", SqlDbType.Int);
                    command.Parameters["@Downloaded"].Value = Downloaded;

                    command.Parameters.Add("@TorrentUrl", SqlDbType.VarChar);
                    command.Parameters["@TorrentUrl"].Value = TorrentUrl;

                    command.Parameters.Add("@TorrentHash", SqlDbType.VarChar);
                    command.Parameters["@TorrentHash"].Value = TorrentHash;

                    command.Parameters.Add("@TorrentMagnetUrl", SqlDbType.VarChar);
                    command.Parameters["@TorrentMagnetUrl"].Value = TorrentMagnetUrl;

                    command.Parameters.Add("@TorrentSeeds", SqlDbType.Int);
                    command.Parameters["@TorrentSeeds"].Value = TorrentSeeds;

                    command.Parameters.Add("@TorrentPeers", SqlDbType.Int);
                    command.Parameters["@TorrentPeers"].Value = TorrentPeers;

                    command.Parameters.Add("@Size", SqlDbType.VarChar);
                    command.Parameters["@Size"].Value = Size;

                    command.Parameters.Add("@SizeByte", SqlDbType.BigInt);
                    command.Parameters["@SizeByte"].Value = SizeByte;

                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }
                catch
                {
                    //If movie id is invalid show NULL! 
                    Console.WriteLine(ID + " - NULL");
                }
            }
            Console.WriteLine("End of Execution");
             Console.ReadKey();
        }
    }
}
