using System.Collections.Generic;
using System.Linq;

namespace AnimeData.Services
{
    public class AnimeService : IAnimeService
    {
        private readonly SammListContext _context;

        public AnimeService(SammListContext context)
        {
            _context = context;
        }
        public AnimeService()
        {
            _context = new SammListContext();
        }

        public Anime GetAnimeByTitle(string animeName)
        {
            return _context.Animes.Where(a => a.AnimeName == animeName).FirstOrDefault();
        }


        public List<Anime> Animes()
        {
            return _context.Animes.OrderBy(a => a.AnimeName).ToList();
        }
        public List<Anime> SearchedAnimes(string search)
        {
            return _context.Animes.Where(a => a.AnimeName.Contains(search)).OrderBy(a => a.AnimeName).ToList();
        }
        public List<Anime> AnimesRatedToWatch()
        {
            return _context.Animes.Where(a => a.Watch == "To Watch").OrderBy(a => a.AnimeName).ToList();
        }
        public List<Anime> AnimesRatedWatching()
        {
            return _context.Animes.Where(a => a.Watch == "Watching").OrderBy(a => a.AnimeName).ToList();
        }
        public List<Anime> AnimesRatedWatched()
        {
            return _context.Animes.Where(a => a.Watch == "Watched").OrderBy(a => a.AnimeName).ToList();
        }
        public List<Anime> AnimesRated()
        {
            return _context.Animes.OrderByDescending(a => a.Rating).ToList();
        }
        public List<Anime> AnimesWithMore()
        {
            return _context.Animes.OrderBy(a => a.More).ToList();
        }



        public void Create(Anime anime)
        {
            _context.Animes.Add(anime);
            _context.SaveChanges();
        }
        public void Update()
        {
            _context.SaveChanges();
        }
        public void Delete(Anime anime)
        {
            _context.Animes.Remove(anime);
            _context.SaveChanges();
        }
    }
}
