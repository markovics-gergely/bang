using Bang.DAL.Domain.Constants.Enums;

namespace Bang.BLL.Infrastructure.Queries.ViewModels
{
    public class RoleViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public RoleType RoleType { get; set; }
    }
}
