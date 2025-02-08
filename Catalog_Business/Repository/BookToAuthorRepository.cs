using Catalog_Business.Repository.IRepository;
using Catalog_DataAccess;
using Catalog_DataAccess.CatalogDB;

namespace Catalog_Business.Repository
{
    public class BookToAuthorRepository : Repository<BookToAuthor>, IBookToAuthorRepository
    {
        public BookToAuthorRepository(ApplicationDbContext _db) : base(_db)
        {
        }
    }
}
