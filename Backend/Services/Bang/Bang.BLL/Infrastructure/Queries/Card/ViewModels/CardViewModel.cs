using Bang.DAL.Domain.Constants.Enums;

namespace Bang.BLL.Infrastructure.Queries.ViewModels
{
    public class CardViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CardEffectType { get; set; }
        public CardType CardType { get; set; }
    }
}
