﻿// <auto-generated />
using Bang.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Bang.DAL.Migrations
{
    [DbContext(typeof(BangDbContext))]
    [Migration("20210908202506_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
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
                            Name = ""
                        },
                        new
                        {
                            Id = 2,
                            CharacterType = "BlackJack",
                            Description = "",
                            MaxHP = 4,
                            Name = ""
                        },
                        new
                        {
                            Id = 3,
                            CharacterType = "CalamityJanet",
                            Description = "",
                            MaxHP = 4,
                            Name = ""
                        },
                        new
                        {
                            Id = 4,
                            CharacterType = "ElGringo",
                            Description = "",
                            MaxHP = 3,
                            Name = ""
                        },
                        new
                        {
                            Id = 5,
                            CharacterType = "JesseJones",
                            Description = "",
                            MaxHP = 4,
                            Name = ""
                        },
                        new
                        {
                            Id = 6,
                            CharacterType = "Jourdonnais",
                            Description = "",
                            MaxHP = 4,
                            Name = ""
                        },
                        new
                        {
                            Id = 7,
                            CharacterType = "KitCarlson",
                            Description = "",
                            MaxHP = 4,
                            Name = ""
                        },
                        new
                        {
                            Id = 8,
                            CharacterType = "LuckyDuke",
                            Description = "",
                            MaxHP = 4,
                            Name = ""
                        },
                        new
                        {
                            Id = 9,
                            CharacterType = "PaulRegret",
                            Description = "",
                            MaxHP = 3,
                            Name = ""
                        },
                        new
                        {
                            Id = 10,
                            CharacterType = "PedroRamirez",
                            Description = "",
                            MaxHP = 4,
                            Name = ""
                        },
                        new
                        {
                            Id = 11,
                            CharacterType = "RoseDoolan",
                            Description = "",
                            MaxHP = 4,
                            Name = ""
                        },
                        new
                        {
                            Id = 12,
                            CharacterType = "SidKetchum",
                            Description = "",
                            MaxHP = 4,
                            Name = ""
                        },
                        new
                        {
                            Id = 13,
                            CharacterType = "SlabTheKiller",
                            Description = "",
                            MaxHP = 4,
                            Name = ""
                        },
                        new
                        {
                            Id = 14,
                            CharacterType = "SuzyLafayette",
                            Description = "",
                            MaxHP = 4,
                            Name = ""
                        },
                        new
                        {
                            Id = 15,
                            CharacterType = "VultureSam",
                            Description = "",
                            MaxHP = 4,
                            Name = ""
                        },
                        new
                        {
                            Id = 16,
                            CharacterType = "WillyTheKid",
                            Description = "",
                            MaxHP = 4,
                            Name = ""
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
                            Name = "",
                            RoleType = "Outlaw"
                        },
                        new
                        {
                            Id = 2,
                            Description = "",
                            Name = "",
                            RoleType = "Renegade"
                        },
                        new
                        {
                            Id = 3,
                            Description = "",
                            Name = "",
                            RoleType = "Sheriff"
                        },
                        new
                        {
                            Id = 4,
                            Description = "",
                            Name = "",
                            RoleType = "Vice"
                        });
                });

            modelBuilder.Entity("Bang.DAL.Domain.GameBoard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ActualPlayerId")
                        .HasColumnType("int");

                    b.Property<bool>("IsOver")
                        .HasColumnType("bit");

                    b.Property<int>("MaxTurnTime")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ActualPlayerId")
                        .IsUnique();

                    b.ToTable("GameBoard");
                });

            modelBuilder.Entity("Bang.DAL.Domain.Joins.GameBoardCards.GameBoardCard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CardId")
                        .HasColumnType("int");

                    b.Property<int>("GameBoardId")
                        .HasColumnType("int");

                    b.Property<string>("StatusType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("GameBoardCard");

                    b.HasDiscriminator<string>("StatusType").HasValue("gameboardcard_base");
                });

            modelBuilder.Entity("Bang.DAL.Domain.Joins.PlayerCard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CardId")
                        .HasColumnType("int");

                    b.Property<int>("PlayerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CardId")
                        .IsUnique();

                    b.HasIndex("PlayerId");

                    b.ToTable("PlayerCard");
                });

            modelBuilder.Entity("Bang.DAL.Domain.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ActualHP")
                        .HasColumnType("int");

                    b.Property<string>("CharacterType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GameBoardId")
                        .HasColumnType("int");

                    b.Property<int>("MaxHP")
                        .HasColumnType("int");

                    b.Property<string>("RoleType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GameBoardId");

                    b.ToTable("Player");
                });

            modelBuilder.Entity("Bang.DAL.Domain.Catalog.Cards.ActiveCard", b =>
                {
                    b.HasBaseType("Bang.DAL.Domain.Catalog.Cards.Card");

                    b.Property<string>("CardType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("card_active");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CardEffectType = "card_active",
                            Description = "",
                            Name = "Bang!",
                            CardType = "Bang"
                        },
                        new
                        {
                            Id = 3,
                            CardEffectType = "card_active",
                            Description = "",
                            Name = "Sör",
                            CardType = "Beer"
                        },
                        new
                        {
                            Id = 4,
                            CardEffectType = "card_active",
                            Description = "",
                            Name = "Cat Balou",
                            CardType = "CatBalou"
                        },
                        new
                        {
                            Id = 5,
                            CardEffectType = "card_active",
                            Description = "",
                            Name = "Párbaj",
                            CardType = "Duel"
                        },
                        new
                        {
                            Id = 7,
                            CardEffectType = "card_active",
                            Description = "",
                            Name = "Gatling",
                            CardType = "Gatling"
                        },
                        new
                        {
                            Id = 8,
                            CardEffectType = "card_active",
                            Description = "",
                            Name = "Zsibvásár",
                            CardType = "GeneralStore"
                        },
                        new
                        {
                            Id = 11,
                            CardEffectType = "card_active",
                            Description = "",
                            Name = "Indiánok!",
                            CardType = "Indians"
                        },
                        new
                        {
                            Id = 13,
                            CardEffectType = "card_active",
                            Description = "",
                            Name = "Elvétve!",
                            CardType = "Missed"
                        },
                        new
                        {
                            Id = 14,
                            CardEffectType = "card_active",
                            Description = "",
                            Name = "Pánik!",
                            CardType = "Panic"
                        },
                        new
                        {
                            Id = 15,
                            CardEffectType = "card_active",
                            Description = "",
                            Name = "",
                            CardType = "Saloon"
                        },
                        new
                        {
                            Id = 16,
                            CardEffectType = "card_active",
                            Description = "",
                            Name = "",
                            CardType = "Stagecoach"
                        },
                        new
                        {
                            Id = 17,
                            CardEffectType = "card_active",
                            Description = "",
                            Name = "",
                            CardType = "WellsFargo"
                        });
                });

            modelBuilder.Entity("Bang.DAL.Domain.Catalog.Cards.PassiveCard", b =>
                {
                    b.HasBaseType("Bang.DAL.Domain.Catalog.Cards.Card");

                    b.Property<string>("CardType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("PassiveCard_CardType");

                    b.HasDiscriminator().HasValue("card_passive");

                    b.HasData(
                        new
                        {
                            Id = 2,
                            CardEffectType = "card_passive",
                            Description = "",
                            Name = "",
                            CardType = "Barrel"
                        },
                        new
                        {
                            Id = 6,
                            CardEffectType = "card_passive",
                            Description = "",
                            Name = "",
                            CardType = "Dynamite"
                        },
                        new
                        {
                            Id = 9,
                            CardEffectType = "card_passive",
                            Description = "",
                            Name = "",
                            CardType = "Guns"
                        },
                        new
                        {
                            Id = 10,
                            CardEffectType = "card_passive",
                            Description = "",
                            Name = "",
                            CardType = "Horses"
                        },
                        new
                        {
                            Id = 12,
                            CardEffectType = "card_passive",
                            Description = "",
                            Name = "",
                            CardType = "Jail"
                        });
                });

            modelBuilder.Entity("Bang.DAL.Domain.Joins.GameBoardCards.DiscardedGameBoardCard", b =>
                {
                    b.HasBaseType("Bang.DAL.Domain.Joins.GameBoardCards.GameBoardCard");

                    b.HasIndex("CardId")
                        .IsUnique();

                    b.HasIndex("GameBoardId");

                    b.HasDiscriminator().HasValue("gameboardcard_discarded");
                });

            modelBuilder.Entity("Bang.DAL.Domain.Joins.GameBoardCards.DrawableGameBoardCard", b =>
                {
                    b.HasBaseType("Bang.DAL.Domain.Joins.GameBoardCards.GameBoardCard");

                    b.HasIndex("CardId")
                        .IsUnique();

                    b.HasIndex("GameBoardId");

                    b.HasDiscriminator().HasValue("gameboardcard_drawable");
                });

            modelBuilder.Entity("Bang.DAL.Domain.GameBoard", b =>
                {
                    b.HasOne("Bang.DAL.Domain.Player", "ActualPlayer")
                        .WithOne()
                        .HasForeignKey("Bang.DAL.Domain.GameBoard", "ActualPlayerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("ActualPlayer");
                });

            modelBuilder.Entity("Bang.DAL.Domain.Joins.PlayerCard", b =>
                {
                    b.HasOne("Bang.DAL.Domain.Catalog.Cards.Card", "Card")
                        .WithOne()
                        .HasForeignKey("Bang.DAL.Domain.Joins.PlayerCard", "CardId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Bang.DAL.Domain.Player", "Player")
                        .WithMany("PlayerCards")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Card");

                    b.Navigation("Player");
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
                    b.HasOne("Bang.DAL.Domain.Catalog.Cards.Card", "Card")
                        .WithOne()
                        .HasForeignKey("Bang.DAL.Domain.Joins.GameBoardCards.DiscardedGameBoardCard", "CardId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Bang.DAL.Domain.GameBoard", "GameBoard")
                        .WithMany("DiscardedGameBoardCards")
                        .HasForeignKey("GameBoardId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Card");

                    b.Navigation("GameBoard");
                });

            modelBuilder.Entity("Bang.DAL.Domain.Joins.GameBoardCards.DrawableGameBoardCard", b =>
                {
                    b.HasOne("Bang.DAL.Domain.Catalog.Cards.Card", "Card")
                        .WithOne()
                        .HasForeignKey("Bang.DAL.Domain.Joins.GameBoardCards.DrawableGameBoardCard", "CardId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Bang.DAL.Domain.GameBoard", "GameBoard")
                        .WithMany("DrawableGameBoardCards")
                        .HasForeignKey("GameBoardId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Card");

                    b.Navigation("GameBoard");
                });

            modelBuilder.Entity("Bang.DAL.Domain.GameBoard", b =>
                {
                    b.Navigation("DiscardedGameBoardCards");

                    b.Navigation("DrawableGameBoardCards");

                    b.Navigation("Players");
                });

            modelBuilder.Entity("Bang.DAL.Domain.Player", b =>
                {
                    b.Navigation("PlayerCards");
                });
#pragma warning restore 612, 618
        }
    }
}