using Catalog_DataAccess.CatalogDB;

namespace Catalog_Business.Repository.IRepository
{
    /// <summary>
    /// Базовый класс работы с сущностями
    /// </summary>
    /// <typeparam name="T">Тип сущности</typeparam>
    public interface IRepository<T>
        where T : BaseEntity
    {
        /// <summary>
        /// Получение всего списка доступных сущностей типа T
        /// </summary>
        /// <returns>Возвращает перечисление типа Т (IEnumerable типизированный Т)</returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Получить сущность типа Т по его Id
        /// </summary>
        /// <param name="id">ИД сущности (int)</param>
        /// <returns>Возвращает полученую по ИД сущность типа T</returns>
        Task<T> GetByIdAsync(int id);

        /// <summary>
        /// Получить список доступных сущностей типа T по списку ИД
        /// </summary>
        /// <param name="ids">Список ИД</param>
        /// <returns>Возвращает перечисление типа Т (IEnumerable типизированный Т)</returns>
        Task<IEnumerable<T>> GetRangeByIdsAsync(List<int> ids);

        /// <summary>
        /// Добавить сущность типа Т
        /// </summary>
        /// <param name="entity">Сущность типа T</param>
        /// <returns>Объект типа Task</returns>
        Task<T> AddAsync(T entity);

        /// <summary>
        /// Обновить сущность типа Т
        /// </summary>
        /// <param name="entity">Обновляемый объект</param>
        /// <returns>Объект типа Task</returns>
        Task<T> UpdateAsync(T entity);

        /// <summary>
        /// Удалить сущность
        /// </summary>
        /// <param name="entity">Удаляемая сущность</param>
        /// <returns>Объект типа Task</returns>
        Task<int> DeleteAsync(T entity);

    }
}
