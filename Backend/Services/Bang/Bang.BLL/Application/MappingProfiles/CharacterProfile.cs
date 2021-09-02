using Bang.BLL.Infrastructure.Queries.ViewModels;
using Bang.BLL.Application.Commands.DataTransferObjects;
using Bang.DAL.Domain;

using AutoMapper;

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
