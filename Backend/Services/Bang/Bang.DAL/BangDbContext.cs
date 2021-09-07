using Bang.DAL.Constants;
using Bang.DAL.Domain;
using Bang.DAL.Domain.Catalog.Cards;
using Bang.DAL.Domain.Catalog.Characters;
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
                .WithOne(p => p.GameBoard);

            modelBuilder.Entity<GameBoard>()
                .HasOne(g => g.ActualPlayer)
                .WithOne(p => p.GameBoard);

            modelBuilder.Entity<GameBoard>()
                .HasMany(g => g.DrawableGameBoardCards)
                .WithOne(d => d.GameBoard);

            modelBuilder.Entity<GameBoard>()
                .HasMany(g => g.DiscardedGameBoardCards)
                .WithOne(d => d.GameBoard);

            modelBuilder.Entity<GameBoardCard>()
                .HasDiscriminator(g => g.StatusType)
                .HasValue<GameBoardCard>(GameBoardCardConstants.Base)
                .HasValue<DrawableGameBoardCard>(GameBoardCardConstants.DrawableCard)
                .HasValue<DiscardedGameBoardCard>(GameBoardCardConstants.DiscardedCard);

            modelBuilder.Entity<Card>()
                .HasDiscriminator(c => c.EffectType)
                .HasValue<Card>(CardConstants.Base)
                .HasValue<ActiveCard>(CardConstants.ActiveCard)
                .HasValue<PassiveCard>(CardConstants.PassiveCard);

            
        }

        public DbSet<Character> Characters { get; set; }
    }
}
