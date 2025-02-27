using Catalog_Domain.CatalogDB;

namespace Catalog_Business.Repository.IRepository
{
    public interface IPublisherRepository : IRepository<Publisher>
    {
        public Task<Publisher> GetPublisherByNameAsync(string name);
    }

}
