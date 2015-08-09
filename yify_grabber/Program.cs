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

                    String genre1, genre2;
                    genre1 = null;
                    genre2 = null;

                    List<String> Genres = new List<String>();

                    foreach (XmlNode node in genresnode)
                    {
                        Genres.Add(node.InnerText);
                    }
                    if (Genres.Count == 1)
                    {
                        genre1 = Genres[0];
                        genre2 = null;
                    }
                    else if (Genres.Count == 2)
                    {
                        genre1 = Genres[0];
                        genre2 = Genres[1];
                    }
                    else
                    {
                        Console.WriteLine("Count of Generes " + Genres.Count);
                        Console.ReadKey();
                    }
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

                    /* TO DO: <torrents> */

                    String InsertQuery = "INSERT INTO movie_details VALUES(@id,@url,@imdb_code,@title,@title_long,@slug,@year,@rating,@runtime,@genre1,@genre2,@language,@mpa_rating,@download_count,@like_count,@rt_critics_score,@rt_critics_rating,@rt_audience_score,@rt_audience_rating,@description_intro,@description_full,@yt_trailer_code,@date_uploaded,@date_uploaded_unix)";
                    //Console.WriteLine(InsertQuery);

                    SqlCommand command = new SqlCommand(InsertQuery, connection);

                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@url", url);
                    command.Parameters.AddWithValue("@imdb_code", imdb_code);
                    command.Parameters.AddWithValue("@title", title);
                    command.Parameters.AddWithValue("@title_long", title_long);
                    command.Parameters.AddWithValue("@slug", slug);
                    command.Parameters.AddWithValue("@year", year);
                    command.Parameters.AddWithValue("@rating", rating);
                    command.Parameters.AddWithValue("@runtime", runtime);
                    command.Parameters.AddWithValue("@genre1", genre1);
                    command.Parameters.AddWithValue("@genre2", ((object)genre2) ?? DBNull.Value);
                    command.Parameters.AddWithValue("@language", language);
                    command.Parameters.AddWithValue("@mpa_rating", mpa_rating);
                    command.Parameters.AddWithValue("@download_count", download_count);
                    command.Parameters.AddWithValue("@like_count", like_count);
                    command.Parameters.AddWithValue("@rt_critics_score", rt_critics_score);
                    command.Parameters.AddWithValue("@rt_critics_rating", rt_critics_rating);
                    command.Parameters.AddWithValue("@rt_audience_score", rt_audience_score);
                    command.Parameters.AddWithValue("@rt_audience_rating", rt_audience_rating);
                    command.Parameters.AddWithValue("@description_intro", description_intro);
                    command.Parameters.AddWithValue("@description_full", description_full);
                    command.Parameters.AddWithValue("@yt_trailer_code", yt_trailer_code);
                    command.Parameters.AddWithValue("@date_uploaded", date_uploaded);
                    command.Parameters.AddWithValue("@date_uploaded_unix", date_uploaded_unix);

                    try
                    {
                        command.ExecuteNonQuery();
                        Console.WriteLine(id + " - " + title_long);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadKey();
                    }
                }
                catch (Exception e)
                {
                    //If movie id is invalid show NULL! 
                    Console.WriteLine(ID + " - NULL");
                    Console.WriteLine(e.Message);
                    Console.ReadKey();
                }
            }
            Console.WriteLine("End of Execution");
            Console.ReadKey();
        }
    }
}
