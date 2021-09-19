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
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Account.UserName));
        }
    }
}
