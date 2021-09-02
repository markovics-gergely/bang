using Bang.DAL.Domain;

using Microsoft.EntityFrameworkCore;

namespace Bang.DAL
{
    public class BangDbContext : DbContext
    {
        public BangDbContext(DbContextOptions<BangDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Character>().HasData(
                new Character() { Id = 1, Name = "Sheriff", Health = 5 },
                new Character() { Id = 2, Name = "Sheriff helyettes", Health = 4 },
                new Character() { Id = 3, Name = "Bandita", Health = 4 },
                new Character() { Id = 4, Name = "Renegát", Health = 4 }
            );
        }

        public DbSet<Character> Characters { get; set; }
    }
}
