using Catalog_Business.Repository.IRepository;
using Catalog_DataAccess;
using Catalog_DataAccess.CatalogDB;

namespace Catalog_Business.Repository
{
    public class BookInstanceRepository : Repository<BookInstance>, IBookInstanceRepository
    {
        public BookInstanceRepository(ApplicationDbContext _db) : base(_db)
        {
        }
    }
}
