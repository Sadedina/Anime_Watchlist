# Code

Package Manager Console

```c#
// https://docs.microsoft.com/en-us/ef/core/cli/powershell

// Scaffold-DbContext '<Source code>' Microsoft.EntityFrameworkCore.SqlServer
// Add-Migration <InitialCreate>
// Remove-Migration
// Update-database (update db schema base on the last migration snapshot_)
// Script-migration (creates SQL script using migration snapshot_)
```

Anime Manager

```c#
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AnimeData;

namespace AnimeBusiness
{
    class AnimeManager
    {
        public Anime SelectedAnime { get; set; }
        public List<Anime> RetrieveRatedAnime()
        {
            var list = new List<Anime>();
            using (var db = new SammListContext())
            {
                var queryRatedAnime =
                    from a in db.Animes 
                    orderby a.Rating descending
                    select a;

                foreach (var a in queryRatedAnime)
                {
                    list.Add(a);
                }

                return list;
            }
        }

        public List<Anime> RetrieveWatchedAnime(int watched)
        {
            var list = new List<Anime>();
            if(watched == 1)
            {
                using (var db = new SammListContext())
                {
                    var queryRatedAnime =
                        from a in db.Animes
                        where a.Watch == "ToWatch"
                        select a;

                    foreach (var a in queryRatedAnime)
                    {
                        list.Add(a);
                    }

                    return list;
                }
            }
            else if (watched == 2)
            {
                using (var db = new SammListContext())
                {
                    var queryRatedAnime =
                        from a in db.Animes
                        where a.Watch == "Watching"
                        select a;

                    foreach (var a in queryRatedAnime)
                    {
                        list.Add(a);
                    }

                    return list;
                }
            }
            else if (watched == 3)
            {
                using (var db = new SammListContext())
                {
                    var queryRatedAnime =
                        from a in db.Animes
                        where a.Watch == "Watched"
                        select a;

                    foreach (var a in queryRatedAnime)
                    {
                        list.Add(a);
                    }

                    return list;
                }
            }
            else
            {
                using (var db = new SammListContext())
                {
                    var queryRatedAnime =
                        from a in db.Animes
                        select a;

                    foreach (var a in queryRatedAnime)
                    {
                        list.Add(a);
                    }

                    return list;
                }
            }

        }

        public void CreateOrUpdateAnime(string animeTitle, string newAnimeTitle, int? episode, int? releaseYear, 
            string status, int? rank, string summary, int? rating, string more, string watch, string upToDate)
        {
            using (var db = new SammListContext())
            {
                var animeId = db.Animes.Where(a => a.AnimeName == animeTitle).FirstOrDefault();

                if (animeId == null)
                {
                    CreateAnime(animeTitle, episode, releaseYear, status, rank, summary, more);
                }
                else
                {
                    UpdateAnime(animeTitle, newAnimeTitle, episode, releaseYear, status, rank, summary, more);
                }
            }
        }

        public void CreateAnime(string animeTitle, int? episode, int? releaseYear,
            string status, int? rank, string summary, string more)
        {
            using (var db = new SammListContext())
            {
                var animeId = db.Animes.Where(a => a.AnimeName == animeTitle).FirstOrDefault();

                var newAnimeDetail = new Anime()
                {
                    AnimeName = animeTitle,
                    Episode = episode,
                    ReleaseYear = releaseYear,
                    Status = status,
                    Rank = rank,
                    Summary = summary,
                    More = more
                };
                db.Animes.Add(newAnimeDetail);
                db.SaveChanges();
            }
        }

        public bool UpdateAnime(string oldAnimeTitle, string newAnimeTitle, int? episode, int? releaseYear,
        string status, int? rank, string summary, string more)
        {
            using (var db = new SammListContext())
            {
                var animeId = db.Animes.Where(a => a.AnimeName == oldAnimeTitle).FirstOrDefault();

                if (animeId == null)
                {
                    Debug.WriteLine($"Anime: {oldAnimeTitle} was not found");
                    return false;
                }

                animeId.AnimeName = newAnimeTitle;
                animeId.Episode = episode;
                animeId.ReleaseYear = releaseYear;
                animeId.Status = status;
                animeId.Rank = rank;
                animeId.Summary = summary;
                animeId.More = more;

                // write changes to database
                try
                {
                    db.SaveChanges();
                    SelectedAnime = animeId;
                }
                catch (Exception)
                {
                    Debug.WriteLine($"Error updating {oldAnimeTitle}");
                    return false;
                }
            }
            return true;
        }

        public bool UpdateWatchlist(string animeTitle, int? rating, string more, string watch, DateTime upToDate)
        {
            using (var db = new SammListContext())
            {
                var animeId = db.Animes.Where(a => a.AnimeName == animeTitle).FirstOrDefault();

                if (animeId == null)
                {
                    Debug.WriteLine($"Anime: {animeTitle} was not found");
                    return false;
                }

                animeId.Rating = rating;
                animeId.More = more;
                animeId.Watch = watch;
                animeId.UpToDate = upToDate;
                
                // write changes to database
                try
                {
                    db.SaveChanges();
                    SelectedAnime = animeId;
                }
                catch (Exception)
                {
                    Debug.WriteLine($"Error updating {animeTitle}");
                    return false;
                }
            }
            return true;
        }

        public bool Delete(string animeTitle)
        {
            if (animeTitle != null)
            {
                using (var db = new SammListContext())
                {
                    var animeId = db.Animes.Where(a => a.AnimeName == animeTitle).FirstOrDefault();

                    db.Animes.RemoveRange(animeId);
                    db.SaveChanges();
                }
            }
            return true;
        }
    }
}

```

Programme

```c#
/*
             * string AnimeName
             * int? Episode
             * int? ReleaseYear
             * string Status
             * int? Rank
             * string Summary
             * int? Rating
             * string More
             * string Watch
             * string UpToDate
             */

/*
 * string animeName
 * int? episode
 * int? releaseYear
 * string status
 * int? rank
 * string summary
 * int? rating
 * string more
 * string watch
 * string upToDate
 */
```



