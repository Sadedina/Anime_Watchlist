using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using AnimeBusiness;
using AnimeData;

namespace AnimeWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AnimeManager _animeManager = new AnimeManager();
        private string _oldAnimeName = "";
        private List<Anime> _allAnime;
        public MainWindow()
        {
            InitializeComponent();
            PopulateListBox();
            _allAnime = _animeManager.RetrieveAllAnime();
        }
        private void ButtonWatchlist_Click(object sender, RoutedEventArgs e)
        {
            ListWindow startPage = new ListWindow();
            startPage.Show();
            this.Close();
        }
        private void PopulateListBox()
        {
            ListBoxAnimeTitles.Items.Clear();
            //EmptyFields();
            //Picture("");

            //ListBoxAnimeTitles.ItemsSource = _animeManager.RetrieveAllAnime();

            var list = _animeManager.RetrieveAllAnime();
            foreach (var item in list)
                ListBoxAnimeTitles.Items.Add(item.AnimeName);
        }
        private void PopulateListBox(string search)
        {
            if (search != null || search == "")
            {
                ListBoxAnimeTitles.Items.Clear();
                //EmptyFields();
                //Picture("");

                var list = _animeManager.RetrieveSearchedAnime(search);
                foreach (var item in list)
                    ListBoxAnimeTitles.Items.Add(item.AnimeName);
                //foreach (var item in _allAnime)
                //{
                //    if(item.AnimeName.Contains(search))
                //        ListBoxAnimeTitles.Items.Add(item.AnimeName);
                //}
            }
        }
        private void EmptyFields()
        {
            TextId.Clear();
            TextPicture.Clear();
            TextAnimeName.Clear();
            TextEpisode.Clear();
            TextReleaseYear.Clear();
            TextStatus.Clear();
            TextRank.Clear();
            TextMore.Clear();
            TextSummary.Clear();
            TextSearch.Clear();
        }
        private void Picture(string picture)
        {
            Uri resourceUri = new Uri(picture, UriKind.Relative);
            imgDynamic.Source = new BitmapImage(resourceUri);
        }
        private void PopulateFields()
        {
            if (_animeManager.SelectedAnime != null)
            {
                TextId.Text = _animeManager.SelectedAnime.AnimeId.ToString();
                TextPicture.Text = _animeManager.SelectedAnime.Picture.Substring(8);
                TextAnimeName.Text = _animeManager.SelectedAnime.AnimeName;
                TextEpisode.Text = _animeManager.SelectedAnime.Episode.ToString();
                TextReleaseYear.Text = _animeManager.SelectedAnime.ReleaseYear.ToString();
                TextStatus.Text = _animeManager.SelectedAnime.Status;
                TextRank.Text = _animeManager.SelectedAnime.Rank.ToString();
                TextMore.Text = _animeManager.SelectedAnime.More;
                TextSummary.Text = _animeManager.SelectedAnime.Summary;

                Picture(_animeManager.SelectedAnime.Picture);
                _oldAnimeName = TextAnimeName.Text;
            }
        }

        private void ListBoxProfile_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBoxAnimeTitles.SelectedItem != null)
            {
                _animeManager.SetSelectedAnimeByTitle(ListBoxAnimeTitles.SelectedItem.ToString());
                //Trace.WriteLineIf(ListBoxAnimeTitles.SelectedItem.ToString().Contains("BLOG"), $"BLOG was selected");
                PopulateFields();
            }
        }
        private void TextSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            PopulateListBox(TextSearch.Text);
        }

        private void ButtonCreate_Click(object sender, RoutedEventArgs e)
        {
            if (TextAnimeName.Text != null || TextAnimeName.Text == "")
            {
                // Converting String to Integer
                int converted_episode, converted_releaseYear, converted_rank;
                bool ep = int.TryParse(TextEpisode.Text, out converted_episode);
                bool rel = int.TryParse(TextReleaseYear.Text, out converted_releaseYear);
                bool ran = int.TryParse(TextRank.Text, out converted_rank);

                /*CreateOrUpdateAnime(string animeTitle, string newAnimeTitle, int? episode, int? releaseYear,
                * string status, int? rank, string summary, string more
                */
                _animeManager.CreateAnime(
                  animeTitle: TextAnimeName.Text,
                  episode: converted_episode,
                  releaseYear: converted_releaseYear,
                  status: TextStatus.Text,
                  rank: converted_rank,
                  summary: TextSummary.Text,
                  more: TextMore.Text);
            }

            // Refresh Data
            _allAnime = _animeManager.RetrieveAllAnime();
            PopulateListBox();
            _animeManager.SetSelectedAnimeByTitle(TextAnimeName.Text);
            EmptyFields();
            PopulateFields();
        }
        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (TextAnimeName.Text != null || TextAnimeName.Text == "")
            {
                // Converting String to Integer
                int converted_episode, converted_releaseYear, converted_rank;
                bool ep = int.TryParse(TextEpisode.Text, out converted_episode);
                bool rel = int.TryParse(TextReleaseYear.Text, out converted_releaseYear);
                bool ran = int.TryParse(TextRank.Text, out converted_rank);

                /*CreateOrUpdateAnime(string animeTitle, string newAnimeTitle, int? episode, int? releaseYear,
                * string status, int? rank, string summary, string more
                */
                _animeManager.UpdateAnime(
                  oldAnimeTitle: _oldAnimeName,
                  newAnimeTitle: TextAnimeName.Text,
                  episode: converted_episode,
                  releaseYear: converted_releaseYear,
                  status: TextStatus.Text,
                  rank: converted_rank,
                  summary: TextSummary.Text,
                  more: TextMore.Text);
            }

            // Refresh Data
            _allAnime = _animeManager.RetrieveAllAnime();
            PopulateListBox();
            EmptyFields();
            PopulateFields();
        }
        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxAnimeTitles.SelectedItem != null)
            {
                var anime = ListBoxAnimeTitles.SelectedItem.ToString();

                MessageBoxResult result = MessageBox.Show($"Would you like to DELETE\n{anime}?", "DELETE", MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        _animeManager.DeleteAnime(anime);

                        // Refresh Data
                        _allAnime = _animeManager.RetrieveAllAnime();
                        PopulateListBox();
                        EmptyFields();
                        Picture("");
                        break;
                    case MessageBoxResult.No:
                        break;
                }
            }
           
            //if (TextAnimeName.Text != null || TextAnimeName.Text == "") _animeManager.DeleteAnime(TextAnimeName.Text);  
        }
        private void ButtonReset_Click(object sender, RoutedEventArgs e)
        {
            TextSearch.Clear();
            _allAnime = _animeManager.RetrieveAllAnime();
            ListBoxAnimeTitles.SelectedItem = null;
            PopulateListBox();
            EmptyFields();
            Picture("");
        }
    }
}
