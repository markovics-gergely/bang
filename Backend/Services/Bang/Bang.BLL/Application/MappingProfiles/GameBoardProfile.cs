using Bang.BLL.Infrastructure.Queries.ViewModels;

using Bang.DAL.Domain;

using AutoMapper;

namespace Bang.BLL.Application.MappingProfiles
{
    public class GameBoardProfile : Profile
    {
        public GameBoardProfile()
        {
            CreateMap<GameBoard, GameBoardViewModel>().ReverseMap();
        }
    }
}
