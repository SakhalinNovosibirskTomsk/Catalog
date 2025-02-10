using Catalog_DataAccess.CatalogDB;

namespace Catalog_Business.Repository.IRepository
{
    /// <summary>
    /// Репозиторий для работы с сущностью БД State
    /// </summary>
    public interface IStateRepository : IRepository<State>
    {

        /// <summary>
        /// Получить статус по его наименованию
        /// </summary>
        /// <param name="name">Наименование статуса</param>
        /// <returns>Возвращает найденый по наименованию статус - объект State</returns>
        public Task<State> GetStateByNameAsync(string name);

        /// <summary>
        /// Получить статус, являющийся статусом по умолчанию для новых экземпляров книг
        /// </summary>
        /// <returns>Возвращает статус, являющийся статусом по умолчанию для новых экземпляров книг - обхект типа State</returns>
        public Task<State> GetIsInitialStateAsync();
    }
}
