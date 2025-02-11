using Catalog_Business.Repository.IRepository;
using Catalog_DataAccess;
using Catalog_DataAccess.CatalogDB;
using Microsoft.EntityFrameworkCore;

namespace Catalog_Business.Repository
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        public AuthorRepository(ApplicationDbContext _db) : base(_db)
        {
        }

        /// <summary>
        /// Получить автора по его имени, фамилии и отчеству
        /// </summary>
        /// <param name="firstName">Имя автора</param>
        /// <param name="lastName">Фамилия автора</param>
        /// <param name="middleName">Отчество автора</param>
        /// <returns>Возвращает найденого автора - объект Author</returns>
        public async Task<Author> GetAuthorByFullNameAsync(string firstName, string lastName, string? middleName)
        {

            // TODO Сделать сравнение строк регистронезависимым и без зависимости от лидирующих и концевых пробелов
            var author = await _db.Authors.FirstOrDefaultAsync(u => u.FirstName == firstName && u.LastName == lastName && u.MiddleName == middleName);
            return author;
        }
    }
}
