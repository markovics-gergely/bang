using Bang.DAL.Domain.Constants.Enums;

namespace Bang.DAL.Domain.Catalog
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MaxHP { get; set; }
        public CharacterType CharacterType { get; set; }
    }
}
