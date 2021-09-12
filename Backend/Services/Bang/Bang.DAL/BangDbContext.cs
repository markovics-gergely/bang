using Bang.DAL.Domain;
using Bang.DAL.Domain.Catalog;
using Bang.DAL.Domain.Catalog.Cards;
using Bang.DAL.Domain.Constants;
using Bang.DAL.Domain.Constants.DescriptionConstants;
using Bang.DAL.Domain.Constants.Enums;
using Bang.DAL.Domain.Constants.NameConstants;
using Bang.DAL.Domain.Joins;
using Bang.DAL.Domain.Joins.GameBoardCards;

using Microsoft.EntityFrameworkCore;

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

            modelBuilder.Entity<PlayerCard>()
                .HasOne(p => p.Card)
                .WithMany()
                .HasForeignKey(c => c.CardId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PlayerCard>()
                .HasOne(p => p.Player)
                .WithMany(p => p.PlayerCards)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<GameBoardCard>()
                .HasDiscriminator(g => g.StatusType)
                .HasValue<GameBoardCard>(GameBoardCardConstants.Base)
                .HasValue<DrawableGameBoardCard>(GameBoardCardConstants.DrawableCard)
                .HasValue<DiscardedGameBoardCard>(GameBoardCardConstants.DiscardedCard);

            modelBuilder.Entity<Card>()
                .HasDiscriminator(c => c.CardEffectType)
                .HasValue<Card>(CardConstants.Base)
                .HasValue<ActiveCard>(CardConstants.ActiveCard)
                .HasValue<PassiveCard>(CardConstants.PassiveCard);

            modelBuilder
                .Entity<Card>()
                .Property(c => c.CardType)
                .HasConversion<string>();

            modelBuilder
                .Entity<Character>()
                .Property(c => c.CharacterType)
                .HasConversion<string>();

            modelBuilder
                .Entity<Role>()
                .Property(r => r.RoleType)
                .HasConversion<string>();

            modelBuilder
                .Entity<ActiveCard>()
                .Property(r => r.CardType)
                .HasConversion<string>();

            modelBuilder
                .Entity<PassiveCard>()
                .Property(r => r.CardType)
                .HasConversion<string>();

            modelBuilder.Entity<ActiveCard>()
                .HasData(
                    new ActiveCard { Id = 1, Name = CardNameConstants.Bang, Description = CardDescriptionConstants.Bang, CardType = CardType.Bang, CardEffectType = CardConstants.ActiveCard },
                    new ActiveCard { Id = 3, Name = CardNameConstants.Beer, Description = CardDescriptionConstants.Beer, CardType = CardType.Beer, CardEffectType = CardConstants.ActiveCard },
                    new ActiveCard { Id = 4, Name = CardNameConstants.CatBalou, Description = CardDescriptionConstants.CatBalou, CardType = CardType.CatBalou, CardEffectType = CardConstants.ActiveCard },
                    new ActiveCard { Id = 5, Name = CardNameConstants.Duel, Description = CardDescriptionConstants.Duel, CardType = CardType.Duel, CardEffectType = CardConstants.ActiveCard },
                    new ActiveCard { Id = 7, Name = CardNameConstants.Gatling, Description = CardDescriptionConstants.Gatling, CardType = CardType.Gatling, CardEffectType = CardConstants.ActiveCard },
                    new ActiveCard { Id = 8, Name = CardNameConstants.GeneralStore, Description = CardDescriptionConstants.GeneralStore, CardType = CardType.GeneralStore, CardEffectType = CardConstants.ActiveCard },
                    new ActiveCard { Id = 11, Name = CardNameConstants.Indians, Description = CardDescriptionConstants.Indians, CardType = CardType.Indians, CardEffectType = CardConstants.ActiveCard },
                    new ActiveCard { Id = 13, Name = CardNameConstants.Missed, Description = CardDescriptionConstants.Missed, CardType = CardType.Missed, CardEffectType = CardConstants.ActiveCard },
                    new ActiveCard { Id = 14, Name = CardNameConstants.Panic, Description = CardDescriptionConstants.Panic, CardType = CardType.Panic, CardEffectType = CardConstants.ActiveCard },
                    new ActiveCard { Id = 15, Name = CardNameConstants.Saloon, Description = CardDescriptionConstants.Saloon, CardType = CardType.Saloon, CardEffectType = CardConstants.ActiveCard },
                    new ActiveCard { Id = 16, Name = CardNameConstants.Stagecoach, Description = CardDescriptionConstants.Stagecoach, CardType = CardType.Stagecoach, CardEffectType = CardConstants.ActiveCard },
                    new ActiveCard { Id = 17, Name = CardNameConstants.WellsFargo, Description = CardDescriptionConstants.WellsFarg, CardType = CardType.WellsFargo, CardEffectType = CardConstants.ActiveCard }
                );

            modelBuilder.Entity<PassiveCard>()
                .HasData(
                    new PassiveCard { Id = 2, Name = CardNameConstants.Barrel, Description = CardDescriptionConstants.Barrel, CardType = CardType.Barrel, CardEffectType = CardConstants.PassiveCard },
                    new PassiveCard { Id = 6, Name = CardNameConstants.Dynamite, Description = CardDescriptionConstants.Dynamite, CardType = CardType.Dynamite, CardEffectType = CardConstants.PassiveCard },
                    new PassiveCard { Id = 9, Name = CardNameConstants.Guns, Description = CardDescriptionConstants.Guns, CardType = CardType.Guns, CardEffectType = CardConstants.PassiveCard },
                    new PassiveCard { Id = 10, Name = CardNameConstants.Horses, Description = CardDescriptionConstants.Horses, CardType = CardType.Horses, CardEffectType = CardConstants.PassiveCard },
                    new PassiveCard { Id = 12, Name = CardNameConstants.Jail, Description = CardDescriptionConstants.Jail, CardType = CardType.Jail, CardEffectType = CardConstants.PassiveCard }
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
