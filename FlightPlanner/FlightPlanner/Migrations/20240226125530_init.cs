using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightPlanner.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Airports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AirportCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fromid = table.Column<int>(type: "int", nullable: false),
                    Toid = table.Column<int>(type: "int", nullable: false),
                    Carrier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartureTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ArrivalTime = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Flights_Airports_Fromid",
                        column: x => x.Fromid,
                        principalTable: "Airports",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                        );
                    table.ForeignKey(
                        name: "FK_Flights_Airports_Toid",
                        column: x => x.Toid,
                        principalTable: "Airports",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction );
                });

            migrationBuilder.CreateIndex(
                name: "IX_Flights_Fromid",
                table: "Flights",
                column: "Fromid");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_Toid",
                table: "Flights",
                column: "Toid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Flights");

            migrationBuilder.DropTable(
                name: "Airports");
        }
    }
}
