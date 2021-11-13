﻿// <auto-generated />
using System;
using Bang.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Bang.DAL.Migrations
{
    [DbContext(typeof(BangDbContext))]
    [Migration("20211112143256_lobbyowner")]
    partial class lobbyowner
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Bang.DAL.Domain.Catalog.Cards.Card", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CardEffectType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CardType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Cards");

                    b.HasDiscriminator<string>("CardEffectType").HasValue("card_base");
                });

            modelBuilder.Entity("Bang.DAL.Domain.Catalog.Character", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CharacterType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaxHP")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Characters");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CharacterType = "BartCassidy",
                            Description = "Minden sebződéskor húzhat egy lapot a pakliból.",
                            MaxHP = 4,
                            Name = "Bart Cassidy"
                        },
                        new
                        {
                            Id = 2,
                            CharacterType = "BlackJack",
                            Description = "Második lapját felcsapva húzza, ha az piros (♥ (kör) vagy ♦ (káró)), húzhat egy harmadikat.",
                            MaxHP = 4,
                            Name = "Black Jack"
                        },
                        new
                        {
                            Id = 3,
                            CharacterType = "CalamityJanet",
                            Description = "Használhat BANG! kártyát Missed!-ként (Elvétve!) és fordítva.Ettől még nem lőhet kettőt).",
                            MaxHP = 4,
                            Name = "Calamity Janet"
                        },
                        new
                        {
                            Id = 4,
                            CharacterType = "ElGringo",
                            Description = "Sebződésenként húzhat egy lapot a sebző kezéből. Ha Dynamite (Dinamit) sebzi, akkor senkitől sem húz.",
                            MaxHP = 3,
                            Name = "El Gringo"
                        },
                        new
                        {
                            Id = 5,
                            CharacterType = "JesseJones",
                            Description = "A saját laphúzás fázisában (1.fázis) mindig eldöntheti, hogy az első lapot a húzópakliból, vagy egy másik játékos kezéből húzza.A második lapot mindig a húzópakliból húzza.",
                            MaxHP = 4,
                            Name = "Jesse Jones"
                        },
                        new
                        {
                            Id = 6,
                            CharacterType = "Jourdonnais",
                            Description = "Úgy kell rá tekinteni, mintha lenne egy Barrel (Hordó) kártya előtte (= „beépített hordó”), vagyis minden ellene kijátszott BANG! ellen felcsaphatja a húzópakli legfelső lapját, és ha az ♥ (kör), akkor elkerülte a lövést. (Ha van még egy hordója, akkor mindkettőre húzhat.) ",
                            MaxHP = 4,
                            Name = "Jourdonnais"
                        },
                        new
                        {
                            Id = 7,
                            CharacterType = "KitCarlson",
                            Description = "A saját laphúzás fázisában (1.fázis) 3 lapot húz, amiből egyet visszatesz a pakli tetejére(tehát nem eldobja!). ",
                            MaxHP = 4,
                            Name = "Kit Carlson"
                        },
                        new
                        {
                            Id = 8,
                            CharacterType = "LuckyDuke",
                            Description = "Mindig, amikor fel kell csapnia egy lapot valamelyik kártya hatása miatt(Dynamite (Dinamit), Barrel (Hordó), Jail (Börtön)),akkor két lapot csaphat fel és a számára kedvezőbbet választhatja.",
                            MaxHP = 4,
                            Name = "Lucky Duke"
                        },
                        new
                        {
                            Id = 9,
                            CharacterType = "PaulRegret",
                            Description = "Minden játékos eggyel nagyobb távolságra levőnek látja őt (= „beépített MUSTANG”). (Ha van még egy MUSTANG - ja, akkor + 2 távra van.)",
                            MaxHP = 3,
                            Name = "Paul Regret"
                        },
                        new
                        {
                            Id = 10,
                            CharacterType = "PedroRamirez",
                            Description = "A saját laphúzás fázisában (1.fázis) az első lapját húzhatja az eldobott lapok tetejéről.A második lapot mindenképpen a húzópakliból húzza.",
                            MaxHP = 4,
                            Name = "Pedro Ramirez"
                        },
                        new
                        {
                            Id = 11,
                            CharacterType = "RoseDoolan",
                            Description = "Minden játékost eggyel kisebb távolságra levőnek lát (= „beépített SCOPE”). (Ha van még egy SCOPE - ja, akkor - 2 távra van)",
                            MaxHP = 4,
                            Name = "Rose Doolan"
                        },
                        new
                        {
                            Id = 12,
                            CharacterType = "SidKetchum",
                            Description = "Két kártyáért visszanyerhet egy életet.",
                            MaxHP = 4,
                            Name = "Sid Ketchum"
                        },
                        new
                        {
                            Id = 13,
                            CharacterType = "SlabTheKiller",
                            Description = "Az ő lövése csak két Missed!-el (Elvétve!) védhető. Az egyik lehet Barrel(Hordó), vagy Jourdonnais esetében akár mindkettő",
                            MaxHP = 4,
                            Name = "Slab the Killer"
                        },
                        new
                        {
                            Id = 14,
                            CharacterType = "SuzyLafayette",
                            Description = "Ha elfogy a lapja a kezéből, azonnal húzhat egy újabbat.",
                            MaxHP = 4,
                            Name = "Suzy Lafayette"
                        },
                        new
                        {
                            Id = 15,
                            CharacterType = "VultureSam",
                            Description = "Mindig, amikor egy játékos kiesik a játékból, elveszi a kieső játékos kézben tartott es asztalon levő lapjait.",
                            MaxHP = 4,
                            Name = "Vulture Sam"
                        },
                        new
                        {
                            Id = 16,
                            CharacterType = "WillyTheKid",
                            Description = "Akárhány lövést leadhat a meglévő fegyverének megfelelő távolságban.",
                            MaxHP = 4,
                            Name = "Willy the Kid"
                        });
                });

            modelBuilder.Entity("Bang.DAL.Domain.Catalog.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "A banditák nyernek, ha a seriff meghal.",
                            Name = "Bandita",
                            RoleType = "Outlaw"
                        },
                        new
                        {
                            Id = 2,
                            Description = "A renegát nyer, ha mindenki mást megöl. (Tehát a renegát csak úgy nyerhet, ha egyedül marad életben és utoljára a seriffet ölte meg.)",
                            Name = "Renegát",
                            RoleType = "Renegade"
                        },
                        new
                        {
                            Id = 3,
                            Description = "A seriff és helyettese(i) nyernek, ha csak ő(k) maradt(ak) életben, azaz akkor, ha minden banditát és a renegátot is megölik és a seriff még él.",
                            Name = "Seriff",
                            RoleType = "Sheriff"
                        },
                        new
                        {
                            Id = 4,
                            Description = "A seriff és helyettese(i) nyernek, ha csak ő(k) maradt(ak) életben, azaz akkor, ha minden banditát és a renegátot is megölik és a seriff még él.",
                            Name = "Seriffhelyettes",
                            RoleType = "Vice"
                        });
                });

            modelBuilder.Entity("Bang.DAL.Domain.GameBoard", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("ActualPlayerId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsOver")
                        .HasColumnType("bit");

                    b.Property<string>("LobbyOwnerId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaxTurnTime")
                        .HasColumnType("int");

                    b.Property<int?>("TargetReason")
                        .HasColumnType("int");

                    b.Property<long?>("TargetedPlayerId")
                        .HasColumnType("bigint");

                    b.Property<long?>("TargetedPlayerId1")
                        .HasColumnType("bigint");

                    b.Property<int>("TurnPhase")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.HasKey("Id");

                    b.HasIndex("ActualPlayerId")
                        .IsUnique()
                        .HasFilter("[ActualPlayerId] IS NOT NULL");

                    b.HasIndex("TargetedPlayerId1");

                    b.ToTable("GameBoards");
                });

            modelBuilder.Entity("Bang.DAL.Domain.Joins.GameBoardCards.GameBoardCard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CardColorType")
                        .HasColumnType("int");

                    b.Property<int>("CardId")
                        .HasColumnType("int");

                    b.Property<int>("FrenchNumber")
                        .HasColumnType("int");

                    b.Property<long>("GameBoardId")
                        .HasColumnType("bigint");

                    b.Property<string>("StatusType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CardId");

                    b.ToTable("GameBoardCards");

                    b.HasDiscriminator<string>("StatusType").HasValue("gameboardcard_base");
                });

            modelBuilder.Entity("Bang.DAL.Domain.Joins.PlayerCard", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CardColorType")
                        .HasColumnType("int");

                    b.Property<int>("CardId")
                        .HasColumnType("int");

                    b.Property<int>("FrenchNumber")
                        .HasColumnType("int");

                    b.Property<long>("PlayerId")
                        .HasColumnType("bigint");

                    b.Property<string>("StatusType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CardId");

                    b.ToTable("PlayerCards");

                    b.HasDiscriminator<string>("StatusType").HasValue("player_card_base");
                });

            modelBuilder.Entity("Bang.DAL.Domain.Player", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ActualHP")
                        .HasColumnType("int");

                    b.Property<int>("CharacterType")
                        .HasColumnType("int");

                    b.Property<long>("GameBoardId")
                        .HasColumnType("bigint");

                    b.Property<int>("MaxHP")
                        .HasColumnType("int");

                    b.Property<int>("Placement")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<string>("PlayedCards")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleType")
                        .HasColumnType("int");

                    b.Property<int>("ShootingRange")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GameBoardId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("Bang.DAL.Domain.Catalog.Cards.ActiveCard", b =>
                {
                    b.HasBaseType("Bang.DAL.Domain.Catalog.Cards.Card");

                    b.HasDiscriminator().HasValue("card_active");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CardEffectType = "card_active",
                            CardType = "Bang",
                            Description = "Rálövés egy játékosra, aki lőtávolon belül van. A megtámadott, ha nem tudja kivédeni(ld.Barrel(Hordó) és Missed!(Elvétve!)), egy életet veszít.Ha meghal és van Beer (Sör) lapja, azonnal kijátszhatja (egyik eset, amikor lapot játszhat ki, aki nincs soron). Egy körben csak egy BANG! játszható ki (kivétel Volcanic fegyver és Willy the Kid személyisége).",
                            Name = "Bang!"
                        },
                        new
                        {
                            Id = 2,
                            CardEffectType = "card_active",
                            CardType = "Missed",
                            Description = "Nem talált!",
                            Name = "Nem talált!"
                        },
                        new
                        {
                            Id = 3,
                            CardEffectType = "card_active",
                            CardType = "Beer",
                            Description = "A játékos egy életet visszanyer. Mindenkinek maximum annyi élete lehet, amennyi kezdéskor volt.Ha valaki meghal és van sör lapja, azonnal kijátszhatja(egyik eset, amikor lapot játszhat ki, aki nincs soron). Ha már csak két játékos van életben, nem lehet sört inni! ",
                            Name = "Sör"
                        },
                        new
                        {
                            Id = 4,
                            CardEffectType = "card_active",
                            CardType = "CatBalou",
                            Description = "Bármelyik másik játékossal eldobathat egy lapot (magával nem) a kezéből véletlenszerűen, vagy az asztalról.Nincs védett lap, minden eldobatható.",
                            Name = "Cat Balou"
                        },
                        new
                        {
                            Id = 5,
                            CardEffectType = "card_active",
                            CardType = "Panic",
                            Description = " Legfeljebb 1 távolságra levő játékostól elhúzhat egy lapot, az asztalról, vagy a kezéből, utóbbiból véletlenszerűen.Nincs védett lap, minden elhúzható.",
                            Name = "Pánik!"
                        },
                        new
                        {
                            Id = 6,
                            CardEffectType = "card_active",
                            CardType = "Duel",
                            Description = "Egy tetszőleges játékos kihívása párbajra (távolságtól függetlenül). A párbaj menete: a felek felváltva lőnek(BANG! lerakásával), a kihívott kezd.Aki először nem tud rakni, az veszít egy életet. (Semelyik elkerülő lap nem érvényesül a párbaj alatt, és a Duel (Párbaj) nem minősül BANG!-nek.) Abban az esetben, ha egy párbajt kezdeményező bandita elveszíti az utolsó életét az érte járó 3 lap jutalmat senki nem kapja meg.",
                            Name = "Párbaj"
                        },
                        new
                        {
                            Id = 7,
                            CardEffectType = "card_active",
                            CardType = "GeneralStore",
                            Description = "Ahány élő játékos játékban van, annyi lapot a pakliból felfordítanak.Mindenki választ egyet, a lapot kijátszó játékos kezdi a sort.",
                            Name = "Szatócsbolt"
                        },
                        new
                        {
                            Id = 8,
                            CardEffectType = "card_active",
                            CardType = "Indians",
                            Description = " Összes többi játékost megtámadja, csak lövéssel (BANG! berakásával) védhető(egyik eset, amikor lapot játszhat ki, aki nincs soron). Nem minősül lövésnek, tehát eben a körben még használható BANG! vagy GATLING.",
                            Name = "Indiánok!"
                        },
                        new
                        {
                            Id = 9,
                            CardEffectType = "card_active",
                            CardType = "Stagecoach",
                            Description = "Kijátszója 2 lapot húzhat a pakliból.",
                            Name = "Postakocsi"
                        },
                        new
                        {
                            Id = 10,
                            CardEffectType = "card_active",
                            CardType = "Gatling",
                            Description = "A kijátszón kívül mindenkire rálő (hordó és elugrás lehet menedék). Nem minősül lövésnek, tehát ebben a körben BANG! még használható. ",
                            Name = "Gatling"
                        },
                        new
                        {
                            Id = 11,
                            CardEffectType = "card_active",
                            CardType = "Saloon",
                            Description = "Mindenki visszanyer egy életet. Úgy használható, mint a Beer (Sör), de mindenki kap egy életet, ha tud(maximum annyi élete lehet mindenkinek, mint kezdéskor volt). Ha már csak két játékos van életben, sört ugyan nem lehet inni, de Saloon(Szalon) lap még kijátszható! ",
                            Name = "Kocsma"
                        },
                        new
                        {
                            Id = 12,
                            CardEffectType = "card_active",
                            CardType = "WellsFargo",
                            Description = "Kijátszója 3 lapot húzhat a pakliból.",
                            Name = "Wells Fargo"
                        });
                });

            modelBuilder.Entity("Bang.DAL.Domain.Catalog.Cards.PassiveCard", b =>
                {
                    b.HasBaseType("Bang.DAL.Domain.Catalog.Cards.Card");

                    b.HasDiscriminator().HasValue("card_passive");

                    b.HasData(
                        new
                        {
                            Id = 13,
                            CardEffectType = "card_passive",
                            CardType = "Jail",
                            Description = "A seriff kivételével bárkire rátehető. Akire kijátszották, amikor sorra kerülne, először felcsap egy lapot, ha az kör, akkor nem kerül börtönbe(megkezdheti a körét az 1. fázissal), különben ebből a körből kimarad(a következőből nem). A lapot mindkét esetben az eldobott lapokra helyezzük.Ha a játékos nem kerül sorra, akkor is el kell dobnia a többletlapjait! Amíg a játékos be van börtönözve, addig lőhetnek rá es használhat Missed! (Elvétve!) es Beer(Sör) lapokat, de Barrel-t(Hordó) nem.Ez a lap is eldobatható Cat Balou-val vagy Panic!-kal(Pánik!). Ha van az asztalon Jail(Börtön) is Dynamite(Dinamit) mellett, akkor az előbb asztalra kerülő érvényesül először. ",
                            Name = "Börtön"
                        },
                        new
                        {
                            Id = 14,
                            CardEffectType = "card_passive",
                            CardType = "Mustang",
                            Description = "Módosítják a távolságokat. Nem lehet két azonos lovunk. A Mustang tulajdonosa mindenkitől egyel messzebb van, ha támadják(védekező lap). Az Scope(Távcső) tulajdonosához mindenki egyel közelebb van, ha támadni akar(támadó lap). (ld.még Paul Regret, Rose Doolan). ",
                            Name = "Musztáng"
                        },
                        new
                        {
                            Id = 15,
                            CardEffectType = "card_passive",
                            CardType = "Barrel",
                            Description = "Aki előtt van hordó, amikor rálőnek, felcsaphat egy lapot, ha az ♥ (kör), akkor nem találták el, ha nem az, akkor még védekezhet más módon. Egy játékos előtt nem lehet több hordó. ",
                            Name = "Hordó"
                        },
                        new
                        {
                            Id = 16,
                            CardEffectType = "card_passive",
                            CardType = "Scope",
                            Description = "Módosítják a távolságokat. Nem lehet két azonos lovunk. A Mustang tulajdonosa mindenkitől egyel messzebb van, ha támadják(védekező lap). Az Scope(Távcső) tulajdonosához mindenki egyel közelebb van, ha támadni akar(támadó lap). (ld.még Paul Regret, Rose Doolan). ",
                            Name = "Távcső"
                        },
                        new
                        {
                            Id = 17,
                            CardEffectType = "card_passive",
                            CardType = "Dynamite",
                            Description = "Aki kijátssza, maga elé helyezi. A következő körtől kezdődően, amikor a dinamitot birtokló játékosra kerül a sor, először felcsap rá egy lapot.Ha ez ♠ (pikk) 2, 3, ..,9, akkor felrobban a dinamit és a játékos 3 életet veszít (nem védhető). Ha nem robbant fel, akkor a lapot a következő játékos elé helyezi. Ez addig megy, míg valakinél fel nem robban, vagy ellopják (pánik / PANIC), eldobatják(Cat Balou). Aki ettől hal meg, annak nincs gyilkosa (ld.jutalmazás-büntetés). Ha van az asztalon Dynamite(Dinamit) is Jail(Börtön) mellett, akkor az előbb asztalra kerülő érvényesül először. ",
                            Name = "Dinamit"
                        },
                        new
                        {
                            Id = 18,
                            CardEffectType = "card_passive",
                            CardType = "Volcanic",
                            Description = "A kártyákon lévő fegyverek meghatározzák, hogy milyen messzire tud lőni a játékos(ld.Lőtávolság). Az asztalra kell helyezni magunk elé. Legfeljebb egy fegyverkártya lehet előttünk, ha újat tesz le valaki, a régit el kell dobnia. Akinek nincs fegyvere, az egy távolságra tud lőni (az alapértelmezett pisztolyával). A Volcanic fegyver sajátossága, hogy tulajdonosa több lövést is leadhat körönként(ld. BANG!, Gatling, Willy the Kid). ",
                            Name = "Gyorstüzelő"
                        },
                        new
                        {
                            Id = 19,
                            CardEffectType = "card_passive",
                            CardType = "Schofield",
                            Description = "A kártyákon lévő fegyverek meghatározzák, hogy milyen messzire tud lőni a játékos(ld.Lőtávolság). Az asztalra kell helyezni magunk elé. Legfeljebb egy fegyverkártya lehet előttünk, ha újat tesz le valaki, a régit el kell dobnia. Akinek nincs fegyvere, az egy távolságra tud lőni (az alapértelmezett pisztolyával). ",
                            Name = "Schofield"
                        },
                        new
                        {
                            Id = 20,
                            CardEffectType = "card_passive",
                            CardType = "Remingtion",
                            Description = "A kártyákon lévő fegyverek meghatározzák, hogy milyen messzire tud lőni a játékos(ld.Lőtávolság). Az asztalra kell helyezni magunk elé. Legfeljebb egy fegyverkártya lehet előttünk, ha újat tesz le valaki, a régit el kell dobnia. Akinek nincs fegyvere, az egy távolságra tud lőni (az alapértelmezett pisztolyával). ",
                            Name = "Remingtion"
                        },
                        new
                        {
                            Id = 21,
                            CardEffectType = "card_passive",
                            CardType = "Karabine",
                            Description = "A kártyákon lévő fegyverek meghatározzák, hogy milyen messzire tud lőni a játékos(ld.Lőtávolság). Az asztalra kell helyezni magunk elé. Legfeljebb egy fegyverkártya lehet előttünk, ha újat tesz le valaki, a régit el kell dobnia. Akinek nincs fegyvere, az egy távolságra tud lőni (az alapértelmezett pisztolyával). ",
                            Name = "Karabély"
                        },
                        new
                        {
                            Id = 22,
                            CardEffectType = "card_passive",
                            CardType = "Winchester",
                            Description = "A kártyákon lévő fegyverek meghatározzák, hogy milyen messzire tud lőni a játékos(ld.Lőtávolság). Az asztalra kell helyezni magunk elé. Legfeljebb egy fegyverkártya lehet előttünk, ha újat tesz le valaki, a régit el kell dobnia. Akinek nincs fegyvere, az egy távolságra tud lőni (az alapértelmezett pisztolyával). ",
                            Name = "Winchester"
                        });
                });

            modelBuilder.Entity("Bang.DAL.Domain.Joins.GameBoardCards.DiscardedGameBoardCard", b =>
                {
                    b.HasBaseType("Bang.DAL.Domain.Joins.GameBoardCards.GameBoardCard");

                    b.HasIndex("GameBoardId");

                    b.HasDiscriminator().HasValue("gameboardcard_discarded");
                });

            modelBuilder.Entity("Bang.DAL.Domain.Joins.GameBoardCards.DrawableGameBoardCard", b =>
                {
                    b.HasBaseType("Bang.DAL.Domain.Joins.GameBoardCards.GameBoardCard");

                    b.HasIndex("GameBoardId");

                    b.HasDiscriminator().HasValue("gameboardcard_drawable");
                });

            modelBuilder.Entity("Bang.DAL.Domain.Joins.GameBoardCards.ScatteredGameBoardCard", b =>
                {
                    b.HasBaseType("Bang.DAL.Domain.Joins.GameBoardCards.GameBoardCard");

                    b.HasIndex("GameBoardId");

                    b.HasDiscriminator().HasValue("gameboardcard_scattered");
                });

            modelBuilder.Entity("Bang.DAL.Domain.Joins.PlayerCards.HandPlayerCard", b =>
                {
                    b.HasBaseType("Bang.DAL.Domain.Joins.PlayerCard");

                    b.HasIndex("PlayerId");

                    b.HasDiscriminator().HasValue("player_card_hand");
                });

            modelBuilder.Entity("Bang.DAL.Domain.Joins.PlayerCards.TablePlayerCard", b =>
                {
                    b.HasBaseType("Bang.DAL.Domain.Joins.PlayerCard");

                    b.HasIndex("PlayerId");

                    b.HasDiscriminator().HasValue("player_card_table");
                });

            modelBuilder.Entity("Bang.DAL.Domain.GameBoard", b =>
                {
                    b.HasOne("Bang.DAL.Domain.Player", "ActualPlayer")
                        .WithOne()
                        .HasForeignKey("Bang.DAL.Domain.GameBoard", "ActualPlayerId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Bang.DAL.Domain.Player", "TargetedPlayer")
                        .WithMany()
                        .HasForeignKey("TargetedPlayerId1");

                    b.Navigation("ActualPlayer");

                    b.Navigation("TargetedPlayer");
                });

            modelBuilder.Entity("Bang.DAL.Domain.Joins.GameBoardCards.GameBoardCard", b =>
                {
                    b.HasOne("Bang.DAL.Domain.Catalog.Cards.Card", "Card")
                        .WithMany()
                        .HasForeignKey("CardId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Card");
                });

            modelBuilder.Entity("Bang.DAL.Domain.Joins.PlayerCard", b =>
                {
                    b.HasOne("Bang.DAL.Domain.Catalog.Cards.Card", "Card")
                        .WithMany()
                        .HasForeignKey("CardId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Card");
                });

            modelBuilder.Entity("Bang.DAL.Domain.Player", b =>
                {
                    b.HasOne("Bang.DAL.Domain.GameBoard", "GameBoard")
                        .WithMany("Players")
                        .HasForeignKey("GameBoardId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("GameBoard");
                });

            modelBuilder.Entity("Bang.DAL.Domain.Joins.GameBoardCards.DiscardedGameBoardCard", b =>
                {
                    b.HasOne("Bang.DAL.Domain.GameBoard", "GameBoard")
                        .WithMany("DiscardedGameBoardCards")
                        .HasForeignKey("GameBoardId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("GameBoard");
                });

            modelBuilder.Entity("Bang.DAL.Domain.Joins.GameBoardCards.DrawableGameBoardCard", b =>
                {
                    b.HasOne("Bang.DAL.Domain.GameBoard", "GameBoard")
                        .WithMany("DrawableGameBoardCards")
                        .HasForeignKey("GameBoardId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("GameBoard");
                });

            modelBuilder.Entity("Bang.DAL.Domain.Joins.GameBoardCards.ScatteredGameBoardCard", b =>
                {
                    b.HasOne("Bang.DAL.Domain.GameBoard", "GameBoard")
                        .WithMany("ScatteredGameBoardCards")
                        .HasForeignKey("GameBoardId")
                        .HasConstraintName("FK_GameBoardCards_GameBoards_GameBoardId1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GameBoard");
                });

            modelBuilder.Entity("Bang.DAL.Domain.Joins.PlayerCards.HandPlayerCard", b =>
                {
                    b.HasOne("Bang.DAL.Domain.Player", "Player")
                        .WithMany("HandPlayerCards")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Player");
                });

            modelBuilder.Entity("Bang.DAL.Domain.Joins.PlayerCards.TablePlayerCard", b =>
                {
                    b.HasOne("Bang.DAL.Domain.Player", "Player")
                        .WithMany("TablePlayerCards")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Player");
                });

            modelBuilder.Entity("Bang.DAL.Domain.GameBoard", b =>
                {
                    b.Navigation("DiscardedGameBoardCards");

                    b.Navigation("DrawableGameBoardCards");

                    b.Navigation("Players");

                    b.Navigation("ScatteredGameBoardCards");
                });

            modelBuilder.Entity("Bang.DAL.Domain.Player", b =>
                {
                    b.Navigation("HandPlayerCards");

                    b.Navigation("TablePlayerCards");
                });
#pragma warning restore 612, 618
        }
    }
}
