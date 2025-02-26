namespace Catalog_DataAccess.CatalogDB
{
    /// <summary>
    /// Справочник издателей
    /// </summary>
    public class Publisher : BaseEntity
    {

        /// <summary>
        /// Наименование издателя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ИД пользователя добавившего запись
        /// </summary>
        public Guid AddUserId { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Дата/время добавления записи
        /// </summary>
        public DateTime AddTime { get; set; } = DateTime.Now;

        /// <summary>
        /// Признак удаления записи в архив
        /// </summary>        
        public bool IsArchive { get; set; } = false;

        public virtual ICollection<Book> BookList { get; set; }
    }
}
