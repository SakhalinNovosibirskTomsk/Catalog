using Catalog_Business.Repository.IRepository;
using Catalog_DataAccess;
using Catalog_DataAccess.CatalogDB;
using Microsoft.EntityFrameworkCore;

namespace Catalog_Business.Repository
{
    public class PublisherRepository : Repository<Publisher>, IPublisherRepository
    {
        public PublisherRepository(ApplicationDbContext _db) : base(_db)
        {
        }

        /// <summary>
        /// Получить издателя по его наименованию
        /// </summary>
        /// <param name="name">Наименование издателя</param>
        /// <returns>Возвращает найденого по наименованию издателя - объект Publisher</returns>
        public async Task<Publisher> GetPublisherByNameAsync(string name)
        {

            // TODO Сделать сравнение строк регистронезависимым и без зависимости от лидирующих и концевых пробелов
            var publisher = await _db.Publishers.FirstOrDefaultAsync(s => s.Name == name);
            return publisher;
        }
    }
}
