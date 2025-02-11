using Catalog_Business.Repository.IRepository;
using Catalog_DataAccess;
using Catalog_DataAccess.CatalogDB;
using Microsoft.EntityFrameworkCore;

namespace Catalog_Business.Repository
{

    /// <summary>
    /// Репозиторий для работы с сущностью БД State
    /// </summary>
    public class StateRepository : Repository<State>, IStateRepository
    {
        public StateRepository(ApplicationDbContext _db) : base(_db)
        {

        }

        /// <summary>
        /// Получить статус по его наименованию
        /// </summary>
        /// <param name="name">Наименование статуса</param>
        /// <returns>Возвращает найденый по наименованию статус - объект State</returns>
        public async Task<State> GetStateByNameAsync(string name)
        {

            // TODO Сделать сравнение строк регистронезависимым и без зависимости от лидирующих и концевых пробелов
            var state = await _db.States.FirstOrDefaultAsync(s => s.Name == name);

            //var state = await _db.States.FirstOrDefaultAsync(item => string.Compare(item.Name, name, true) == 0);
            return state;
        }

        /// <summary>
        /// Получить статус, являющийся статусом по умолчанию для новых экземпляров книг
        /// </summary>
        /// <returns>Возвращает статус, являющийся статусом по умолчанию для новых экземпляров книг - обхект типа State</returns>
        public async Task<State> GetIsInitialStateAsync()
        {
            var state = await _db.States.FirstOrDefaultAsync(s => s.IsInitialState);
            return state;
        }

    }
}
