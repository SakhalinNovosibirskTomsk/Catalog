using Catalog_DataAccess.CatalogDB;
using static Catalog_Common.SD;

namespace Catalog_Business.Repository.IRepository
{
    public interface IBookInstanceRepository : IRepository<BookInstance>
    {
        public Task<IEnumerable<BookInstance>> GetAllBookInstancesAsync(GetAllItems? getAllItems = GetAllItems.All);

        public Task<IEnumerable<BookInstance>> GetBookInstancesByBookIdAsync(int bookId);

        public Task<IEnumerable<BookInstance>> GetBookInstancesByStateIdAsync(int stateId);

        public Task<IEnumerable<BookInstance>> GetBookInstancesByBookIdAndStateIdAsync(int bookId, int stateId);

        public Task<BookInstance> GetBookInstanceByIdAsync(int id);

    }
}
