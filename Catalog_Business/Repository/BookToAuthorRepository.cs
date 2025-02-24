using Catalog_Business.Repository.IRepository;
using Catalog_DataAccess;
using Catalog_DataAccess.CatalogDB;
using Microsoft.EntityFrameworkCore;

namespace Catalog_Business.Repository
{
    public class BookToAuthorRepository : Repository<BookToAuthor>, IBookToAuthorRepository
    {
        public BookToAuthorRepository(ApplicationDbContext _db) : base(_db)
        {
        }

        public async Task<BookToAuthor> FindBookToAuthorByBookIdAndAuthorId(int bookId, int authorId)
        {
            var retVar = await _db.BookToAuthors.FirstOrDefaultAsync(u => u.BookId == bookId && u.AuthorId == authorId);
            return retVar;
        }

        public async Task<IEnumerable<BookToAuthor>> FindBookToAuthorsByBookId(int bookId)
        {
            var retVar = _db.BookToAuthors.Where(u => u.BookId == bookId);
            return retVar;
        }


    }
}
