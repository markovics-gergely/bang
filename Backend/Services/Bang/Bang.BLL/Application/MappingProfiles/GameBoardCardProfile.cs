using Bang.BLL.Application.Commands.DataTransferObjects;
using Bang.DAL.Domain.Joins.GameBoardCards;
using Bang.BLL.Application.Commands.ViewModels;

using AutoMapper;

namespace Bang.BLL.Application.MappingProfiles
{
    public class GameBoardCardProfile : Profile
    {
        public GameBoardCardProfile()
        {
            CreateMap<DrawableGameBoardCard, CardGameBoardCardViewModel>()
                .ForMember(d => d.CardId, opt => opt.MapFrom(c => c.CardId))
                .ForMember(d => d.Name, opt => opt.MapFrom(c => c.Card.Name))
                .ForMember(d => d.Description, opt => opt.MapFrom(c => c.Card.Description))
                .ForMember(d => d.CardEffectType, opt => opt.MapFrom(c => c.Card.CardEffectType))
                .ForMember(d => d.CardType, opt => opt.MapFrom(c => c.Card.CardType));
            CreateMap<DiscardedGameBoardCard, CardGameBoardCardViewModel>()
                .ForMember(d => d.CardId, opt => opt.MapFrom(c => c.CardId))
                .ForMember(d => d.Name, opt => opt.MapFrom(c => c.Card.Name))
                .ForMember(d => d.Description, opt => opt.MapFrom(c => c.Card.Description))
                .ForMember(d => d.CardEffectType, opt => opt.MapFrom(c => c.Card.CardEffectType))
                .ForMember(d => d.CardType, opt => opt.MapFrom(c => c.Card.CardType));
            CreateMap<GameBoardCard, GameBoardCardViewModel>().IncludeAllDerived().ReverseMap();
            CreateMap<DrawableGameBoardCard, GameBoardCardViewModel>().ReverseMap();
            CreateMap<DiscardedGameBoardCard, GameBoardCardViewModel>().ReverseMap();
        }
    }
}
