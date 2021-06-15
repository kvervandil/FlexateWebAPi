using Microsoft.EntityFrameworkCore.Migrations;

namespace FlexateWebApi.Infrastructure.Migrations
{
    public partial class UpdateOffice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Offices",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Offices");
        }
    }
}
