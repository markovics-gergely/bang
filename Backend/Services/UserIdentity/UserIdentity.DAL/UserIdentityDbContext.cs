using UserIdentity.DAL.Domain;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace UserIdentity.DAL
{
    public class UserIdentityDbContext : IdentityDbContext<User>
    {
        public UserIdentityDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
