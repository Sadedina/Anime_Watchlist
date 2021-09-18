using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace AnimeData
{
    public partial class SammListContext : DbContext
    {
        public SammListContext()
        {
        }

        public SammListContext(DbContextOptions<SammListContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Anime> Animes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SammList;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Anime>(entity =>
            {
                entity.Property(e => e.AnimeId).HasColumnName("AnimeID");

                entity.Property(e => e.AnimeName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("animeName");

                entity.Property(e => e.Episode).HasColumnName("episode");

                entity.Property(e => e.EpisodeWatched).HasColumnName("episodeWatched");

                entity.Property(e => e.More)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("more");

                entity.Property(e => e.Picture)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("picture");

                entity.Property(e => e.Rank).HasColumnName("rank");

                entity.Property(e => e.Rating).HasColumnName("rating");

                entity.Property(e => e.ReleaseYear).HasColumnName("releaseYear");

                entity.Property(e => e.Status)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.Property(e => e.Summary)
                    .IsUnicode(false)
                    .HasColumnName("summary");

                entity.Property(e => e.UpToDate)
                    .HasColumnType("datetime")
                    .HasColumnName("upToDate");

                entity.Property(e => e.Watch)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("watch");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
