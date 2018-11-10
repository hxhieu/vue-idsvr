using System;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using VueIdServer.Data.Common;

namespace VueIdServer.Stores.MongoDb
{
    public class ClientStore : IClientStore
    {
        private readonly IRepository _repository;

        public ClientStore(IRepository repository)
        {
            _repository = repository;
        }

        public Task<Client> FindClientByIdAsync(string clientId)
        {
            var client = _repository.Single<Client>(c => c.ClientId == clientId);
            return Task.FromResult(client);
        }
    }
}
