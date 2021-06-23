using Microsoft.EntityFrameworkCore.Migrations;

namespace FlexateWebApi.Infrastructure.Migrations
{
    public partial class AddPersonCar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_People_PersonId",
                table: "Cars");

            migrationBuilder.AlterColumn<int>(
                name: "PersonId",
                table: "Cars",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "PersonCar",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<int>(nullable: false),
                    CarId = table.Column<int>(nullable: false),
                    Vin = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Color = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonCar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonCar_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonCar_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonCar_CarId",
                table: "PersonCar",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonCar_PersonId",
                table: "PersonCar",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_People_PersonId",
                table: "Cars",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_People_PersonId",
                table: "Cars");

            migrationBuilder.DropTable(
                name: "PersonCar");

            migrationBuilder.AlterColumn<int>(
                name: "PersonId",
                table: "Cars",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_People_PersonId",
                table: "Cars",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
