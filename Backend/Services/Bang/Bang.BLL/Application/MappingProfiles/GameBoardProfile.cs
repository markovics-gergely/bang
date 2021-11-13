using Bang.BLL.Infrastructure.Queries.ViewModels;

using Bang.DAL.Domain;

using AutoMapper;
using Bang.BLL.Application.Commands.DataTransferObjects;
using System.Linq;

namespace Bang.BLL.Application.MappingProfiles
{
    public class GameBoardProfile : Profile
    {
        public GameBoardProfile()
        {
            CreateMap<GameBoard, GameBoardViewModel>()
                .ForMember(g => g.Players, opt => opt.MapFrom(g => g.Players));
            CreateMap<GameBoard, GameBoardDto>().ReverseMap();
            CreateMap<GameBoard, GameBoardByUserViewModel>()
                .ForMember(g => g.LastDiscardedGameBoardCard, opt => opt.MapFrom(g => g.DiscardedGameBoardCards.LastOrDefault()))
                .ForMember(g => g.OwnPlayer, opt => opt.Ignore())
                .ForMember(g => g.OtherPlayers, opt => opt.Ignore());
        }
    }
}
