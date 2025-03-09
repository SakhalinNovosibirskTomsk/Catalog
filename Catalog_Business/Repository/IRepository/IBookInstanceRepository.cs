using Catalog_Domain.CatalogDB;

namespace Catalog_Business.Repository.IRepository
{
    public interface IBookInstanceRepository : IRepository<BookInstance>
    {
        public Task<IEnumerable<BookInstance>> GetAllBookInstancesAsync();

        public Task<IEnumerable<BookInstance>> GetBookInstancesByBookIdAsync(int bookId);

        public Task<BookInstance> GetBookInstanceByIdAsync(int id);

        public Task<BookInstance> GetBookInstanceByInventoryNumberAsync(string inventoryNumber);

    }
}
