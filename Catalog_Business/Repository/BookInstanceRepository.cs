using Catalog_Business.Repository.IRepository;
using Catalog_DataAccess;
using Catalog_Domain.CatalogDB;
using Microsoft.EntityFrameworkCore;

namespace Catalog_Business.Repository
{
    public class BookInstanceRepository : Repository<BookInstance>, IBookInstanceRepository
    {
        public BookInstanceRepository(ApplicationDbContext _db) : base(_db)
        {
        }

        public async Task<IEnumerable<BookInstance>> GetAllBookInstancesAsync()
        {
            var gotBookInstances = _db.BookInstances
                .Include(b => b.Book)
                .ToList();

            return gotBookInstances;
        }


        public async Task<IEnumerable<BookInstance>> GetBookInstancesByBookIdAsync(int bookId)
        {
            var gotBookInstances = _db.BookInstances.Where(u => u.BookId == bookId)
                .Include(b => b.Book)
                .ToList();
            return gotBookInstances;

        }

        public async Task<BookInstance> GetBookInstanceByIdAsync(int id)
        {
            var gotBookInstance = _db.BookInstances
                            .Include(b => b.Book)
                            .FirstOrDefault(u => u.Id == id);

            return gotBookInstance;
        }

        public async Task<BookInstance> GetBookInstanceByInventoryNumberAsync(string inventoryNumber)
        {
            var gotBookInstance = _db.BookInstances
                .Include(b => b.Book)
                .FirstOrDefault(u => u.InventoryNumber.Trim().ToUpper() == inventoryNumber.Trim().ToUpper());

            return gotBookInstance;
        }

    }
}
