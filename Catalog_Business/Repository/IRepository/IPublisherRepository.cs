using Catalog_DataAccess.CatalogDB;

namespace Catalog_Business.Repository.IRepository
{
    public interface IPublisherRepository : IRepository<Publisher>
    {
        public Task<Publisher> GetPublisherByNameAsync(string name);
    }

}
