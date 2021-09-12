using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bang.DAL.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                });

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
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    GameBoardId = table.Column<long>(type: "bigint", nullable: false),
                    CharacterType = table.Column<int>(type: "int", nullable: false),
                    RoleType = table.Column<int>(type: "int", nullable: false),
                    ActualHP = table.Column<int>(type: "int", nullable: false),
                    MaxHP = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Players_Account_UserId",
                        column: x => x.UserId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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

            migrationBuilder.CreateTable(
                name: "PlayerCards",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
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
                    table.ForeignKey(
                        name: "FK_PlayerCards_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id");
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
                    table.ForeignKey(
                        name: "FK_GameBoardCards_GameBoards_GameBoardId",
                        column: x => x.GameBoardId,
                        principalTable: "GameBoards",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "Id", "CardEffectType", "CardType", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "card_active", "Bang", "Bang!", "Bang!" },
                    { 22, "card_passive", "Winchester", "Winchester", "Winchester" },
                    { 20, "card_passive", "Remingtion", "Remingtion", "Remingtion" },
                    { 19, "card_passive", "Schofield", "Schofield", "Schofield" },
                    { 18, "card_passive", "Volcanic", "Gyorstüzelő", "Gyorstüzelő" },
                    { 17, "card_passive", "Dynamite", "Dinamit", "Dinamit" },
                    { 16, "card_passive", "Scope", "Távcső", "Távcső" },
                    { 15, "card_passive", "Barrel", "Hordó", "Hordó" },
                    { 14, "card_passive", "Horses", "Musztáng", "Musztáng" },
                    { 13, "card_passive", "Jail", "Börtön", "Börtön" },
                    { 12, "card_active", "WellsFargo", "Wells Fargo", "Wells Fargo" },
                    { 21, "card_passive", "Karabine", "Karabély", "Karabély" },
                    { 10, "card_active", "Gatling", "Gatling", "Gatling" },
                    { 9, "card_active", "Stagecoach", "Postakocsi", "Postakocsi" },
                    { 8, "card_active", "Indians", "Indiánok!", "Indiánok!" },
                    { 7, "card_active", "GeneralStore", "Szatócsbolt", "Szatócsbolt" },
                    { 6, "card_active", "Duel", "Párbaj", "Párbaj" },
                    { 5, "card_active", "Panic", "Pánik!", "Pánik!" },
                    { 4, "card_active", "CatBalou", "Cat Balou", "Cat Balou" },
                    { 3, "card_active", "Beer", "Sör", "Sör" },
                    { 2, "card_active", "Missed", "Nem talált!", "Nem talált!" },
                    { 11, "card_active", "Saloon", "Kocsma", "Kocsma" }
                });

            migrationBuilder.InsertData(
                table: "Characters",
                columns: new[] { "Id", "CharacterType", "Description", "MaxHP", "Name" },
                values: new object[,]
                {
                    { 11, "RoseDoolan", "", 4, "Rose Doolan" },
                    { 12, "SidKetchum", "", 4, "Sid Ketchum" },
                    { 16, "WillyTheKid", "", 4, "Willy the Kid" },
                    { 14, "SuzyLafayette", "", 4, "Suzy Lafayette" },
                    { 15, "VultureSam", "", 4, "Vulture Sam" },
                    { 10, "PedroRamirez", "", 4, "Pedro Ramirez" },
                    { 13, "SlabTheKiller", "", 4, "Slab the Killer" },
                    { 9, "PaulRegret", "", 3, "Paul Regret" },
                    { 5, "JesseJones", "", 4, "Jesse Jones" },
                    { 7, "KitCarlson", "", 4, "Kit Carlson" },
                    { 6, "Jourdonnais", "", 4, "Jourdonnais" },
                    { 4, "ElGringo", "", 3, "El Gringo" },
                    { 3, "CalamityJanet", "", 4, "Calamity Janet" },
                    { 2, "BlackJack", "", 4, "Black Jack" },
                    { 1, "BartCassidy", "", 4, "Bart Cassidy" },
                    { 8, "LuckyDuke", "", 4, "Lucky Duke" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Description", "Name", "RoleType" },
                values: new object[,]
                {
                    { 3, "", "Seriff", "Sheriff" },
                    { 1, "", "Bandita", "Outlaw" },
                    { 2, "", "Renegát", "Renegade" },
                    { 4, "", "Seriffhelyettes", "Vice" }
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

            migrationBuilder.CreateIndex(
                name: "IX_Players_UserId",
                table: "Players",
                column: "UserId");

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

            migrationBuilder.DropTable(
                name: "Account");
        }
    }
}
