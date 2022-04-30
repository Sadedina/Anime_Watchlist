using System.Collections.Generic;
using System.Linq;

namespace AnimeData.Services
{
    public class AnimeService : IAnimeService
    {
        private readonly SammListContext context;

        public AnimeService(SammListContext context)
        {
            this.context = context;
        }
        public AnimeService()
        {
            this.context = new SammListContext();
        }

        public Anime GetAnimeByTitle(string animeName) =>
            context.Animes.Where(a => a.AnimeName == animeName).FirstOrDefault();


        public List<Anime> Animes() =>
            context.Animes.OrderBy(a => a.AnimeName).ToList();

        public List<Anime> SearchedAnimes(string search) =>
            context.Animes.Where(a => a.AnimeName.Contains(search)).OrderBy(a => a.AnimeName).ToList();

        public List<Anime> AnimesRatedToWatch() =>
            context.Animes.Where(a => a.Watch == "To Watch").OrderBy(a => a.AnimeName).ToList();

        public List<Anime> AnimesRatedWatching() =>
            context.Animes.Where(a => a.Watch == "Watching").OrderBy(a => a.AnimeName).ToList();

        public List<Anime> AnimesRatedWatched() =>
            context.Animes.Where(a => a.Watch == "Watched").OrderBy(a => a.AnimeName).ToList();

        public List<Anime> AnimesRated() =>
            context.Animes.OrderByDescending(a => a.Rating).ToList();

        public List<Anime> AnimesWithMore() =>
            context.Animes.Where(a => a.More == "Maybe").OrderBy(a => a.AnimeName).ToList();


        public void Create(Anime anime)
        {
            context.Animes.Add(anime);
            context.SaveChanges();
        }
        public void Update()
        {
            context.SaveChanges();
        }
        public void Delete(Anime anime)
        {
            context.Animes.Remove(anime);
            context.SaveChanges();
        }
    }
}