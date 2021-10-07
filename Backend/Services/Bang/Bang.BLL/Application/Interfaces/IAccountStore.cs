using Bang.DAL.Domain;
using Bang.DAL.Domain.Catalog.Cards;
using Bang.DAL.Domain.Constants.Enums;
using Bang.DAL.Domain.Joins;
using Bang.DAL.Domain.Joins.GameBoardCards;
using Bang.DAL.Domain.Joins.PlayerCards;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Bang.BLL.Application.Interfaces
{
    public interface IAccountStore
    {
        string GetActualAccountId();
    }
}
