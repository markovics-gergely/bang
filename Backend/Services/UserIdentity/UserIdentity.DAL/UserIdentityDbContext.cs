using UserIdentity.DAL.Domain;

using Microsoft.AspNetCore.Identity;
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
        }

        public DbSet<Friend> Friends { get; set; }
    }
}
