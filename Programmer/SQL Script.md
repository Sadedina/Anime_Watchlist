# Initializing SQL Database

```sql
CREATE DATABASE SammList
USE SammList

DROP TABLE IF EXISTS Animes

TRUNCATE TABLE Animes


CREATE TABLE Animes (
    AnimeID int IDENTITY(1,1) NOT NULL,
    animeName varchar(255) NOT NULL,
    episode int,
    releaseYear int,
    [status] varchar(255),
    [rank] int,
    picture varchar(255),
    summary varchar(MAX),
    more varchar(30),
    rating int,
    watch varchar(30),
    episodeWatched int,
    upToDate DateTime,
    PRIMARY KEY (AnimeID)
);

SELECT * FROM Animes

```



```sql
INSERT INTO Animes (animeName, episode, releaseYear, [status], [rank], picture, summary, more, rating, watch, episodeWatched, upToDate)
```



Data

```sql
INSERT INTO Animes (animeName, episode, releaseYear, [status], [rank], picture, summary, more, rating, watch, episodeWatched, upToDate)

```

