using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookTrackerFirstApi.Migrations
{
    /// <inheritdoc />
    public partial class seedingDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "Description", "IsRead", "Name" },
                values: new object[,]
                {
                    { 1, "Ivo Andrić", "Istorijski roman o mostu.", true, "Na Drini ćuprija" },
                    { 2, "Meša Selimović", "Psihološka drama o pojedincu i sistemu.", false, "Tvrđava" },
                    { 3, "Robert C. Martin", "Biblija za developere.", true, "Clean Code" },
                    { 4, "Andrew Hunt", "Savjeti za moderan razvoj softvera.", false, "The Pragmatic Programmer" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
