using System;
using System.Linq;

namespace AnimeBusiness
{
    public class Program
    {
        public static void Main()
        {
            // Change null into Maybe in More coloumn
            Change.PrintAllAnimes();

            // Print all the animes table in SQL script format
            //Change.PrintAllAnimes();
        } 
    }
    public class Change
    {
        private readonly AnimeData.SammListContext context;
        public Change(AnimeData.SammListContext context)
        {
            this.context = context;
        }
        public Change()
        {
            this.context = new AnimeData.SammListContext();
        }
        private System.Collections.Generic.List<AnimeData.Anime> AnimesWithMoreNull() =>
            context.Animes.Where(a => a.More == null || a.More == "").ToList();

        private void Update() =>
            context.SaveChanges();

        public static void ChangeNullToMaybe()
        {
            Console.WriteLine("START");
            var anime = new Change();
            var addToAList = anime.AnimesWithMoreNull();
            
            foreach (var item in addToAList)
                item.More = "Maybe";

            anime.Update();
            Console.WriteLine("FINISH");
        }

        public static void PrintAllAnimes()
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