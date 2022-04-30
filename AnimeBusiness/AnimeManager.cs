using System;
using System.Collections.Generic;
using System.Diagnostics;
using AnimeData;
using AnimeData.Services;

namespace AnimeBusiness
{
    public class AnimeManager
    {
        // Service Set-Up
        private readonly IAnimeService service;
        public AnimeManager(IAnimeService service)
        {
            if (service == null)
                throw new ArgumentException("AnimeService object cannot be null");

            this.service = service;
        }
        public AnimeManager()
        {
            this.service = new AnimeService();
        }

        // Anime Selection
        public Anime SelectedAnime { get; set; }
        
        public void SetSelectedAnime(object selectedAnime) =>
            SelectedAnime = (Anime)selectedAnime;

        public void SetSelectedAnimeByTitle(string selectedAnime) =>
            SelectedAnime = service.GetAnimeByTitle(selectedAnime);


        // Anime To list
        public Anime SelectAnimeByName(string title) =>
            service.GetAnimeByTitle(title);

        public List<Anime> RetrieveAllAnime() =>
            service.Animes();

        public List<Anime> RetrieveSearchedAnime(string search)
        {
            if (search == null || search == "")
                return service.Animes();
            else
            {
                var text = char.ToUpper(search[0]) + search.Substring(1).ToLower();
                return service.SearchedAnimes(text);
            }
        }
        public List<Anime> RetrieveSpecificAnime(int? watched)
        {
            if (watched == 1)       return service.AnimesRatedToWatch();
            else if (watched == 2)  return service.AnimesRatedWatching();
            else if (watched == 3)  return service.AnimesRatedWatched();
            else if (watched == 4)  return service.AnimesRated();
            else if (watched == 5)  return service.AnimesWithMore();
            else                    return service.Animes();
        }

        // Anime CRUD
        
        public bool CreateAnime(string animeTitle, int? episode, int? releaseYear,
            string status, int? rank, string summary, string more)
        {
            var animeId = service.GetAnimeByTitle(animeTitle);
            //if (animeId != null)
            //{
            //    Debug.WriteLine($"Anime {animeTitle} already exists");
            //    return false;
            //}
            if (animeId == null)
            {
                var new_animeTitle = char.ToUpper(animeTitle[0]) + animeTitle.Substring(1).ToLower();
                string isItCompelete, isThereMore;

                isItCompelete = IsItCompelete(status);
                isThereMore = IsThereMore(more);

                var newAnimeDetail = new Anime()
                {
                    AnimeName = new_animeTitle,
                    Episode = episode,
                    ReleaseYear = releaseYear,
                    Status = isItCompelete,
                    Rank = rank,
                    Picture = $"/Animes/{new_animeTitle}.png",
                    Summary = summary,
                    More = isThereMore
                };
                service.Create(newAnimeDetail);
                return true;
            }
            return false;
        }

        public bool UpdateAnime(string oldAnimeTitle, string newAnimeTitle, int? episode, int? releaseYear,
        string status, int? rank, string summary, string more)
        {
            var animeId = service.GetAnimeByTitle(oldAnimeTitle);
            //if (animeId == null)
            //{
            //    Debug.WriteLine($"Anime: {oldAnimeTitle} was not found");
            //    return false;
            //}
            if (animeId != null)
            {
                var new_animeTitle = char.ToUpper(newAnimeTitle[0]) + newAnimeTitle.Substring(1).ToLower();
                string isItCompelete, isThereMore;

                isItCompelete = IsItCompelete(status);
                isThereMore = IsThereMore(more);

                animeId.AnimeName = new_animeTitle;
                animeId.Episode = episode;
                animeId.ReleaseYear = releaseYear;
                animeId.Status = isItCompelete;
                animeId.Rank = rank;
                animeId.Picture = $"/Animes/{new_animeTitle}.png";
                animeId.Summary = summary;
                animeId.More = isThereMore;

                // write changes to database
                try
                {
                    service.Update();
                    SelectedAnime = animeId;
                }
                catch (Exception)
                {
                    Debug.WriteLine($"Error updating {oldAnimeTitle}");
                    return false;
                }
                return true;
            }
            return false;
        }
        public bool UpdateWatchlist(string animeTitle, int? rating, string more, string watch, int? episodeWatched, DateTime? upToDate)
        {
            var animeId = service.GetAnimeByTitle(animeTitle);

            if (animeId == null)
            {
                Debug.WriteLine($"Anime: {animeTitle} was not found");
                return false;
            }
            animeId.Rating = rating;
            animeId.More = more;
            animeId.Watch = watch;
            if (episodeWatched > animeId.Episode) episodeWatched = animeId.Episode;
            animeId.EpisodeWatched = episodeWatched;
            animeId.UpToDate = upToDate;

            // write changes to database
            try
            {
                service.Update();
                SelectedAnime = animeId;
            }
            catch (Exception)
            {
                Debug.WriteLine($"Error updating {animeTitle}");
                return false;
            }

            return true;
        }
        
        public bool DeleteAnime(string animeTitle)
        {
            var animeId = service.GetAnimeByTitle(animeTitle);
            if (animeId == null)
            {
                Debug.WriteLine($"Anime {animeTitle} not found");
                return false;
            }

            service.Delete(animeId);
            return true;
        }

        #region Faulty CreateOrUpdateAnime
        public int CreateOrUpdateAnime(string animeTitle, string newAnimeTitle, int? episode, int? releaseYear,
        string status, int? rank, string summary, string more)
        {
            var new_animeTitle = char.ToUpper(newAnimeTitle[0]) + newAnimeTitle.Substring(1).ToLower();

            var animeId = service.GetAnimeByTitle(animeTitle);
            string isItCompelete, isThereMore;

            if (status.ToLower().Contains("n") || status.ToLower().Contains("g") || status.ToLower().Contains("i"))
                isItCompelete = "Ongoing";
            else if (status.ToLower().Contains("c") || status.ToLower().Contains("m") || status.ToLower().Contains("p") || status.ToLower().Contains("l") || status.ToLower().Contains("e") || status.ToLower().Contains("t") || status.ToLower().Contains("d"))
                isItCompelete = "Completed";
            else
                isItCompelete = null;

            if (more.ToLower().Contains("y") || more.ToLower().Contains("e") || more.ToLower().Contains("s"))
                isThereMore = "Yes";
            else if (more.ToLower().Contains("n") || more.ToLower().Contains("o"))
                isThereMore = "No";
            else
                isThereMore = null;


            if (animeId == null)
            {
                CreateAnime(new_animeTitle, episode, releaseYear, isItCompelete, rank, summary, isThereMore);
                return 1;
            }
            else
            {
                var old_animeTitle = char.ToUpper(animeTitle[0]) + animeTitle.Substring(1).ToLower();
                UpdateAnime(old_animeTitle, new_animeTitle, episode, releaseYear, isItCompelete, rank, summary, isThereMore);
                return 2;
            }
        }
        #endregion

        private string IsItCompelete(string status)
        {
            if (status.ToLower().Contains("n") || status.ToLower().Contains("g") || status.ToLower().Contains("i"))
                return "Ongoing";
            else if (status.ToLower().Contains("c") || status.ToLower().Contains("m") || status.ToLower().Contains("p") || status.ToLower().Contains("l") || status.ToLower().Contains("e") || status.ToLower().Contains("t") || status.ToLower().Contains("d"))
                return "Completed";
            else
                return null;
        }

        private string IsThereMore(string more)
        {
            if (more.ToLower().Contains("y") || more.ToLower().Contains("e") || more.ToLower().Contains("s"))
                return "Yes";
            else if (more.ToLower().Contains("n") || more.ToLower().Contains("o"))
                return "No";
            else
                return null;
        }
    }
}