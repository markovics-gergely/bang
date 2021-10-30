using Bang.BLL.Infrastructure.Queries.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bang.BLL.Infrastructure.Queries.Queries
{
    public class GetPermissionsByUserQuery : IRequest<PermissionViewModel>
    {
        public string UserId { get; set; }

        public GetPermissionsByUserQuery(string userId)
        {
            UserId = userId;
        }
    }
}
