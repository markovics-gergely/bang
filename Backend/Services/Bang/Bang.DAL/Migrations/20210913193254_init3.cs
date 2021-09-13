using Microsoft.EntityFrameworkCore.Migrations;

namespace Bang.DAL.Migrations
{
    public partial class init3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CardColorType",
                table: "PlayerCards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FrenchNumber",
                table: "PlayerCards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CardColorType",
                table: "GameBoardCards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FrenchNumber",
                table: "GameBoardCards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 14,
                column: "CardType",
                value: "Mustang");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CardColorType",
                table: "PlayerCards");

            migrationBuilder.DropColumn(
                name: "FrenchNumber",
                table: "PlayerCards");

            migrationBuilder.DropColumn(
                name: "CardColorType",
                table: "GameBoardCards");

            migrationBuilder.DropColumn(
                name: "FrenchNumber",
                table: "GameBoardCards");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 14,
                column: "CardType",
                value: "Horses");
        }
    }
}
