namespace Catalog_DataAccess.CatalogDB
{

    /// <summary>
    /// Справочник книг
    /// </summary>    
    public class Book : BaseEntity
    {
        /// <summary>
        /// Наименование книги
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ISBN
        /// </summary>
        public string? ISBN { get; set; }

        /// <summary>
        /// ИД издателя
        /// </summary>
        public int PublisherId { get; set; }

        /// <summary>
        /// Издатель
        /// </summary>
        public virtual Publisher? Publisher { get; set; }
        //public Publisher? Publisher { get; set; }

        /// <summary>
        /// Дата издания
        /// </summary>        
        public DateTime PublishDate { get; set; }

        /// <summary>
        /// Ссылка на электронную версию книги
        /// </summary>
        public string? EBookLink { get; set; } = string.Empty;

        /// <summary>
        /// Количество скачиваний электронной версии книги
        /// </summary>        
        public int EBookDownloadCount { get; set; } = 0;

        /// <summary>
        /// ИД пользователя, добавившего книгу
        /// </summary>
        public Guid AddUserId { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Дата/время добавления книги
        /// </summary>
        public DateTime AddTime { get; set; } = DateTime.Now;

        /// <summary>
        /// Признак удаления книги в архив
        /// </summary>        
        public bool IsArchive { get; set; } = false;

        public virtual ICollection<BookToAuthor> BookToAuthorList { get; set; }

        public virtual ICollection<BookInstance> BookInstanceList { get; set; }




    }
}
