using Microsoft.EntityFrameworkCore.Migrations;

namespace FlexateWebApi.Infrastructure.Migrations
{
    public partial class UpdateOfficePerson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsGroundFloor",
                table: "PersonOffice");

            migrationBuilder.DropColumn(
                name: "SpaceType",
                table: "PersonOffice");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Offices");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Offices");

            migrationBuilder.DropColumn(
                name: "ZipCode",
                table: "Offices");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "PersonOffice",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ZipCode",
                table: "PersonOffice",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsGroundFloor",
                table: "Offices",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SpaceType",
                table: "Offices",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "PersonOffice");

            migrationBuilder.DropColumn(
                name: "ZipCode",
                table: "PersonOffice");

            migrationBuilder.DropColumn(
                name: "IsGroundFloor",
                table: "Offices");

            migrationBuilder.DropColumn(
                name: "SpaceType",
                table: "Offices");

            migrationBuilder.AddColumn<bool>(
                name: "IsGroundFloor",
                table: "PersonOffice",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SpaceType",
                table: "PersonOffice",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Offices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Offices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ZipCode",
                table: "Offices",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
