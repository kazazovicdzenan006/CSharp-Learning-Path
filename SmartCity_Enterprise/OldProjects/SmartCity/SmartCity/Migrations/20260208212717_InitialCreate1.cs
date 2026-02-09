using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartCity.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CityNodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityZone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StreetName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityNodes", x => x.Id);
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
                        name: "FK_CrossRoads_CityNodes_Id",
                        column: x => x.Id,
                        principalTable: "CityNodes",
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
                        name: "FK_ParkingLots_CityNodes_Id",
                        column: x => x.Id,
                        principalTable: "CityNodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CrossRoads");

            migrationBuilder.DropTable(
                name: "ParkingLots");

            migrationBuilder.DropTable(
                name: "CityNodes");
        }
    }
}
