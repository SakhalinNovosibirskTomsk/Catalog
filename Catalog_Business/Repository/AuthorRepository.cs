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

            var author = await _db.Authors.FirstOrDefaultAsync(u => u.FirstName.Trim().ToUpper() == firstName.Trim().ToUpper()
                        && u.LastName.Trim().ToUpper() == lastName.Trim().ToUpper()
                        && u.MiddleName.Trim().ToUpper() == middleName.Trim().ToUpper());
            return author;
        }
    }
}
