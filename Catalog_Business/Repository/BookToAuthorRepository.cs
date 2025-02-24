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

        public async Task<IEnumerable<BookToAuthor>> GetAllBookToAuthorAsync()
        {
            var retVar = await _db.BookToAuthors
                .Include("Book")
                .Include("Author")
                .OrderBy(u => u.BookId)
                .ThenBy(u => u.AuthorId)
                .ToListAsync();
            return retVar;
        }

        public async Task<BookToAuthor> GetBookToAuthorByIdAsync(int id)
        {
            var retVar = await _db.BookToAuthors
                .Include("Book")
                .Include("Author")
                .FirstOrDefaultAsync(u => u.Id == id);
            return retVar;
        }

        public async Task<BookToAuthor> FindBookToAuthorByBookIdAndAuthorIdAsync(int bookId, int authorId)
        {
            var retVar = await _db.BookToAuthors
                .Include("Book")
                .Include("Author")
                .FirstOrDefaultAsync(u => u.BookId == bookId && u.AuthorId == authorId);
            return retVar;
        }

        public async Task<IEnumerable<BookToAuthor>> FindBookToAuthorsByBookIdAsync(int bookId)
        {
            var retVar = _db.BookToAuthors
                .Include("Book")
                .Include("Author")
                .OrderBy(u => u.AuthorId)
                .Where(u => u.BookId == bookId);
            return retVar;
        }
    }
}
