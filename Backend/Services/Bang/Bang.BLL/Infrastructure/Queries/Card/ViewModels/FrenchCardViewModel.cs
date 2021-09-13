using Bang.DAL.Domain.Constants.Enums;

using System;

namespace Bang.BLL.Infrastructure.Queries.ViewModels
{
    public class FrenchCardViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CardEffectType { get; set; }
        public CardType CardType { get; set; }
        public CardColorType CardColorType { get; set; }
        public int FrenchNumber { get; set; }
    }
}
