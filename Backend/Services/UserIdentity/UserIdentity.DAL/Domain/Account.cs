using System.Collections.Generic;

using Microsoft.AspNetCore.Identity;

namespace UserIdentity.DAL.Domain
{
    public class Account : IdentityUser
    {
        public bool InGame { get; set; }
    }
}
