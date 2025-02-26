using Catalog_Business.Repository.IRepository;
using Catalog_DataAccess;
using Catalog_DataAccess.CatalogDB;
using Microsoft.EntityFrameworkCore;
using static Catalog_Common.SD;

namespace Catalog_Business.Repository
{
    public class BookInstanceRepository : Repository<BookInstance>, IBookInstanceRepository
    {
        public BookInstanceRepository(ApplicationDbContext _db) : base(_db)
        {
        }

        public async Task<IEnumerable<BookInstance>> GetAllBookInstancesAsync(GetAllItems? getAllItems = GetAllItems.All)
        {
            switch (getAllItems)
            {
                case GetAllItems.All:
                    {
                        var gotBookInstances = _db.BookInstances
                            .Include(b => b.Book)
                            .Include(s => s.State)
                            .ToList();

                        return gotBookInstances;
                    }
                case GetAllItems.ArchiveOnly:
                    {
                        var gotBookInstances = _db.BookInstances.Where(u => u.IsArchive == true)
                            .Include(b => b.Book)
                            .Include(s => s.State)
                            .ToList();
                        return gotBookInstances;
                    }
                case GetAllItems.NotArchiveOnly:
                    {
                        var gotBookInstances = _db.BookInstances.Where(u => u.IsArchive != true)
                            .Include(b => b.Book)
                            .Include(s => s.State)
                            .ToList();
                        return gotBookInstances;
                    }
                default:
                    {
                        var gotBookInstances = _db.BookInstances
                            .Include(b => b.Book)
                            .Include(s => s.State)
                            .ToList();
                        return gotBookInstances;
                    }
            }
        }

        public async Task<IEnumerable<BookInstance>> GetBookInstancesByBookIdAsync(int bookId)
        {
            var gotBookInstances = _db.BookInstances.Where(u => u.BookId == bookId)
                .Include(b => b.Book)
                .Include(s => s.State)
                .ToList();
            return gotBookInstances;

        }

        public async Task<IEnumerable<BookInstance>> GetBookInstancesByStateIdAsync(int stateId)
        {
            var gotBookInstances = _db.BookInstances.Where(u => u.StateId == stateId)
                .Include(b => b.Book)
                .Include(s => s.State)
                .ToList();
            return gotBookInstances;
        }


        public async Task<IEnumerable<BookInstance>> GetBookInstancesByBookIdAndStateIdAsync(int bookId, int stateId)
        {
            var gotBookInstances = _db.BookInstances.Where(u => u.BookId == bookId && u.StateId == stateId)
                .Include(b => b.Book)
                .Include(s => s.State)
                .ToList();
            return gotBookInstances;
        }

        public async Task<BookInstance> GetBookInstanceByIdAsync(int id)
        {
            var gotBookInstance = _db.BookInstances
                            .Include(b => b.Book)
                            .Include(s => s.State)
                            .FirstOrDefault(u => u.Id == id);

            return gotBookInstance;
        }

        public async Task<BookInstance> GetBookInstanceByInventoryNumberAsync(string inventoryNumber)
        {
            var gotBookInstance = _db.BookInstances
                .Include(b => b.Book)
                .Include(s => s.State)
                .FirstOrDefault(u => u.InventoryNumber.Trim().ToUpper() == inventoryNumber.Trim().ToUpper());

            return gotBookInstance;
        }

    }
}
