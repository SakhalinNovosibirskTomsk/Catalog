using Catalog_DataAccess.CatalogDB;

namespace Catalog_Business.Repository.IRepository
{
    public interface IBookToAuthorRepository : IRepository<BookToAuthor>
    {

        public Task<IEnumerable<BookToAuthor>> GetAllBookToAuthorAsync();
        public Task<BookToAuthor> GetBookToAuthorByIdAsync(int id);
        public Task<BookToAuthor> FindBookToAuthorByBookIdAndAuthorIdAsync(int bookId, int author);
        public Task<IEnumerable<BookToAuthor>> FindBookToAuthorsByBookIdAsync(int bookId);
    }


}
