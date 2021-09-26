using Bang.DAL.Domain.Constants.Enums;

using System;

namespace UserIdentity.BLL.Infrastructure.Queries.ViewModels
{
    public class HistoryViewModel
    {
        public string Id { get; set; }
        public int Placement { get; set; }
        public DateTime CreatedAt { get; set; }
        public RoleType PlayedRole { get; set; }
    }
}
