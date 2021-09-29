using UserIdentity.BLL.Infrastructure.Queries.ViewModels;
using UserIdentity.DAL.Domain;

using AutoMapper;

namespace UserIdentity.BLL.Application.MappingProfiles
{
    public class HistoryProfile : Profile
    {
        public HistoryProfile()
        {
            CreateMap<History, HistoryViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.AccountId));
        }
    }
}
