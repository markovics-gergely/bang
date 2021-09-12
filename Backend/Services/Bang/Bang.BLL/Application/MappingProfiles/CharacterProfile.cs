using Bang.BLL.Application.Commands.DataTransferObjects;
using Bang.BLL.Infrastructure.Queries.ViewModels;
using Bang.DAL.Domain.Catalog;

using AutoMapper;

namespace Bang.BLL.Application.MappingProfiles
{
    public class CharacterProfile : Profile
    {
        public CharacterProfile()
        {
            CreateMap<Character, CharacterViewModel>().ReverseMap();
        }
    }
}
