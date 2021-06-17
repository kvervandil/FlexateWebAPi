using Microsoft.EntityFrameworkCore.Migrations;

namespace FlexateWebApi.Infrastructure.Migrations
{
    public partial class UpdatePeopleCollections : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_People_PersonId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Offices_People_PersonId",
                table: "Offices");

            migrationBuilder.DropIndex(
                name: "IX_Offices_PersonId",
                table: "Offices");

            migrationBuilder.DropIndex(
                name: "IX_Cars_PersonId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "Offices");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "Cars");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PersonId",
                table: "Offices",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PersonId",
                table: "Cars",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Offices_PersonId",
                table: "Offices",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_PersonId",
                table: "Cars",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_People_PersonId",
                table: "Cars",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Offices_People_PersonId",
                table: "Offices",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
