using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VillaProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitSecondUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "ID", "CreatedDate", "Description", "ImageUrl", "Occupancy", "Price", "UpdatedDate", "VillaName" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 5, 18, 1, 22, 1, 534, DateTimeKind.Local).AddTicks(8267), "VillaNameOne and 4 rooms and bathroom", "Https://placeholder.com", 4, 19000.50m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "VillaNameOne" },
                    { 2, new DateTime(2024, 5, 18, 1, 22, 1, 534, DateTimeKind.Local).AddTicks(8317), "VillaNameTwo and 1 rooms", "Https://placeholder.com", 1, 2500.50m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "VillaNameTwo" },
                    { 3, new DateTime(2024, 5, 18, 1, 22, 1, 534, DateTimeKind.Local).AddTicks(8323), "VillaNameThree and 2 rooms", "Https://placeholder.com", 2, 5000.57m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "VillaNameTwo" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
