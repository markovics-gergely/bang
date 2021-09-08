﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Bang.BLL.Application.Interfaces.Catalog;
using Bang.BLL.Infrastructure.Queries.Catalog.Role.ViewModels;
using Bang.BLL.Infrastructure.Queries.Catalog.Role.Queries;

namespace Bang.BLL.Infrastructure.Queries.Handlers
{
    public class RoleQueryHandler :
        IRequestHandler<GetRoleQuery, RoleViewModel>,
        IRequestHandler<GetRoleByTypeQuery, RoleViewModel>,
        IRequestHandler<GetRolesQuery, IEnumerable<RoleViewModel>>
    {
        private readonly IMapper _mapper;
        private readonly IRoleStore _roleStore;

        public RoleQueryHandler(IMapper mapper, IRoleStore roleStore)
        {
            _mapper = mapper;
            _roleStore = roleStore;
        }

        public async Task<RoleViewModel> Handle(GetRoleQuery request, CancellationToken cancellationToken)
        {
            var domain = await _roleStore.GetRoleAsync(request.Id, cancellationToken);

            return _mapper.Map<RoleViewModel>(domain);
        }

        public async Task<IEnumerable<RoleViewModel>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            var domain = await _roleStore.GetRolesAsync(cancellationToken);

            return _mapper.Map<IEnumerable<RoleViewModel>>(domain);
        }

        public async Task<RoleViewModel> Handle(GetRoleByTypeQuery request, CancellationToken cancellationToken)
        {
            var domain = await _roleStore.GetRoleByTypeAsync(request.Type, cancellationToken);

            return _mapper.Map<RoleViewModel>(domain);
        }
    }
}
