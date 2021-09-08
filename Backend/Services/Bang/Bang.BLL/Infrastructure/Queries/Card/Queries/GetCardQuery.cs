using Bang.BLL.Infrastructure.Queries.ViewModels;

using MediatR;

namespace Bang.BLL.Infrastructure.Queries.Queries
{
    public class GetCardQuery : IRequest<CardViewModel>
    {
        public int Id { get; set; }

        public GetCardQuery(int id)
        {
            Id = id;
        }
    }
}
