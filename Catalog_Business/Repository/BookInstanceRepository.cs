using Catalog_Business.Repository.IRepository;
using Catalog_Common;
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

        public async Task<IEnumerable<BookInstance>> GetAllBookInstancesByFlagsAsync(SD.BookInstancesFags bookInstancesFag, bool isTrue = false)
        {
            switch (bookInstancesFag)
            {
                case SD.BookInstancesFags.IsCheckedOut:
                    {
                        var gotBookInstances = _db.BookInstances
                            .Where(u => u.IsCheckedOut == isTrue)
                            .Include(b => b.Book)
                            .ToList();

                        return gotBookInstances;
                    }
                case SD.BookInstancesFags.IsBooked:
                    {
                        var gotBookInstances = _db.BookInstances
                            .Where(u => u.IsBooked == isTrue)
                            .Include(b => b.Book)
                            .ToList();

                        return gotBookInstances;
                    }
                case SD.BookInstancesFags.IsWroteOff:
                    {
                        var gotBookInstances = _db.BookInstances
                            .Where(u => u.IsWroteOff == isTrue)
                            .Include(b => b.Book)
                            .ToList();
                        return gotBookInstances;
                    }
                case SD.BookInstancesFags.IsFree:
                    {
                        var gotBookInstances = _db.BookInstances
                            .Where(u => u.IsCheckedOut != true && u.IsBooked != true && u.IsWroteOff != true)
                            .Include(b => b.Book)
                            .ToList();
                        return gotBookInstances;
                    }
                case SD.BookInstancesFags.IsBusy:
                    {
                        var gotBookInstances = _db.BookInstances
                            .Where(u => u.IsCheckedOut == true || u.IsBooked == true || u.IsWroteOff == true)
                            .Include(b => b.Book)
                            .ToList();
                        return gotBookInstances;
                    }
                default:
                    {
                        var gotBookInstances = _db.BookInstances
                            .Include(b => b.Book)
                            .ToList();
                        return gotBookInstances;
                    }
            }

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
