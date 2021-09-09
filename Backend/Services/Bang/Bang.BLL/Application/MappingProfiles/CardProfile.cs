using Bang.BLL.Infrastructure.Queries.ViewModels;
using Bang.DAL.Domain.Catalog.Cards;

using AutoMapper;

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
