using Bang.DAL.Domain.Constants.Enums;

namespace Bang.BLL.Infrastructure.Queries.Catalog.Character.ViewModels
{
    public class CharacterViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Health { get; set; }
        public string Description { get; set; }
        public int MaxHP { get; set; }
        public CharacterType CharacterType { get; set; }
    }
}
