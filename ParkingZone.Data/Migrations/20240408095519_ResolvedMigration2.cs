using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parking_Zone.Data.Migrations
{
    /// <inheritdoc />
    public partial class ResolvedMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "ParkingZones");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "ParkingZones");

            migrationBuilder.DropColumn(
                name: "Region",
                table: "ParkingZones");

            migrationBuilder.RenameColumn(
                name: "Street",
                table: "ParkingZones",
                newName: "Address");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ParkingZones",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfEstablishment",
                table: "ParkingZones",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfEstablishment",
                table: "ParkingZones");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "ParkingZones",
                newName: "Street");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "ParkingZones",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "ParkingZones",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "ParkingZones",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Region",
                table: "ParkingZones",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
