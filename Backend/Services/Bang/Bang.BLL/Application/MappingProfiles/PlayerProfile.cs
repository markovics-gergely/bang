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
            CreateMap<Player, PlayerViewModel>()
                //.ForMember(p => p.UserName, opts => opts.MapFrom(p => p.User.UserName))
                .ForMember(p => p.PlayerCards, opts => opts.MapFrom(p => p.PlayerCards));
            CreateMap<Player, PlayerCreateViewModel>().ReverseMap();
        }
    }
}
