using AutoMapper;
using Bang.BLL.Infrastructure.Queries.Catalog.Card.ViewModels;
using Bang.DAL.Domain.Catalog.Cards;

namespace Bang.BLL.Application.MappingProfiles
{
    public class CardProfile : Profile
    {
        public CardProfile()
        {
            CreateMap<Card, CardViewModel>().ReverseMap();
        }
    }
}
