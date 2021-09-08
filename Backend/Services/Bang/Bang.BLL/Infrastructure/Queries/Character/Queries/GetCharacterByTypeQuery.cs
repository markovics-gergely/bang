using Bang.BLL.Infrastructure.Queries.ViewModels;
using Bang.DAL.Domain.Constants.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bang.BLL.Infrastructure.Queries.Queries
{
    public class GetCharacterByTypeQuery : IRequest<CharacterViewModel>
    {
        public CharacterType Type { get; set; }

        public GetCharacterByTypeQuery(CharacterType type)
        {
            Type = type;
        }
    }
}
