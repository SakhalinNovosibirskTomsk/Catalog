using Catalog_DataAccess.CatalogDB;
using static Catalog_Common.SD;

namespace Catalog_Business.Repository.IRepository
{
    public interface IBookInstanceRepository : IRepository<BookInstance>
    {
        public Task<Publisher> GetAllBooksAsync(GetAllItems? getAllItems = GetAllItems.All);
    }
}
