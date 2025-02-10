using Catalog_Business.Repository.IRepository;
using Catalog_DataAccess;
using Catalog_DataAccess.CatalogDB;
using System.Data.Entity;

namespace Catalog_Business.Repository
{
    public class StateRepository : Repository<State>, IStateRepository
    {
        public StateRepository(ApplicationDbContext _db) : base(_db)
        {

        }
        public async Task<State> GetStateByNameAsync(string name)
        {
            var state = await _db.States.FirstOrDefaultAsync(s => s.Name.Trim().ToUpper() == name.Trim().ToUpper());
            return state;
        }

        public async Task<State> GetIsInitialStateAsync()
        {
            var state = await _db.States.FirstOrDefaultAsync(s => s.IsInitialState == true);
            return state;
        }

    }
}
