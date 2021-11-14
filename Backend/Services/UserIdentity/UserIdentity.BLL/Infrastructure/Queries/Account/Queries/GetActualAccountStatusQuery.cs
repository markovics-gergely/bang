using MediatR;
using UserIdentity.BLL.Infrastructure.Queries.ViewModels;

namespace UserIdentity.BLL.Infrastructure.Queries.Queries
{
    public class GetActualAccountStatusQuery : IRequest<StatusViewModel>
    {
    }
}
