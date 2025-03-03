using Catalog_Common;
using Catalog_Domain.CatalogDB;

namespace Catalog_Business.Repository.IRepository
{
    public interface IBookInstanceRepository : IRepository<BookInstance>
    {
        public Task<IEnumerable<BookInstance>> GetAllBookInstancesAsync();

        public Task<IEnumerable<BookInstance>> GetBookInstancesByBookIdAsync(int bookId);

        public Task<IEnumerable<BookInstance>> GetAllBookInstancesByFlagsAsync(SD.BookInstancesFags bookInstancesFag, bool isTrue = false);

        public Task<BookInstance> GetBookInstanceByIdAsync(int id);

        public Task<BookInstance> GetBookInstanceByInventoryNumberAsync(string inventoryNumber);

    }
}
