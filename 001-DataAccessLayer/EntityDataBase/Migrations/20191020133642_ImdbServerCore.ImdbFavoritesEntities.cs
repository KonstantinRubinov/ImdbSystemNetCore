using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ImdbServerCore.Migrations
{
    public partial class ImdbServerCoreImdbFavoritesEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "USERS",
                columns: table => new
                {
                    userID = table.Column<string>(nullable: false),
                    userFirstName = table.Column<string>(maxLength: 40, nullable: false),
                    userLastName = table.Column<string>(maxLength: 40, nullable: false),
                    userNickName = table.Column<string>(maxLength: 40, nullable: false),
                    userPassword = table.Column<string>(nullable: false),
                    userEmail = table.Column<string>(nullable: false),
                    userGender = table.Column<string>(nullable: false),
                    userBirthDate = table.Column<DateTime>(nullable: true),
                    userPicture = table.Column<string>(nullable: true),
                    userImdbPass = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERS", x => x.userID);
                });

            migrationBuilder.CreateTable(
                name: "MOVIES",
                columns: table => new
                {
                    movieImdbID = table.Column<string>(nullable: false),
                    movieTitle = table.Column<string>(nullable: true),
                    moviePoster = table.Column<string>(nullable: true),
                    userID = table.Column<string>(nullable: true),
                    movieYear = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MOVIES", x => x.movieImdbID);
                    table.ForeignKey(
                        name: "FK_MOVIES_USERS_userID",
                        column: x => x.userID,
                        principalTable: "USERS",
                        principalColumn: "userID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MOVIEEXTENDS",
                columns: table => new
                {
                    movieImdbID = table.Column<string>(nullable: false),
                    moviePlot = table.Column<string>(nullable: true),
                    movieUrl = table.Column<string>(nullable: true),
                    movieRated = table.Column<string>(nullable: true),
                    movieImdbRating = table.Column<float>(nullable: false),
                    movieSeen = table.Column<bool>(nullable: false),
                    userID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MOVIEEXTENDS", x => x.movieImdbID);
                    table.ForeignKey(
                        name: "FK_MOVIEEXTENDS_MOVIES_movieImdbID",
                        column: x => x.movieImdbID,
                        principalTable: "MOVIES",
                        principalColumn: "movieImdbID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MOVIEEXTENDS_USERS_userID",
                        column: x => x.userID,
                        principalTable: "USERS",
                        principalColumn: "userID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MOVIEEXTENDS_userID",
                table: "MOVIEEXTENDS",
                column: "userID");

            migrationBuilder.CreateIndex(
                name: "IX_MOVIES_userID",
                table: "MOVIES",
                column: "userID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MOVIEEXTENDS");

            migrationBuilder.DropTable(
                name: "MOVIES");

            migrationBuilder.DropTable(
                name: "USERS");
        }
    }
}
