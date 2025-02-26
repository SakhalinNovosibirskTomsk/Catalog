namespace Catalog_DataAccess.CatalogDB
{

    /// <summary>
    /// Экземпляры книг
    /// </summary>    
    public class BookInstance : BaseEntity
    {
        /// <summary>
        /// ИД книги
        /// </summary>

        public int BookId { get; set; }

        public virtual Book Book { get; set; }


        /// <summary>
        /// Инвентарный номер экземпляра книги
        /// </summary>
        public string InventoryNumber { get; set; }

        /// <summary>
        /// Дата получения экземпляра книги в библиотеку
        /// </summary>
        public DateTime ReceiptDate { get; set; } = DateTime.Now;


        /// <summary>
        /// Признак, что экземпляр книги можно выдавать только в читальный зал
        /// </summary>        
        public bool OnlyForReadingRoom { get; set; } = false;


        /// <summary>
        /// Признак, что в данный момент экземпляр книги выдан читателю
        /// </summary>        
        public bool IsCheckedOut { get; set; } = false;

        /// <summary>
        /// Дата списания экземпляра книги из библиотеки
        /// </summary>        
        public DateTime? WriteOffDate { get; set; }

        /// <summary>
        /// ИД причины списания экземпляра книги из библиотеки
        /// </summary>        
        public int? WriteOffReasonId { get; set; } = null;


        /// <summary>
        /// ИД пользователя списавшего экземпляр книги
        /// </summary>        
        public Guid? WriteOffUserId { get; set; } = null;

        /// <summary>
        /// ИД статуса состояния книги
        /// </summary>        
        public int StateId { get; set; }

        /// <summary>
        /// Статус книги
        /// </summary>        
        public virtual State State { get; set; }

        /// <summary>
        /// ИД комментария к статусу состояния книги
        /// </summary>        
        public int? FactCommentId { get; set; } = null;

        /// <summary>
        /// Текст комментария к статусу состояния книги
        /// </summary>        
        public string? FactCommentText { get; set; } = String.Empty;

        /// <summary>
        /// Максимальное кол-во дней, на которые можно выдать читателю экземпляр книги
        /// </summary>        
        public int OutMaxDays { get; set; } = 14;

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

    }
}



