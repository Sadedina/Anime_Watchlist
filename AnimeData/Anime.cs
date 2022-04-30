using System;

#nullable disable

namespace AnimeData
{
    public partial class Anime
    {
        public int AnimeId { get; set; }
        public string AnimeName { get; set; }
        public int? Episode { get; set; }
        public int? ReleaseYear { get; set; }
        public string Status { get; set; }
        public int? Rank { get; set; }
        public string Picture { get; set; }
        public string Summary { get; set; }
        public string More { get; set; }
        public int? Rating { get; set; }
        public string Watch { get; set; }
        public int? EpisodeWatched { get; set; }
        public DateTime? UpToDate { get; set; }
    }
}