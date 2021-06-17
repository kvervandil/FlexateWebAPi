using Microsoft.EntityFrameworkCore.Migrations;

namespace FlexateWebApi.Infrastructure.Migrations
{
    public partial class AddPersonOffice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offices_People_PersonId",
                table: "Offices");

            migrationBuilder.AlterColumn<int>(
                name: "PersonId",
                table: "Offices",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "ZipCode",
                table: "Offices",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PersonOffice",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(nullable: true),
                    SpaceType = table.Column<string>(nullable: true),
                    IsGroundFloor = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PersonId = table.Column<int>(nullable: false),
                    OfficeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonOffice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonOffice_Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Offices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonOffice_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonOffice_OfficeId",
                table: "PersonOffice",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonOffice_PersonId",
                table: "PersonOffice",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Offices_People_PersonId",
                table: "Offices",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offices_People_PersonId",
                table: "Offices");

            migrationBuilder.DropTable(
                name: "PersonOffice");

            migrationBuilder.DropColumn(
                name: "ZipCode",
                table: "Offices");

            migrationBuilder.AlterColumn<int>(
                name: "PersonId",
                table: "Offices",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Offices_People_PersonId",
                table: "Offices",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
