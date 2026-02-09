using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMasterDb1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gradovi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gradovi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Artikli",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naslov = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GodinaIzdanja = table.Column<int>(type: "int", nullable: false),
                    GradId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artikli", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Artikli_Gradovi_GradId",
                        column: x => x.GradId,
                        principalTable: "Gradovi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Nodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityZone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StreetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GradId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Nodes_Gradovi_GradId",
                        column: x => x.GradId,
                        principalTable: "Gradovi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Uredjaji",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GradId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uredjaji", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Uredjaji_Gradovi_GradId",
                        column: x => x.GradId,
                        principalTable: "Gradovi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Autor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrojStranica = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Artikli_Id",
                        column: x => x.Id,
                        principalTable: "Artikli",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Reziser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrajanjeUMinutama = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movies_Artikli_Id",
                        column: x => x.Id,
                        principalTable: "Artikli",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CrossRoads",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    CrossName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrafficJamPercantage = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrossRoads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CrossRoads_Nodes_Id",
                        column: x => x.Id,
                        principalTable: "Nodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParkingLots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ParkingName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalParkingSpots = table.Column<int>(type: "int", nullable: false),
                    AvailableParkingSpots = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingLots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParkingLots_Nodes_Id",
                        column: x => x.Id,
                        principalTable: "Nodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Controllers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ModelKontrolera = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    BrojKanala = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Controllers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Controllers_Uredjaji_Id",
                        column: x => x.Id,
                        principalTable: "Uredjaji",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Senzors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Vrijednost = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Senzors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Senzors_Uredjaji_Id",
                        column: x => x.Id,
                        principalTable: "Uredjaji",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Artikli_GradId",
                table: "Artikli",
                column: "GradId");

            migrationBuilder.CreateIndex(
                name: "IX_Nodes_GradId",
                table: "Nodes",
                column: "GradId");

            migrationBuilder.CreateIndex(
                name: "IX_Uredjaji_GradId",
                table: "Uredjaji",
                column: "GradId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Controllers");

            migrationBuilder.DropTable(
                name: "CrossRoads");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "ParkingLots");

            migrationBuilder.DropTable(
                name: "Senzors");

            migrationBuilder.DropTable(
                name: "Artikli");

            migrationBuilder.DropTable(
                name: "Nodes");

            migrationBuilder.DropTable(
                name: "Uredjaji");

            migrationBuilder.DropTable(
                name: "Gradovi");
        }
    }
}
