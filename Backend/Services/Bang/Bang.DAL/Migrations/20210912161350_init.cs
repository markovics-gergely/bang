using Microsoft.EntityFrameworkCore.Migrations;

namespace Bang.DAL.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CardEffectType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CardType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxHP = table.Column<int>(type: "int", nullable: false),
                    CharacterType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameBoardCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameBoardId = table.Column<long>(type: "bigint", nullable: false),
                    CardId = table.Column<int>(type: "int", nullable: false),
                    StatusType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameBoardCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameBoardCards_Cards_CardId",
                        column: x => x.CardId,
                        principalTable: "Cards",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PlayerCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerId = table.Column<long>(type: "bigint", nullable: false),
                    CardId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerCards_Cards_CardId",
                        column: x => x.CardId,
                        principalTable: "Cards",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GameBoardId = table.Column<long>(type: "bigint", nullable: false),
                    CharacterType = table.Column<int>(type: "int", nullable: false),
                    RoleType = table.Column<int>(type: "int", nullable: false),
                    ActualHP = table.Column<int>(type: "int", nullable: false),
                    MaxHP = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameBoards",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActualPlayerId = table.Column<long>(type: "bigint", nullable: true),
                    MaxTurnTime = table.Column<int>(type: "int", nullable: false),
                    IsOver = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameBoards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameBoards_Players_ActualPlayerId",
                        column: x => x.ActualPlayerId,
                        principalTable: "Players",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "Id", "CardEffectType", "CardType", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "card_active", "Bang", "", "Bang!" },
                    { 12, "card_passive", "Jail", "", "Börtön" },
                    { 10, "card_passive", "Horses", "", "Musztáng" },
                    { 9, "card_passive", "Guns", "", "" },
                    { 6, "card_passive", "Dynamite", "", "Dinamit" },
                    { 17, "card_active", "WellsFargo", "", "W" },
                    { 16, "card_active", "Stagecoach", "", "" },
                    { 15, "card_active", "Saloon", "", "" },
                    { 2, "card_passive", "Barrel", "", "" },
                    { 13, "card_active", "Missed", "", "Elvétve!" },
                    { 11, "card_active", "Indians", "", "Indiánok!" },
                    { 8, "card_active", "GeneralStore", "", "Zsibvásár" },
                    { 7, "card_active", "Gatling", "", "Gatling" },
                    { 5, "card_active", "Duel", "", "Párbaj" },
                    { 4, "card_active", "CatBalou", "", "Cat Balou" },
                    { 3, "card_active", "Beer", "", "Sör" },
                    { 14, "card_active", "Panic", "", "Pánik!" }
                });

            migrationBuilder.InsertData(
                table: "Characters",
                columns: new[] { "Id", "CharacterType", "Description", "MaxHP", "Name" },
                values: new object[,]
                {
                    { 10, "PedroRamirez", "", 4, "" },
                    { 16, "WillyTheKid", "", 4, "" },
                    { 15, "VultureSam", "", 4, "" },
                    { 14, "SuzyLafayette", "", 4, "" },
                    { 13, "SlabTheKiller", "", 4, "" },
                    { 12, "SidKetchum", "", 4, "" },
                    { 11, "RoseDoolan", "", 4, "" },
                    { 9, "PaulRegret", "", 3, "" },
                    { 2, "BlackJack", "", 4, "" },
                    { 7, "KitCarlson", "", 4, "" },
                    { 6, "Jourdonnais", "", 4, "" },
                    { 5, "JesseJones", "", 4, "" },
                    { 4, "ElGringo", "", 3, "" },
                    { 3, "CalamityJanet", "", 4, "" },
                    { 1, "BartCassidy", "", 4, "" },
                    { 8, "LuckyDuke", "", 4, "" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Description", "Name", "RoleType" },
                values: new object[,]
                {
                    { 3, "", "Sheriff", "Sheriff" },
                    { 1, "", "Bandita", "Outlaw" },
                    { 2, "", "Renegát", "Renegade" },
                    { 4, "", "Sheriffhelyettes", "Vice" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameBoardCards_CardId",
                table: "GameBoardCards",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_GameBoardCards_GameBoardId",
                table: "GameBoardCards",
                column: "GameBoardId");

            migrationBuilder.CreateIndex(
                name: "IX_GameBoards_ActualPlayerId",
                table: "GameBoards",
                column: "ActualPlayerId",
                unique: true,
                filter: "[ActualPlayerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerCards_CardId",
                table: "PlayerCards",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerCards_PlayerId",
                table: "PlayerCards",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_GameBoardId",
                table: "Players",
                column: "GameBoardId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameBoardCards_GameBoards_GameBoardId",
                table: "GameBoardCards",
                column: "GameBoardId",
                principalTable: "GameBoards",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerCards_Players_PlayerId",
                table: "PlayerCards",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_GameBoards_GameBoardId",
                table: "Players",
                column: "GameBoardId",
                principalTable: "GameBoards",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_GameBoards_GameBoardId",
                table: "Players");

            migrationBuilder.DropTable(
                name: "Characters");

            migrationBuilder.DropTable(
                name: "GameBoardCards");

            migrationBuilder.DropTable(
                name: "PlayerCards");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropTable(
                name: "GameBoards");

            migrationBuilder.DropTable(
                name: "Players");
        }
    }
}
