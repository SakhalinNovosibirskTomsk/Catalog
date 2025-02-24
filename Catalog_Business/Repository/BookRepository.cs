using Catalog_Business.Repository.IRepository;
using Catalog_DataAccess;
using Catalog_DataAccess.CatalogDB;
using System.Data.Entity;
using static Catalog_Common.SD;

namespace Catalog_Business.Repository
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(ApplicationDbContext _db) : base(_db)
        {
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync(GetAllItems? getAllItems = GetAllItems.All)
        {
            switch (getAllItems)
            {
                case GetAllItems.All:
                    {
                        var gotBooks = _db.Books.Include("Publisher")
                            .Include("BookToAuthorList")
                            .Include("BookToAuthorList.Author");
                        return gotBooks;
                    }
                case GetAllItems.ArchiveOnly:
                    {
                        var gotBooks = _db.Books.Where(u => u.IsArchive == true)
                            .Include("Publisher")
                            .Include("BookToAuthorList")
                            .Include("BookToAuthorList.Author");
                        return gotBooks;
                    }
                case GetAllItems.NotArchiveOnly:
                    {
                        var gotBooks = _db.Books.Where(u => u.IsArchive != true)
                            .Include("Publisher")
                            .Include("BookToAuthorList")
                            .Include("BookToAuthorList.Author");
                        return gotBooks;
                    }
                default:
                    {
                        var gotBooks = _db.Books.Include("Publisher")
                            .Include("BookToAuthorList")
                            .Include("BookToAuthorList.Author");
                        return gotBooks;
                    }
            }
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {

            var gotBook = await _db.Books
                .Include("Publisher")
                .Include("BookToAuthorList")
                .Include("BookToAuthorList.Author")
                .FirstOrDefaultAsync(u => u.Id == id);

            return gotBook;
        }


        public async Task<Book> GetBookByNameAsync(string name)
        {
            var gotBook = await _db.Books
                .Include("Publisher")
                .Include("BookToAuthorList")
                .Include("BookToAuthorList.Author")
                .FirstOrDefaultAsync(u => u.Name.Trim().ToUpper() == name.Trim().ToUpper());

            return gotBook;
        }
    }
}
