using Microsoft.EntityFrameworkCore.Migrations;

namespace Bang.DAL.Migrations
{
    public partial class gameboardedit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PlayedCards",
                table: "Players",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TargetReason",
                table: "GameBoards",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TurnPhase",
                table: "GameBoards",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlayedCards",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "TargetReason",
                table: "GameBoards");

            migrationBuilder.DropColumn(
                name: "TurnPhase",
                table: "GameBoards");
        }
    }
}
