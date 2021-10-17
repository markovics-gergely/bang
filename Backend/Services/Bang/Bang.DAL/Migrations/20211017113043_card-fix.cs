using Microsoft.EntityFrameworkCore.Migrations;

namespace Bang.DAL.Migrations
{
    public partial class cardfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_GameBoardCards_GameBoards_GameBoardId1",
                table: "GameBoardCards",
                column: "GameBoardId",
                principalTable: "GameBoards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameBoardCards_GameBoards_GameBoardId1",
                table: "GameBoardCards");
        }
    }
}
