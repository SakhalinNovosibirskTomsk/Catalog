namespace Catalog_Domain.CatalogDB
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
        /// Признак, что экземпляр книги можно выдавать только в читальный зал
        /// </summary>        
        public bool OnlyForReadingRoom { get; set; } = false;

        /// <summary>
        /// Максимальное кол-во дней, на которые можно выдать читателю экземпляр книги
        /// </summary>        
        public int OutMaxDays { get; set; } = 14;

    }
}



