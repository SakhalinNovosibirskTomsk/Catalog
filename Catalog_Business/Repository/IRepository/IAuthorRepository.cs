using Catalog_DataAccess.CatalogDB;

namespace Catalog_Business.Repository.IRepository
{
    public interface IAuthorRepository : IRepository<Author>
    {
        public Task<Author> GetAuthorByFullNameAsync(string firstName, string lastName, string? middleName);
    }
}
