using Bang.BLL.Application.Interfaces;
using Bang.BLL.Infrastructure.Queries.Queries;
using Bang.BLL.Infrastructure.Queries.ViewModels;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using MediatR;

namespace Bang.BLL.Infrastructure.Queries.Handlers
{
    public class CharacterQueryHandler :
        IRequestHandler<GetCharacterByTypeQuery, CharacterViewModel>,
        IRequestHandler<GetCharactersQuery, IEnumerable<CharacterViewModel>>
    {
        private readonly IMapper _mapper;
        private readonly ICharacterStore _characterStore;

        public CharacterQueryHandler(IMapper mapper, ICharacterStore characterStore)
        {
            _mapper = mapper;
            _characterStore = characterStore;
        }

        public async Task<IEnumerable<CharacterViewModel>> Handle(GetCharactersQuery request, CancellationToken cancellationToken)
        {
            var domain = await _characterStore.GetCharactersAsync(cancellationToken);

            return _mapper.Map<IEnumerable<CharacterViewModel>>(domain);
        }

        public async Task<CharacterViewModel> Handle(GetCharacterByTypeQuery request, CancellationToken cancellationToken)
        {
            var domain = await _characterStore.GetCharacterByTypeAsync(request.Type, cancellationToken);

            return _mapper.Map<CharacterViewModel>(domain);
        }
    }
}
