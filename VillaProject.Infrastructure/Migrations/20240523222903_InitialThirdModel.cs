using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VillaProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialThirdModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "Amenitys",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    VillaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Amenitys", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Amenitys_Villas_VillaID",
                        column: x => x.VillaID,
                        principalTable: "Villas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Amenitys_VillaID",
                table: "Amenitys",
                column: "VillaID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Amenitys");

            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "ID", "CreatedDate", "Description", "ImageUrl", "Occupancy", "Price", "UpdatedDate", "VillaName" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 5, 19, 5, 55, 50, 112, DateTimeKind.Local).AddTicks(4637), "VillaNameOne and 4 rooms and bathroom", "Https://placeholder.com", 4, 19000.50m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "VillaNameOne" },
                    { 2, new DateTime(2024, 5, 19, 5, 55, 50, 112, DateTimeKind.Local).AddTicks(4684), "VillaNameTwo and 1 rooms", "Https://placeholder.com", 1, 2500.50m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "VillaNameTwo" },
                    { 3, new DateTime(2024, 5, 19, 5, 55, 50, 112, DateTimeKind.Local).AddTicks(4691), "VillaNameThree and 2 rooms", "Https://placeholder.com", 2, 5000.57m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "VillaNameTwo" }
                });
        }
    }
}
