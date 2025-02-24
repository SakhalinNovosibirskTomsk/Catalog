using Catalog_DataAccess.CatalogDB;
using static Catalog_Common.SD;

namespace Catalog_Business.Repository.IRepository
{

    /// <summary>
    /// Репозиторий работы с книгами
    /// </summary>
    public interface IBookRepository : IRepository<Book>
    {

        public Task<IEnumerable<Book>> GetAllBooksAsync(GetAllItems? getAllItems = GetAllItems.All);

        public Task<Book> GetBookByIdAsync(int id);

        public Task<Book> GetBookByNameAsync(string name);

        public Task<Book> AddBookAsync(Book book, List<Author> authorList);
    }
}
