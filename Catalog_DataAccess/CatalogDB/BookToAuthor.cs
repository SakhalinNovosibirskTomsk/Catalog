namespace Catalog_DataAccess.CatalogDB
{

    /// <summary>
    /// Связь книги с автором
    /// </summary> 
    public class BookToAuthor : BaseEntity
    {
        /// <summary>
        /// ИД книги
        /// </summary>
        public int BookId { get; set; }

        /// <summary>
        /// Книга
        /// </summary>
        public virtual Book Book { get; set; }

        /// <summary>
        /// ИД автора
        /// </summary>
        public int AuthorId { get; set; }

        /// <summary>
        /// Автор
        /// </summary>
        public virtual Author Author { get; set; }

        /// <summary>
        /// ИД пользователя добавившего запись
        /// </summary>
        public Guid AddUserId { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Дата/время добавления записи
        /// </summary>
        public DateTime AddTime { get; set; } = DateTime.Now;

    }
}
