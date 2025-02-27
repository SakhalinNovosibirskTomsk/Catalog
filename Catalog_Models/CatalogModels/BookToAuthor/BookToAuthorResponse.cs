﻿namespace Catalog_Models.CatalogModels.BookToAuthor
{
    /// <summary>
    /// Привязка авторов к книгам
    /// </summary>
    public class BookToAuthorResponse
    {

        /// <summary>
        /// ИД записи
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Книга
        /// </summary>        
        public BookShortResponse Book { get; set; }

        /// <summary>
        /// Автор
        /// </summary>
        public AuthorShortResponse Author { get; set; }

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
