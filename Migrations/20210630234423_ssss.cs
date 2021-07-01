using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ActivityCenter.Migrations
{
    public partial class ssss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Time",
                table: "Activits");

            migrationBuilder.AlterColumn<int>(
                name: "Duration",
                table: "Activits",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Activits",
                nullable: false);

            migrationBuilder.AddColumn<int>(
                name: "MaxAge",
                table: "Activits",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MinAge",
                table: "Activits",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumbeOfFemale",
                table: "Activits",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfMale",
                table: "Activits",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "Activits",
                nullable: false);

            migrationBuilder.AddColumn<double>(
                name: "price",
                table: "Activits",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "totalnumber",
                table: "Activits",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "Activits");

            migrationBuilder.DropColumn(
                name: "MaxAge",
                table: "Activits");

            migrationBuilder.DropColumn(
                name: "MinAge",
                table: "Activits");

            migrationBuilder.DropColumn(
                name: "NumbeOfFemale",
                table: "Activits");

            migrationBuilder.DropColumn(
                name: "NumberOfMale",
                table: "Activits");

            migrationBuilder.DropColumn(
                name: "Tag",
                table: "Activits");

            migrationBuilder.DropColumn(
                name: "price",
                table: "Activits");

            migrationBuilder.DropColumn(
                name: "totalnumber",
                table: "Activits");

            migrationBuilder.AlterColumn<int>(
                name: "Duration",
                table: "Activits",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Time",
                table: "Activits",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
