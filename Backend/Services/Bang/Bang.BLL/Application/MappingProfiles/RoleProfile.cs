﻿using AutoMapper;
using Bang.DAL.Domain.Catalog;
using Bang.BLL.Infrastructure.Queries.Catalog.Role.ViewModels;

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