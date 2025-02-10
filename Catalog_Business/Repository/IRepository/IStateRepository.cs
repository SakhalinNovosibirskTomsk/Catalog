using Catalog_DataAccess.CatalogDB;

namespace Catalog_Business.Repository.IRepository
{
    public interface IStateRepository : IRepository<State>
    {
        public Task<State> GetStateByNameAsync(string name);

        public Task<State> GetIsInitialStateAsync();
    }
}
