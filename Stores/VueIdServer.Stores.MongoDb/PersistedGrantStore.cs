using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using VueIdServer.Data.Common;

namespace VueIdServer.Stores.MongoDb
{
    public class PersistedGrantStore : IPersistedGrantStore
    {
        private readonly IRepository _repository;

        public PersistedGrantStore(IRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<PersistedGrant>> GetAllAsync(string subjectId)
        {
            var result = _repository.Where<PersistedGrant>(i => i.SubjectId == subjectId);
            return Task.FromResult(result.AsEnumerable());
        }

        public Task<PersistedGrant> GetAsync(string key)
        {
            var result = _repository.Single<PersistedGrant>(i => i.Key == key);
            return Task.FromResult(result);
        }

        public Task RemoveAllAsync(string subjectId, string clientId)
        {
            _repository.Delete<PersistedGrant>(i => i.SubjectId == subjectId && i.ClientId == clientId);
            return Task.FromResult(0);
        }

        public Task RemoveAllAsync(string subjectId, string clientId, string type)
        {
            _repository.Delete<PersistedGrant>(i => i.SubjectId == subjectId && i.ClientId == clientId && i.Type == type);
            return Task.FromResult(0);
        }

        public Task RemoveAsync(string key)
        {
            _repository.Delete<PersistedGrant>(i => i.Key == key);
            return Task.FromResult(0);
        }

        public Task StoreAsync(PersistedGrant grant)
        {
            _repository.Add<PersistedGrant>(grant);
            return Task.FromResult(0);
        }
    }
}