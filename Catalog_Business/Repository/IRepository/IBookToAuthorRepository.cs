using Catalog_DataAccess.CatalogDB;

namespace Catalog_Business.Repository.IRepository
{
    public interface IBookToAuthorRepository : IRepository<BookToAuthor>
    {
        public Task<BookToAuthor> FindBookToAuthorByBookIdAndAuthorId(int bookId, int author);
        public Task<IEnumerable<BookToAuthor>> FindBookToAuthorsByBookId(int bookId);
    }


}
