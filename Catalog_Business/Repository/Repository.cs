using Catalog_Business.Repository.IRepository;
using Catalog_DataAccess;
using Catalog_DataAccess.CatalogDB;
using Microsoft.EntityFrameworkCore;

namespace Catalog_Business.Repository
{
    public class Repository<T>
    : IRepository<T>
    where T : BaseEntity
    {
        protected readonly ApplicationDbContext _db;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var entities = await _db.Set<T>().ToListAsync();

            return entities;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var entity = await _db.Set<T>().FirstOrDefaultAsync(x => x.Id == id);

            return entity;
        }

        public async Task<IEnumerable<T>> GetRangeByIdsAsync(List<int> ids)
        {
            var entities = await _db.Set<T>().Where(x => ids.Contains(x.Id)).ToListAsync();
            return entities;
        }

        public async Task<T> AddAsync(T entity)
        {
            var addedEntity = await _db.Set<T>().AddAsync(entity);
            await _db.SaveChangesAsync();
            return addedEntity.Entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<int> DeleteAsync(T entity)
        {
            _db.Set<T>().Remove(entity);
            return await _db.SaveChangesAsync();
        }
    }
}
