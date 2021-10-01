using Microsoft.EntityFrameworkCore.Migrations;

namespace UserIdentity.DAL.Migrations
{
    public partial class AddedFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "GameBoardId",
                table: "Lobbies",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "InGame",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GameBoardId",
                table: "Lobbies");

            migrationBuilder.DropColumn(
                name: "InGame",
                table: "AspNetUsers");
        }
    }
}
