using Bang.BLL.Application.Commands.DataTransferObjects;
using AutoMapper;
using Bang.BLL.Infrastructure.Queries.ViewModels;
using Bang.DAL.Domain.Catalog;

namespace Bang.BLL.Application.MappingProfiles
{
    public class CharacterProfile : Profile
    {
        public CharacterProfile()
        {
            CreateMap<Character, CharacterViewModel>().ReverseMap();
            CreateMap<Character, CharacterDto>().ReverseMap();
        }
    }
}
