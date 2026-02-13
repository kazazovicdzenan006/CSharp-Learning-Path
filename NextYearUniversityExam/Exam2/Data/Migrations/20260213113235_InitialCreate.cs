using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clients_People_Id",
                        column: x => x.Id,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Workers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExperienceInYears = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Workers_People_Id",
                        column: x => x.Id,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Dženan Kazazović" },
                    { 2, "Amar Delić" },
                    { 3, "Lejla Hodžić" },
                    { 4, "Sara Ibrahimović" },
                    { 5, "Atlantbh" },
                    { 6, "Microsoft" },
                    { 7, "Google" },
                    { 8, "Mistral" }
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "CompanyName", "Email" },
                values: new object[,]
                {
                    { 5, "Atlantbh d.o.o.", "info@atlantbh.com" },
                    { 6, "Microsoft Corporation", "support@microsoft.com" },
                    { 7, "Google LLC", null },
                    { 8, "Mistral Technologies", "contact@mistral.ba" }
                });

            migrationBuilder.InsertData(
                table: "Workers",
                columns: new[] { "Id", "ExperienceInYears", "Position" },
                values: new object[,]
                {
                    { 1, 2.5, "Backend Developer" },
                    { 2, 3.0, "Frontend Developer" },
                    { 3, 1.2, "DevOps Engineer" },
                    { 4, null, "Junior QA" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Workers");

            migrationBuilder.DropTable(
                name: "People");
        }
    }
}
