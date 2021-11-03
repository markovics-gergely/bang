using UserIdentity.BLL.Infrastructure.Queries.ViewModels;
using UserIdentity.DAL.Domain;

using AutoMapper;

namespace UserIdentity.BLL.Application.MappingProfiles
{
    public class LobbyProfile : Profile
    {
        public LobbyProfile()
        {
            CreateMap<LobbyAccount, LobbyAccountViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.AccountId))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Account.UserName));

            CreateMap<Lobby, LobbyViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.LobbyOwner, opt => opt.MapFrom(src => src.Owner.UserName));
        }
    }
}
