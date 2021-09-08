using Bang.BLL.Infrastructure.Queries.ViewModels;

using Bang.DAL.Domain.Catalog;

using AutoMapper;

namespace Bang.BLL.Application.MappingProfiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, RoleViewModel>().ReverseMap();
        }
    }
}
