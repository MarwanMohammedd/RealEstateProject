using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VillaProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialSecondModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VillaNumber",
                columns: table => new
                {
                    VillaNumberID = table.Column<int>(type: "int", nullable: false),
                    VillaID = table.Column<int>(type: "int", nullable: false),
                    MoreInformation = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VillaNumber", x => x.VillaNumberID);
                    table.ForeignKey(
                        name: "FK_VillaNumber_Villas_VillaID",
                        column: x => x.VillaID,
                        principalTable: "Villas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "ID", "CreatedDate", "Description", "ImageUrl", "Occupancy", "Price", "UpdatedDate", "VillaName" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 5, 19, 5, 55, 50, 112, DateTimeKind.Local).AddTicks(4637), "VillaNameOne and 4 rooms and bathroom", "Https://placeholder.com", 4, 19000.50m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "VillaNameOne" },
                    { 2, new DateTime(2024, 5, 19, 5, 55, 50, 112, DateTimeKind.Local).AddTicks(4684), "VillaNameTwo and 1 rooms", "Https://placeholder.com", 1, 2500.50m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "VillaNameTwo" },
                    { 3, new DateTime(2024, 5, 19, 5, 55, 50, 112, DateTimeKind.Local).AddTicks(4691), "VillaNameThree and 2 rooms", "Https://placeholder.com", 2, 5000.57m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "VillaNameTwo" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_VillaNumber_VillaID",
                table: "VillaNumber",
                column: "VillaID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VillaNumber");

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "ID",
                keyValue: 3);
        }
    }
}
