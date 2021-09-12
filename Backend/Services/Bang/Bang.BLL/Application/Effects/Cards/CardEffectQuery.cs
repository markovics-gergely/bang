using Bang.DAL;

namespace Bang.BLL.Application.Effects.Cards
{
    public class CardEffectQuery
    {
        private readonly BangDbContext _dbContext;

        public CardEffectQuery(BangDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public CardEffectQuery()
        {
        }
    }
}
