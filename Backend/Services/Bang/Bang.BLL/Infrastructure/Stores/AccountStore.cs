using Bang.BLL.Application.Exceptions;
using Bang.BLL.Application.Interfaces;
using Bang.DAL;
using Bang.DAL.Domain.Catalog.Cards;
using Bang.DAL.Domain.Constants;
using Bang.DAL.Domain.Constants.Enums;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Bang.DAL.Domain.Joins;
using Bang.DAL.Domain.Joins.GameBoardCards;
using Bang.DAL.Domain.Joins.PlayerCards;
using Bang.BLL.Application.Effects.Cards;
using Bang.DAL.Domain;
using Microsoft.AspNetCore.Http;
using System.Net.Http;

namespace Bang.BLL.Infrastructure.Stores
{
    public class AccountStore : IAccountStore
    {
        private readonly HttpClient _httpClient;

        public AccountStore(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("useridentity");
        }

        public async Task<string> GetActualAccountIdAsync(CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetAsync($"Account/actual-account", cancellationToken);
            var body = await response.Content.ReadAsStringAsync();
            return body;
        }
    }
}
