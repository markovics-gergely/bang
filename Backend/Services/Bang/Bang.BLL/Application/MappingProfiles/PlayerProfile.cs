using Bang.BLL.Infrastructure.Queries.ViewModels;

using Bang.DAL.Domain;

using AutoMapper;
using Bang.BLL.Application.Commands.DataTransferObjects;

namespace Bang.BLL.Application.MappingProfiles
{
    public class PlayerProfile : Profile
    {
        public PlayerProfile()
        {
            CreateMap<Player, PlayerViewModel>().ReverseMap();
            CreateMap<Player, PlayerCreateViewModel>().ReverseMap();
        }
    }
}
