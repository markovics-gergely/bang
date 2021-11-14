using UserIdentity.DAL.Domain.Bang;

using System;

namespace UserIdentity.BLL.Infrastructure.Queries.ViewModels
{
    public class HistoryViewModel
    {
        public long Id { get; set; }
        public int Placement { get; set; }
        public DateTime CreatedAt { get; set; }
        public RoleType PlayedRole { get; set; }
    }
}
