using Bang.BLL.Application.Commands.Commands;
using Bang.BLL.Application.Interfaces;
using Bang.DAL.Domain.Catalog;

using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using MediatR;
using Bang.BLL.Application.Effects.CardEffects;
using Bang.DAL.Domain.Constants.Enums;

namespace Bang.BLL.Application.Commands.Handlers
{
    public class CharacterCommandHandler :
        IRequestHandler<CreateCharacterCommand, long>,
        IRequestHandler<UpdateCharacterCommand>,
        IRequestHandler<DeleteCharacterCommand>
    {
        private readonly IMapper _mapper;
        private readonly ICharacterStore _characterStore;

        public CharacterCommandHandler(IMapper mapper, ICharacterStore characterStore)
        {
            _mapper = mapper;
            _characterStore = characterStore;
        }

        public async Task<long> Handle(CreateCharacterCommand request, CancellationToken cancellationToken)
        {
            var domain = _mapper.Map<Character>(request.Dto);

            return await _characterStore.CreateCharacterAsync(domain, cancellationToken);
        }

        public async Task<Unit> Handle(UpdateCharacterCommand request, CancellationToken cancellationToken)
        {
            var domain = _mapper.Map<Character>(request.Dto);

            await _characterStore.UpdateCharacterAsync(request.Id, domain, cancellationToken);

            return Unit.Value;
        }

        public async Task<Unit> Handle(DeleteCharacterCommand request, CancellationToken cancellationToken)
        {
            await _characterStore.DeleteCharacterAsync(request.Id, cancellationToken);

            return Unit.Value;
        }
    }
}
