using UserIdentity.BLL.Infrastructure.Queries.ViewModels;
using UserIdentity.DAL.Domain;

using AutoMapper;

namespace UserIdentity.BLL.Application.MappingProfiles
{
    public class FriendProfile : Profile
    {
        public FriendProfile()
        {
            CreateMap<Friend, FriendViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ReceiverId));
        }
    }
}
