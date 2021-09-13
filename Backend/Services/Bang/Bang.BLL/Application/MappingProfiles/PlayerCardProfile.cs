using Bang.DAL.Domain.Joins;
using Bang.BLL.Application.Commands.ViewModels;
using Bang.BLL.Infrastructure.Queries.ViewModels;

using AutoMapper;

namespace Bang.BLL.Application.MappingProfiles
{
    public class PlayerCardProfile : Profile
    {
        public PlayerCardProfile()
        {
            CreateMap<PlayerCard, PlayerCardViewModel>().ReverseMap();
            CreateMap<PlayerCard, CardViewModel>()
                .ForMember(d => d.Id, opt => opt.MapFrom(c => c.CardId))
                .ForMember(d => d.Name, opt => opt.MapFrom(c => c.Card.Name))
                .ForMember(d => d.Description, opt => opt.MapFrom(c => c.Card.Description))
                .ForMember(d => d.CardEffectType, opt => opt.MapFrom(c => c.Card.CardEffectType))
                .ForMember(d => d.CardType, opt => opt.MapFrom(c => c.Card.CardType));
            CreateMap<PlayerCard, FrenchCardViewModel>()
                .ForMember(d => d.Id, opt => opt.MapFrom(c => c.CardId))
                .ForMember(d => d.Name, opt => opt.MapFrom(c => c.Card.Name))
                .ForMember(d => d.Description, opt => opt.MapFrom(c => c.Card.Description))
                .ForMember(d => d.CardEffectType, opt => opt.MapFrom(c => c.Card.CardEffectType))
                .ForMember(d => d.CardType, opt => opt.MapFrom(c => c.Card.CardType))
                .ForMember(d => d.CardColorType, opt => opt.MapFrom(c => c.CardColorType))
                .ForMember(d => d.FrenchNumber, opt => opt.MapFrom(c => c.FrenchNumber));
        }
    }
}
