using Bang.BLL.Application.Interfaces;
using Bang.BLL.Infrastructure.Queries.ViewModels;
using Bang.BLL.Infrastructure.Queries.Queries;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using MediatR;

namespace Bang.BLL.Infrastructure.Queries.Handlers
{
    public class RoleQueryHandler :
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
