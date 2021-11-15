using Microsoft.EntityFrameworkCore.Migrations;

namespace Bang.DAL.Migrations
{
    public partial class target : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameBoards_Players_TargetedPlayerId1",
                table: "GameBoards");

            migrationBuilder.DropIndex(
                name: "IX_GameBoards_TargetedPlayerId1",
                table: "GameBoards");

            migrationBuilder.DropColumn(
                name: "TargetedPlayerId1",
                table: "GameBoards");

            migrationBuilder.CreateIndex(
                name: "IX_GameBoards_TargetedPlayerId",
                table: "GameBoards",
                column: "TargetedPlayerId",
                unique: true,
                filter: "[TargetedPlayerId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_GameBoards_Players_TargetedPlayerId",
                table: "GameBoards",
                column: "TargetedPlayerId",
                principalTable: "Players",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameBoards_Players_TargetedPlayerId",
                table: "GameBoards");

            migrationBuilder.DropIndex(
                name: "IX_GameBoards_TargetedPlayerId",
                table: "GameBoards");

            migrationBuilder.AddColumn<long>(
                name: "TargetedPlayerId1",
                table: "GameBoards",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameBoards_TargetedPlayerId1",
                table: "GameBoards",
                column: "TargetedPlayerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_GameBoards_Players_TargetedPlayerId1",
                table: "GameBoards",
                column: "TargetedPlayerId1",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
