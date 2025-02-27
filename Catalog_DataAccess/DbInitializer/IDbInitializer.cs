namespace Catalog_DataAccess.DbInitializer
{
    /// <summary>
    /// Инициализация БД и заполнение данными
    /// </summary>
    public interface IDbInitializer
    {
        /// <summary>
        /// Метод заполения БД начальными значениями сущностей
        /// </summary>
        public void InitializeDb();
    }
}
