using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AnimeData.Migrations
{
    public partial class InitialCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Animes",
                columns: table => new
                {
                    AnimeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    animeName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    episode = table.Column<int>(type: "int", nullable: true),
                    releaseYear = table.Column<int>(type: "int", nullable: true),
                    status = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    rank = table.Column<int>(type: "int", nullable: true),
                    picture = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    summary = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    more = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    rating = table.Column<int>(type: "int", nullable: true),
                    watch = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    episodeWatched = table.Column<int>(type: "int", nullable: true),
                    upToDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animes", x => x.AnimeID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Animes");
        }
    }
}
