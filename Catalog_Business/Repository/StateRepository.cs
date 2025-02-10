using Catalog_Business.Repository.IRepository;
using Catalog_DataAccess;
using Catalog_DataAccess.CatalogDB;
using Microsoft.EntityFrameworkCore;

namespace Catalog_Business.Repository
{
    public class StateRepository : Repository<State>, IStateRepository
    {
        public StateRepository(ApplicationDbContext _db) : base(_db)
        {

        }
        public async Task<State> GetStateByNameAsync(string name)
        {
            var state = await _db.States.FirstOrDefaultAsync(s => s.Name == name);

            //var state = await _db.States.FirstOrDefaultAsync(item => string.Compare(item.Name, name, true) == 0);
            return state;
        }

        public async Task<State> GetIsInitialStateAsync()
        {
            var state = await _db.States.FirstOrDefaultAsync(s => s.IsInitialState);
            return state;
        }

    }
}
