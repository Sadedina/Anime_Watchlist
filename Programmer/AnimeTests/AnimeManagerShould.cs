using System;
using AnimeBusiness;
using AnimeData;
using AnimeData.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace AnimeTests
{
    public class AnimeServiceTests
    {
        private AnimeManager service;

        [Test]
        public void ThrowException_WhenAnimeServiceIsNull()
        {
            Assert.That(() => new AnimeManager(null), Throws.ArgumentException);
            Assert.That(() => new AnimeManager(null), Throws.TypeOf< ArgumentException>()
                .With.Message.Contains("AnimeService object cannot be null"));
        }

        [Test]
        public void BeAbleToConstruct()
        {
            // Arrange
            var mockAnimeService = new Mock<IAnimeService>();
            service = new AnimeManager(mockAnimeService.Object);

            Assert.That(service, Is.InstanceOf<AnimeManager>());
        }

        //===========================================       Select Anime By Name      ===========================================
        [Test]
        public void GivenAnAnimeTitle_SelectAnimeByName_DetailsAreCorrect()
        {
            // Arrange
            var mockAnimeService = new Mock<IAnimeService>();
            var originaleAnime = new Anime()
            {
                AnimeId = 1,
                AnimeName = "Tokyo Revengers",
                Episode = 24,
                ReleaseYear = 2021,
                Status = "Ongoing",
                Rank = null,
                Picture = "Tk.jpg",
                Summary = "The story is amazing",
                More = "No",
                Rating = 5,
                Watch = "Watching",
                EpisodeWatched = 3,
                UpToDate = new DateTime(2021, 03, 10, 8, 30, 52)
            };
            mockAnimeService.Setup(cs => cs.GetAnimeByTitle("Tokyo Revengers")).Returns(originaleAnime);
            service = new AnimeManager(mockAnimeService.Object);

            // Act
            var result = service.SelectAnimeByName("Tokyo Revengers");

            // Assert
            Assert.That(result, Is.InstanceOf<Anime>());
            Assert.That(result.Episode, Is.EqualTo(24));
            Assert.That(result.ReleaseYear, Is.EqualTo(2021));
            Assert.That(result.Status, Is.EqualTo("Ongoing"));
            Assert.That(result.Rank, Is.Null);
            Assert.That(result.Picture, Is.EqualTo("Tk.jpg"));
            Assert.That(result.Summary, Is.EqualTo("The story is amazing"));
            Assert.That(result.More, Is.EqualTo("No"));
            Assert.That(result.EpisodeWatched, Is.EqualTo(3));
            Assert.That(result.Rating, Is.EqualTo(5));
            Assert.That(result.Watch, Is.EqualTo("Watching"));
            Assert.That(result.UpToDate, Is.EqualTo(new DateTime(2021, 03, 10, 8, 30, 52)));
        }

        //===========================================       Retrieve Rated Anime      ===========================================
        [Test]
        [Ignore("Test broken")]
        public void GivenAnAnimeTitle_RetrieveRatedAnimee_DetailsAreCorrect()
        {
            // Arrange
            var mockAnimeService = new Mock<IAnimeService>();
            var result = service.RetrieveSpecificAnime(4).Count;
            var animeOne = new Anime()
            {
                AnimeId = 1,
                AnimeName = "Tokyo Revengers",
                Episode = 24,
                ReleaseYear = 2021,
                Status = "Ongoing",
                Rank = null,
                Picture = "Tk.jpg",
                Summary = "The story is amazing",
                More = "No",
                Rating = 5,
                Watch = "Watching",
                EpisodeWatched = 3,
                UpToDate = new DateTime(2021, 03, 10, 8, 30, 52)
            };
            mockAnimeService.Setup(cs => cs.GetAnimeByTitle("Tokyo Revengers")).Returns(animeOne);
            service = new AnimeManager(mockAnimeService.Object);

            // Act
            var expected = service.RetrieveSpecificAnime(4).Count;

            // Assert
            Assert.That(result, Is.EqualTo(expected - 1));
            //Assert.That(result.ReleaseYear, Is.EqualTo(2021));
            //Assert.That(result.Status, Is.EqualTo("Ongoing"));
            //Assert.That(result.Rank, Is.Null);
            //Assert.That(result.Picture, Is.EqualTo("Tk.jpg"));
            //Assert.That(result.Summary, Is.EqualTo("The story is amazing"));
            //Assert.That(result.More, Is.EqualTo("No"));
            //Assert.That(result.EpisodeWatched, Is.EqualTo(3));
            //Assert.That(result.Rating, Is.EqualTo(5));
            //Assert.That(result.Watch, Is.EqualTo("Watching"));
            //Assert.That(result.UpToDate, Is.EqualTo(new DateTime(2021, 03, 10, 8, 30, 52)));
        }

        //===========================================       Create      ===========================================
        #region
        [Test]
        public void CreateAnime_ReturnsTrue_GivenNewAnimeTitle()
        {
            // Arrange
            var mockAnimeService = new Mock<IAnimeService>();
            service = new AnimeManager(mockAnimeService.Object);

            // Act
            var result = service.CreateAnime(animeTitle: "Tokyo Revengers", episode: 24, releaseYear: 2021,
            status: "Ongoing", rank: null, summary: "The story is amazing", more: "No");

            // Assert
            Assert.That(result);
        }

        [Test]
        public void CreateAnime_ReturnsFalse_GivenAnimeTitleAlreadyInDatabase()
        {
            // Arrange
            var mockAnimeService = new Mock<IAnimeService>();
            var originaleAnime = new Anime()
            {
                AnimeId = 1,
                AnimeName = "Tokyo Revengers",
                Episode = 24,
                ReleaseYear = 2021,
                Status = "Ongoing",
                Rank = null,
                Picture = "Tk.jpg",
                Summary = "The story is amazing",
                More = "No",
                Rating = 5,
                Watch = "Watching",
                EpisodeWatched = 3,
                UpToDate = new DateTime(2021, 03, 10, 8, 30, 52)
            };
            mockAnimeService.Setup(a => a.GetAnimeByTitle("Tokyo Revengers")).Returns(originaleAnime);
            service = new AnimeManager(mockAnimeService.Object);

            // Act
            var result = service.CreateAnime(animeTitle: "Tokyo Revengers", episode: 24, releaseYear: 2021,
            status: "Ongoing", rank: null, summary: "The story is amazing", more: "No");

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        [Ignore("Test broken")]
        public void CreateAnime_CallsCreate_GivenNewAnime()
        {
            // Arrange
            var mockAnimeService = new Mock<IAnimeService>();
            // No SetUp required
            //mockAnimeService.Setup(a => a.GetAnimeByTitle(It.IsAny<string>())).Returns(new Anime());
            service = new AnimeManager(mockAnimeService.Object);

            // Act
            var result = service.CreateAnime(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(),
                It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(),
                It.IsAny<string>());

            // Assert
            mockAnimeService.Verify(a => a.Create(It.IsAny<Anime>()), Times.Once);
            mockAnimeService.VerifyAll();
        }

        [Test]
        public void CreateAnime_ReturnsFalse_WhenDatabaseExceptionIsThrown()
        {
            // Arrange
            var mockAnimeService = new Mock<IAnimeService>();
            mockAnimeService.Setup(cs => cs.GetAnimeByTitle("Tokyo Ghoul")).Returns(new Anime());
            mockAnimeService.Setup(cs => cs.Create((Anime)null)).Throws<DbUpdateConcurrencyException>();
            service = new AnimeManager(mockAnimeService.Object);

            // Act
            var result = service.CreateAnime(animeTitle: "Tokyo Ghoul", episode: It.IsAny<int>(),
                releaseYear: It.IsAny<int>(), status: It.IsAny<string>(), rank: null,
                summary: It.IsAny<string>(), more: It.IsAny<string>());

            // Assert
            Assert.That(result, Is.False);
        }
        #endregion
        //===========================================       Update Anime      ===========================================
        #region
        [Test]
        public void UpdateAnime_ReturnsTrue_GivenAnAnimeAnimeTitleToUpdate()
        {
            // Arrange
            var mockAnimeService = new Mock<IAnimeService>();
            var originaleAnime = new Anime()
            {
                AnimeId = 1,
                AnimeName = "Tokyo Revengers",
                Episode = 24,
                ReleaseYear = 2021,
                Status = "Ongoing",
                Rank = null,
                Picture = "Tk.jpg",
                Summary = "The story is amazing",
                More = "No",
                Rating = 5,
                Watch = "Watching",
                EpisodeWatched = 3,
                UpToDate = new DateTime(2021, 03, 10, 8, 30, 52)
            };
            mockAnimeService.Setup(a => a.GetAnimeByTitle("Tokyo Revengers")).Returns(originaleAnime);
            service = new AnimeManager(mockAnimeService.Object);

            // Act
            var result = service.UpdateAnime(oldAnimeTitle: "Tokyo Revengers", newAnimeTitle: "TK", episode: 1, releaseYear: 2000,
            status: "Completed", rank: null, summary: "The story is sucks", more: "Yes");

            // Assert
            Assert.That(result);
        }

        [Test]
        [Ignore("Test broken")]
        public void GivenAnAnimeTitle_UpdateAnime_DetailsAreCorrect()
        {
            // Arrange
            var mockAnimeService = new Mock<IAnimeService>();
            var originaleAnime = new Anime()
            {
                AnimeId = 1,
                AnimeName = "Tokyo Revengers",
                Episode = 24,
                ReleaseYear = 2021,
                Status = "Ongoing",
                Rank = null,
                Picture = "Tk.jpg",
                Summary = "The story is amazing",
                More = "No",
                Rating = 5,
                Watch = "Watching",
                EpisodeWatched = 3,
                UpToDate = new DateTime(2021, 03, 10, 8, 30, 52)
            };
            mockAnimeService.Setup(a => a.GetAnimeByTitle("Tokyo Revengers")).Returns(originaleAnime);
            service = new AnimeManager(mockAnimeService.Object);

            // Act
            var result = service.UpdateAnime(oldAnimeTitle: "Tokyo Revengers", newAnimeTitle: "TK", episode: 1, releaseYear: 2000,
            status: "Completed", rank: null, summary: "The story is sucks", more: "Yes");

            // Assert
            Assert.That(result, Is.InstanceOf<Boolean>());
            Assert.That(service.SelectedAnime.AnimeName, Is.EqualTo("TK"));
            Assert.That(service.SelectedAnime.Episode, Is.EqualTo(1));
            Assert.That(service.SelectedAnime.ReleaseYear, Is.EqualTo(2000));
            Assert.That(service.SelectedAnime.Status, Is.EqualTo("Completed"));
            Assert.That(service.SelectedAnime.Rank, Is.Null);
            Assert.That(service.SelectedAnime.Summary, Is.EqualTo("The story is sucks"));
            Assert.That(service.SelectedAnime.More, Is.EqualTo("Yes"));
            Assert.That(service.SelectedAnime.EpisodeWatched, Is.EqualTo(3));
            Assert.That(service.SelectedAnime.Rating, Is.EqualTo(5));
            Assert.That(service.SelectedAnime.Watch, Is.EqualTo("Watching"));
            Assert.That(service.SelectedAnime.UpToDate, Is.EqualTo(new DateTime(2021, 03, 10, 8, 30, 52)));
        }

        [Test]
        public void UpdateAnime_ReturnsFalse_GivenANewAnimeAnimeTitle()
        {
            // Arrange
            var mockAnimeService = new Mock<IAnimeService>();
            mockAnimeService.Setup(a => a.GetAnimeByTitle("Tokyo Revengers")).Returns((Anime)null);
            service = new AnimeManager(mockAnimeService.Object);

            // Act
            var result = service.UpdateAnime(oldAnimeTitle: "My Hero Academia", newAnimeTitle: It.IsAny<string>(), episode: It.IsAny<int>(), releaseYear: It.IsAny<int>(),
            status: It.IsAny<string>(), rank: null, summary: It.IsAny<string>(), more: It.IsAny<string>());

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void UpdateAnime_ReturnsFalse_WhenDatabaseExceptionIsThrown()
        {
            // Arrange
            var mockCustomerService = new Mock<IAnimeService>();
            mockCustomerService.Setup(cs => cs.GetAnimeByTitle("Tokyo Ghoul")).Returns(new Anime());
            mockCustomerService.Setup(cs => cs.Update()).Throws<DbUpdateConcurrencyException>();
            service = new AnimeManager(mockCustomerService.Object);

            // Act
            var result = service.UpdateAnime(oldAnimeTitle: "My Hero Academia", newAnimeTitle: It.IsAny<string>(), episode: It.IsAny<int>(), 
                releaseYear: It.IsAny<int>(), status: It.IsAny<string>(), rank: null, 
                summary: It.IsAny<string>(), more: It.IsAny<string>());

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        [Ignore("Test broken")]
        public void UpdateAnime_CallsUpdate_WhenGivenAnimeTitleAlreadyInDatabase()
        {
            // Arrange
            var mockCustomerService = new Mock<IAnimeService>();
            mockCustomerService.Setup(cs => cs.GetAnimeByTitle("Tokyo Ghoul")).Returns(new Anime());

            service = new AnimeManager(mockCustomerService.Object);

            // Act
            var result = service.UpdateAnime(oldAnimeTitle: "Tokyo Ghoul", newAnimeTitle: It.IsAny<string>(), episode: It.IsAny<int>(),
                releaseYear: It.IsAny<int>(), status: It.IsAny<string>(), rank: null,
                summary: It.IsAny<string>(), more: It.IsAny<string>());

            // Assert
            mockCustomerService.Verify(cs => cs.Update(), Times.Once);
        }

        [Test]
        public void UpdateAnime_ReturnsFalse_GivenAnAnimeAnimeTitleToUpdate_WithStrictBehaviour()
        {
            // Arrange
            var mockAnimeService = new Mock<IAnimeService>(MockBehavior.Strict);
            var originaleAnime = new Anime()
            {
                AnimeId = 1,
                AnimeName = "Tokyo Revengers",
                Episode = 24,
                ReleaseYear = 2021,
                Status = "Ongoing",
                Rank = null,
                Picture = "Tk.jpg",
                Summary = "The story is amazing",
                More = "No",
                Rating = 5,
                Watch = "Watching",
                EpisodeWatched = 3,
                UpToDate = new DateTime(2021, 03, 10, 8, 30, 52)
            };
            mockAnimeService.Setup(a => a.GetAnimeByTitle("Tokyo Revengers")).Returns(originaleAnime);
            //mockAnimeService.Setup(a => a.Update());
            service = new AnimeManager(mockAnimeService.Object);

            // Act
            var result = service.UpdateAnime(oldAnimeTitle: "Tokyo Revengers", newAnimeTitle: "TK", episode: 1, releaseYear: 2000,
            status: "Completed", rank: null, summary: "The story is sucks", more: "Yes");

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void UpdateAnime_ReturnsTrue_GivenAnAnimeAnimeTitleToUpdate_WithStrictBehaviour()
        {
            // Arrange
            var mockAnimeService = new Mock<IAnimeService>(MockBehavior.Strict);
            var originaleAnime = new Anime()
            {
                AnimeId = 1,
                AnimeName = "Tokyo Revengers",
                Episode = 24,
                ReleaseYear = 2021,
                Status = "Ongoing",
                Rank = null,
                Picture = "Tk.jpg",
                Summary = "The story is amazing",
                More = "No",
                Rating = 5,
                Watch = "Watching",
                EpisodeWatched = 3,
                UpToDate = new DateTime(2021, 03, 10, 8, 30, 52)
            };
            mockAnimeService.Setup(a => a.GetAnimeByTitle("Tokyo Revengers")).Returns(originaleAnime);
            mockAnimeService.Setup(a => a.Update());
            service = new AnimeManager(mockAnimeService.Object);

            // Act
            var result = service.UpdateAnime(oldAnimeTitle: "Tokyo Revengers", newAnimeTitle: "TK", episode: 1, releaseYear: 2000,
            status: "Completed", rank: null, summary: "The story is sucks", more: "Yes");

            // Assert
            Assert.That(result);
        }
        #endregion
        //===========================================       Update Watchlist      ===========================================
        #region
        [Test]
        public void UpdateWatchlist_ReturnsTrue_GivenAnAnimeTitleToUpdate()
        {
            // Arrange
            var mockAnimeService = new Mock<IAnimeService>();
            var originaleAnime = new Anime()
            {
                AnimeId = 1,
                AnimeName = "Tokyo Revengers",
                Episode = 24,
                ReleaseYear = 2021,
                Status = "Ongoing",
                Rank = null,
                Picture = "Tk.jpg",
                Summary = "The story is amazing",
                More = "No",
                Rating = 5,
                Watch = "Watching",
                EpisodeWatched = 3,
                UpToDate = new DateTime(2021, 03, 10, 8, 30, 52)
            };
            mockAnimeService.Setup(a => a.GetAnimeByTitle("Tokyo Revengers")).Returns(originaleAnime);
            service = new AnimeManager(mockAnimeService.Object);

            // Act
            var result = service.UpdateWatchlist(animeTitle: "Tokyo Revengers", rating: 5, more: "Yes", watch: "Watching",
            episodeWatched: 5, upToDate: new DateTime(2020, 03, 10, 8, 30, 52));

            // Assert
            Assert.That(result);
        }

        [Test]
        public void GivenAnAnimeTitle_UpdateWatchlist_DetailsAreCorrect()
        {
            // Arrange
            var mockAnimeService = new Mock<IAnimeService>();
            var originaleAnime = new Anime()
            {
                AnimeId = 1,
                AnimeName = "Tokyo Revengers",
                Episode = 24,
                ReleaseYear = 2021,
                Status = "Ongoing",
                Rank = null,
                Picture = "Tk.jpg",
                Summary = "The story is amazing",
                More = "No",
                Rating = 5,
                Watch = "Watching",
                EpisodeWatched = 3,
                UpToDate = new DateTime(2021, 03, 10, 8, 30, 52)
            };
            mockAnimeService.Setup(a => a.GetAnimeByTitle("Tokyo Revengers")).Returns(originaleAnime);
            service = new AnimeManager(mockAnimeService.Object);

            // Act
            var result = service.UpdateWatchlist(animeTitle: "Tokyo Revengers", rating: 2, more: "Yes", watch: "Watched",
            episodeWatched: 5, upToDate: new DateTime(2020, 03, 10, 8, 30, 52));

            // Assert
            Assert.That(result, Is.InstanceOf<Boolean>());
            Assert.That(service.SelectedAnime.AnimeName, Is.EqualTo("Tokyo Revengers"));
            Assert.That(service.SelectedAnime.Episode, Is.EqualTo(24));
            Assert.That(service.SelectedAnime.ReleaseYear, Is.EqualTo(2021));
            Assert.That(service.SelectedAnime.Status, Is.EqualTo("Ongoing"));
            Assert.That(service.SelectedAnime.Rank, Is.Null);
            Assert.That(service.SelectedAnime.Picture, Is.EqualTo("Tk.jpg"));
            Assert.That(service.SelectedAnime.Summary, Is.EqualTo("The story is amazing"));
            Assert.That(service.SelectedAnime.More, Is.EqualTo("Yes"));
            Assert.That(service.SelectedAnime.EpisodeWatched, Is.EqualTo(5));
            Assert.That(service.SelectedAnime.Rating, Is.EqualTo(2));
            Assert.That(service.SelectedAnime.Watch, Is.EqualTo("Watched"));
            Assert.That(service.SelectedAnime.UpToDate, Is.EqualTo(new DateTime(2020, 03, 10, 8, 30, 52)));
        }

        [Test]
        public void UpdateWatchlist_ReturnsFalse_GivenANewAnimeAnimeTitle()
        {
            // Arrange
            var mockAnimeService = new Mock<IAnimeService>();
            mockAnimeService.Setup(a => a.GetAnimeByTitle("Tokyo Revengers")).Returns((Anime)null);
            service = new AnimeManager(mockAnimeService.Object);

            // Act
            var result = service.UpdateWatchlist(animeTitle: "Tokyo Revengers", rating: 2, more: "Yes", watch: "Watched",
            episodeWatched: 5, upToDate: new DateTime(2020, 03, 10, 8, 30, 52));

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void UpdateWatchlist_ReturnsFalse_WhenDatabaseExceptionIsThrown()
        {
            // Arrange
            var mockCustomerService = new Mock<IAnimeService>();
            mockCustomerService.Setup(cs => cs.GetAnimeByTitle("Tokyo Ghoul")).Returns(new Anime());
            mockCustomerService.Setup(cs => cs.Update()).Throws<DbUpdateConcurrencyException>();
            service = new AnimeManager(mockCustomerService.Object);

            // Act
            var result = service.UpdateWatchlist(animeTitle: "Tokyo Revengers", rating: 2, more: "Yes", watch: "Watched",
            episodeWatched: 5, upToDate: new DateTime(2020, 03, 10, 8, 30, 52));

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void UpdateWatchlist_CallsUpdate_WhenGivenAnimeTitleAlreadyInDatabase()
        {
            // Arrange
            var mockCustomerService = new Mock<IAnimeService>();
            mockCustomerService.Setup(cs => cs.GetAnimeByTitle("Tokyo Ghoul")).Returns(new Anime());

            service = new AnimeManager(mockCustomerService.Object);

            // Act
            var result = service.UpdateWatchlist(animeTitle: "Tokyo Ghoul", rating: 2, more: "Yes", watch: "Watched",
            episodeWatched: 5, upToDate: new DateTime(2020, 03, 10, 8, 30, 52));

            // Assert
            mockCustomerService.Verify(cs => cs.Update(), Times.Once);
        }

        [Test]
        public void UpdateWatchlist_ReturnsFalse_GivenAnAnimeAnimeTitleToUpdate_WithStrictBehaviour()
        {
            // Arrange
            var mockAnimeService = new Mock<IAnimeService>(MockBehavior.Strict);
            var originaleAnime = new Anime()
            {
                AnimeId = 1,
                AnimeName = "Tokyo Revengers",
                Episode = 24,
                ReleaseYear = 2021,
                Status = "Ongoing",
                Rank = null,
                Picture = "Tk.jpg",
                Summary = "The story is amazing",
                More = "No",
                Rating = 5,
                Watch = "Watching",
                EpisodeWatched = 3,
                UpToDate = new DateTime(2021, 03, 10, 8, 30, 52)
            };
            mockAnimeService.Setup(a => a.GetAnimeByTitle("Tokyo Revengers")).Returns(originaleAnime);
            //mockAnimeService.Setup(a => a.Update());
            service = new AnimeManager(mockAnimeService.Object);

            // Act
            var result = service.UpdateWatchlist(animeTitle: "Tokyo Revengers", rating: 2, more: "Yes", watch: "Watched",
            episodeWatched: 5, upToDate: new DateTime(2020, 03, 10, 8, 30, 52));

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void UpdateWatchlist_ReturnsTrue_GivenAnAnimeAnimeTitleToUpdate_WithStrictBehaviour()
        {
            // Arrange
            var mockAnimeService = new Mock<IAnimeService>(MockBehavior.Strict);
            var originaleAnime = new Anime()
            {
                AnimeId = 1,
                AnimeName = "Tokyo Revengers",
                Episode = 24,
                ReleaseYear = 2021,
                Status = "Ongoing",
                Rank = null,
                Picture = "Tk.jpg",
                Summary = "The story is amazing",
                More = "No",
                Rating = 5,
                Watch = "Watching",
                EpisodeWatched = 3,
                UpToDate = new DateTime(2021, 03, 10, 8, 30, 52)
            };
            mockAnimeService.Setup(a => a.GetAnimeByTitle("Tokyo Revengers")).Returns(originaleAnime);
            mockAnimeService.Setup(a => a.Update());
            service = new AnimeManager(mockAnimeService.Object);

            // Act
            var result = service.UpdateWatchlist(animeTitle: "Tokyo Revengers", rating: 2, more: "Yes", watch: "Watched",
            episodeWatched: 5, upToDate: new DateTime(2020, 03, 10, 8, 30, 52));

            // Assert
            Assert.That(result);
        }
        #endregion
        //===========================================       Create Or Update      ===========================================
        #region
        [Test]
        public void CreateOrUpdateAnime_ReturnsOne_GivenAnAnimeTitleToCreate()
        {
            // Arrange
            var mockAnimeService = new Mock<IAnimeService>();
            service = new AnimeManager(mockAnimeService.Object);

            // Act
            var result = service.CreateOrUpdateAnime(animeTitle: "Tokyo Revengers", newAnimeTitle: "Tokyo Revengers", 
                episode: 24, releaseYear: 2021, status: "Ongoing", rank: null, 
                summary: "The story is amazing", more: "No");

            // Assert
            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void CreateOrUpdateAnime_ReturnsTwo_GivenAnAnimeAnimeTitleToUpdate()
        {
            // Arrange
            var mockAnimeService = new Mock<IAnimeService>();
            var originaleAnime = new Anime()
            {
                AnimeId = 1,
                AnimeName = "Tokyo Revengers",
                Episode = 24,
                ReleaseYear = 2021,
                Status = "Ongoing",
                Rank = null,
                Picture = "Tk.jpg",
                Summary = "The story is amazing",
                More = "No",
                Rating = 5,
                Watch = "Watching",
                EpisodeWatched = 3,
                UpToDate = new DateTime(2021, 03, 10, 8, 30, 52)
            };
            mockAnimeService.Setup(a => a.GetAnimeByTitle("Tokyo Revengers")).Returns(originaleAnime);
            service = new AnimeManager(mockAnimeService.Object);

            // Act
            var result = service.CreateOrUpdateAnime(animeTitle: "Tokyo Revengers", newAnimeTitle: "Tokyo Revengers",
                episode: 24, releaseYear: 2021, status: "Ongoing", rank: null,
                summary: "The story is amazing", more: "No");

            // Assert
            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        [Ignore("Test broken")]
        public void CreateOrUpdateAnime_CallsCreate_GivenNewAnime()
        {
            // Arrange
            var mockAnimeService = new Mock<IAnimeService>();
            // No SetUp required
            //mockAnimeService.Setup(a => a.GetAnimeByTitle(It.IsAny<string>())).Returns(new Anime());
            service = new AnimeManager(mockAnimeService.Object);

            // Act
            var result = service.CreateOrUpdateAnime(animeTitle: It.IsAny<string>(), newAnimeTitle: It.IsAny<string>(),
                episode: It.IsAny<int>(), releaseYear: It.IsAny<int>(), status: It.IsAny<string>(),
                rank: null, summary: It.IsAny<string>(), more: It.IsAny<string>());

            // Assert
            mockAnimeService.Verify(a => a.Create(It.IsAny<Anime>()), Times.Once);
            mockAnimeService.VerifyAll();
        }

        [Test]
        [Ignore("Test broken")]
        public void CreateOrUpdateAnime_CallsUpdate_WhenGivenAnimeTitleAlreadyInDatabase()
        {
            // Arrange
            var mockAnimeService = new Mock<IAnimeService>();
            mockAnimeService.Setup(a => a.GetAnimeByTitle(It.IsAny<string>())).Returns(new Anime());
            service = new AnimeManager(mockAnimeService.Object);

            // Act
            var result = service.CreateOrUpdateAnime(animeTitle: It.IsAny<string>(), newAnimeTitle: It.IsAny<string>(),
                episode: It.IsAny<int>(), releaseYear: It.IsAny<int>(), status: It.IsAny<string>(), 
                rank: null, summary: It.IsAny<string>(), more: It.IsAny<string>());

            // Assert
            mockAnimeService.Verify(a => a.Update(), Times.Once);
            mockAnimeService.VerifyAll();
        }
        #endregion
        //===========================================       Delete      ===========================================
        #region
        [Test]
        public void DeleteAnime_ReturnsTrue_GivenAnAnimeInTheDatabase()
        {
            // Arrange
            var mockAnimeService = new Mock<IAnimeService>();
            var originaleAnime = new Anime()
            {
                AnimeId = 1,
                AnimeName = "Tokyo Revengers",
                Episode = 24,
                ReleaseYear = 2021,
                Status = "Ongoing",
                Rank = null,
                Picture = "Tk.jpg",
                Summary = "The story is amazing",
                More = "No",
                Rating = 5,
                Watch = "Watching",
                EpisodeWatched = 3,
                UpToDate = new DateTime(2021, 03, 10, 8, 30, 52)
            };
            mockAnimeService.Setup(a => a.GetAnimeByTitle("Tokyo Revengers")).Returns(originaleAnime);
            service = new AnimeManager(mockAnimeService.Object);

            // Act
            var result = service.DeleteAnime("Tokyo Revengers");

            // Assert
            Assert.That(result);
        }

        [Test]
        public void DeleteAnime_ReturnsFalse_GivenAnAnimeNotInTheDatabase()
        {
            // Arrange
            var mockAnimeService = new Mock<IAnimeService>();
            service = new AnimeManager(mockAnimeService.Object);

            // Act
            var result = service.UpdateWatchlist(animeTitle: "Tokyo Revengers", rating: 2, more: "Yes", watch: "Watched",
            episodeWatched: 5, upToDate: new DateTime(2020, 03, 10, 8, 30, 52));

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void DeleteAnime_CallsDelete_GivenAnimeTitle()
        {
            // Arrange
            var mockAnimeService = new Mock<IAnimeService>();
            mockAnimeService.Setup(a => a.GetAnimeByTitle(It.IsAny<string>())).Returns(new Anime());
            service = new AnimeManager(mockAnimeService.Object);

            // Act
            var result = service.DeleteAnime(It.IsAny<string>());

            // Assert
            mockAnimeService.Verify(a => a.Delete(It.IsAny<Anime>()), Times.Once);
            mockAnimeService.VerifyAll();
        }

        [Test]
        public void DeleteAnime_ReturnsFalse_WhenDatabaseExceptionIsThrown()
        {
            // Arrange
            var mockAnimeService = new Mock<IAnimeService>();
            var originaleAnime = new Anime()
            {
                AnimeId = 1,
                AnimeName = "Tokyo Revengers",
                Episode = 24,
                ReleaseYear = 2021,
                Status = "Ongoing",
                Rank = null,
                Picture = "Tk.jpg",
                Summary = "The story is amazing",
                More = "No",
                Rating = 5,
                Watch = "Watching",
                EpisodeWatched = 3,
                UpToDate = new DateTime(2021, 03, 10, 8, 30, 52)
            };
            mockAnimeService.Setup(a => a.GetAnimeByTitle("Tokyo Revengers")).Returns(originaleAnime);
            mockAnimeService.Setup(cs => cs.Delete((Anime)null)).Throws<DbUpdateConcurrencyException>();
            service = new AnimeManager(mockAnimeService.Object);

            // Act
            var result = service.DeleteAnime(animeTitle: "Tokyo Ghoul");

            // Assert
            Assert.That(result, Is.False);
        }
        #endregion
    }
}