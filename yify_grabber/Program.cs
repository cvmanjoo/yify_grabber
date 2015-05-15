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
            Console.WriteLine("Local Database has "+ID+" Records!");

            //Get Maximum MovieID from Yify API
            XmlDocument list = new XmlDocument();
            list.Load("https://yts.to/api/v2/list_movies.xml");

            XmlNodeList elemlist = list.GetElementsByTagName("movie_count");
            String MaxMovieID = elemlist[0].InnerText;

            Console.WriteLine("Yify Database has "+ MaxMovieID + " Movies!");
            Int32 MovieCount = Convert.ToInt32(MaxMovieID);

            //Collect and insert into database!
            for (ID = ID + 1; ID <= MovieCount; ID++)
            {
                String XmlUrl = "https://yts.to/api/v2/movie_details.xml?movie_id=" + ID.ToString();

                XmlDocument doc = new XmlDocument();
                doc.Load(XmlUrl);

                try
                {
                    XmlNodeList elemList = doc.GetElementsByTagName("id");
                    String id = elemList[0].InnerText;

                    elemList = doc.GetElementsByTagName("url");
                    String url = elemList[0].InnerText;

                    elemList = doc.GetElementsByTagName("imdb_code");
                    String imdb_code = elemList[0].InnerText;

                    elemList = doc.GetElementsByTagName("title");
                    String title = elemList[0].InnerText;

                    elemList = doc.GetElementsByTagName("title_long");
                    String title_long = elemList[0].InnerText;

                    elemList = doc.GetElementsByTagName("slug");
                    String slug = elemList[0].InnerText;

                    elemList = doc.GetElementsByTagName("year");
                    String year = elemList[0].InnerText;

                    elemList = doc.GetElementsByTagName("rating");
                    String rating = elemList[0].InnerText;

                    elemList = doc.GetElementsByTagName("runtime");
                    String runtime = elemList[0].InnerText;
/*
                    elemList = doc.GetElementsByTagName("genre1");
                    String Language = elemList[0].InnerText;

                    elemList = doc.GetElementsByTagName("genre2");
                    String Subtitles = elemList[0].InnerText;
*/
                    elemList = doc.GetElementsByTagName("language");
                    String language = elemList[0].InnerText;

                    elemList = doc.GetElementsByTagName("mpa_rating");
                    String mpa_rating = elemList[0].InnerText;

                    elemList = doc.GetElementsByTagName("download_count");
                    String download_count = elemList[0].InnerText;

                    elemList = doc.GetElementsByTagName("like_count");
                    String like_count = elemList[0].InnerText;

                    elemList = doc.GetElementsByTagName("rt_critics_score");
                    String rt_critics_score = elemList[0].InnerText;

                    elemList = doc.GetElementsByTagName("rt_critics_rating");
                    String rt_critics_rating = elemList[0].InnerText;

                    elemList = doc.GetElementsByTagName("rt_audience_score");
                    String rt_audience_score = elemList[0].InnerText;

                    elemList = doc.GetElementsByTagName("rt_audience_rating");
                    String rt_audience_rating = elemList[0].InnerText;

                    elemList = doc.GetElementsByTagName("description_intro");
                    String description_intro = elemList[0].InnerText;

                    elemList = doc.GetElementsByTagName("description_full");
                    String description_full = elemList[0].InnerText;

                    elemList = doc.GetElementsByTagName("yt_trailer_code");
                    String yt_trailer_code = elemList[0].InnerText;

                    elemList = doc.GetElementsByTagName("date_uploaded");
                    String date_uploaded = elemList[0].InnerText;

                    elemList = doc.GetElementsByTagName("date_uploaded_unix");
                    String date_uploaded_unix = elemList[0].InnerText;

                    
                    Console.WriteLine(id + " - " + title_long);

                    String InsertQuery = "INSERT INTO movie_details VALUES(@id,@url,@imdb_code,@title,@title_long,@slug,@year,@rating,@runtime,@language,@mpa_rating,@download_count,@like_count,@rt_critics_score,@rt_critics_rating,@rt_audience_score,@rt_audience_rating,@description_intro,@description_full,@yt_trailer_code,@date_uploaded,@date_uploaded_unix)";
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
