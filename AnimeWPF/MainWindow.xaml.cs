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
        private AnimeManager animeManager = new AnimeManager();
        private string oldAnimeName = "";
        private List<Anime> allAnime;
        public MainWindow()
        {
            InitializeComponent();
            PopulateListBox();
            allAnime = animeManager.RetrieveAllAnime();
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

            var list = animeManager.RetrieveAllAnime();
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

                var list = animeManager.RetrieveSearchedAnime(search);
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
            if (animeManager.SelectedAnime != null)
            {
                TextId.Text = animeManager.SelectedAnime.AnimeId.ToString();
                TextPicture.Text = animeManager.SelectedAnime.Picture.Substring(8);
                TextAnimeName.Text = animeManager.SelectedAnime.AnimeName;
                TextEpisode.Text = animeManager.SelectedAnime.Episode.ToString();
                TextReleaseYear.Text = animeManager.SelectedAnime.ReleaseYear.ToString();
                TextStatus.Text = animeManager.SelectedAnime.Status;
                TextRank.Text = animeManager.SelectedAnime.Rank.ToString();
                TextMore.Text = animeManager.SelectedAnime.More;
                TextSummary.Text = animeManager.SelectedAnime.Summary;

                Picture(animeManager.SelectedAnime.Picture);
                oldAnimeName = TextAnimeName.Text;
            }
        }

        private void ListBoxProfile_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBoxAnimeTitles.SelectedItem != null)
            {
                animeManager.SetSelectedAnimeByTitle(ListBoxAnimeTitles.SelectedItem.ToString());
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
                animeManager.CreateAnime(
                  animeTitle: TextAnimeName.Text,
                  episode: converted_episode,
                  releaseYear: converted_releaseYear,
                  status: TextStatus.Text,
                  rank: converted_rank,
                  summary: TextSummary.Text,
                  more: TextMore.Text);
            }

            // Refresh Data
            allAnime = animeManager.RetrieveAllAnime();
            PopulateListBox();
            animeManager.SetSelectedAnimeByTitle(TextAnimeName.Text);
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
                animeManager.UpdateAnime(
                  oldAnimeTitle: oldAnimeName,
                  newAnimeTitle: TextAnimeName.Text,
                  episode: converted_episode,
                  releaseYear: converted_releaseYear,
                  status: TextStatus.Text,
                  rank: converted_rank,
                  summary: TextSummary.Text,
                  more: TextMore.Text);
            }

            // Refresh Data
            allAnime = animeManager.RetrieveAllAnime();
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
                        animeManager.DeleteAnime(anime);

                        // Refresh Data
                        allAnime = animeManager.RetrieveAllAnime();
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
            allAnime = animeManager.RetrieveAllAnime();
            ListBoxAnimeTitles.SelectedItem = null;
            PopulateListBox();
            EmptyFields();
            Picture("");
        }
    }
}