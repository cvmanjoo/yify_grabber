using System;
using System.Collections.Generic;
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
            string connectionString = "Data Source=MONSTER-PC;Initial Catalog=yify;Integrated Security=True";

            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            //Get Maxiumum MovieID from Database!
            String CountQuery = "SELECT MAX(id) FROM movie_details;";
            SqlCommand CountCommand = new SqlCommand(CountQuery, connection);

            try
            {
                ID = (int)CountCommand.ExecuteScalar();
            }
            catch
            {
                ID = 0;
            }
            Console.WriteLine("Local Database has " + ID + " Records!");

            //Get Maximum MovieID from Yify API

            XmlDocument list = new XmlDocument();
            list.Load("https://yts.to/api/v2/list_movies.xml");

            XmlNodeList elemlist = list.GetElementsByTagName("movie_count");
            String MaxMovieID = elemlist[0].InnerText;

            Console.WriteLine("Yify Database has " + MaxMovieID + " Movies!");
            Int32 MovieCount = Convert.ToInt32(MaxMovieID);

            //Collect and insert into database!

            for (ID = ID + 1; ID <= MovieCount; ID++)
            {
                String XmlUrl = "https://yts.to/api/v2/movie_details.xml?movie_id=" + ID.ToString();

                XmlDocument doc = new XmlDocument();
                doc.Load(XmlUrl); //  TO-DO try  catch WebException 502

                try
                {
                    XmlNode dataNode = doc.SelectSingleNode("/xml/data");

                    String id = dataNode.SelectSingleNode("id").InnerText;
                    String url = dataNode.SelectSingleNode("url").InnerText;
                    String imdb_code = dataNode.SelectSingleNode("imdb_code").InnerText;
                    String title = dataNode.SelectSingleNode("title").InnerText;
                    String title_long = dataNode.SelectSingleNode("title_long").InnerText;
                    String slug = dataNode.SelectSingleNode("slug").InnerText;
                    String year = dataNode.SelectSingleNode("year").InnerText;
                    String rating = dataNode.SelectSingleNode("rating").InnerText;
                    String runtime = dataNode.SelectSingleNode("runtime").InnerText;

                    XmlNode genresnode = doc.SelectSingleNode("/xml/data/genres");
                    List<String> Genres = new List<String>();

                    foreach (XmlNode node in genresnode)
                    {
                        Genres.Add(node.InnerText);
                    }

                    String genre1 = Genres[0];
                    String genre2 = Genres[1];

                    //Console.WriteLine(genre1);
                    //Console.WriteLine(genre2);

                    String language = dataNode.SelectSingleNode("language").InnerText;
                    String mpa_rating = dataNode.SelectSingleNode("mpa_rating").InnerText;
                    String download_count = dataNode.SelectSingleNode("download_count").InnerText;
                    String like_count = dataNode.SelectSingleNode("like_count").InnerText;
                    String rt_critics_score = dataNode.SelectSingleNode("rt_critics_score").InnerText;
                    String rt_critics_rating = dataNode.SelectSingleNode("rt_critics_rating").InnerText;
                    String rt_audience_score = dataNode.SelectSingleNode("rt_audience_score").InnerText;
                    String rt_audience_rating = dataNode.SelectSingleNode("rt_audience_rating").InnerText;
                    String description_intro = dataNode.SelectSingleNode("description_intro").InnerText;
                    String description_full = dataNode.SelectSingleNode("description_full").InnerText;
                    String yt_trailer_code = dataNode.SelectSingleNode("yt_trailer_code").InnerText;
                    String date_uploaded = dataNode.SelectSingleNode("date_uploaded").InnerText;
                    String date_uploaded_unix = dataNode.SelectSingleNode("date_uploaded_unix").InnerText;

                    /* <torrents> */

                    Console.WriteLine(id + " - " + title_long);

                    String InsertQuery = "INSERT INTO movie_details VALUES(@id,@url,@imdb_code,@title,@title_long,@slug,@year,@rating,@runtime,@genre1,@genre2,@language,@mpa_rating,@download_count,@like_count,@rt_critics_score,@rt_critics_rating,@rt_audience_score,@rt_audience_rating,@description_intro,@description_full,@yt_trailer_code,@date_uploaded,@date_uploaded_unix)";
                    //Console.WriteLine(InsertQuery);

                    SqlCommand command = new SqlCommand(InsertQuery, connection);

                    command.Parameters.Add("@id", SqlDbType.Int);
                    command.Parameters["@id"].Value = id;

                    command.Parameters.Add("@url", SqlDbType.VarChar);
                    command.Parameters["@url"].Value = url;

                    command.Parameters.Add("@imdb_code", SqlDbType.VarChar);
                    command.Parameters["@imdb_code"].Value = imdb_code;

                    command.Parameters.Add("@title", SqlDbType.VarChar);
                    command.Parameters["@title"].Value = title;

                    command.Parameters.Add("@title_long", SqlDbType.VarChar);
                    command.Parameters["@title_long"].Value = title_long;

                    command.Parameters.Add("@slug", SqlDbType.VarChar);
                    command.Parameters["@slug"].Value = slug;

                    command.Parameters.Add("@year", SqlDbType.VarChar);
                    command.Parameters["@year"].Value = year;

                    command.Parameters.Add("@rating", SqlDbType.VarChar);
                    command.Parameters["@rating"].Value = rating;

                    command.Parameters.Add("@runtime", SqlDbType.VarChar);
                    command.Parameters["@runtime"].Value = runtime;

                    command.Parameters.Add("@genre1", SqlDbType.VarChar);
                    command.Parameters["@genre1"].Value = genre1;

                    command.Parameters.Add("@genre2", SqlDbType.VarChar);
                    command.Parameters["@genre2"].Value = genre2;

                    command.Parameters.Add("@language", SqlDbType.VarChar);
                    command.Parameters["@language"].Value = language;

                    command.Parameters.Add("@mpa_rating", SqlDbType.VarChar);
                    command.Parameters["@mpa_rating"].Value = mpa_rating;

                    command.Parameters.Add("@download_count", SqlDbType.Int);
                    command.Parameters["@download_count"].Value = download_count;

                    command.Parameters.Add("@like_count", SqlDbType.Int);
                    command.Parameters["@like_count"].Value = like_count;

                    command.Parameters.Add("@rt_critics_score", SqlDbType.Int);
                    command.Parameters["@rt_critics_score"].Value = rt_critics_score;

                    command.Parameters.Add("@rt_critics_rating", SqlDbType.VarChar);
                    command.Parameters["@rt_critics_rating"].Value = rt_critics_rating;

                    command.Parameters.Add("@rt_audience_score", SqlDbType.Int);
                    command.Parameters["@rt_audience_score"].Value = rt_audience_score;

                    command.Parameters.Add("@rt_audience_rating", SqlDbType.VarChar);
                    command.Parameters["@rt_audience_rating"].Value = rt_audience_rating;

                    command.Parameters.Add("@description_intro", SqlDbType.Text);
                    command.Parameters["@description_intro"].Value = description_intro;

                    command.Parameters.Add("@description_full", SqlDbType.Text);
                    command.Parameters["@description_full"].Value = description_full;

                    command.Parameters.Add("@yt_trailer_code", SqlDbType.VarChar);
                    command.Parameters["@yt_trailer_code"].Value = yt_trailer_code;

                    command.Parameters.Add("@date_uploaded", SqlDbType.DateTime);
                    command.Parameters["@date_uploaded"].Value = date_uploaded;

                    command.Parameters.Add("@date_uploaded_unix", SqlDbType.Float);
                    command.Parameters["@date_uploaded_unix"].Value = date_uploaded_unix;

                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {

                        // Console.WriteLine(e.ToString());
                        Console.WriteLine(e.Message);
                    }
                }
                catch (Exception e)
                {
                    //If movie id is invalid show NULL! 
                    Console.WriteLine(ID + " - NULL");
                    Console.WriteLine(e.Message);
                }
            }

            Console.WriteLine("End of Execution");
            Console.ReadKey();
        }
    }
}
