using System.Collections.Generic;

namespace AnimeData.Services
{
    public interface IAnimeService
    {
        public Anime GetAnimeByTitle(string animeName);

        public List<Anime> Animes();
        public List<Anime> SearchedAnimes(string search);
        public List<Anime> AnimesRatedToWatch();
        public List<Anime> AnimesRatedWatching();
        public List<Anime> AnimesRatedWatched();
        public List<Anime> AnimesRated();
        public List<Anime> AnimesWithMore();

        public void Create(Anime context);
        public void Update();
        public void Delete(Anime context);       
    }
}