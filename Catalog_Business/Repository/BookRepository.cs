using Catalog_Business.Repository.IRepository;
using Catalog_Common;
using Catalog_DataAccess;
using Catalog_DataAccess.CatalogDB;
using System.Data.Entity;
using static Catalog_Common.SD;

namespace Catalog_Business.Repository
{
    public class BookRepository : Repository<Book>, IBookRepository
    {

        private readonly IBookToAuthorRepository _bookToAuthorRepository;

        public BookRepository(ApplicationDbContext _db, IBookToAuthorRepository bookToAuthorRepository) : base(_db)
        {
            _bookToAuthorRepository = bookToAuthorRepository;
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

        public async Task<Book> AddBookAsync(Book book, List<Author> authorList)
        {
            var addedBook = await AddAsync(
               new Book
               {
                   Name = book.Name,
                   ISBN = book.ISBN,
                   PublisherId = book.PublisherId,
                   PublishDate = book.PublishDate,
                   EBookLink = book.EBookLink,
                   EBookDownloadCount = book.EBookDownloadCount,
                   AddUserId = book.AddUserId,
                   AddTime = book.AddTime,
                   IsArchive = book.IsArchive,
               }, false);

            foreach (var author in authorList)
            {
                var addedBookToAuthor = await _bookToAuthorRepository.AddAsync(
                new BookToAuthor
                {
                    BookId = addedBook.Id,
                    AuthorId = author.Id,
                    AddUserId = SD.UserIdForInitialData,
                    AddTime = DateTime.Now,
                }, false);
            }
            await _db.SaveChangesAsync();
            addedBook = await GetBookByIdAsync(addedBook.Id);
            return addedBook;
        }

        public async Task<Book> UpdateBookAsync(Book book, List<Author> authorList)
        {
            var foundBookToAuthorsList = await _bookToAuthorRepository.FindBookToAuthorsByBookId(book.Id);

            List<int> authorsIdInDb = foundBookToAuthorsList.Select(u => u.AuthorId).ToList();
            List<int> authorsIdNew = authorList.Select(u => u.Id).ToList();

            List<int> authorsForDelete = authorsIdInDb.Except(authorsIdNew).ToList();
            List<int> authorsForAdding = authorsIdNew.Except(authorsIdInDb).ToList();

            foreach (var authorId in authorsForDelete)
            {
                var foundBookToAuthor = await _bookToAuthorRepository.FindBookToAuthorByBookIdAndAuthorId(book.Id, authorId);

                if (foundBookToAuthor != null)
                    await _bookToAuthorRepository.DeleteAsync(foundBookToAuthor, false);
            }

            foreach (var authorId in authorsForAdding)
            {
                var foundBookToAuthor = await _bookToAuthorRepository.FindBookToAuthorByBookIdAndAuthorId(book.Id, authorId);

                if (foundBookToAuthor == null)
                    await _bookToAuthorRepository.AddAsync(
                        new BookToAuthor
                        {
                            BookId = book.Id,
                            AuthorId = authorId,
                            AddUserId = SD.UserIdForInitialData,
                            AddTime = DateTime.Now,
                        }, false);
            }

            var editedBook = await UpdateAsync(
               new Book
               {
                   Name = book.Name,
                   ISBN = book.ISBN,
                   PublisherId = book.PublisherId,
                   PublishDate = book.PublishDate,
                   EBookLink = book.EBookLink,
                   EBookDownloadCount = book.EBookDownloadCount,
                   AddUserId = book.AddUserId,
                   AddTime = book.AddTime,
                   IsArchive = book.IsArchive,
               }, false);

            await _db.SaveChangesAsync();
            editedBook = await GetBookByIdAsync(editedBook.Id);
            return editedBook;
        }
    }
}
