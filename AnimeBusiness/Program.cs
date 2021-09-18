using System;

namespace AnimeBusiness
{
    class Program
    {
        static void Main()
        {
            //INSERT INTO Animes(animeName, episode, releaseYear, [status], [rank], picture, summary, more, rating, watch, episodeWatched, upToDate)
            //INSERT INTO Customers(CustomerName, ContactName, Address, City, PostalCode, Country)
            //VALUES('Cardinal', 'Tom B. Erichsen', 'Skagen 21', 'Stavanger', '4006', 'Norway');

            Console.WriteLine("VALUES ");

            var anime = new AnimeManager();
            var addToAList = anime.RetrieveAllAnime();
                foreach (var item in addToAList)
                Console.WriteLine($"('{item.AnimeName.Replace("'", "''")}', '{item.Episode}',  '{item.ReleaseYear}',  '{item.Status}'," +
                    $"  '{item.Rank}',  '{item.Picture.Replace("'", "''")}',  '{item.Summary.Replace("'", "''")}',  '{item.More}', '{item.Rating}', '{item.Watch}'," +
                    $" '{item.EpisodeWatched}', '{item.UpToDate}'),\n");
        } 
    }
}
