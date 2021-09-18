using NUnit.Framework;
using AnimeData;
using AnimeData.Services;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;

namespace AnimeTests
{
    public class AnimeManagerShould
    {
        private AnimeService _sut;
        private SammListContext _context;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var options = new DbContextOptionsBuilder<SammListContext>().UseInMemoryDatabase(databaseName: "Example_DB").Options;
            _context = new SammListContext(options);
            _sut = new AnimeService(_context);

            // Seed the database
            _sut.Create(new Anime
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
            });
            _sut.Create(new Anime
            {
                AnimeId = 2,
                AnimeName = "One Piece",
                Episode = 984,
                ReleaseYear = 1994,
                Status = "Ongoing",
                Rank = 6,
                Picture = "OP.jpg",
                Summary = "The story is long",
                More = "No",
                Rating = 5,
                Watch = "Watching",
                EpisodeWatched = 940,
                UpToDate = new DateTime(2020, 07, 10, 8, 30, 52)
            });
        }

        [Test]
        public void GivenAValidId_CorrectAnimeListIsReturned()
        {
            var result = _sut.GetAnimeByTitle("Tokyo Revengers");
            Assert.That(result, Is.TypeOf<Anime>());
            Assert.That(result.AnimeId, Is.EqualTo(1));
            Assert.That(result.Episode, Is.EqualTo(24));
            Assert.That(result.ReleaseYear, Is.EqualTo(2021));
            Assert.That(result.Status, Is.EqualTo("Ongoing"));
            Assert.That(result.Rank, Is.Null);
            Assert.That(result.Picture, Is.EqualTo("Tk.jpg"));
            Assert.That(result.Summary, Is.EqualTo("The story is amazing"));
            Assert.That(result.More, Is.EqualTo("No"));
            Assert.That(result.Rating, Is.EqualTo(5));
            Assert.That(result.Watch, Is.EqualTo("Watching"));
            Assert.That(result.EpisodeWatched, Is.EqualTo(3));
            Assert.That(result.UpToDate, Is.EqualTo(new DateTime(2021, 03, 10, 8, 30, 52)));
        }

        [Test]
        public void GivenANewAnime_CreateAddItToTheDatabase()
        {
            // Arrange
            var numberOfAnimeBefore = _context.Animes.Count();
            var newAnime = new Anime
            {
                AnimeId = 3,
                AnimeName = "Mob Psycho 100",
                Episode = 24,
                ReleaseYear = 2016,
                Status = "Complete",
                Rank = 5,
                Picture = "Mb.jpg",
                Summary = "The story is incredible",
                More = "Yes",
                Rating = 5,
                Watch = "Watched",
                EpisodeWatched = 24,
                UpToDate = new DateTime(2018, 03, 10, 8, 30, 52)
            };

            // Act
            _sut.Create(newAnime);
            var result = _sut.GetAnimeByTitle("Mob Psycho 100");

            // Assert
            Assert.That(result, Is.TypeOf<Anime>());
            Assert.That(result.AnimeId, Is.EqualTo(3));
            Assert.That(result.Episode, Is.EqualTo(24));
            Assert.That(result.ReleaseYear, Is.EqualTo(2016));
            Assert.That(result.Status, Is.EqualTo("Complete"));
            Assert.That(result.Rank, Is.EqualTo(5));
            Assert.That(result.Picture, Is.EqualTo("Mb.jpg"));
            Assert.That(result.Summary, Is.EqualTo("The story is incredible"));
            Assert.That(result.More, Is.EqualTo("Yes"));
            Assert.That(result.Rating, Is.EqualTo(5));
            Assert.That(result.Watch, Is.EqualTo("Watched"));
            Assert.That(result.EpisodeWatched, Is.EqualTo(24));
            Assert.That(result.UpToDate, Is.EqualTo(new DateTime(2018, 03, 10, 8, 30, 52)));

            Assert.That(_context.Animes.Count(), Is.EqualTo(numberOfAnimeBefore + 1));

            // Clean Up
            _sut.Delete(result);
            Assert.That(_context.Animes.Count(), Is.EqualTo(numberOfAnimeBefore));
        }

        [Test]
        public void GivenANewAnime_DeleteRemovesAnimeFromTheDatabase()
        {
            // Arrange
            var numberOfAnimeBefore = _context.Animes.Count();
            var animeToDelete = _sut.GetAnimeByTitle("Tokyo Revengers");

            // Act
            _sut.Delete(animeToDelete);
            var result = _sut.GetAnimeByTitle("Tokyo Revengers");

            // Assert
            Assert.That(result, Is.Null);
            Assert.That(_context.Animes.Count(), Is.EqualTo(numberOfAnimeBefore - 1));

            // Clean Up
            _sut.Create(new Anime
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
            });
            Assert.That(_context.Animes.Count(), Is.EqualTo(numberOfAnimeBefore));
        }


    }
}