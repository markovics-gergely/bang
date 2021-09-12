using Bang.BLL.Infrastructure.Queries.ViewModels;

using Bang.DAL.Domain;

using AutoMapper;
using Bang.BLL.Application.Commands.DataTransferObjects;
using System.Collections.Generic;
using Bang.DAL.Domain.Joins.GameBoardCards;
using System.Linq;

namespace Bang.BLL.Application.MappingProfiles
{
    public class GameBoardProfile : Profile
    {
        public GameBoardProfile()
        {
            CreateMap<GameBoard, GameBoardViewModel>()
                .ForMember(g => g.Players, opt => opt.MapFrom(g => g.Players))
                .ForMember(g => g.DiscardedGameBoardCards, opt => opt.MapFrom(g => g.DiscardedGameBoardCards))
                .ForMember(g => g.DrawableGameBoardCards, opt => opt.MapFrom(g => g.DrawableGameBoardCards));
            CreateMap<GameBoard, GameBoardDto>().ReverseMap();
        }
    }
}
