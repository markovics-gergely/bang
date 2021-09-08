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
                    CardType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PassiveCard_CardType = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "GameBoardCard",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameBoardId = table.Column<int>(type: "int", nullable: false),
                    CardId = table.Column<int>(type: "int", nullable: false),
                    StatusType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameBoardCard", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameBoardCard_Cards_CardId",
                        column: x => x.CardId,
                        principalTable: "Cards",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PlayerCard",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    CardId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerCard", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerCard_Cards_CardId",
                        column: x => x.CardId,
                        principalTable: "Cards",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GameBoardId = table.Column<int>(type: "int", nullable: false),
                    CharacterType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActualHP = table.Column<int>(type: "int", nullable: false),
                    MaxHP = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameBoard",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActualPlayerId = table.Column<int>(type: "int", nullable: false),
                    MaxTurnTime = table.Column<int>(type: "int", nullable: false),
                    IsOver = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameBoard", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameBoard_Player_ActualPlayerId",
                        column: x => x.ActualPlayerId,
                        principalTable: "Player",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "Id", "CardEffectType", "CardType", "Description", "Name" },
                values: new object[] { 1, "card_active", "Bang", "", "Bang!" });

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "Id", "CardEffectType", "PassiveCard_CardType", "Description", "Name" },
                values: new object[,]
                {
                    { 12, "card_passive", "Jail", "", "" },
                    { 10, "card_passive", "Horses", "", "" },
                    { 9, "card_passive", "Guns", "", "" },
                    { 6, "card_passive", "Dynamite", "", "" }
                });

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "Id", "CardEffectType", "CardType", "Description", "Name" },
                values: new object[,]
                {
                    { 17, "card_active", "WellsFargo", "", "" },
                    { 16, "card_active", "Stagecoach", "", "" },
                    { 15, "card_active", "Saloon", "", "" }
                });

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "Id", "CardEffectType", "PassiveCard_CardType", "Description", "Name" },
                values: new object[] { 2, "card_passive", "Barrel", "", "" });

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "Id", "CardEffectType", "CardType", "Description", "Name" },
                values: new object[,]
                {
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
                    { 3, "", "", "Sheriff" },
                    { 1, "", "", "Outlaw" },
                    { 2, "", "", "Renegade" },
                    { 4, "", "", "Vice" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameBoard_ActualPlayerId",
                table: "GameBoard",
                column: "ActualPlayerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameBoardCard_CardId",
                table: "GameBoardCard",
                column: "CardId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameBoardCard_GameBoardId",
                table: "GameBoardCard",
                column: "GameBoardId");

            migrationBuilder.CreateIndex(
                name: "IX_Player_GameBoardId",
                table: "Player",
                column: "GameBoardId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerCard_CardId",
                table: "PlayerCard",
                column: "CardId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlayerCard_PlayerId",
                table: "PlayerCard",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameBoardCard_GameBoard_GameBoardId",
                table: "GameBoardCard",
                column: "GameBoardId",
                principalTable: "GameBoard",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerCard_Player_PlayerId",
                table: "PlayerCard",
                column: "PlayerId",
                principalTable: "Player",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Player_GameBoard_GameBoardId",
                table: "Player",
                column: "GameBoardId",
                principalTable: "GameBoard",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameBoard_Player_ActualPlayerId",
                table: "GameBoard");

            migrationBuilder.DropTable(
                name: "Characters");

            migrationBuilder.DropTable(
                name: "GameBoardCard");

            migrationBuilder.DropTable(
                name: "PlayerCard");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropTable(
                name: "Player");

            migrationBuilder.DropTable(
                name: "GameBoard");
        }
    }
}
