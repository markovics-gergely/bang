using UserIdentity.DAL.Domain;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace UserIdentity.BLL.Application.Interfaces
{
    public interface IAccountStore
    {      
        Task<bool> CreateAccountAsync(Account account, string password, CancellationToken cancellationToken);
        Task DeleteAccountAsync(Account account, CancellationToken cancellationToken);
        Task<string> GetAccountIdByName(string name, CancellationToken cancellationToken);
        Task<string> GetActualAccountName();
        string GetActualAccountId();
    }
}
