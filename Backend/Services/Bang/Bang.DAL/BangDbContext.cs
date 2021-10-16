using Bang.DAL.Domain;
using Bang.DAL.Domain.Catalog;
using Bang.DAL.Domain.Catalog.Cards;
using Bang.DAL.Domain.Constants;
using Bang.DAL.Domain.Constants.DescriptionConstants;
using Bang.DAL.Domain.Constants.Enums;
using Bang.DAL.Domain.Constants.NameConstants;
using Bang.DAL.Domain.Joins;
using Bang.DAL.Domain.Joins.GameBoardCards;
using Bang.DAL.Domain.Joins.PlayerCards;

using System.Linq;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Bang.DAL.Converters;
using Bang.DAL.Comparers;

namespace Bang.DAL
{
    public class BangDbContext : DbContext
    {
        public BangDbContext(DbContextOptions<BangDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<GameBoard>()
                .HasMany(g => g.Players)
                .WithOne(p => p.GameBoard)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<GameBoard>()
                .HasOne(g => g.ActualPlayer)
                .WithOne()
                .HasForeignKey<GameBoard>(d => d.ActualPlayerId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<GameBoard>()
                .HasMany(g => g.DrawableGameBoardCards)
                .WithOne(d => d.GameBoard)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<GameBoard>()
                .HasMany(g => g.DiscardedGameBoardCards)
                .WithOne(d => d.GameBoard)
                .HasForeignKey(d => d.GameBoardId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<DiscardedGameBoardCard>()
                .HasOne(g => g.Card)
                .WithMany()
                .HasForeignKey(c => c.CardId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<DrawableGameBoardCard>()
                .HasOne(g => g.Card)
                .WithMany()
                .HasForeignKey(c => c.CardId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ScatteredGameBoardCard>()
                .HasOne(g => g.Card)
                .WithMany()
                .HasForeignKey(c => c.CardId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<HandPlayerCard>()
                .HasOne(p => p.Card)
                .WithMany()
                .HasForeignKey(c => c.CardId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<HandPlayerCard>()
                .HasOne(p => p.Player)
                .WithMany(p => p.HandPlayerCards)
                .HasForeignKey(c => c.PlayerId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TablePlayerCard>()
                .HasOne(p => p.Card)
                .WithMany()
                .HasForeignKey(c => c.CardId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TablePlayerCard>()
                .HasOne(p => p.Player)
                .WithMany(p => p.TablePlayerCards)
                .HasForeignKey(c => c.PlayerId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<GameBoardCard>()
                .HasDiscriminator(g => g.StatusType)
                .HasValue<GameBoardCard>(GameBoardCardConstants.Base)
                .HasValue<DrawableGameBoardCard>(GameBoardCardConstants.DrawableCard)
                .HasValue<DiscardedGameBoardCard>(GameBoardCardConstants.DiscardedCard)
                .HasValue<ScatteredGameBoardCard>(GameBoardCardConstants.ScatteredCard);

            modelBuilder.Entity<PlayerCard>()
                .HasDiscriminator(p => p.StatusType)
                .HasValue<PlayerCard>(PlayerCardConstants.Base)
                .HasValue<HandPlayerCard>(PlayerCardConstants.HandPlayerCard)
                .HasValue<TablePlayerCard>(PlayerCardConstants.TablePlayerCard);

            modelBuilder.Entity<Card>()
                .HasDiscriminator(c => c.CardEffectType)
                .HasValue<Card>(CardConstants.Base)
                .HasValue<ActiveCard>(CardConstants.ActiveCard)
                .HasValue<PassiveCard>(CardConstants.PassiveCard);

            var cardConverter = new EnumToStringConverter<CardType>();
            modelBuilder
                .Entity<Card>()
                .Property(c => c.CardType)
                .HasConversion(cardConverter);

            var characterConverter = new EnumToStringConverter<CharacterType>();
            modelBuilder
                .Entity<Character>()
                .Property(c => c.CharacterType)
                .HasConversion(characterConverter);

            var roleConverter = new EnumToStringConverter<RoleType>();
            modelBuilder
                .Entity<Role>()
                .Property(r => r.RoleType)
                .HasConversion(roleConverter);

            modelBuilder
                .Entity<ActiveCard>()
                .Property(r => r.CardType)
                .HasConversion(cardConverter);

            modelBuilder
                .Entity<PassiveCard>()
                .Property(r => r.CardType)
                .HasConversion(cardConverter);

            modelBuilder
                .Entity<Player>()
                .Property(p => p.ShootingRange)
                .HasDefaultValue(1);

            modelBuilder
                .Entity<Player>()
                .Property(p => p.Placement)
                .HasDefaultValue(0);

            var playedConverter = new EnumCollectionJsonValueConverter<CardType>();
            var playedComparer = new CollectionValueComparer<CardType>();
            modelBuilder
                .Entity<Player>()
                .Property(p => p.PlayedCards)
                .HasConversion(playedConverter)
                .Metadata.SetValueComparer(playedComparer);

            modelBuilder
                .Entity<GameBoard>()
                .Property(g => g.TurnPhase)
                .HasDefaultValue(PhaseEnum.Drawing);

            modelBuilder.Entity<ActiveCard>()
                .HasData(
                    new ActiveCard { Id = 1, Name = CardNameConstants.Bang, Description = CardDescriptionConstants.Bang, CardType = CardType.Bang, CardEffectType = CardConstants.ActiveCard },
                    new ActiveCard { Id = 2, Name = CardNameConstants.Missed, Description = CardDescriptionConstants.Missed, CardType = CardType.Missed, CardEffectType = CardConstants.ActiveCard },
                    new ActiveCard { Id = 3, Name = CardNameConstants.Beer, Description = CardDescriptionConstants.Beer, CardType = CardType.Beer, CardEffectType = CardConstants.ActiveCard },
                    new ActiveCard { Id = 4, Name = CardNameConstants.CatBalou, Description = CardDescriptionConstants.CatBalou, CardType = CardType.CatBalou, CardEffectType = CardConstants.ActiveCard },
                    new ActiveCard { Id = 5, Name = CardNameConstants.Panic, Description = CardDescriptionConstants.Panic, CardType = CardType.Panic, CardEffectType = CardConstants.ActiveCard },
                    new ActiveCard { Id = 6, Name = CardNameConstants.Duel, Description = CardDescriptionConstants.Duel, CardType = CardType.Duel, CardEffectType = CardConstants.ActiveCard },      
                    new ActiveCard { Id = 7, Name = CardNameConstants.GeneralStore, Description = CardDescriptionConstants.GeneralStore, CardType = CardType.GeneralStore, CardEffectType = CardConstants.ActiveCard },
                    new ActiveCard { Id = 8, Name = CardNameConstants.Indians, Description = CardDescriptionConstants.Indians, CardType = CardType.Indians, CardEffectType = CardConstants.ActiveCard },                    
                    new ActiveCard { Id = 9, Name = CardNameConstants.Stagecoach, Description = CardDescriptionConstants.Stagecoach, CardType = CardType.Stagecoach, CardEffectType = CardConstants.ActiveCard },
                    new ActiveCard { Id = 10, Name = CardNameConstants.Gatling, Description = CardDescriptionConstants.Gatling, CardType = CardType.Gatling, CardEffectType = CardConstants.ActiveCard },
                    new ActiveCard { Id = 11, Name = CardNameConstants.Saloon, Description = CardDescriptionConstants.Saloon, CardType = CardType.Saloon, CardEffectType = CardConstants.ActiveCard },
                    new ActiveCard { Id = 12, Name = CardNameConstants.WellsFargo, Description = CardDescriptionConstants.WellsFargo, CardType = CardType.WellsFargo, CardEffectType = CardConstants.ActiveCard }
                );

            modelBuilder.Entity<PassiveCard>()
                .HasData(
                    new PassiveCard { Id = 13, Name = CardNameConstants.Jail, Description = CardDescriptionConstants.Jail, CardType = CardType.Jail, CardEffectType = CardConstants.PassiveCard },
                    new PassiveCard { Id = 14, Name = CardNameConstants.Horses, Description = CardDescriptionConstants.Mustang, CardType = CardType.Mustang, CardEffectType = CardConstants.PassiveCard },
                    new PassiveCard { Id = 15, Name = CardNameConstants.Barrel, Description = CardDescriptionConstants.Barrel, CardType = CardType.Barrel, CardEffectType = CardConstants.PassiveCard },
                    new PassiveCard { Id = 16, Name = CardNameConstants.Scope, Description = CardDescriptionConstants.Scope, CardType = CardType.Scope, CardEffectType = CardConstants.PassiveCard },
                    new PassiveCard { Id = 17, Name = CardNameConstants.Dynamite, Description = CardDescriptionConstants.Dynamite, CardType = CardType.Dynamite, CardEffectType = CardConstants.PassiveCard },
                    new PassiveCard { Id = 18, Name = CardNameConstants.Volcanic, Description = CardDescriptionConstants.Volcanic, CardType = CardType.Volcanic, CardEffectType = CardConstants.PassiveCard },
                    new PassiveCard { Id = 19, Name = CardNameConstants.Schofield, Description = CardDescriptionConstants.Schofield, CardType = CardType.Schofield, CardEffectType = CardConstants.PassiveCard },
                    new PassiveCard { Id = 20, Name = CardNameConstants.Remingtion, Description = CardDescriptionConstants.Remingtion, CardType = CardType.Remingtion, CardEffectType = CardConstants.PassiveCard },
                    new PassiveCard { Id = 21, Name = CardNameConstants.Karabine, Description = CardDescriptionConstants.Karabine, CardType = CardType.Karabine, CardEffectType = CardConstants.PassiveCard },
                    new PassiveCard { Id = 22, Name = CardNameConstants.Winchester, Description = CardDescriptionConstants.Winchester, CardType = CardType.Winchester, CardEffectType = CardConstants.PassiveCard }
                );

            modelBuilder.Entity<Role>()
                .HasData(
                    new Role { Id = 1, Name = RoleNameConstants.Outlaw, Description = RoleDescriptionConstants.Outlaw, RoleType = RoleType.Outlaw },
                    new Role { Id = 2, Name = RoleNameConstants.Renegade, Description = RoleDescriptionConstants.Renegade, RoleType = RoleType.Renegade },
                    new Role { Id = 3, Name = RoleNameConstants.Sheriff, Description = RoleDescriptionConstants.Sheriff, RoleType = RoleType.Sheriff },
                    new Role { Id = 4, Name = RoleNameConstants.Vice, Description = RoleDescriptionConstants.Vice, RoleType = RoleType.Vice }
                );

            modelBuilder.Entity<Character>()
                .HasData(
                    new Character { Id = 1, Name = CharacterNameConstants.BartCassidy, Description = CharacterDescriptionConstants.BartCassidy, CharacterType = CharacterType.BartCassidy, MaxHP = 4 },
                    new Character { Id = 2, Name = CharacterNameConstants.BlackJack, Description = CharacterDescriptionConstants.BlackJack, CharacterType = CharacterType.BlackJack, MaxHP = 4 },
                    new Character { Id = 3, Name = CharacterNameConstants.CalamityJanet, Description = CharacterDescriptionConstants.CalamityJanet, CharacterType = CharacterType.CalamityJanet, MaxHP = 4 },
                    new Character { Id = 4, Name = CharacterNameConstants.ElGringo, Description = CharacterDescriptionConstants.ElGringo, CharacterType = CharacterType.ElGringo, MaxHP = 3 },
                    new Character { Id = 5, Name = CharacterNameConstants.JesseJones, Description = CharacterDescriptionConstants.JesseJones, CharacterType = CharacterType.JesseJones, MaxHP = 4 },
                    new Character { Id = 6, Name = CharacterNameConstants.Jourdonnais, Description = CharacterDescriptionConstants.Jourdonnais, CharacterType = CharacterType.Jourdonnais, MaxHP = 4 },
                    new Character { Id = 7, Name = CharacterNameConstants.KitCarlson, Description = CharacterDescriptionConstants.KitCarlson, CharacterType = CharacterType.KitCarlson, MaxHP = 4 },
                    new Character { Id = 8, Name = CharacterNameConstants.LuckyDuke, Description = CharacterDescriptionConstants.LuckyDuke, CharacterType = CharacterType.LuckyDuke, MaxHP = 4 },
                    new Character { Id = 9, Name = CharacterNameConstants.PaulRegret, Description = CharacterDescriptionConstants.PaulRegret, CharacterType = CharacterType.PaulRegret, MaxHP = 3 },
                    new Character { Id = 10, Name = CharacterNameConstants.PedroRamirez, Description = CharacterDescriptionConstants.PedroRamirez, CharacterType = CharacterType.PedroRamirez, MaxHP = 4 },
                    new Character { Id = 11, Name = CharacterNameConstants.RoseDoolan, Description = CharacterDescriptionConstants.RoseDoolan, CharacterType = CharacterType.RoseDoolan, MaxHP = 4 },
                    new Character { Id = 12, Name = CharacterNameConstants.SidKetchum, Description = CharacterDescriptionConstants.SidKetchum, CharacterType = CharacterType.SidKetchum, MaxHP = 4 },
                    new Character { Id = 13, Name = CharacterNameConstants.SlabTheKiller, Description = CharacterDescriptionConstants.SlabTheKiller, CharacterType = CharacterType.SlabTheKiller, MaxHP = 4 },
                    new Character { Id = 14, Name = CharacterNameConstants.SuzyLafayette, Description = CharacterDescriptionConstants.SuzyLafayette, CharacterType = CharacterType.SuzyLafayette, MaxHP = 4 },
                    new Character { Id = 15, Name = CharacterNameConstants.VultureSam, Description = CharacterDescriptionConstants.VultureSam, CharacterType = CharacterType.VultureSam, MaxHP = 4 },
                    new Character { Id = 16, Name = CharacterNameConstants.WillyTheKid, Description = CharacterDescriptionConstants.WillyTheKid, CharacterType = CharacterType.WillyTheKid, MaxHP = 4 }
                );
        }

        public DbSet<Character> Characters { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Role> Roles { get; set; }

        public DbSet<GameBoard> GameBoards { get; set; }
        public DbSet<Player> Players { get; set; }

        public DbSet<GameBoardCard> GameBoardCards { get; set; }
        public DbSet<PlayerCard> PlayerCards { get; set; }
    }
}
