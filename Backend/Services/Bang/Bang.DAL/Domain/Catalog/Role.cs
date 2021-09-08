using Bang.DAL.Domain.Constants.Enums;

namespace Bang.DAL.Domain.Catalog
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public RoleType RoleType { get; set; }
    }
}
