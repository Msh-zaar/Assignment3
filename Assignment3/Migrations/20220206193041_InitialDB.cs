using Microsoft.EntityFrameworkCore.Migrations;

namespace Assignment3.Migrations
{
    public partial class InitialDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Character",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Alias = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Character", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Franchise",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Franchise", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movie",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ReleaseYear = table.Column<int>(type: "int", maxLength: 4, nullable: false),
                    Director = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Trailer = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    FranchiseId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movie", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movie_Franchise_FranchiseId",
                        column: x => x.FranchiseId,
                        principalTable: "Franchise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MovieCharacters",
                columns: table => new
                {
                    CharacterId = table.Column<int>(type: "int", nullable: false),
                    MovieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieCharacters", x => new { x.CharacterId, x.MovieId });
                    table.ForeignKey(
                        name: "FK_MovieCharacters_Character_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Character",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieCharacters_Movie_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Character",
                columns: new[] { "Id", "Alias", "Gender", "Name", "Photo" },
                values: new object[,]
                {
                    { 1, "Rocky", "Male", "Robert Balboa", "https://upload.wikimedia.org/wikipedia/en/5/53/Rocky_balboa.jpeg" },
                    { 2, "Adrian", "Female", "Adriana Pennino", "https://upload.wikimedia.org/wikipedia/commons/c/cd/Talia_Shire_1976_edit.JPG" },
                    { 3, "Mickey", "Male", "Michael Goldmill", "https://upload.wikimedia.org/wikipedia/en/7/7b/Mickey_Goldmill.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Franchise",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Movie about you get knocked down and you get back up again", "Rocky" },
                    { 2, "Movie about some shorties carrying some jewelry", "Lord of the Rings" },
                    { 3, "Movie 'bout some wizard bloke", "Harry Potter" }
                });

            migrationBuilder.InsertData(
                table: "Movie",
                columns: new[] { "Id", "Director", "FranchiseId", "Genre", "Picture", "ReleaseYear", "Title", "Trailer" },
                values: new object[] { 1, "John G. Avildsen", 1, "Sports Drama", null, 1976, "Rocky", null });

            migrationBuilder.InsertData(
                table: "Movie",
                columns: new[] { "Id", "Director", "FranchiseId", "Genre", "Picture", "ReleaseYear", "Title", "Trailer" },
                values: new object[] { 2, "Sylvester Stallone", 1, "Sports Drama", null, 1979, "Rocky II", null });

            migrationBuilder.InsertData(
                table: "Movie",
                columns: new[] { "Id", "Director", "FranchiseId", "Genre", "Picture", "ReleaseYear", "Title", "Trailer" },
                values: new object[] { 3, "Sylvester Stallone", 1, "Sports Drama", null, 1982, "Rocky III", null });

            migrationBuilder.InsertData(
                table: "MovieCharacters",
                columns: new[] { "CharacterId", "MovieId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "MovieCharacters",
                columns: new[] { "CharacterId", "MovieId" },
                values: new object[] { 2, 1 });

            migrationBuilder.InsertData(
                table: "MovieCharacters",
                columns: new[] { "CharacterId", "MovieId" },
                values: new object[] { 3, 2 });

            migrationBuilder.CreateIndex(
                name: "IX_Movie_FranchiseId",
                table: "Movie",
                column: "FranchiseId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieCharacters_MovieId",
                table: "MovieCharacters",
                column: "MovieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieCharacters");

            migrationBuilder.DropTable(
                name: "Character");

            migrationBuilder.DropTable(
                name: "Movie");

            migrationBuilder.DropTable(
                name: "Franchise");
        }
    }
}
