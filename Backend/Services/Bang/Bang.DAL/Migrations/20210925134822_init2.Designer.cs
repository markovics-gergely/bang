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
    [Migration("20210925134822_init2")]
    partial class init2
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
                            Description = "",
                            MaxHP = 4,
                            Name = "Bart Cassidy"
                        },
                        new
                        {
                            Id = 2,
                            CharacterType = "BlackJack",
                            Description = "",
                            MaxHP = 4,
                            Name = "Black Jack"
                        },
                        new
                        {
                            Id = 3,
                            CharacterType = "CalamityJanet",
                            Description = "",
                            MaxHP = 4,
                            Name = "Calamity Janet"
                        },
                        new
                        {
                            Id = 4,
                            CharacterType = "ElGringo",
                            Description = "",
                            MaxHP = 3,
                            Name = "El Gringo"
                        },
                        new
                        {
                            Id = 5,
                            CharacterType = "JesseJones",
                            Description = "",
                            MaxHP = 4,
                            Name = "Jesse Jones"
                        },
                        new
                        {
                            Id = 6,
                            CharacterType = "Jourdonnais",
                            Description = "",
                            MaxHP = 4,
                            Name = "Jourdonnais"
                        },
                        new
                        {
                            Id = 7,
                            CharacterType = "KitCarlson",
                            Description = "",
                            MaxHP = 4,
                            Name = "Kit Carlson"
                        },
                        new
                        {
                            Id = 8,
                            CharacterType = "LuckyDuke",
                            Description = "",
                            MaxHP = 4,
                            Name = "Lucky Duke"
                        },
                        new
                        {
                            Id = 9,
                            CharacterType = "PaulRegret",
                            Description = "",
                            MaxHP = 3,
                            Name = "Paul Regret"
                        },
                        new
                        {
                            Id = 10,
                            CharacterType = "PedroRamirez",
                            Description = "",
                            MaxHP = 4,
                            Name = "Pedro Ramirez"
                        },
                        new
                        {
                            Id = 11,
                            CharacterType = "RoseDoolan",
                            Description = "",
                            MaxHP = 4,
                            Name = "Rose Doolan"
                        },
                        new
                        {
                            Id = 12,
                            CharacterType = "SidKetchum",
                            Description = "",
                            MaxHP = 4,
                            Name = "Sid Ketchum"
                        },
                        new
                        {
                            Id = 13,
                            CharacterType = "SlabTheKiller",
                            Description = "",
                            MaxHP = 4,
                            Name = "Slab the Killer"
                        },
                        new
                        {
                            Id = 14,
                            CharacterType = "SuzyLafayette",
                            Description = "",
                            MaxHP = 4,
                            Name = "Suzy Lafayette"
                        },
                        new
                        {
                            Id = 15,
                            CharacterType = "VultureSam",
                            Description = "",
                            MaxHP = 4,
                            Name = "Vulture Sam"
                        },
                        new
                        {
                            Id = 16,
                            CharacterType = "WillyTheKid",
                            Description = "",
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
                            Description = "",
                            Name = "Bandita",
                            RoleType = "Outlaw"
                        },
                        new
                        {
                            Id = 2,
                            Description = "",
                            Name = "Renegát",
                            RoleType = "Renegade"
                        },
                        new
                        {
                            Id = 3,
                            Description = "",
                            Name = "Seriff",
                            RoleType = "Sheriff"
                        },
                        new
                        {
                            Id = 4,
                            Description = "",
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

                    b.Property<int>("MaxTurnTime")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ActualPlayerId")
                        .IsUnique()
                        .HasFilter("[ActualPlayerId] IS NOT NULL");

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

                    b.Property<int>("RoleType")
                        .HasColumnType("int");

                    b.Property<int>("ShootingRange")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<string>("UserId")
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
                            Description = "Bang!",
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
                            Description = "Sör",
                            Name = "Sör"
                        },
                        new
                        {
                            Id = 4,
                            CardEffectType = "card_active",
                            CardType = "CatBalou",
                            Description = "Cat Balou",
                            Name = "Cat Balou"
                        },
                        new
                        {
                            Id = 5,
                            CardEffectType = "card_active",
                            CardType = "Panic",
                            Description = "Pánik!",
                            Name = "Pánik!"
                        },
                        new
                        {
                            Id = 6,
                            CardEffectType = "card_active",
                            CardType = "Duel",
                            Description = "Párbaj",
                            Name = "Párbaj"
                        },
                        new
                        {
                            Id = 7,
                            CardEffectType = "card_active",
                            CardType = "GeneralStore",
                            Description = "Szatócsbolt",
                            Name = "Szatócsbolt"
                        },
                        new
                        {
                            Id = 8,
                            CardEffectType = "card_active",
                            CardType = "Indians",
                            Description = "Indiánok!",
                            Name = "Indiánok!"
                        },
                        new
                        {
                            Id = 9,
                            CardEffectType = "card_active",
                            CardType = "Stagecoach",
                            Description = "Postakocsi",
                            Name = "Postakocsi"
                        },
                        new
                        {
                            Id = 10,
                            CardEffectType = "card_active",
                            CardType = "Gatling",
                            Description = "Gatling",
                            Name = "Gatling"
                        },
                        new
                        {
                            Id = 11,
                            CardEffectType = "card_active",
                            CardType = "Saloon",
                            Description = "Kocsma",
                            Name = "Kocsma"
                        },
                        new
                        {
                            Id = 12,
                            CardEffectType = "card_active",
                            CardType = "WellsFargo",
                            Description = "Wells Fargo",
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
                            Description = "Börtön",
                            Name = "Börtön"
                        },
                        new
                        {
                            Id = 14,
                            CardEffectType = "card_passive",
                            CardType = "Mustang",
                            Description = "Musztáng",
                            Name = "Musztáng"
                        },
                        new
                        {
                            Id = 15,
                            CardEffectType = "card_passive",
                            CardType = "Barrel",
                            Description = "Hordó",
                            Name = "Hordó"
                        },
                        new
                        {
                            Id = 16,
                            CardEffectType = "card_passive",
                            CardType = "Scope",
                            Description = "Távcső",
                            Name = "Távcső"
                        },
                        new
                        {
                            Id = 17,
                            CardEffectType = "card_passive",
                            CardType = "Dynamite",
                            Description = "Dinamit",
                            Name = "Dinamit"
                        },
                        new
                        {
                            Id = 18,
                            CardEffectType = "card_passive",
                            CardType = "Volcanic",
                            Description = "Gyorstüzelő",
                            Name = "Gyorstüzelő"
                        },
                        new
                        {
                            Id = 19,
                            CardEffectType = "card_passive",
                            CardType = "Schofield",
                            Description = "Schofield",
                            Name = "Schofield"
                        },
                        new
                        {
                            Id = 20,
                            CardEffectType = "card_passive",
                            CardType = "Remingtion",
                            Description = "Remingtion",
                            Name = "Remingtion"
                        },
                        new
                        {
                            Id = 21,
                            CardEffectType = "card_passive",
                            CardType = "Karabine",
                            Description = "Karabély",
                            Name = "Karabély"
                        },
                        new
                        {
                            Id = 22,
                            CardEffectType = "card_passive",
                            CardType = "Winchester",
                            Description = "Winchester",
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

                    b.Navigation("ActualPlayer");
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
