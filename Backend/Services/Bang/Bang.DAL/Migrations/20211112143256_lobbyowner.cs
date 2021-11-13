using Microsoft.EntityFrameworkCore.Migrations;

namespace Bang.DAL.Migrations
{
    public partial class lobbyowner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LobbyPassword",
                table: "GameBoards",
                newName: "LobbyOwnerId");

            migrationBuilder.AlterColumn<int>(
                name: "TurnPhase",
                table: "GameBoards",
                type: "int",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LobbyOwnerId",
                table: "GameBoards",
                newName: "LobbyPassword");

            migrationBuilder.AlterColumn<int>(
                name: "TurnPhase",
                table: "GameBoards",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 1);
        }
    }
}
