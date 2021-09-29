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
                .ForMember(p => p.HandPlayerCards, opts => opts.MapFrom(p => p.HandPlayerCards))
                .ForMember(p => p.TablePlayerCards, opts => opts.MapFrom(p => p.TablePlayerCards));
            CreateMap<Player, PlayerCreateViewModel>().ReverseMap();
            CreateMap<Player, PlayerByUserViewModel>()
                .ForMember(p => p.HandPlayerCardCount, opts => opts.MapFrom(p => p.HandPlayerCards.Count))
                .ForMember(p => p.TablePlayerCards, opts => opts.MapFrom(p => p.TablePlayerCards));
        }
    }
}
