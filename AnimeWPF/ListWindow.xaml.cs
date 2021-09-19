using AnimeBusiness;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace AnimeWPF
{
    /// <summary>
    /// Interaction logic for ListWindow.xaml
    /// </summary>
    public partial class ListWindow : Window
    {
        private AnimeManager _animeManager = new AnimeManager();
        private int? _filter = null;
        public ListWindow()
        {
            InitializeComponent();
            PopulateListBox();
        }

        private void ButtonAnime_Click(object sender, RoutedEventArgs e)
        {
            MainWindow startPage = new MainWindow();
            startPage.Show();
            this.Close();
        }
        
        private string Date(DateTime? lastWatched)
        {
            if (lastWatched == null) return "";
            
            var difference = DateTime.Now - lastWatched;
            //TimeSpan
            //TimeSpan(int hours, int minutes, int seconds)
            //TimeSpan(int days, int hours, int minutes, int seconds)
            //TimeSpan(int days, int hours, int minutes, int seconds, int milliseconds)
            TimeSpan duration = new TimeSpan(7, 0, 0, 0);

            if (difference <= duration) return "/Background/UpDate.png";
            else return "/Background/DownDate.png";
        }
        private string Rate(int? rate)
        {
            return rate == 5 ? "/Background/5Star.png" :
                   rate == 4 ? "/Background/4Star.png" :
                   rate == 3 ? "/Background/3Star.png" :
                   rate == 2 ? "/Background/2Star.png" :
                   rate == 1 ? "/Background/1Star.png" : "";
        }
        private string Status(string status)
        {
            return status == "Ongoing" ? "/Background/Ongoing.png" :
                   status == "Completed" ? "/Background/Completed.png" : "";
        }


        private void Picture(string picture)
        {
            Uri resourceUri = new Uri(picture, UriKind.Relative);
            imgDynamic.Source = new BitmapImage(resourceUri);
        }
        private void UpToDatePicture(DateTime? lastWatched)
        {
            string picture;
            var difference = DateTime.Now - lastWatched;
            TimeSpan duration = new TimeSpan(7, 0, 0, 0);

            if (difference <= duration) picture = "/Background/Tick.png";
            else if (difference > duration) picture = "/Background/Cross.png";
            else picture = "";

            Uri resourceUri = new Uri(picture, UriKind.Relative);
            UpToDate.Source = new BitmapImage(resourceUri);
        }


        private void animeTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (animeTable.SelectedItem != null)
            {
                var animeName = animeTable.SelectedItem.ToString();
                _animeManager.SetSelectedAnimeByTitle(animeName);

                //Trace.WriteLineIf(ListBoxAnimeTitles.SelectedItem.ToString().Contains("BLOG"), $"BLOG was selected");
                PopulateFields();
            }
        }
        private void PopulateListBox()
        {
            var lists = new List<WPFTable>();
            var allAnimeList = _animeManager.RetrieveSpecificAnime(_filter);
            foreach (var item in allAnimeList)
            {
                lists.Add(new WPFTable()
                {
                    Date = Date(item.UpToDate),
                    Title = item.AnimeName,
                    Episode = item.EpisodeWatched.ToString(),
                    Watch = item.Watch,
                    Ratings = Rate(item.Rating),
                    Status = Status(item.Status),
                    More = item.More
                });
            }
            animeTable.ItemsSource = lists;
            NumberToWatch.Text = _animeManager.RetrieveSpecificAnime(1).Count.ToString();
            NumberWatching.Text = _animeManager.RetrieveSpecificAnime(2).Count.ToString();
            NumberWatched.Text = _animeManager.RetrieveSpecificAnime(3).Count.ToString();
            NumberAll.Text = _animeManager.RetrieveSpecificAnime(0).Count.ToString();
        }
        private void PopulateFields()
        {
            if (_animeManager.SelectedAnime != null)
            {
                TextEpisode.Text = _animeManager.SelectedAnime.EpisodeWatched.ToString();
                TextDetails.Text = $"Title: {_animeManager.SelectedAnime.AnimeName}" +
                                    $"\nRank: {_animeManager.SelectedAnime.Rank}" +
                                    $"\nEpisodes: {_animeManager.SelectedAnime.Episode}" +
                                    $"\nYear: {_animeManager.SelectedAnime.ReleaseYear}" +
                                    $"\nStatus: {_animeManager.SelectedAnime.Status}";
                TextSummary.Text = $"Summary\n\n{_animeManager.SelectedAnime.Summary}";

                Picture(_animeManager.SelectedAnime.Picture);
                UpToDatePicture(_animeManager.SelectedAnime.UpToDate);
            }
        }
        private void RefreshPage()
        {
            animeTable.ItemsSource = null;
            PopulateListBox();
            PopulateFields();
        }

        private void TextSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextSearch.Text != null || TextSearch.Text == "")
            {
                var lists = new List<WPFTable>();
                var allAnimeList = _animeManager.RetrieveSearchedAnime(TextSearch.Text);
                foreach (var item in allAnimeList)
                {
                    lists.Add(new WPFTable()
                    {
                        Date = Date(item.UpToDate),
                        Title = item.AnimeName,
                        Watch = item.Watch,
                        Ratings = Rate(item.Rating),
                        Status = Status(item.Status),
                        More = item.More
                    });
                }
                animeTable.ItemsSource = lists;
            }
        }

        #region Ratings
        private void UpdateRate(int? rate)
        {
            _animeManager.UpdateWatchlist(
                  animeTitle: _animeManager.SelectedAnime.AnimeName,
                  rating: rate,
                  more: _animeManager.SelectedAnime.More,
                  watch: _animeManager.SelectedAnime.Watch,
                  episodeWatched: _animeManager.SelectedAnime.EpisodeWatched,
                  upToDate: _animeManager.SelectedAnime.UpToDate);

            RefreshPage();
            PopulateListBox();
        }
        private void ButtonRateDelete_Click(object sender, RoutedEventArgs e)
        {
            UpdateRate(null);
        }
        private void ButtonRate1_Click(object sender, RoutedEventArgs e)
        {
            UpdateRate(1);
        }
        private void ButtonRate2_Click(object sender, RoutedEventArgs e)
        {
            UpdateRate(2);
        }
        private void ButtonRate3_Click(object sender, RoutedEventArgs e)
        {
            UpdateRate(3);
        }
        private void ButtonRate4_Click(object sender, RoutedEventArgs e)
        {
            UpdateRate(4);
        }
        private void ButtonRate5_Click(object sender, RoutedEventArgs e)
        {
            UpdateRate(5);
        }
        #endregion

        #region Watchings
        private void UpdateWatch(string watch)
        {
            _animeManager.UpdateWatchlist(
                  animeTitle: _animeManager.SelectedAnime.AnimeName,
                  rating: _animeManager.SelectedAnime.Rating,
                  more: _animeManager.SelectedAnime.More,
                  watch: watch,
                  episodeWatched: _animeManager.SelectedAnime.EpisodeWatched,
                  upToDate: _animeManager.SelectedAnime.UpToDate);

            RefreshPage();
            PopulateListBox();
        }
        private void ButtonWatchDelete_Click(object sender, RoutedEventArgs e)
        {
            UpdateWatch(null);
        }
        private void ButtonToWatchSet_Click(object sender, RoutedEventArgs e)
        {
            UpdateWatch("To Watch");
        }
        private void ButtonWatchingSet_Click(object sender, RoutedEventArgs e)
        {
            UpdateWatch("Watching");
        }
        private void ButtonWatchedSet_Click(object sender, RoutedEventArgs e)
        {
            UpdateWatch("Watched");
        }
        #endregion

        #region Episodes
        private void UpdateEpisode(int? episode)
        {
            _animeManager.UpdateWatchlist(
                  animeTitle: _animeManager.SelectedAnime.AnimeName,
                  rating: _animeManager.SelectedAnime.Rating,
                  more: _animeManager.SelectedAnime.More,
                  watch: _animeManager.SelectedAnime.Watch,
                  episodeWatched: episode,
                  upToDate: _animeManager.SelectedAnime.UpToDate);

            RefreshPage();
            TextEpisode.Text = _animeManager.SelectedAnime.EpisodeWatched.ToString();
        }
        private void ButtonEpisodeUpdate_Click(object sender, RoutedEventArgs e)
        {
            var text = TextEpisode.Text.Replace(" ", "");

            if (text == "" || text == null)
                UpdateEpisode(null);
            else
            {
                int episode;
                bool ep = int.TryParse(text, out episode);
                UpdateEpisode(episode);
            }
        }
        private void ButtonEpisodeUp_Click(object sender, RoutedEventArgs e)
        {
            var text = TextEpisode.Text.Replace(" ", "");
            int num;

            if (text == "" || text == null || text == "0")
            {
                UpdateEpisode(1);
                num = 1;
            }
            else
            {
                int episode;
                bool ep = int.TryParse(text, out episode);
                num = episode + 1;
                UpdateEpisode(num);
            }
        }
        private void ButtonEpisodeDown_Click(object sender, RoutedEventArgs e)
        {
            var text = TextEpisode.Text.Replace(" ", "");
            int num;

            if (text == "" || text == null || text == "0" || text.Contains("-"))
            {
                UpdateEpisode(0);
                num = 0;
            }  
            else
            {
                int episode;
                bool ep = int.TryParse(text, out episode);
                num = episode - 1;
                UpdateEpisode(num);
            }
        }
        #endregion

        #region Up to Date
        private void UpdateDate(DateTime? dateTime)
        {
            _animeManager.UpdateWatchlist(
                  animeTitle: _animeManager.SelectedAnime.AnimeName,
                  rating: _animeManager.SelectedAnime.Rating,
                  more: _animeManager.SelectedAnime.More,
                  watch: _animeManager.SelectedAnime.Watch,
                  episodeWatched: _animeManager.SelectedAnime.EpisodeWatched,
                  upToDate: dateTime);

            RefreshPage();
            PopulateListBox();
        }
        private void ButtonUpToDateDelete_Click(object sender, RoutedEventArgs e)
        {
            UpdateDate(null);
        }
        private void ButtonUpToDate_Click(object sender, RoutedEventArgs e)
        {
            UpdateDate(DateTime.Now);
        }
        #endregion

        #region More
        private void UpdateMore(string more)
        {
            _animeManager.UpdateWatchlist(
                  animeTitle: _animeManager.SelectedAnime.AnimeName,
                  rating: _animeManager.SelectedAnime.Rating,
                  more: more,
                  watch: _animeManager.SelectedAnime.Watch,
                  episodeWatched: _animeManager.SelectedAnime.EpisodeWatched,
                  upToDate: _animeManager.SelectedAnime.UpToDate);

            RefreshPage();
            PopulateListBox();
        }
        private void ButtonMoreDelete_Click(object sender, RoutedEventArgs e)
        {
            UpdateMore(null);
        }
        private void ButtonMoreYes_Click(object sender, RoutedEventArgs e)
        {
            UpdateMore("Yes");
        }
        private void ButtonMoreMaybe_Click(object sender, RoutedEventArgs e)
        {
            UpdateMore("Maybe");
        }
        private void ButtonMoreNo_Click(object sender, RoutedEventArgs e)
        {
            UpdateMore("No");
        }
        #endregion

        #region Filters
        private void ButtonFilterReset_Click(object sender, RoutedEventArgs e)
        {
            _filter = null;
            TextEpisode.Text = "";
            TextDetails.Text = "";
            TextSummary.Text = "";
            TextSearch.Text = "";
            PopulateListBox();
            Picture("");
            UpToDatePicture(null);
        }
        private void ButtonFilterRate_Click(object sender, RoutedEventArgs e)
        {
            _filter = 4;
            TextEpisode.Text = "";
            TextDetails.Text = "";
            TextSummary.Text = "";
            TextSearch.Text = "";
            PopulateListBox();
            Picture("");
            UpToDatePicture(null);
        }
        private void ButtonFilterMore_Click(object sender, RoutedEventArgs e)
        {
            _filter = 5;
            TextEpisode.Text = "";
            TextDetails.Text = "";
            TextSummary.Text = "";
            TextSearch.Text = "";
            PopulateListBox();
            Picture("");
            UpToDatePicture(null);
        }
        private void ButtonFilterToWatch_Click(object sender, RoutedEventArgs e)
        {
            _filter = 1;
            TextEpisode.Text = "";
            TextDetails.Text = "";
            TextSummary.Text = "";
            TextSearch.Text = "";
            PopulateListBox();
            Picture("");
            UpToDatePicture(null);
        }
        private void ButtonFilterWatching_Click(object sender, RoutedEventArgs e)
        {
            _filter = 2;
            TextEpisode.Text = "";
            TextDetails.Text = "";
            TextSummary.Text = "";
            TextSearch.Text = "";
            PopulateListBox();
            Picture("");
            UpToDatePicture(null);
        }
        private void ButtonFilterWatched_Click(object sender, RoutedEventArgs e)
        {
            _filter = 3;
            TextEpisode.Text = "";
            TextDetails.Text = "";
            TextSummary.Text = "";
            TextSearch.Text = "";
            PopulateListBox();
            Picture("");
            UpToDatePicture(null);
        }

        #endregion

        
    }
}
