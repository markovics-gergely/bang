using Bang.BLL.Application.Commands.DataTransferObjects;
using Bang.DAL.Domain.Joins.GameBoardCards;

using AutoMapper;
using Bang.BLL.Infrastructure.Queries.ViewModels;

namespace Bang.BLL.Application.MappingProfiles
{
    public class GameBoardCardProfile : Profile
    {
        public GameBoardCardProfile()
        {
            CreateMap<DrawableGameBoardCard, FrenchCardViewModel>()
                .ForMember(d => d.Id, opt => opt.MapFrom(c => c.CardId))
                .ForMember(d => d.Name, opt => opt.MapFrom(c => c.Card.Name))
                .ForMember(d => d.Description, opt => opt.MapFrom(c => c.Card.Description))
                .ForMember(d => d.CardEffectType, opt => opt.MapFrom(c => c.Card.CardEffectType))
                .ForMember(d => d.CardType, opt => opt.MapFrom(c => c.Card.CardType))
                .ForMember(d => d.CardColorType, opt => opt.MapFrom(c => c.CardColorType))
                .ForMember(d => d.FrenchNumber, opt => opt.MapFrom(c => c.FrenchNumber));
            CreateMap<DiscardedGameBoardCard, FrenchCardViewModel>()
                .ForMember(d => d.Id, opt => opt.MapFrom(c => c.CardId))
                .ForMember(d => d.Name, opt => opt.MapFrom(c => c.Card.Name))
                .ForMember(d => d.Description, opt => opt.MapFrom(c => c.Card.Description))
                .ForMember(d => d.CardEffectType, opt => opt.MapFrom(c => c.Card.CardEffectType))
                .ForMember(d => d.CardType, opt => opt.MapFrom(c => c.Card.CardType))
                .ForMember(d => d.CardColorType, opt => opt.MapFrom(c => c.CardColorType))
                .ForMember(d => d.FrenchNumber, opt => opt.MapFrom(c => c.FrenchNumber));
            CreateMap<GameBoardCard, GameBoardCardViewModel>().IncludeAllDerived().ReverseMap();
            CreateMap<DrawableGameBoardCard, GameBoardCardViewModel>().ReverseMap();
            CreateMap<DiscardedGameBoardCard, GameBoardCardViewModel>().ReverseMap();
        }
    }
}
