using UserIdentity.DAL.Domain;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace UserIdentity.DAL
{
    public class UserIdentityDbContext : IdentityDbContext<Account>
    {
        public UserIdentityDbContext(DbContextOptions<UserIdentityDbContext> options) : base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Friend>()
                .HasOne(g => g.Sender)
                .WithMany()
                .HasForeignKey(c => c.SenderId)
                .OnDelete(DeleteBehavior.NoAction);
            
            modelBuilder.Entity<Friend>()
                .HasOne(g => g.Receiver)
                .WithMany()
                .HasForeignKey(c => c.ReceiverId)
                .OnDelete(DeleteBehavior.NoAction);         
            
            modelBuilder.Entity<Lobby>()
                .HasMany(g => g.LobbyAccounts)
                .WithOne(d => d.Lobby)
                .HasForeignKey(d => d.LobbyId)
                .OnDelete(DeleteBehavior.NoAction);
            
            modelBuilder.Entity<LobbyAccount>()
                .HasOne(g => g.Lobby)
                .WithMany(g => g.LobbyAccounts)
                .HasForeignKey(d => d.LobbyId)
                .OnDelete(DeleteBehavior.NoAction);
            
            modelBuilder.Entity<Lobby>()
                .HasOne(g => g.Owner)
                .WithOne()
                .HasForeignKey<Lobby>(d => d.OwnerId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<LobbyAccount>()
                .HasOne(g => g.Account)
                .WithOne()
                .HasForeignKey<LobbyAccount>(d => d.AccountId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<History>()
                .HasOne(g => g.Account)
                .WithMany()
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.NoAction);

        }

        public DbSet<Friend> Friends { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<Lobby> Lobbies { get; set; }
        public DbSet<LobbyAccount> LobbyAccounts { get; set; }
    }
}
