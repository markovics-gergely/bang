using UserIdentity.DAL.Domain.Bang;

using System;
using System.ComponentModel.DataAnnotations;

namespace UserIdentity.DAL.Domain
{
    public class History
    {
        public long Id { get; set; }
        public string AccountId { get; set; }
        public Account Account { get; set; }
        public int Placement { get; set; }
        public DateTime CreatedAt { get; set; }
        public RoleType PlayedRole { get; set; }
    }
}
